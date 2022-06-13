<Query Kind="Program" />

void Main()
{
	string fileToProcess = @"C:\Users\Kim\Documents\LINQPad Queries\PC\1testfile.txt";

	Dictionary<string, List<Word>> words = new Dictionary<string, List<Word>>();
	using (var input = System.IO.File.OpenText(fileToProcess))
  	{
		//first line is the number of words for the dictionary
		int numOfWords = IntParseFast(input.ReadLine());
		//load the dictionary of words
		for (int i = 0; i < numOfWords; i++) //run until we have no more words to load
		{
			Word newWord = new Word(input.ReadLine());
			if (words.ContainsKey(newWord.pattern))
				words[newWord.pattern].Add(newWord);
			else
			{
				List<Word> wordToAdd = new List<Word>();
				wordToAdd.Add(newWord);
				words.Add(newWord.pattern, wordToAdd);
			}
		}
		//words.Dump();
		bool patternsFound = true;
		string line = "dlwkpozk ulhsrbx kzxkwb ulybxo rxcrm swpv hbnnzon fcsrzkb lxvmpopnb vlzslo nllo";
		//"sos esoeir ensss snccses nctsndos eoscoed dottnet tosnsecn tscsnoed ssto ena";
		string result = string.Empty;
		//while(!string.IsNullOrWhiteSpace(line = input.ReadLine()))
		//{
			Word inputLine = new Word(line);
			string[] wordsToParse = line.Split(' ');
			List<Word> patternizedWords = new List<Word>();
			foreach (var aword in wordsToParse)
			{
				Word pWord = new Word(aword);
				patternizedWords.Add(pWord);
				if (!words.ContainsKey(pWord.pattern))
				{
					patternsFound = false;
					break; //break out of the for loop, no reason to continue
				}
			}
			
			if (patternsFound)
			{
				//patternizedWords.Dump();
				List<State> backtrackArray = new List<State>();
				//initialize to so we aren't repeatedly checking for having to add
				
				for (int i = 0; i <= wordsToParse.Length; i++)
				{
					backtrackArray.Add(new State());
				}
				
				int depth = 0;
				bool notfound = true;
				while (notfound)
				{
					while (depth < wordsToParse.Length && depth >= 0)
					{
						//need to have a way of getting out the loop entirely
						//if depth gets all the way back to 0 and used all of the words, then no match, break
						if (depth > 0 && words[patternizedWords[depth].pattern].Count() == backtrackArray[depth].wordInDictionary)
						{
							backtrackArray[depth].wordInDictionary = 0;
							depth--;
						}
						else
						{
							//get word for the depth
							string nextDepthWord = words[patternizedWords[depth].pattern][backtrackArray[depth].wordInDictionary++].word;
							//append to the depth's string
							backtrackArray[depth + 1].backtrackString = backtrackArray[depth++].backtrackString + " " + nextDepthWord;
						}
					}
					if (depth == -1)
					{
						result = GenerateBadLine(wordsToParse);
						notfound = false;
					}
					else
					{
						//evaluate generated string
						Word testPhrase = new Word(backtrackArray[depth].backtrackString);
						if (testPhrase.pattern.Equals(inputLine.pattern))
						{
							//match -> found our translation
							notfound = false;
							result = testPhrase.word;
						}
						else
						{
							//not match
							//need to backtrack a level until we have used all words in the current level
							--depth;
						}
					}
					backtrackArray.Dump();
				}
			}
			else
			{
				result = GenerateBadLine(wordsToParse);
			}
			result.Dump();
		//}
	}
}

// Define other methods and classes here
public class Word
{
	public string pattern;
	public string word;
	public IEnumerable<char> letters;
	
	private Word(){	}
	
	public Word(string input)
	{
		word = input;
		letters = input.Distinct();
		pattern = input;
		int letterCount = 0;
		foreach (var letter in letters)
		{
			if (!letter.Equals(' '))
				pattern = pattern.Replace(letter.ToString(), (++letterCount).ToString() + "|");
		} 
	}
}

public class State
{
	public int wordInDictionary {get; set;}
	public string backtrackString {get; set;}
	
	public State()
	{
		wordInDictionary = 0;
		backtrackString = string.Empty;
	}
}

public static string GenerateBadLine(string[] words)
{
	StringBuilder lineToReturn = new StringBuilder();
	lineToReturn.Capacity = 81;
	string badSubstitution ="xxxxxxxxxxxxxxxxxxxx";
	
	foreach (var element in words)
	{
		lineToReturn.AppendFormat("{0} ", badSubstitution.Substring(0, element.Length));
	}
	return lineToReturn.ToString().Trim();
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