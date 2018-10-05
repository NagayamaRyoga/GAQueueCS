using System.Collections.Generic;
using System.Linq;

namespace GAQueueCS.Problem.Onemax
{
	class Onemax: IProblem
	{
		public double Evaluate(IEnumerable<double> gene)
		{
			return gene.Sum();
		}
	}
}
