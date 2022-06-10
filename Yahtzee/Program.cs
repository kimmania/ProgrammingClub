using Yahtzee;
using Yahtzee.Models;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//will use to generate some randome files
//Game game = new Game();
//Console.Write(game.ToString());


//read data in from file
var games = StaticUtilities.LoadGamesFromFile("./SampleFiles/simpleTest.txt");
foreach (var game in games)
{
    Console.WriteLine($"{game}\n\n");
}

////tests hand scoring
//var testThrow = "1 2 3 4 5";
//var loadedThrow = new Throw(testThrow);
//Console.WriteLine(loadedThrow.Score.ToString());
//Console.WriteLine($"1 2 3 4 5 0 15 0 0 0 25 35 0");

//testThrow = "2 3 4 5 6";
//loadedThrow = new Throw(testThrow);
//Console.WriteLine(loadedThrow.Score.ToString());
//Console.WriteLine($"0 2 3 4 5 6 20 0 0 0 25 35 0");

//testThrow = "2 3 4 5 5";
//loadedThrow = new Throw(testThrow);
//Console.WriteLine(loadedThrow.Score.ToString());
//Console.WriteLine($"0 2 3 4 10 0 19 0 0 0 25 0 0");

//testThrow = "2 2 2 6 6";
//loadedThrow = new Throw(testThrow);
//Console.WriteLine(loadedThrow.Score.ToString());
//Console.WriteLine($"0 6 0 0 0 12 18 18 0 0 0 0 40");

//testThrow = "2 2 2 2 6";
//loadedThrow = new Throw(testThrow);
//Console.WriteLine(loadedThrow.Score.ToString());
//Console.WriteLine($"0 8 0 0 0 6 14 14 14 0 0 0 0");

//testThrow = "2 2 2 2 2";
//loadedThrow = new Throw(testThrow);
//Console.WriteLine(loadedThrow.Score.ToString());
//Console.WriteLine($"0 10 0 0 0 0 10 10 10 50 0 0 0");