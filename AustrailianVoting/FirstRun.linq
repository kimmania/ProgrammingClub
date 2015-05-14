<Query Kind="Program" />

void Main()
{
	Stopwatch numberOfTicks = Stopwatch.StartNew();
	string fileToProcess = @"C:\Users\Kim\Documents\LINQPad Queries\PC\AustralianVoting-input-2.txt";
	string outputFile = @"C:\Users\Kim\Documents\LINQPad Queries\PC\AustralianVoting-output-2.txt";
	int numOfElections = 0;
	StringBuilder results = new StringBuilder();
	
	using (var input = System.IO.File.OpenText(fileToProcess))
  	{
		//first line is the number of elections
		numOfElections = IntParseFast(input.ReadLine());
		input.ReadLine(); // blank line
		
		
		for (int i = 0; i < numOfElections; i++) //run until we have no more elections to run
		{
			Election currentElection = new Election(i, input);
			results.AppendLine(currentElection.CalculateWinner());
			//results.AppendLine();
			//currentElection.CalculateWinner().Dump();
		}
	}

	System.IO.File.WriteAllText(outputFile, results.ToString());

	numberOfTicks.Stop();
	//numberOfTicks.ElapsedTicks.Dump("ElapsedTicks");
	numberOfTicks.ElapsedMilliseconds.Dump("Elapsed ms");
}

public class Election
{
	int numOfViableCandidates = 0;
	int totalVotes = 1;	// setting to 1 so that when dividing by 2, I don't need to worry about adjusting for rounding
	int minimumVotesToWin = 0;
	public int ElectionNumber;	//to use for parallel processing of elections and be able to sort final results
	int currentMaxNumberOfVotes = 0;
	
	//storing the number as a string so that I don't have to cast to numbers while reading votes
	Dictionary<string, Candidate> candidates = new Dictionary<string, Candidate>();
	
	public Election()
	{
		throw new NotImplementedException();
	}
	
	//load the election data
	public Election(int election, StreamReader input)
	{
		ElectionNumber = election;
		
		//read the number of candidates
		numOfViableCandidates = IntParseFast(input.ReadLine());
		for(int i = 1; i <= numOfViableCandidates; i++)
		{
			//read the name, create the candidate
			candidates.Add(i.ToString(), new Candidate{CandidateName = input.ReadLine(), IsStillInTheRunning = true});
		}	
		
		string candidate;
		string line;
		string remaining;
		//read the votes until we get to an empty line
		while(!string.IsNullOrWhiteSpace(line = input.ReadLine()))
		{
			totalVotes++;
			remaining = null;
			candidate = GrabNextVote(line, out remaining);
			AddVoteToNewCandidate(candidate, remaining);
		}
		
		minimumVotesToWin = (totalVotes/2) + 1;		
	}
	
	public int AddVoteToNewCandidate(string candidateName, string remainingVotes)
	{
		int candidateCount = candidates[candidateName].AddVoter(remainingVotes);
			
		if (candidateCount > currentMaxNumberOfVotes)
		{
			currentMaxNumberOfVotes = candidateCount;
		}
		return candidateCount;
	}
	
	public string CalculateWinner()
	{
		do
		{
			if (currentMaxNumberOfVotes >= minimumVotesToWin)
			{
				//we know we have a clear winner, so just stop and return
				string result = candidates.First (c => c.Value.IsStillInTheRunning && c.Value.NumberOfVotes == currentMaxNumberOfVotes).Value.CandidateName;
				return result;
			}
			
			//need to find the next batch of lowest vote getters
			var currentLowestVoteTotal = candidates.Where (c => c.Value.IsStillInTheRunning == true).Min (c => c.Value.NumberOfVotes);
			var candidatesToProcess = candidates.Where (c => c.Value.NumberOfVotes == currentLowestVoteTotal).Select (c => c.Key).ToList();
			
			//check if we have any candidates left after we would process -- if not, exit with the list as our winners (tie)
			if (numOfViableCandidates == candidatesToProcess.Count())
			{
				string result = string.Empty;
				foreach (var element in candidatesToProcess)
				{
					result +=  candidates[element].CandidateName + "\n";
				}
				
				return result.Remove(result.Length - 1);
			}
			
			foreach (var candidate in candidatesToProcess)
			{
				--numOfViableCandidates;
				candidates[candidate].MarkToBeCleared();
			}
			
			if (numOfViableCandidates == 1)
			{
				//find the remaining and return
				string result = candidates.First (c => c.Value.IsStillInTheRunning).Value.CandidateName;
				return result;
			}
			
			//process entries
			foreach (var candidate in candidatesToProcess)
			{
				foreach (var vote in candidates[candidate].RemainingVotes)
				{	
					string newcandidate;
					string remaining = vote;
					do
					{
						newcandidate = GrabNextVote(remaining, out remaining);
					}while(newcandidate != null && !candidates[newcandidate].IsStillInTheRunning && remaining != null);
					
					if (newcandidate != null)
					{
						AddVoteToNewCandidate(newcandidate, remaining);
						if (currentMaxNumberOfVotes >= minimumVotesToWin)
							return candidates[newcandidate].CandidateName;
					}
				}
			}
		} while(currentMaxNumberOfVotes < minimumVotesToWin);
		return "none"; // I am wrong here....
	}
	
	private string GrabNextVote(string input, out string remaining)
	{
		string[] results = input.Split(new char[] { ' ' }, 2);
		if (results.Count() == 2)
		{
			remaining = results[1];
			return results[0];
		}
		else if (results.Count() == 1)
		{
			remaining = null;
			return results[0];
		}
		remaining = null;
		return null;
	}
}

public class Candidate
{
	public string CandidateName;
	public int NumberOfVotes
	{
		get 
		{
			return numberOfVotes;
		}
	}
	private int numberOfVotes = 0;
	
	public bool IsStillInTheRunning
	{
		get;
		set;
	}
	
	public bool ToBeCleared
	{
		get;
		set;
	}
	public List<string> RemainingVotes = new List<string>();
		
	public int AddVoter(string input)
	{
		if (input != null)
			RemainingVotes.Add(input);
			
		return ++numberOfVotes;
	}
	
	public void MarkToBeCleared()
	{
		IsStillInTheRunning = false;
		ToBeCleared = false;
	}

}

// Define other methods and classes here
public static int IntParseFast(string value)
{
  int result = 0;
  for (int i = 0; i < value.Length; i++)
  {
      result = 10 * result + (value[i] - 48);
  }
  return result;
}