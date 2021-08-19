using System;

namespace Common.Model
{
	//Apsolutna vrednost odstupanja za jedan sat
	public class ConsumptionRecord
	{
		//Sat
		public int Hour { get; set; }

		//Datum
		public DateTime Date { get; set; }

		//Izracunata vrednost
		public decimal Value { get; set; }

		//Izracunata apsolutna vrednost
		public decimal AbsoluteValue { get; set; }

		public ConsumptionRecord()
		{
		}

		public ConsumptionRecord(int hour, DateTime date, decimal value, decimal absoluteValue)
		{
			Hour = hour;
			Date = date;
			Value = value;
			AbsoluteValue = absoluteValue;
		}
	}
}