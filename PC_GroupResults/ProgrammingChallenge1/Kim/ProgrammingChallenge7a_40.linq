<Query Kind="Program" />

//using 4.0 AsParallel()
void Main()
{
   Func<uint, uint> findSequenceLength = null;
		findSequenceLength = (uint n) =>
		{

			if (n == 1) { return 1; }
			uint multiplesOfTwo = n & ~(n - 1);
			if (multiplesOfTwo == 1)
				return (1 + findSequenceLength((n * 3) + 1));
			else
				return ((uint)DeBruijn.Position((int)multiplesOfTwo) + findSequenceLength(n >> DeBruijn.Position((int)multiplesOfTwo)));
		};
		   
	string line;
	int totalTime = 0;
	System.IO.StreamReader file = new StreamReader(@"C:\Users\u0148803\Downloads\testdata.txt");
	while (( line = file.ReadLine()) != null)
	{
		int start, final, stop, diff, currentCount, time;
		uint maxCount = 1;
		string[] numbers = line.Split(' ');
		string result;
		start = int.Parse(numbers[0]);
		final = int.Parse(numbers[1]);

		Stopwatch numberOfTicks = Stopwatch.StartNew();         
		if (start > final)
		{
			stop = (start/2) < final ? final : start / 2;
			diff = start - stop + 1;
		}
		else
		{
			stop = (final/2) < start ? start : final / 2;
			diff = final - stop + 1;
		}
		maxCount = (from n in Enumerable.Range(stop, diff).AsParallel() select findSequenceLength((uint)n)).Max();
		time = numberOfTicks.Elapsed.Milliseconds;
	   
		result = string.Format("{0} {1} {2}", line, maxCount, time);
		totalTime += time;
		result.Dump();
	   
	}
	totalTime.Dump("totalTime");
}


static class DeBruijn
{
	static int[] _positions =
	{
		0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8,
		31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9
	};

	/// <summary>
	/// Returns the first set bit (FFS), or 0 if no bits are set.
	/// </summary>
	public static int Position(int number)
	{
		uint res = unchecked((uint)(number & -number) * 0x077CB531U) >> 27;
		return _positions[res];
	}
}