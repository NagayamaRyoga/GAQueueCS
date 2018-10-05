using System.Collections.Generic;

namespace GAQueueCS
{
	interface IProblem
	{
		double Evaluate(IEnumerable<double> gene);
	}
}
