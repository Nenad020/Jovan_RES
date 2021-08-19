namespace Common.Model
{
	//Na osnovu jednog reda iz CSV fajla kreira se jedna istanca ovog objekta
	public class PowerRecord
	{
		//Sat iz CSV fajla
		public int Hour { get; set; }

		//Datum koji se nalazi u naslovu fajla
		public string Date { get; set; }

		//Potrošnje u mW/h
		public int Load { get; set; }

		//Geografski region
		public string Region { get; set; }

		//Vremenski zig tj vreme kad smo izvukli podatke iz CSV fajla
		public string Timestamp { get; set; }

		public PowerRecord()
		{
		}

		public PowerRecord(int hour, string date, int load, string region, string timestamp)
		{
			Hour = hour;
			Date = date;
			Load = load;
			Region = region;
			Timestamp = timestamp;
		}
	}
}