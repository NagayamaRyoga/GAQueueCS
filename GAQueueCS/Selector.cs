using System;
using System.Collections.Generic;
using System.Linq;

namespace GAQueueCS
{
    static class Selector
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> arg)
		{
			return arg.OrderBy(_ => Guid.NewGuid());
		}

		public static IEnumerable<T> TakeAccurately<T>(this IEnumerable<T> arg, int count)
		{
			if (arg.Count() < count) throw new ArgumentOutOfRangeException();
			var ans = arg.ToList();
			return ans.Take(count);
		}

		public static IEnumerable<Individual> Reduce(this IEnumerable<Individual> arg, double rate)
		{
			var ans = arg.ToList();
			return ans.Take((int) Math.Floor(ans.Count() * rate));
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
					var coi = indiv.CalcCoefficientOfInbreeding(i, maxDepth);
					if (!coi.HasValue || coi <= minCOI)
					{
						flag = false;
						break;
					}
				}
				if (flag) ans.Add(indiv);
			}

			return ans;
		}

		public static IEnumerable<Individual> CrampMaxCoefficientOfInbreeding(this IEnumerable<Individual> arg, double maxCOI)
		{
			var ans = new List<Individual>();
			int maxDepth = (int) -Math.Ceiling(Math.Log(maxCOI / 2) / Math.Log(2));

			foreach (var indiv in arg)
			{
				bool flag = true;
				foreach (var i in ans)
				{
					if (indiv.CalcCoefficientOfInbreeding(i, maxDepth) >= maxCOI)
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
