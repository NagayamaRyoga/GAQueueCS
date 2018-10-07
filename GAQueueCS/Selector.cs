using System;
using System.Collections.Generic;
using System.Linq;

namespace GAQueueCS
{
    static class Selector
	{
		public static IEnumerable<Individual> Reduce(this IEnumerable<Individual> arg, double rate)
		{
			return arg.Take((int) Math.Floor(arg.Count() * rate));
		}

		/*
		Operator crampCoefficientOfInbreeding(float maxCOI)
		{

		}
		*/
	}
}
