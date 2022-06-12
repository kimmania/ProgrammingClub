//using KuhnMunkresCSharp;
using Yahtzee;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//will use to generate some randome files
//Game game = new Game();
//Console.Write(game.ToString());

//KuhnMunkres kh = new KuhnMunkres();
//int[,] original = new int[,]
//{
//    { 5, 0, 0, 0, 0, 0, 5, 5, 5, 50, 0, 0, 0 },
//    { 0, 0, 0, 0, 0, 30, 30, 30, 30, 50, 0, 0, 0},
//    { 2, 0, 0, 0, 0, 18, 20, 20, 0, 0, 0, 0, 40},
//    { 3, 4, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 40},
//    { 3, 2, 3, 0, 0, 0, 8, 8, 0, 0, 0, 0, 0},
//    { 1, 2, 3, 4, 5, 0, 15, 0, 0, 0, 25, 35, 0},
//    { 1, 2, 3, 4, 0, 6, 16, 0, 0, 0, 25, 0, 0},
//    { 1, 2, 0, 0, 0, 18, 21, 21, 0, 0, 0, 0, 0},
//    { 1, 0, 0, 4, 15, 0, 20, 20, 0, 0, 0, 0, 0},
//    { 0, 0, 0, 0, 20, 6, 26, 26, 26, 0, 0, 0, 0},
//    { 0, 0, 0, 12, 5, 6, 23, 23, 0, 0, 0, 0, 0},
//    { 1, 0, 9, 0, 0, 6, 16, 16, 0, 0, 0, 0, 0},
//    { 0, 6, 0, 4, 0, 6, 16, 16, 0, 0, 0, 0, 0},
//};

//int[,] dissimilarity = new int[,]
//{
//    { 45,50,50,50,50,50,45,45,45,0, 50,50,50 },
//    { 50,50,50,50,50,20,20,20,20,0, 50,50,50 },
//    { 48,50,50,50,50,32,30,30,50,50,50,50,10 },
//    { 47,46,50,50,50,50,43,43,50,50,50,50,10 },
//    { 47,48,47,50,50,50,42,42,50,50,50,50,50 },
//    { 49,48,47,46,45,50,35,50,50,50,25,15,50 },
//    { 49,48,47,46,50,44,34,50,50,50,25,50,50 },
//    { 49,48,50,50,50,32,29,29,50,50,50,50,50 },
//    { 49,50,50,46,35,50,30,30,50,50,50,50,50 },
//    { 50,50,50,50,30,44,24,24,24,50,50,50,50 },
//    { 50,50,50,38,45,44,27,27,50,50,50,50,50 },
//    { 49,50,41,50,50,44,34,34,50,50,50,50,50 },
//    { 50,44,50,46,50,44,34,34,50,50,50,50,50 }
//};

//int[] res = kh.Solve(dissimilarity);

//for (int i = 0; i < res.Count(); i++)
//    Console.WriteLine($"{i}, {res[i]}, {original[i, res[i]]}");
//Console.WriteLine("");


//read data in from file
var games = StaticUtilities.LoadGamesFromFile("./SampleFiles/simpleTest.txt");
foreach (var game in games)
{
    //Console.WriteLine($"{game.PrintRowScores()}\n\n");
    game.EvaluateScores();
    Console.WriteLine(game.PrintFinalScore());
}