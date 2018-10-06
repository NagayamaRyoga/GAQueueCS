using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAQueueCS
{
    static class Selector
	{
		public static IEnumerable<Individual> Take(this IEnumerable<Individual> arg, double rate)
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
