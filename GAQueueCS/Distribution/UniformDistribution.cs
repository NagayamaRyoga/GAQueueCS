using System;

namespace GAQueueCS.Distribution
{
	class UniformDistribution: IDistribution
	{
		public double Min { get; set; } = 0;
		public double Max { get; set; } = 1;

		public UniformDistribution FromStdDev(double mean, double stdDev)
		{
			var q = 6 * Math.Pow(stdDev, 2);

			return new UniformDistribution
			{
				Min = mean - q,
				Max = mean + q,
			};
		}

		public double Sample(Random random)
		{
			return Min + (Max - Min) * random.NextDouble();
		}
	}
}
