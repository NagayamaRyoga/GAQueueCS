using System;
using System.Collections.Generic;
using System.Linq;

namespace GAQueueCS
{
	public struct Gene
	{
		public double[] Values { get; private set; }

		public int Count => Values.Count();

		public static Gene Randomized(int size, Random rand)
		{
			return new Gene
			{
				Values = Enumerable.Range(0, size)
					.Select(_ => rand.NextDouble())
					.ToArray(),
			};
		}

		public static Gene ZeroInitialized(int size)
		{
			return new Gene
			{
				Values = Enumerable.Repeat(0.0, size)
					.ToArray(),
			};
		}

		public static Gene ListInitialized(IEnumerable<double> arg)
		{
			return new Gene
			{
				Values = arg.ToArray()
			};
		}

		public override string ToString()
		{
			return String.Join(", ", Values.Select(x => $"{x:F3}"));
		}
	}
}
