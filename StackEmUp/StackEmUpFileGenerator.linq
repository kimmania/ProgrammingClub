<Query Kind="Program" />

public StringBuilder results = new StringBuilder();
public string outputFile = @"C:\Users\Kim\Documents\LINQPad Queries\PC\StackEmUp-input-1.txt";

void Main()
{
	Random rnd = new Random();		
	int maxDealers = 1000;
	int maxKnownShuffles = 100;
	int maxShuffles = 1000;
	
	WriteOutputLine(maxDealers);
	for (int i = 0; i < maxDealers; i++)
	{	
		WriteWhiteLine(); // whiteline between scenarios
		int knownShuffles = rnd.Next(1,maxKnownShuffles + 1); 
		WriteOutputLine(knownShuffles);
		for (int j= 1;  j <= knownShuffles;  j++)
		{
			var cardPrinted = 0;
			foreach (var element in Enumerable.Range(1,52).OrderBy(c => Guid.NewGuid()).Take(52))
			{
				WriteSingleOutput(element, ++cardPrinted != 52);
			}
		}
		//write out the order of the shuffles
		int numberOfShuffles = rnd.Next(1, maxShuffles + 1);
		for (int j = 1; j <= numberOfShuffles; j++)
		{
			WriteOutputLine(rnd.Next(1, knownShuffles + 1));
		}
	}	
	System.IO.File.WriteAllText(outputFile, results.ToString());
	//results.ToString().Dump();
}

// Define other methods and classes here
public void WriteOutputLine(int line){
	results.AppendLine(line.ToString());
}

public void WriteWhiteLine() {
	results.AppendLine();
}

public void WriteSingleOutput(int card, bool printSpace) {
	results.Append(card);
	if (printSpace)
		results.Append(" ");
	else
		results.AppendLine();
}