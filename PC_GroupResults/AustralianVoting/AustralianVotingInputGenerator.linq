<Query Kind="Statements" />

Random rnd = new Random();
string outputFile = @"C:\Users\Kim\Documents\LINQPad Queries\PC\AustralianVoting-input-4.txt";
int numberOfElections = 500;
int minNumberOfCandidates = 3;
int minNumberOfVoters = 10;
int maxNumberOfVoters = 1000;

string[] candidateNames = new string[] {"John", "Jane", "Jesus", "Javier", "Jessica", "Jamie", "James", "Juan", "Jerry", "Matt", "Greg", "Peter", "Paul", "Sarah", "Kim", "Brad", "Ed", "Dani", "Brian", "Gaby", "Chetan"};
int maxNumberNames = candidateNames.Count();

Console.WriteLine(numberOfElections);

StringBuilder results = new StringBuilder();
results.AppendLine(numberOfElections.ToString());
for (int i = 0; i < numberOfElections; i++)
{	
	//Console.WriteLine(); // write blank line to separate the elections
	results.AppendLine();
	//Random number of candidates
	int candidates = rnd.Next(minNumberOfCandidates,maxNumberNames); // random is not inclusive of end, so we want to go one over
	//Console.WriteLine(candidates);	// print number of candidates
	results.AppendLine(candidates.ToString());
	//print candidates names
	foreach (var element in Enumerable.Range(0,maxNumberNames - 1).OrderBy(k => Guid.NewGuid()).Take(candidates)) // range starts with 0, so subtract one from the number of names, randomize, then take the number of canidates
	{
		//Console.WriteLine(candidateNames[element]);
		results.AppendLine(candidateNames[element]);
	}

	//Random number of voters
	int numberOfVoters = rnd.Next(minNumberOfVoters, maxNumberOfVoters + 1);
	for (int j = 0; j < numberOfVoters; j++)
	{
		/*Console.WriteLine(
			String.Join(" ", 
						Enumerable.Range(1,candidates).OrderBy(k => Guid.NewGuid()).ToArray()	//Random votes, concat with space separator
			)
		);*/
		results.AppendLine(
		String.Join(" ", 
						Enumerable.Range(1,candidates).OrderBy(k => Guid.NewGuid()).ToArray()	//Random votes, concat with space separator
			)
		);
	}
	System.IO.File.WriteAllText(outputFile, results.ToString());
	
}