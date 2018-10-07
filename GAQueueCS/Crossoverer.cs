using System;
using System.Collections.Generic;
using System.Linq;
using GAQueueCS.Distribution;

namespace GAQueueCS
{
	static class Crossoverer
	{
		/*
		Operator rex(const Distribution<float, std::mt19937>& dist);
		Operator rex(const Distribution<float, std::mt19937>& dist, std::size_t outputNum);
		*/

		static int Hoge(Individual individual)
		{
			return individual.Gene.Values.Count();
		}
		
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
			var dist = new UniformDistribution();
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
							.Sum();
					} while (sum < 0 || sum > 1);
					c[j] = sum;
				}
				children.Add(new Individual(Gene.ListInitialized(c)));
			}

			return children;
		}
	}
}
