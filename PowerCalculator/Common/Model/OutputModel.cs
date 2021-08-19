using System.Collections.Generic;

namespace Common.Model
{
	//Model u kojem se nalaze podaci za ispis na ekran
	public class OutputModel
	{
		//Lista izracunatih apsolutnih vrednosti
		public List<CalculatedPower> CalculatedPowers { get; set; }

		//Apsolutna srednja devijacija
		public decimal MeanDeviation { get; set; }

		//Kvadratna devijacija
		public double SquareDeviation { get; set; }

		public OutputModel()
		{
		}

		public OutputModel(List<CalculatedPower> calculatedPowers, decimal meanDeviation, double squareDeviation)
		{
			CalculatedPowers = calculatedPowers;
			MeanDeviation = meanDeviation;
			SquareDeviation = squareDeviation;
		}
	}
}