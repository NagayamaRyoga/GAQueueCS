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

			Func<IEnumerable<Individual>, IEnumerable<Individual>> op = arg => 
			{
				return arg.ToList()
					//.CrampMaxCoefficientOfInbreeding(0.2)
					.CrampMinCoefficientOfInbreeding(0.05)
					.OrderByDescending(indiv => indiv.Fitness)
					.Take(2)
					.RememberParents(hoge => hoge.REX(5));
			};

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
