using Common.Helper.Interfaces;
using System;

namespace Common.Helper
{
	public class DataTypeParser : IDataTypeParser
	{
		//Pretvara string u broj tj int
		public int ConvertIntFromString(string data)
		{
			try
			{
				return int.Parse(data);
			}
			catch
			{
				throw new Exception("Failed to convert int type from string type");
			}
		}

		//Pretvara string u vreme tj datetime
		public DateTime ConvertDateTimeFromString(string data)
		{
			try
			{
				return DateTime.Parse(data);
			}
			catch
			{
				throw new Exception("Failed to convert date time type from string type");
			}
		}

		//Izvlacimo datum iz imena fajla
		//Primer => path = D:\Projekti\RES\Jovan_RES\ImportFiles\ostv_2020_05_07.csv
		public string GetDateFromFileName(string path)
		{
			//ostv_2020_05_07
			string fileName = GetFileNameFromPath(path);

			//{ostv} {2020} {05} {07}
			string[] part = fileName.Split('_');

			return new DateTime(ConvertIntFromString(part[1]), ConvertIntFromString(part[2]), ConvertIntFromString(part[3])).ToString();
		}

		//Prosledi se puna putanja fajla, kao rezultat se vraca samo naziv fajla
		//Primer => path = D:\Projekti\RES\Jovan_RES\ImportFiles\ostv_2020_05_07.csv
		public string GetFileNameFromPath(string path)
		{
			//{D:} {Projekti} {RES} {Jovan_RES} {ImportFiles} {ostv_2020_05_07.csv}
			string[] pathParts = path.Split('\\');

			//{ostv_2020_05_07} {csv}
			string[] fileParts = pathParts[pathParts.Length - 1].Split('.');

			//ostv_2020_05_07
			return fileParts[0];
		}
	}
}