using System;
using System.Collections.Generic;

namespace Common.Model
{
	//U ovoj klasi cuvamo sve izracunate apsolutne vrednosti za jedan dan
	public class CalculatedPower
	{
		//Datum na koje se odnose izracunate vrednosti
		public DateTime Date { get; set; }

		//Lista izracunatih apsolutnih vrednosti
		public List<ConsumptionRecord> AverageRecords { get; set; }

		public CalculatedPower()
		{
		}

		public CalculatedPower(DateTime date, List<ConsumptionRecord> averageRecords)
		{
			Date = date;
			AverageRecords = averageRecords;
		}
	}
}