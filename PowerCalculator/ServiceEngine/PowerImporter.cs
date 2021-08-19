using Common.FileUpload;
using Common.Model;
using DatabaseAccess;
using System;
using System.Collections.Generic;

namespace ServiceEngine
{
	public class PowerImporter
	{
		private FileDialog fileDialog;
		private SqliteDataAccess sqliteDataAccess;

		public PowerImporter(FileDialog fileDialog, SqliteDataAccess sqliteDataAccess)
		{
			this.fileDialog = fileDialog;
			this.sqliteDataAccess = sqliteDataAccess;
		}

		//Funkcija koja poziva funkcije za proveru validacije i funkcije za upis podataka u bazu
		public void CollectData(string expetedPowerPath, string actualPowerPath)
		{
			List<PowerRecord> expetedPowerImporters = fileDialog.Load(expetedPowerPath);
			List<PowerRecord> actualPowerImporters = fileDialog.Load(actualPowerPath);

			ValidatePowerConsumtionTimePeriods(expetedPowerImporters);
			ValidatePowerConsumtionTimePeriods(actualPowerImporters);

			ValidateFilesCount(expetedPowerImporters, actualPowerImporters);
			ValidateDataFromFiles(expetedPowerImporters, actualPowerImporters);

			//Sledi upis u bazu
			sqliteDataAccess.SaveRecords(expetedPowerImporters, "ExpetedConsumption");
			sqliteDataAccess.SaveRecords(actualPowerImporters, "ActualConsumption");
		}

		//Racunamo koliko postoji unetih sati po regionu unutar jednog fajla
		public void ValidatePowerConsumtionTimePeriods(List<PowerRecord> powerRecords)
		{
			int counter = 1;
			string currentRegion = powerRecords[0].Region;

			for (int i = 1; i < powerRecords.Count; i++)
			{
				if (powerRecords[i].Region != currentRegion)
				{
					CheckTimePeriodsNumber(counter);

					counter = 1;
					currentRegion = powerRecords[i].Region;
				}
				else
				{
					counter++;
				}
			}
		}

		//Proveramo koliko postoji unetih sati po regionu unutar jednog fajla
		public void CheckTimePeriodsNumber(int numberOfHoursByRegion)
		{
			if (numberOfHoursByRegion < 23 || numberOfHoursByRegion > 25)
			{
				throw new Exception("Import files must have 23, 24 or 25 hours per region!");
			}
		}

		//Proveravamo da li fajlovi sadrze jednak broj redova
		public void ValidateFilesCount(List<PowerRecord> expeted, List<PowerRecord> actual)
		{
			if (expeted.Count != actual.Count)
			{
				throw new Exception("Import files don't have equal data rows, please import valid files!");
			}
		}

		//Proveravamo da li redovi iz fajlova imaju iste nazive za regione i iste sate po redu
		public void ValidateDataFromFiles(List<PowerRecord> expeted, List<PowerRecord> actual)
		{
			for (int i = 0; i < expeted.Count; i++)
			{
				if (expeted[i].Hour != actual[i].Hour || expeted[i].Region != actual[i].Region)
				{
					throw new Exception("Files are inconsistent, please import valid files!");
				}
			}
		}
	}
}