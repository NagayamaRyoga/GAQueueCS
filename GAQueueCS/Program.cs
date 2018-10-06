using System;
using System.Linq;
using GAQueueCS.Problem.Onemax;

namespace GAQueueCS
{
	class Program
	{
		static void Main(string[] args)
		{
			var rand = new Random();

			var geneSize = 5;
			var firstSize = 5;

			var firstGeneration = Enumerable.Repeat(0, firstSize)
				.Select(_ => new Individual(Gene.Randomized(geneSize, rand), null, 0))
				.ToArray();

			var system = new GAQSystem(new Onemax(), 0, firstGeneration, arg => { return arg.REX(); });
			system.Step(20);
			foreach (var i in system.History)
			{
				Console.WriteLine(i);
			}
		}
	}
}
