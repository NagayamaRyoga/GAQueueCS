using System;
using System.Linq;

namespace GAQueueCS
{
	struct Gene
	{
		public double[] Values { get; private set; }

		public static Gene Randomized(int size, Random rand)
		{
			return new Gene
			{
				Values = Enumerable.Range(0, size)
					.Select(_ => rand.NextDouble())
					.ToArray(),
			};
		}

		public override string ToString()
		{
			return String.Join(", ", Values.Select(x => $"{x:F3}"));
		}
	}
}
