using System;
using System.Collections.Generic;
using System.Linq;

namespace GAQueueCS
{
    class Program
    {
        static void Main(string[] args)
        {
			var system = new GAQSystem(5, Problem.Onemax.Onemax.Evaluator, 0, 5, (IEnumerable<Individual> arg) => { return arg; });
			system.Step(5);
			foreach (var i in system.History)
			{
				Console.WriteLine(i);
			}
        }
	}
}
