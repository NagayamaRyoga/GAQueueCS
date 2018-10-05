using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAQueueCS
{
	class Selector
	{
		Operator Resize(int size)
		{
			return delegate (IEnumerable<Individual> arg)
			{
				return arg.Take(size);
			};
		}

		Operator Resize(float rate)
		{
			return delegate (IEnumerable<Individual> arg)
			{
				return arg.Take((int) Math.Floor(arg.Count() * rate));
			};
		}

		Operator Unique()
		{
			return delegate (IEnumerable<Individual> arg)
			{
				return arg.Distinct();
			};
		}

		/*
		Operator crampCoefficientOfInbreeding(float maxCOI)
		{
			return delegate (IEnumerable<Individual> arg)
			{

			};
		}
		*/
	}
}
