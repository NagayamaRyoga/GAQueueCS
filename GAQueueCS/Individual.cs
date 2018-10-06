using System.Collections.Generic;

namespace GAQueueCS
{
	class Individual
	{
		public Gene Gene { get; }
		public double? Fitness { get; set; } = null;
		public double? RawFitness { get; set; } = null;
		public uint? BirthYear { get; set; }
		public ISet<Individual> Parents { get; } = new HashSet<Individual>();

		public Individual(Gene gene, double? fitness = null, uint? birthYear = null)
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
