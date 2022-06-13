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
		string[] lines = File.ReadAllLines(input);
		int	counter,
			lineCount = lines.Length, //both count of lines and number of values on a line
			sizeOfNumbers = lines[0].Length;//sizeofnumber equals number of digits representing the size of our numbers

		int[] tempStorage = Utilities.SplitLineIntoNumbers(lines[--lineCount], sizeOfNumbers).ToArray(); // load the initial line
		while(lineCount-- > 0)
		{
			counter = 0; // need to keep track of where I am in my storage arrary, so start over for each line
			foreach (int currentValue in Utilities.SplitLineIntoNumbers(lines[lineCount], sizeOfNumbers))
			{
				//notes on what I am doing here
				//have the postfix ++, means that it gets incremented immediately after I use it
				//then am 50/50 on whether I need to backtrack one if the first value is less than the second
				//gives me a benefit of removing a variable (which includes assignments)
				tempStorage[counter] = (tempStorage[(tempStorage[counter++] > tempStorage[counter]) ? counter - 1 : counter] + currentValue);
			}
		}
		result = tempStorage[0];
		numberOfTicks.Stop();
		fastestRun = fastestRun < numberOfTicks.ElapsedTicks ? fastestRun : numberOfTicks.ElapsedTicks;
		totalTime += numberOfTicks.ElapsedTicks;
	}
	result.Dump("Result");
	fastestRun.Dump("fastestRun");
	(totalTime / numberOfTests).Dump("Average Time");
}

public static class Utilities
{
	public static IEnumerable<int> SplitLineIntoNumbers(this string line, int intLength)
	{
		int i, 
			index = 0, 
			result = 0, 
			entries = line.Length;

		for (index = 0; index < entries; index++)
		{	//logic from fast parsing of an int, but incorporating as a yield
			result = 0;
			for (i = 0; i < intLength; i++)
			{
				result = 10 * result + (line[index++] - 48);
			}
			yield return result;
		}
	}
}