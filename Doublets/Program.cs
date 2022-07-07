
/*
 * Steps to complete:
 * 1. Load dictionary by processing words into meaningful arrangements
 * 2. Loop over pairs of words
 *    a. Process for possible solutions
*/


using Doublets;

var fileName = @"./DataFiles/Test1/Input.txt";
var dictionary = new DoubletDictionary();
using StreamReader reader = new StreamReader(fileName);
dictionary.InitializeDictionary(reader);


//I can test individual words prior to reviewing all
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("booster roasted"));

while (!reader.EndOfStream)
{
    Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet(reader.ReadLine()));
}