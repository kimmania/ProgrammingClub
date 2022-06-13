<Query Kind="Program" />

void Main()
{
	int numberOfTests = 10000;
	long fastestRun = 10000000;
	long totalTime = 0;
	int result = 0;
	for (int k = 0; k < numberOfTests; k++)
	{
		Stopwatch numberOfTicks = Stopwatch.StartNew();
		//var input = @"C:\Users\Kim\Documents\LINQPad Queries\PC\p018_triangle.txt";
		var input = @"C:\Users\Kim\Documents\LINQPad Queries\PC\p067_triangle.txt";
		//general variables
		int firstNumber, secondNumber, i, j, currentValue;
		string[] line;
		
		string[] lines = File.ReadAllLines(input);
		int lineCount = lines.Length;
		int[] tempStorage = ParseLine(lines[--lineCount]); // load the initial line

		for (i = lineCount - 1; i >= 0; i--)
		{
			line = lines[i].Split(' ');
			for (j = 0; j < line.Length; j++) //apparently, c# has an bounds check optimization if array.Length is used in the for condition
			{
				currentValue = IntParseFast(line[j]);
				firstNumber = tempStorage[j];
				secondNumber = tempStorage[j + 1];
				tempStorage[j] = firstNumber > secondNumber ? firstNumber + currentValue : secondNumber + currentValue;
			}
		}
		result = tempStorage[0];
		numberOfTicks.Stop();
		//numberOfTicks.ElapsedTicks.Dump("ElapsedTicks");
		//numberOfTicks.ElapsedMilliseconds.Dump("Elapsed ms");
		fastestRun = fastestRun < numberOfTicks.ElapsedTicks ? fastestRun : numberOfTicks.ElapsedTicks;
		totalTime += numberOfTicks.ElapsedTicks;
	}
	result.Dump("Result");
	fastestRun.Dump("fastestRun");
	(totalTime / numberOfTests).Dump("Average Time");
}

public static int[] ParseLine(string line)
{
	return line.Split(' ').Select(a => IntParseFast(a)).ToArray();
}

public static int IntParseFast(string value)
{
	int result = 0;
	for (int i = 0; i < value.Length; i++)
	{
		result = 10 * result + (value[i] - 48);
	}
	return result;
}