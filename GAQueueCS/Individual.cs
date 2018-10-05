using System;
using System.Linq;
using System.Collections.Generic;

namespace GAQueueCS
{
	using Population = List<Individual>;

	class Individual
	{
		public Gene Gene { get; }
		public double? Fitness { get; set; } = null;
		public double? RawFitness { get; set; } = null;
		public uint BirthYear { get; set; }
		public Population Parents { get; } = new Population();

		public Individual(int size, uint birthYear, Random rand)
			: this(new Gene(), null, birthYear)
		{
			Gene = Gene.Randomized(size, rand);
		}

		public Individual(Gene gene, double? fitness, uint birthYear)
		{
			Gene = gene;
			Fitness = fitness;
			BirthYear = birthYear;
		}

		public override string ToString()
		{
			return $"{Fitness:F3}/{RawFitness:F3} {Gene}";
		}
	}
}
