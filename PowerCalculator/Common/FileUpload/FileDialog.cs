using Common.FileUpload.Interfaces;
using Common.Helper.Interfaces;
using Common.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Common.FileUpload
{
	public class FileDialog : IFileDialog
	{
		private IDataTypeParser dataTypeParser;

		public FileDialog(IDataTypeParser dataTypeParser)
		{
			this.dataTypeParser = dataTypeParser;
		}

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

		//Ucitavamo podatke iz CSV fajla
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
				//Ako ne pronadje fajl, baci exception
				throw new Exception("Csv files aren't loaded successfully");
			}

			//Obrisi zaglavlje
			rows.RemoveAt(0);

			foreach (var row in rows)
			{
				//Razdvoji red na osnovu zareza, i dobicemo niz vrednosti
				string[] part = row.Split(',');
				PowerRecord entity = new PowerRecord();

				//Posto se sve u fajlu cuva kao string, sad je potrebno taj string konvertovati u broj
				entity.Hour = dataTypeParser.ConvertIntFromString(part[0]);

				//Posto se sve u fajlu cuva kao string, sad je potrebno taj string konvertovati u broj
				entity.Load = dataTypeParser.ConvertIntFromString(part[1]);

				entity.Region = part[2];
				entity.Date = dataTypeParser.GetDateFromFileName(path);
				entity.Timestamp = DateTime.Now.ToString();

				powerRecords.Add(entity);
			}

			return powerRecords;
		}

		//Izracunate podatke smestamo u XML fajl
		public void SaveToXML(OutputModel outputModel, string path)
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(outputModel.GetType());
				StreamWriter writer = new StreamWriter(path);
				serializer.Serialize(writer, outputModel);
				writer.Close();
			}
			catch
			{
				throw new Exception("Exporting result to xml has failed!");
			}
		}
	}
}