﻿using Common.Helper;
using Common.Model;
using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEngine
{
	public class PowerConsumptionCalculator
	{
		private SqliteDataAccess sqliteDataAccess;
		private DataTypeParser dataTypeParser;

		public PowerConsumptionCalculator(SqliteDataAccess sqliteDataAccess, DataTypeParser dataTypeParser)
		{
			this.sqliteDataAccess = sqliteDataAccess;
			this.dataTypeParser = dataTypeParser;
		}

		public OutputModel CalculatePower(DateTime from, DateTime to, string region)
		{
			List<PowerRecord> expetedPowerImporters = sqliteDataAccess.LoadRecords("ExpetedConsumption");
			List<PowerRecord> actualPowerImporters = sqliteDataAccess.LoadRecords("ActualConsumption");

			expetedPowerImporters = CheckRegionRange(from, to, region, expetedPowerImporters);
			actualPowerImporters = CheckRegionRange(from, to, region, actualPowerImporters);

			Dictionary<DateTime, List<ConsumptionRecord>> calculatedPowers = CalculateAbsoluteValues(expetedPowerImporters, actualPowerImporters);

			decimal calculatedMeanDeviation = CalculateAbsoluteMeanDeviation(calculatedPowers);
			double calculatedSquareDeviation = CalculateSquareDeviation(calculatedPowers);

			return new OutputModel(calculatedPowers, calculatedMeanDeviation, calculatedSquareDeviation);
		}

		public List<PowerRecord> CheckRegionRange(DateTime from, DateTime to, string region, List<PowerRecord> powerRecords)
		{
			List<PowerRecord> output = new List<PowerRecord>();

			foreach (var powerRecord in powerRecords)
			{
				DateTime date = dataTypeParser.ConvertDateTimeFromString(powerRecord.Date);

				if (date >= from && date <= to && powerRecord.Region == region)
				{
					output.Add(powerRecord);
				}
			}

			return output;
		}

		public Dictionary<DateTime, List<ConsumptionRecord>> CalculateAbsoluteValues(List<PowerRecord> expeted, List<PowerRecord> actual)
		{
			Dictionary<DateTime, List<ConsumptionRecord>> output = new Dictionary<DateTime, List<ConsumptionRecord>>();

			for (int i = 0; i < expeted.Count; i++)
			{
				DateTime date = dataTypeParser.ConvertDateTimeFromString(expeted[i].Date);
				decimal averageValue = CalculateValuePerHour(expeted[i].Load, actual[i].Load);
				decimal averageAbsoluteValue = CalculateAbsoluteValuePerHour(averageValue);

				if (output.ContainsKey(date))
				{
					output[date].Add(new ConsumptionRecord(expeted[i].Hour, date, averageValue, averageAbsoluteValue));
				}
				else
				{
					output.Add(date, new List<ConsumptionRecord>()
					{
						new ConsumptionRecord(expeted[i].Hour, date, averageValue, averageAbsoluteValue)			
					});
				}
			}

			return output;
		}

		public decimal CalculateValuePerHour(int expeted, int actual)
		{
			return decimal.Divide(actual - expeted, actual) * 100;
		}

		public decimal CalculateAbsoluteValuePerHour(decimal value)
		{
			return Math.Abs(value);
		}

		public decimal CalculateSquareValue(decimal value)
		{
			return value * value;
		}

		public double CalculateRootOfValue(decimal value)
		{
			return Math.Sqrt(Convert.ToDouble(value));
		}

		public decimal CalculateAbsoluteMeanDeviation(Dictionary<DateTime, List<ConsumptionRecord>> absoluteValues)
		{
			decimal sum = 0;
			int counter = 0;

			foreach (var key in absoluteValues)
			{
				foreach (var value in key.Value)
				{
					sum += value.AbsoluteValue;
					counter++;
				}
			}

			return decimal.Divide(sum, counter);
		}

		public double CalculateSquareDeviation(Dictionary<DateTime, List<ConsumptionRecord>> absoluteValues)
		{
			decimal sum = 0;
			int counter = 0;

			foreach (var key in absoluteValues)
			{
				foreach (var value in key.Value)
				{
					sum += value.AbsoluteValue;
					counter++;
				}
			}

			sum = decimal.Divide(sum, counter);
			sum = CalculateSquareValue(sum);
			return CalculateRootOfValue(sum);
		}
	}
}