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

		public static IEnumerable<Individual> CrampMinCoefficientOfInbreeding(this IEnumerable<Individual> arg, double minCOI)
		{
			var ans = new List<Individual>();
			int maxDepth = (int) -Math.Ceiling(Math.Log(minCOI / 2) / Math.Log(2));

			foreach (var indiv in arg)
			{
				bool flag = true;
				foreach (var i in ans)
				{
					if (indiv.CalcCoefficientOfInbreeding(i, maxDepth) < minCOI)
					{
						flag = false;
						break;
					}
				}
				if (flag) ans.Add(indiv);
			}

			return ans;
		}
	}
}
