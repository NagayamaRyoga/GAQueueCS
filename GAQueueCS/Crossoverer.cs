using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAQueueCS
{
	using System.Collections;
	using Operator = Func<IEnumerable<Individual>, IEnumerable<Individual>>;

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

		public static IEnumerable<Individual> REX<Individual>(this IEnumerable<Individual> parents)
		{
			int n = parents.First().Gene.Values.Count();
			int k = parents.Count() - n;
			double[] gList = new double[n];
			for (var i = 0; i < n; i++)
			{
				gList[i] = parents
					.Select(p => p.Gene.Values[i])
					.Sum() / parents.Count();
			}
			var g = Gene.ListInitialized(gList);

			// todo

			return parents;
		}
	}
}
