<Query Kind="Program" />

public string outputFile = @"C:\Users\Kim\Documents\LINQPad Queries\PC\StackEmUp-output-1.txt";
public string inputFile = @"C:\Users\Kim\Documents\LINQPad Queries\PC\StackEmUp-input-1.txt";
public static string[] cards = {"2 of Clubs",
"3 of Clubs",
"4 of Clubs",
"5 of Clubs",
"6 of Clubs",
"7 of Clubs",
"8 of Clubs",
"9 of Clubs",
"10 of Clubs",
"Jack of Clubs",
"Queen of Clubs",
"King of Clubs",
"Ace of Clubs",
"2 of Diamonds",
"3 of Diamonds",
"4 of Diamonds",
"5 of Diamonds",
"6 of Diamonds",
"7 of Diamonds",
"8 of Diamonds",
"9 of Diamonds",
"10 of Diamonds",
"Jack of Diamonds",
"Queen of Diamonds",
"King of Diamonds",
"Ace of Diamonds",
"2 of Hearts",
"3 of Hearts",
"4 of Hearts",
"5 of Hearts",
"6 of Hearts",
"7 of Hearts",
"8 of Hearts",
"9 of Hearts",
"10 of Hearts",
"Jack of Hearts",
"Queen of Hearts",
"King of Hearts",
"Ace of Hearts",
"2 of Spades",
"3 of Spades",
"4 of Spades",
"5 of Spades",
"6 of Spades",
"7 of Spades",
"8 of Spades",
"9 of Spades",
"10 of Spades",
"Jack of Spades",
"Queen of Spades",
"King of Spades",
"Ace of Spades"
};
void Main()
{
	Stopwatch numberOfTicks = Stopwatch.StartNew();

	int numberOfDecks = 0;
	StringBuilder results;
	using (var input = System.IO.File.OpenText(inputFile))
	{
		//first line is the number of elections
		numberOfDecks = IntParseFast(input.ReadLine());
		results = new StringBuilder(690 * numberOfDecks); // allocate the size for the final results
		input.ReadLine(); // blank line

		int numberOfShuffles = 0;
		string shuffleToApply;
		for (int i = 0; i < numberOfDecks; i++)
		{
			numberOfShuffles = IntParseFast(input.ReadLine());
			List<int[]> shuffles = new List<int[]>();
			shuffles.Add(ParseLine("0"));
			for (int j = 1; j <= numberOfShuffles; j++)
			{
				shuffles.Add(ParseLine(input.ReadLine()));
			}
			string[] currentDeck = cards.ToArray(); // set up the initial deck

			while (!string.IsNullOrWhiteSpace(shuffleToApply = input.ReadLine()))
			{
				Array.Sort(shuffles[IntParseFast(shuffleToApply)], currentDeck);
			}

			results.AppendLine(string.Join("\n",  currentDeck));
			results.AppendLine();
		}
	}
	System.IO.File.WriteAllText(outputFile, results.ToString());

	numberOfTicks.Stop();
	//numberOfTicks.ElapsedTicks.Dump("ElapsedTicks");
	numberOfTicks.ElapsedMilliseconds.Dump("Elapsed ms");
}

// Define other methods and classes here

public static int[] ParseLine(string line) {
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