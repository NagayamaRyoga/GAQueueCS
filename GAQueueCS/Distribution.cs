using System;

namespace GAQueueCS
{
	class Distribution
	{
		public static Func<Random, double> Uniform(double mean = 0, double stddev = 3)
		{
			// mean == (min + max) / 2, stddev == Math.Sqrt((max - min) / 12)
			double min = mean - 6 * Math.Pow(stddev, 2);
			double max = mean + 6 * Math.Pow(stddev, 2);

			return rand => min + (max - min) * rand.NextDouble();
		}

		// https://stackoverflow.com/questions/218060/random-gaussian-variables
		// Box-Muller transform
		public static Func<Random, double> Normal(double mean = 0, double stddev = 1)
		{
			return rand =>
			{
				double r1 = 1.0 - rand.NextDouble(); // uniform (0.0, 1.0], not [0.0, 1.0)
				double r2 = 1.0 - rand.NextDouble();
				double normal01 = Math.Sqrt(-2.0 * Math.Log(r1)) * Math.Sin(2.0 * Math.PI * r2); // normal (0.0, 1.0)
				return mean + stddev * normal01;
			};
		}
	}
}
