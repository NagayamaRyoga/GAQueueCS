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

		public static IEnumerable<Individual> ClampCoefficientOfInbreeding(
			this IEnumerable<Individual> arg, int minCount, double? min = null, double? max = null)
		{
			if (!min.HasValue && !max.HasValue)	return arg;

			var ans = new List<Individual>();
			int maxDepth = (int) -Math.Ceiling(Math.Log((min ?? max.Value) / 2) / Math.Log(2));

			foreach (var indiv in arg)
			{
				bool flag = true;
				foreach (var i in ans)
				{
					var coi = indiv.CalcCoefficientOfInbreeding(i, maxDepth);
					if (!coi.HasValue || min > coi || coi < max)
					{
						flag = false;
						break;
					}
				}
				if (flag) ans.Add(indiv);
			}

			//Console.WriteLine($"cramp {arg.Count()}=>{ans.Count()}");

			if (ans.Count() < minCount) return arg;
			//foreach (var i in ans) Console.WriteLine(i);
			//Console.WriteLine();

			return ans;
		}
	}
}
