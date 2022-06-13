<Query Kind="Statements" />

string outputFile = @"C:\Users\Kim\Documents\LINQPad Queries\PC\PokerHands-input.txt";
int numberOfHands = 1000;


string[] cards = new string[] {"2C","3C","4C","5C","6C","7C","8C","9C","TC","JC","QC","KC","AC","2D","3D","4D","5D","6D","7D","8D","9D","TD","JD","QD","KD","AD","2H","3H","4H","5H","6H","7H","8H","9H","TH","JH","QH","KH","AH","2S","3S","4S","5S","6S","7S","8S","9S","TS","JS","QS","KS","AS"};

StringBuilder results = new StringBuilder();
for (int i = 0; i < numberOfHands; i++)
{	

	int cardsPrinted = 0;
	//print cards
	foreach (var element in Enumerable.Range(0,52).OrderBy(k => Guid.NewGuid()).Take(10)) // range starts with 0, so subtract one from the number of names, randomize, then take the number of canidates
	{
		//Console.Write(cards[element]);
		
		results.Append(cards[element]);
		if( ++cardsPrinted != 10)
			//Console.Write(" ");
			results.Append(" ");
	}
	results.AppendLine();
	//Console.WriteLine();
	System.IO.File.WriteAllText(outputFile, results.ToString());
}