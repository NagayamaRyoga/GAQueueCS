using System;

namespace GAQueueCS.Distribution
{
	class NormalDistribution: IDistribution
	{
		public double Mean { get; set; } = 0;
		public double StdDev { get; set; } = 1;

		// https://stackoverflow.com/questions/218060/random-gaussian-variables
		// Box-Muller transform
		public double Sample(Random random)
		{
			var r1 = 1.0 - random.NextDouble(); // uniform (0.0, 1.0], not [0.0, 1.0)
			var r2 = 1.0 - random.NextDouble();
			var normal01 = Math.Sqrt(-2.0 * Math.Log(r1)) * Math.Sin(2.0 * Math.PI * r2); // normal (0.0, 1.0)

			return Mean + StdDev * normal01;
		}
	}
}
