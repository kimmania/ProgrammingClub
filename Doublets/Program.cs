
/*
 * Steps to complete:
 * 1. Load dictionary by processing words into meaningful arrangements
 *    a. Dictionary of words that point to the possible matching patterns; # patterns = # of letters, with each instance having one letter replaced with a period
 *    b. Dictionary of patterns that point to the matching words
 *    c. Clean out any record in the dictionary of patterns that only has one word to match as these are useless for this exercise
 * 2. Loop over pairs of words
 *    a. Process for possible solutions
*/


using Doublets;

var fileName = @"./DataFiles/Test2/Input.txt";
var dictionary = new DoubletDictionary();
using StreamReader reader = new StreamReader(fileName);
dictionary.InitializeDictionary(reader);


//I can test individual words prior to reviewing all
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("beer roof"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("a z"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("abase agave"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("abash agave"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("abbe quay"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("acorn runic"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("adobe runic"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("aloud runic"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("badge runic"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("barge runic"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("barre runic"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("runic badge"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("quack belch"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("belch quack"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("belie quack"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("comic quack"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("quack runic"));
//Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet("belch jowly"));

int test = 1;
while (!reader.EndOfStream)
{
    Console.WriteLine($"Test #{test++}:");
    Console.WriteLine(dictionary.DetermineShortestSequenceToDoublet(reader.ReadLine()));
}