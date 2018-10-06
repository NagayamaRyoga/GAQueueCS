using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAQueueCS
{
	class Distribution
	{
		public Func<Random, double> Uniform(double mean, double stddev)
		{
			double min = mean - Math.Sqrt(3 * stddev);
			double max = mean + Math.Sqrt(3 * stddev);
			double gap = max - mean;
			return rand =>
			{
				return min + gap * rand.NextDouble();
			};
		}

		// https://stackoverflow.com/questions/218060/random-gaussian-variables
		// Box-Muller transform
		public Func<Random, double> Normal(double mean, double stddev)
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
