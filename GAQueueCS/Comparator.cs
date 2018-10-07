
namespace GAQueueCS
{
	class Comparator
	{
		public static int Through(Individual _, Individual __) => 0;

		public static double Fitness(Individual lhs, Individual rhs)
		{
			if (!lhs.Fitness.HasValue || !rhs.Fitness.HasValue) return 0;
			return lhs.Fitness.Value - rhs.Fitness.Value;
		}

		public static double RawFitness(Individual lhs, Individual rhs)
		{
			if (!lhs.RawFitness.HasValue || !rhs.RawFitness.HasValue) return 0;
			return lhs.RawFitness.Value - rhs.RawFitness.Value;
		}

		public static uint Recentness(Individual lhs, Individual rhs)
		{
			return lhs.BirthYear.Value - rhs.BirthYear.Value;
		}
	}
}
