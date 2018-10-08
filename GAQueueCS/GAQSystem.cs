using System;
using System.Collections.Generic;
using System.Linq;

namespace GAQueueCS
{
	using Operator = Func<IEnumerable<Individual>, IEnumerable<Individual>>;

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

		public IReadOnlyList<Individual> History => history;

		public IProblem Problem { get; }
		public int MinQueueSize { get; }
		public Operator Op { get; }

		private List<Individual> history { get; } = new List<Individual>();
		private Queue<Individual> queue { get; }
		private uint age = 0;

		public GAQSystem(IProblem problem,
			int minQueueSize,
			IEnumerable<Individual> firstGeneration,
			Operator op)
		{
			Problem = problem;
			MinQueueSize = minQueueSize;
			Op = op;
			queue = new Queue<Individual>(firstGeneration);
		}

		public void SupplyQueue(uint age) {
			if (queue.Count() > MinQueueSize) return;
			var population = Op(History);

			foreach (var indiv in population)
			{
				indiv.BirthYear = age;
				queue.Enqueue(indiv);
			}
		}

		public void Step(int count = 1)
		{
			for (var i = 0; i < count; i++){
				age++;
				var indiv = queue.Dequeue();
				indiv.Fitness = Problem.Evaluate(indiv.Gene.Values);
				history.Add(indiv);
				SupplyQueue(age);
			}
		}

		public void CalcRawFitness(IProblem problem)
		{
			foreach (var indiv in History)
			{
				indiv.RawFitness = problem.Evaluate(indiv.Gene.Values);
			}
		}
	}
}
