﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GAQueueCS
{
	delegate List<Individual> Operator(List<Individual> arg);
	delegate double Evaluator(List<double> arg);
	delegate void Initializer();

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

		public List<Individual> History;
		public Queue<Individual> Queue;
		private uint age;
		private int geneSize;
		public Evaluator Evaluator;
		public int MinQueueSize;
		public Operator Op;

		GAQSystem(int geneSize,
				  Evaluator evaluator,
				  int minQueueSize,
				  Initializer initializer,
				  Operator op)
		{
			this.geneSize = geneSize;
			Evaluator = evaluator;
			MinQueueSize = minQueueSize;
			Op = op;
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
				indiv.Fitness = Evaluator(indiv.Gene);
				AddHistory(indiv);
				SupplyQueue(age);
			}
		}

		public void CalcRawFitness(Evaluator evaluator)
		{
			foreach (var indiv in History)
			{
				indiv.RawFitness = evaluator(indiv.Gene);
			}
		}
	}
}
