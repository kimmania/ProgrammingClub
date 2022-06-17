<Query Kind="Program" />

void Main()
{
	//Problem WERTYU: 110301
	//User has shifted their homekeys on the keyboard by one, to transalate the resultant text to what was intended

	//Simple dictionary lookup to translate string to the correct values.
	//Not doing a complex solution for this as I'm not doing anything interesting with the solution
	Dictionary<char, char> translations = new Dictionary<char, char>(){
		{'2', '1'}, {'3', '2'}, {'4', '3'}, {'5', '4'}, {'6', '5'}, {'7', '6'}, {'8', '7'}, {'9', '8'}, {'0', '9'}, {'-', '0'}, {'=', '-'},
		{'W', 'Q'}, {'E', 'W'}, {'R', 'E'}, {'T', 'R'}, {'Y', 'T'}, {'U', 'Y'}, {'I', 'U'}, {'O', 'I'}, {'P', 'O'}, {'[', 'P'}, {']', '['}, {'\\',']'},
		{'S', 'A'},	{'D', 'S'},	{'F', 'D'},	{'G', 'F'},	{'H', 'G'},	{'J', 'H'},	{'K', 'J'},	{'L', 'K'},	{';', 'L'},	{'\'', ';'}, 
		{'X', 'Z'}, {'C', 'X'}, {'V', 'C'}, {'B', 'V'},{'N', 'B'}, {'M', 'N'}, {',', 'M'}, {'.', ','},	{'/', '.'},
		{' ', ' '},
	};
	
	var testString = "O S, GOMR YPFSU/";
	var output = new StringBuilder("");
	foreach (var c in testString.Select(x =>x))
	{
		output.Append(translations[c]);
	}
	output.ToString().Dump();
}

