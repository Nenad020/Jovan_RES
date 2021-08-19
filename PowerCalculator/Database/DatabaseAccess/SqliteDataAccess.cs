using Common.Model;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace DatabaseAccess
{
	public class SqliteDataAccess
	{
		//Ucitavamo sve podatke iz baze
		//Na osnovu prosledjenog parametra znamo iz koje tabele vucemo podatke
		public List<PowerRecord> LoadRecords(string table)
		{
			using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
			{
				var output = cnn.Query<PowerRecord>($"select * from {table}", new DynamicParameters());
				return output.ToList();
			}
		}

		//Ucitavamo samo nazive regiona iz baze i vracamo je samo jedinstvene vrednosti
		//Na osnovu prosledjenog parametra znamo iz koje tabele vucemo podatke
		public List<string> LoadRegions(string table)
		{
			using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
			{
				var output = cnn.Query<string>($"select distinct Region from {table}", new DynamicParameters());
				return output.ToList();
			}
		}

		//Podatke smestamo u bazu
		//Na osnovu prosledjenog parametra znamo iz koje tabele vucemo podatke
		public void SaveRecords(List<PowerRecord> powerRecords, string table)
		{
			using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
			{
				foreach (var powerRecord in powerRecords)
				{
					cnn.Execute($"insert into {table} (Hour, Date, Load, Region, Timestamp) values (@Hour, @Date, @Load, @Region, @Timestamp)", powerRecord);
				}
			}
		}

		//Vracamo connection string za povezivanje sa bazom
		private string LoadConnectionString(string id = "Default")
		{
			return ConfigurationManager.ConnectionStrings[id].ConnectionString;
		}
	}
}