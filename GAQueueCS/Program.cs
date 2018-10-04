using System;
using System.Collections.Generic;
using System.Linq;

namespace GAQueueCS
{
    class Program
    {
        static void Main(string[] args)
        {
			var gaqsys = new GAQSystem(5, evaluator_Onemax, 0, 5, (IEnumerable<Individual> arg) => { return arg; });
			gaqsys.Step();
        }

		static double evaluator_Onemax(List<double> arg)
		{
			return arg.Sum();
		}
	}
}
