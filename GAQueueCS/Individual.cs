using System;
using System.Linq;
using System.Collections.Generic;

namespace GAQueueCS
{
	using Gene       = List<double>;
	using Population = List<Individual>;

	class Individual
	{
		public Gene Gene;
		public double? Fitness = null;
		public double? RawFitness = null;
		public uint BirthYear = 0;
		public Population Parents = new Population();

		public Individual(int size, uint birthYear = 0)
			: this(new Gene(), null, birthYear)
		{
			var rand = new Random();
			Gene = Enumerable.Repeat(0.0, size).Select(_ => rand.NextDouble()).ToList();
		}

		public Individual(Gene gene, double? fitness, uint birthYear = 0)
		{
			Gene = gene;
			Fitness = fitness;
			BirthYear = birthYear;
		}

		public override string ToString()
		{
			var ret = string.Format("{0}/{1} ", Fitness, RawFitness);
			Gene.ForEach(g => ret += string.Format("{0:F3}, ", g));
			return ret;
		}
	}
}
