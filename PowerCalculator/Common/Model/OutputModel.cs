﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
	public class OutputModel
	{
		public Dictionary<DateTime, List<ConsumptionRecord>> CalculatedPowers { get; set; }
		public decimal MeanDeviation { get; set; }
		public double SquareDeviation { get; set; }

		public OutputModel()
		{
		}

		public OutputModel(Dictionary<DateTime, List<ConsumptionRecord>> calculatedPowers, decimal meanDeviation, double squareDeviation)
		{
			CalculatedPowers = calculatedPowers;
			MeanDeviation = meanDeviation;
			SquareDeviation = squareDeviation;
		}
	}
}