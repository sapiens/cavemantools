using System;

namespace CavemanTools.Extensions
{
	public static class RandomUtils
	{
		/// <summary>
		/// True if hit
		/// </summary>
		/// <param name="number">between 1-100</param>
		/// <returns></returns>
		public static bool ChanceHit(this Random Randomizer,int number)
		{
			if (number == 100) return true;
			if (number <= 0) return false;
			var dice = Randomizer.Next(1, 101);
			if (dice <= number) return true;
			return false;
		}

		/// <summary>
		/// True if hit
		/// </summary>
		/// <param name="number">between 1-100</param>
		/// <returns></returns>
		public static bool ChanceHit(this Random Randomizer,float number)
		{
			//if (number >= 100) return true;
			//if (number <= 0) return false;
			//var dice = (float)Randomizer.Next(0, 10001)/100;
			////var dice = (float)Math.Round(Randomizer.NextDouble(),2) * 100;
			//if (dice <= number) return true;
			//return false;
			var d = 0f;
			return Randomizer.ChanceHit(number, out d);
		}

		/// <summary>
		/// True if hit
		/// </summary>
		/// <param name="number">between 1-100</param>
		/// <returns></returns>
		public static bool ChanceHit(this Random Randomizer, float number, out float dice)
		{
			dice = 0;
			if (number >= 100) return true;
			if (number <= 0) return false;
			dice = Randomizer.Next(0, 101);
			//var dice = (float)Math.Round(Randomizer.NextDouble(),2) * 100;
			if (dice <= Math.Round(number)) return true;
			return false;
		}
	}
}