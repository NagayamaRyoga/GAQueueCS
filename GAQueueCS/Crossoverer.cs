using System;
using System.Collections.Generic;
using System.Linq;
using GAQueueCS.Distribution;

namespace GAQueueCS
{
	static class Crossoverer
	{
		/**
		 * g = Center of gravity(parents)
		 * n = gene.size()
		 * k = Choose any value. n/2 ~ 2n are fine.
		 * n + k == parents.size() == children.size()
		 * each \xi = dist(rand)
		 * each child = g + \Sum_{i=1}^{n+k} (\xi_i * (parents_i - g))
		 */
		public static IEnumerable<Individual> REX(this IEnumerable<Individual> parents, int? childrenCount = null)
		{
			int n = parents.First().Gene.Values.Count();
			int k = parents.Count() - n;
			double[] g = new double[n];
			for (var i = 0; i < n; i++)
			{
				g[i] = parents
					.Select(p => p.Gene.Values[i])
					.Average();
			}

			var rand = new Random();
			var dist = new NormalDistribution { Mean = 0, StdDev = 1.0 / (n + k) };
			var children = new List<Individual>();

			for (var i = 0; i < (childrenCount ?? n + k); i++)
			{
				double[] c = new double[n];
				for (var j = 0; j < n; j++)
				{
					double sum;
					do
					{
						sum = parents
							.Select(p => (p.Gene.Values[j] - g[j]) * dist.Sample(rand))
							.Sum() + g[j];
					} while (sum < 0.0 || sum > 1.0);
					c[j] = sum;
				}
				children.Add(new Individual(Gene.ListInitialized(c)));
			}

			return children;
		}

		public static IEnumerable<Individual> RememberParents(this IEnumerable<Individual> arg,
															  Func<IEnumerable<Individual>, IEnumerable<Individual>> f)
		{
			var argCopy = arg.ToList();
			var children = f(arg);
			foreach (var child in children)
			{
				child.Parents = argCopy;
			}
			return children;
		}
	}
}
