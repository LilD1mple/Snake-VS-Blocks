using System;

namespace SnakeVSBlocks.Extensions
{
	public static class Extensions
	{
		public static void CompareByTernaryOperation(this bool condition, Action trueCompare, Action falseCompare)
		{
			if (condition)
				trueCompare.Invoke();
			else
				falseCompare.Invoke();
		}
	}
}
