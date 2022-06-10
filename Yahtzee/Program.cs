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

