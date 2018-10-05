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
		public double? Fitness { get; set; } = null;
		public double? RawFitness { get; set; } = null;
		public uint BirthYear { get; set; }
		public Population Parents { get; } = new Population();

		public Individual(int size, uint birthYear, Random rand)
			: this(new Gene(), null, birthYear)
		{
			Gene = Enumerable.Repeat(0.0, size).Select(_ => rand.NextDouble()).ToList();
		}

		public Individual(Gene gene, double? fitness, uint birthYear)
		{
			Gene = gene;
			Fitness = fitness;
			BirthYear = birthYear;
		}

		public override string ToString()
		{
			var ret = string.Format("{0:F3}/{1:F3} ", Fitness, RawFitness);
			Gene.ForEach(g => ret += string.Format("{0:F3}, ", g));
			return ret;
		}
	}
}
