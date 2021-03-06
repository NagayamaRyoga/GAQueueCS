using System;
using System.Collections.Generic;

namespace GAQueueCS
{
	class Individual
	{
		public Gene Gene { get; }
		public double? Fitness { get; set; } = null;
		public double? RawFitness { get; set; } = null;
		public uint? BirthYear { get; set; }
		public IEnumerable<Individual> Parents { get; set; } = new List<Individual>();
		public IDictionary<Individual, double?> CoefficientsOfInbreeding = new Dictionary<Individual, double?>();

		public Individual(Gene gene, double? fitness = null, uint? birthYear = null)
		{
			Gene = gene;
			Fitness = fitness;
			BirthYear = birthYear;
		}

		/**
		 * coefficient of inbreeding
		 * https://en.wikipedia.org/wiki/Coefficient_of_inbreeding
		 */
		public double? CalcCoefficientOfInbreeding(Individual other, int maxDepth)
		{
			if (CoefficientsOfInbreeding.ContainsKey(other)){
				return CoefficientsOfInbreeding[other];
			}

			List<Individual>[] myFamily = new List<Individual>[maxDepth + 1];
			List<Individual>[] othersFamily = new List<Individual>[maxDepth + 1];
			myFamily[0] = new List<Individual> { this };
			othersFamily[0] = new List<Individual> { other };
			int parentCount = (Parents as List<Individual>).Count;

			for (int depth = 1; depth <= maxDepth; depth++)
			{
				var partOfMyFamily = new List<Individual>();
				foreach (var i in myFamily[depth - 1])
				{
					partOfMyFamily.AddRange(i.Parents);
				}
				myFamily[depth] = partOfMyFamily;

				var partOfOthersFamily = new List<Individual>();
				foreach (var i in myFamily[depth - 1])
				{
					partOfOthersFamily.AddRange(i.Parents);
				}
				othersFamily[depth] = partOfOthersFamily;
			}

			double? ans = null;

			for (int myDepth = 0; myDepth <= maxDepth; myDepth++)
			{
				for (int othersDepth = 0; othersDepth <= maxDepth; othersDepth++)
				{
					foreach (var oneOfMyFamily in myFamily[myDepth])
					{
						foreach (var oneOfOthersFamily in othersFamily[othersDepth])
						{
							if (oneOfMyFamily != oneOfOthersFamily) continue;
							var coi = Math.Pow(1.0 / parentCount, myDepth) +
							          Math.Pow(1.0 / parentCount, othersDepth);
							ans = ans < coi ? ans : coi;
						}
					}
				}
			}

			CoefficientsOfInbreeding[other] = ans;
			other.CoefficientsOfInbreeding[this] = ans;

			return ans;
		}

		public override string ToString()
		{
			return $"{Fitness:F3}/{RawFitness:F3}({BirthYear}) {Gene}";
		}
	}
}
