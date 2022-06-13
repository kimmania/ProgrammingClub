<Query Kind="Program">
  <Reference>C:\Dev\LinqPad\LinqPad utilities\LinqPadUtilities\bin\Debug\LinqPadUtilities.dll</Reference>
</Query>

/*
21 22 23 24 25
20  7  8  9 10
19  6  1  2 11
18  5  4  3 12
17 16 15 14 13
*/



/*
111
110  73  74  75  76  77  78  79  80  81  82
109  72  43  44  45  46  47  48  49  50  83
108  71  42  21  22  23  24  25  26  51  84
107  70  41  20   7   8   9  10  27  52  85
106  69  40  19   6   1   2  11  28  53  86
105  68  39  18   5   4   3  12  29  54  87
104  67  38  17  16  15  14  13  30  55  88
103  66  37  36  35  34  33  32  31  56  89
102  65  64  63  62  61  60  59  58  57  90
101 100  99  98  97  96  95  94  93  92  91
*/

private double size = 1001D;
private double sum;

void Main()
{
	long minTime = long.MaxValue;
	long elapsed;
	for (int runNumber = 0; runNumber <= 9; runNumber++)
	{
		elapsed = runProgram();
		minTime = Math.Min(minTime, elapsed);
	}
	sum.Dump("sum");
	minTime.Dump("minimum run time (ticks)");
}

long runProgram()
{
	Stopwatch sw = new Stopwatch();
	sw.Start();
//	int a5 = calc(5);
//	int a7 = calc(7);
//	(a7 - a5).Dump();
//	int a9 = calc(9);
//	(a9 - a7).Dump();
	calc();
	sw.Stop();
	return sw.ElapsedTicks;
}

void calc()
{
	sum = 1;
	double number = 1;
	double maxIterations = Math.Floor(size / 2);
	for (double iteration = 1; iteration <= maxIterations; iteration++)
	{
		for (int i = 1; i <= 4; i++)
		{
			number += (2 * iteration);
			//number.Dump();
			sum += number;
		}
	}
	//sum.Dump(size.ToString());
	//return sum;
}

// Define other methods and classes here
