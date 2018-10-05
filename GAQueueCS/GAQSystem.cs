using System;
using System.Collections.Generic;
using System.Linq;

namespace GAQueueCS
{
	delegate IEnumerable<Individual> Operator(IEnumerable<Individual> arg);

	class GAQSystem
	{
		/*
		 * overview diagram here
		 * each arrow shows flow of individual data
		 * bold arrow shows that 2 individual flows there at once
		 *
		 * (evaluate) <--(popQueue)-- [queue] <=====
		 *  |                                      ||
		 * (addHistory)                            ||
		 *  |                                      ||
		 *  V                                      ||
		 * [history] ==(  s u p p l y Q u e u e  )==
		 *              * * * * * * * * * * * * *
		 *              * select 2 from history *
		 *              * crossover selected 2  *
		 *              * mutate each new indiv *
		 *              * push new 2 to queue   *
		 *              * * * * * * * * * * * * *
		 *
		 * these have some options to choose from
		 * 		- size of gene
		 * 		- evaluate  function
		 * 		- select    function
		 * 		- crossover function
		 * 		- mutate    function
 		 */

		public List<Individual> History { get; } = new List<Individual>();
		public Queue<Individual> Queue { get; } = new Queue<Individual>();
		private uint age;
		private int geneSize;
		public IProblem Problem { get; }
		public int MinQueueSize { get; }
		public Operator Op { get; }

		public GAQSystem(int geneSize,
				  IProblem problem,
				  int minQueueSize,
				  int initQueueSize,
				  Operator op)
			: this(geneSize, problem, minQueueSize, Enumerable.Repeat(0, initQueueSize).Select(_ => new Individual(geneSize, 0, new Random())), op)
		{
		}

		public GAQSystem(int geneSize,
				  IProblem problem,
				  int minQueueSize,
				  IEnumerable<Individual> firstGeneration,
				  Operator op)
		{
			this.geneSize = geneSize;
			Problem = problem;
			MinQueueSize = minQueueSize;
			Op = op;
			
			foreach (var indiv in firstGeneration)
			{
				Queue.Enqueue(indiv);
			}
		}

		public void AddHistory(Individual indiv) { History.Add(indiv); }

		public void SupplyQueue(uint age) {
			if (Queue.Count() > MinQueueSize) return;
			var population = Op(History);

			foreach (var indiv in population)
			{
				indiv.BirthYear = age;
				Queue.Enqueue(indiv);
			}
		}

		public Individual PopQueue() { return Queue.Dequeue(); }

		public void Step(int count = 1)
		{
			for (var i = 0; i < count; i++){
				age++;
				var indiv = PopQueue();
				indiv.Fitness = Problem.Evaluate(indiv.Gene.Values);
				AddHistory(indiv);
				SupplyQueue(age);
			}
		}

		public void CalcRawFitness()
		{
			foreach (var indiv in History)
			{
				indiv.RawFitness = Problem.Evaluate(indiv.Gene.Values);
			}
		}
	}
}
