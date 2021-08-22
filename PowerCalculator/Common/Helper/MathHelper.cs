using Common.Helper.Interfaces;
using System;

namespace Common.Helper
{
	public class MathHelper : IMathHelper
	{
		//Apsolutna vrednost odstupanja za jedan sat se izračunava po formuli
		public decimal CalculateValuePerHour(int expeted, int actual)
		{
			return decimal.Divide(actual - expeted, actual) * 100;
		}

		//Izracunavanje aposultne vrednosti
		public decimal CalculateAbsoluteValuePerHour(decimal value)
		{
			return Math.Abs(value);
		}

		//Racunanje kvadratne vrednosti nekog broja
		public decimal CalculateSquareValue(decimal value)
		{
			return value * value;
		}

		//Racunanje korena nekog broja
		public double CalculateRootOfValue(decimal value)
		{
			return Math.Sqrt(Convert.ToDouble(value));
		}

		//Deli dva broja i vraca rezultat
		public decimal CalculateDivisionValue(decimal a, decimal b)
		{
			return decimal.Divide(a, b);
		}
	}
}