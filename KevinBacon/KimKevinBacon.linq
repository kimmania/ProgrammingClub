<Query Kind="Program" />

void Main()
{
	//string fileFolder = @"C:\Users\Kim\Documents\LINQPad Queries\PC\Bacon\data\TestData\";
	string fileFolder = @"C:\Users\Kim\Documents\LINQPad Queries\PC\Bacon\data\RealData\";
	Process process = new Process(fileFolder);
	process.Run().Dump();
}

public class Process
{
	public Process(string folder)
	{
		FileFolder = folder;
		results = new StringBuilder();
	}
	#region publicProperties
	public string FileFolder { get; private set; }
	public string OutputFile { get { return FileFolder + "KevinBacon-output.txt"; } }
	#endregion publicProperties
	
	#region privateProperties
	private string ActorFileName { get { return FileFolder + "actors.txt"; } }
	private string MovieActorFileName { get { return FileFolder + "movie-actors.txt"; } }
	private StringBuilder results; 
	private Stopwatch numberOfTicks;
	private Dictionary<int, Actor> ActorDictionary = new Dictionary<int, Actor>();
	private Dictionary<int, Movie> MovieDictionary = new Dictionary<int, Movie>();
	private int KevinBaconId = 0; 
	#endregion privateProperties
	
	#region publicMethods
	public long Run()
	{
		numberOfTicks = Stopwatch.StartNew();
		
		LoadActorsFile();
		LoadMovieActorsFile();
		CalculateDegreeOfSeparation();
		GenerateOutput();
		numberOfTicks.Stop();
		return numberOfTicks.ElapsedMilliseconds;
	}
	#endregion publicMethods
	#region privateMethods
	private void LoadActorsFile()
	{
		var input = System.IO.File.ReadAllLines(ActorFileName);
		foreach (var line in input)
		{
			var temp = new Actor(line);
			if (temp.Name == "Kevin Bacon")
			{
				KevinBaconId = temp.ActorId;
				temp.UpdateDegree(0);
			}
			ActorDictionary.Add(temp.ActorId, temp);
		}
		input = null;
	}
	
	private void LoadMovieActorsFile()
	{
		var input = System.IO.File.ReadAllLines(MovieActorFileName);
		int currentMovie = 0;
		Movie tempMovie = new Movie();
		
		//might need to switch this to read line by line for memory reasons....
		foreach (var line in input)
		{
			Tuple<int, int> values = Utils.IntTupleParse(line);
			if (values.Item1 != currentMovie)
			{
				MovieDictionary.Add(currentMovie, tempMovie); // the overhead of having an extra movie in here is worth not having an extra if statement for the zero initialization
				currentMovie = values.Item1; //set for the next break
				tempMovie = new Movie();
			}
			tempMovie.AddActor(values.Item2);
			ActorDictionary[values.Item2].AddMovie(values.Item1); // because I am loading the actors, I am saying the data is classified as bad if this line fails...
		}
		//deal with the last movie
		MovieDictionary.Add(currentMovie, tempMovie);
		input = null;		
	}
	private void CalculateDegreeOfSeparation()
	{
		Queue<Tuple<int,Movie>> toProcess = new Queue<System.Tuple<int, Movie>>();
		
		//Start with Kevin Bacon 
		foreach (var movieID in ActorDictionary[KevinBaconId].NextMovie)
		{
			toProcess.Enqueue(Tuple.Create(1,MovieDictionary[movieID]));
			MovieDictionary.Remove(movieID);
		}
	
		//dequeue movie, loop on actors and update, then load the actor's movies into the queue
		Tuple<int, Movie> tempMovie;
		Actor tempActor;
		int nextKevinNumber = 0;
		while (toProcess.Count() > 0)
		{
			tempMovie = toProcess.Dequeue();
			nextKevinNumber = tempMovie.Item1 + 1;
			foreach (var actorID in tempMovie.Item2.NextActor)
			{
				tempActor = ActorDictionary[actorID];
				if (!tempActor.processed)
				{
					tempActor.UpdateDegree(tempMovie.Item1);
					
					foreach (var movieID in tempActor.NextMovie)
					{
						if (MovieDictionary.ContainsKey(movieID))
						{
							toProcess.Enqueue(Tuple.Create(nextKevinNumber, MovieDictionary[movieID]));
							MovieDictionary.Remove(movieID);
						}//still exists in the dictionary of movies
					}//foreach movies
				}//if not processed
			}//foreach on actors
		}//while
	}

	private void GenerateOutput()
	{
		System.IO.File.WriteAllText(OutputFile, string.Join("\n", ActorDictionary.Values.OrderBy(v => v.OutputLine).Select(v => v.OutputLine)));
	}
	private void GenerateOutput2()
	{
		System.IO.File.WriteAllText(OutputFile, string.Join("\n", ActorDictionary.Values.OrderBy(v => v.CurrentKevinDegrees).Select(v => v.OutputLine2)));
	}
	#endregion privateMethods
}

public class Actor
{
	public Actor(string line)
	{
		CurrentKevinDegrees = 10000;
		processed = false;
		var temp = Utils.ActorTupleParse(line);
		ActorId = temp.Item1;
		Name = temp.Item2;
	}

	public void AddMovie(int movie)
	{
		Movies.Add(movie);
	}

	public void UpdateDegree(int value)
	{
		if (value < CurrentKevinDegrees)
			CurrentKevinDegrees = value;
	}

	public string OutputLine
	{
		get
		{
			if (CurrentKevinDegrees < 1000)
				return Name + "|" + CurrentKevinDegrees;
			else
				return Name + "|infinity";
		}
	}
	public string OutputLine2
	{
		get
		{
			if (CurrentKevinDegrees < 1000)
				return CurrentKevinDegrees + "|" + Name;
			else
				return "infinity|" + Name;
		}
	}
	public int ActorId { get; private set; } 
	public int CurrentKevinDegrees { get; private set; }
	public bool processed { get; private set; }
	List<int> Movies = new List<int>();
	public string Name { get; private set;}
	public IEnumerable<int> NextMovie 
	{ 
		get 
		{ 
			processed = true;
			return Movies.Select(o => o); 
		} 
	}
}

public class Movie
{
	public Movie()
	{
	}

	public Movie(int actor)
	{
		Actors.Add(actor);
	}
	public void AddActor(int actor)
	{
		Actors.Add(actor);
	}
	
	List<int> Actors = new List<int>();
	public IEnumerable<int> NextActor 
	{ 
		get 
		{ 
			return Actors.Select(o => o); 
		} 
	}
}

public static class Utils
{
	public static int IntParseFast(string value)
	{
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			result = 10 * result + (value[i] - 48);
		}
		return result;
	}

	public static Tuple<int, int> IntTupleParse(string value, char delimiter = '|')
	{
		//todo: use the refactor to use logic like parsefast instead of using the split method
		var values = value.Split(delimiter);
		return Tuple.Create(IntParseFast(values[0]), IntParseFast(values[1]));
	}
	
	public static Tuple<int, string> ActorTupleParse(string value, char delimiter = '|')
	{
		var values =value.Split(delimiter);
		return Tuple.Create(IntParseFast(values[0]), values[1]);
	}
}