﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace GAQueueCS
{
	using Gene       = List<double>;
	using Population = List<Individual>;

	class Individual
	{
		public Gene Gene;
		public double? Fitness;
		public double? RawFitness = null;
		public uint BirthYear = 0;
		public Population Parents;

		public Individual(int size, uint birthYear = 0)
			: this(Enumerable.Repeat(0.0, size).ToList(), null, birthYear)
		{
			var rand = new Random();
			for (var i = 0; i < Gene.Count; i++)
			{
				Gene[i] = rand.NextDouble();
			}
		}

		public Individual(Gene gene, double? fitness, uint birthYear = 0)
		{
			Gene = gene;
			Fitness = fitness;
			BirthYear = birthYear;
		}

		public override string ToString()
		{
			var ret = string.Format("{0}/{1} ", Fitness, RawFitness);
			Gene.ForEach(g => ret += string.Format("{0:F3}, ", g));
			return ret;
		}
	}
}
