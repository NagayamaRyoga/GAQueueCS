using System;
using System.Collections.Generic;
using System.Linq;
using GAQueueCS.Problem.Onemax;

namespace GAQueueCS
{
	class Program
	{
		static void PrintFamilyTree(Individual child, ISet<Individual> printed, bool root = true)
		{
			if (printed.Contains(child)) return;
			printed.Add(child);

			if (root)
			{
				Console.WriteLine("```mermaid");
				Console.WriteLine("graph TD");
			}

			foreach (var parent in child.Parents)
			{
				Console.WriteLine($"{parent.GetHashCode()}({parent.BirthYear}:{parent.RawFitness:F3})" +
					$"-->{child.GetHashCode()}({child.BirthYear}:{child.RawFitness:F3})");
			}

			foreach (var parent in child.Parents)
			{
				PrintFamilyTree(parent, printed, false);
			}

			if (root)
			{
				Console.WriteLine("```");
			}
		}

		static void Main(string[] args)
		{
			var rand = new Random();

			var geneSize = 5;
			var firstSize = 5;

			var firstGeneration = Enumerable.Repeat(0, firstSize)
				.Select(_ => new Individual(Gene.Randomized(geneSize, rand), null, 0))
				.ToArray();

			IEnumerable<Individual> op(IEnumerable<Individual> arg)
			{
				return arg
					.OrderByDescending(indiv => indiv.Fitness)
					//.CrampMinCoefficientOfInbreeding(minCOI: 0.1, minCount: 2)
					.CrampMaxCoefficientOfInbreeding(maxCOI: 0.2, minCount: 2)
					.TakeAccurately(2)
					.RememberParents(hoge => hoge.REX(rand, 5));
			}

			var system = new GAQSystem(new Onemax(), 0, firstGeneration, op);
			system.Step(200);
			system.CalcRawFitness(new Onemax());
			foreach (var i in system.History)
			{
				//Console.WriteLine(i);
			}
			system.History.OrderByDescending(indiv => indiv.BirthYear);

			PrintFamilyTree(system.History.Last(), new HashSet<Individual>());
		}
	}
}
