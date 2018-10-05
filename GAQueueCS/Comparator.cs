using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public static double Recentness(Individual lhs, Individual rhs)
		{
			return lhs.BirthYear - rhs.BirthYear;
		}
	}
}
