using Common.Logging;
using Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Common.FileUpload
{
	public class FileDialog
	{
		//Otvara se prozor za odabir csv fajla za import podataka
		//Kao rezultat vraca se putanja do fajla
		public string GetDialogPathName()
		{
			using (OpenFileDialog fileDialog = new OpenFileDialog())
			{
				fileDialog.Filter = "Excel files (*.csv)|*.csv";

				DialogResult result = fileDialog.ShowDialog();

				if (result == DialogResult.OK)
				{
					return fileDialog.InitialDirectory + fileDialog.FileName;
				}
			}

			return string.Empty;
		}

		public List<PowerRecord> Load(string path)
		{
			List<PowerRecord> powerRecords = new List<PowerRecord>();
			List<string> rows;

			try
			{
				//Ocitas sve redove iz fajla
				rows = File.ReadAllLines(path).ToList();
			}
			catch
			{
				//Ako ne pronadje fajl, vrati praznu listu
				LogHelper.Log(Operation.Warning, "Csv files aren't loaded successfully");
				return powerRecords;
			}

			//Obrisi zaglavlje
			rows.RemoveAt(0);

			foreach (var row in rows)
			{
				//Razdvoji red na osnovu zareza, i dobicemo niz vrednosti
				string[] part = row.Split(',');
				PowerRecord entity = new PowerRecord();

				//Posto se sve u fajlu cuva kao string, sad je potrebno taj string konvertovati u broj
				entity.Hour = int.Parse(part[0]);

				//Posto se sve u fajlu cuva kao string, sad je potrebno taj string konvertovati u broj
				entity.Load = int.Parse(part[1]);

				entity.Region = part[2];
				entity.Date = GetDateFromFileName(path);
				entity.Timestamp = DateTime.Now;

				powerRecords.Add(entity);
			}

			return powerRecords;
		}

		//Prosledi se puna putanja fajla, kao rezultat se vraca samo naziv fajla
		public string GetFileNameFromPath(string path)
		{
			string[] pathParts = path.Split('\\');
			string[] fileParts = pathParts[pathParts.Length - 1].Split('.');

			return fileParts[0];
		}

		//Izvlacimo datim iz imena fajla
		public DateTime GetDateFromFileName(string path)
		{
			string fileName = GetFileNameFromPath(path);
			string[] part = fileName.Split('_');

			return new DateTime(int.Parse(part[1]), int.Parse(part[2]), int.Parse(part[3]));
		}
	}
}