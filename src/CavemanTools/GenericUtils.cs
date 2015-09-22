using System;
using CavemanTools.Extensions;

namespace CavemanTools
{
	public class GenericUtils
	{
		public Random Randomizer;

		private GenericUtils()
		{
			Randomizer = new Random();
		}

		private static readonly GenericUtils inst = new GenericUtils();

		public static GenericUtils Instance
		{
			get { return inst; }
		}

		public bool ChanceHit(float n)
		{
			return Randomizer.ChanceHit(n);
		}
	}
}