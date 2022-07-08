
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
//Console.WriteLine((new DoubletProcessor(dictionary, "beer roof").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "a z").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "abase agave").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "abash agave").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "abbe quay").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "acorn runic").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "adobe runic").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "aloud runic").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "badge runic").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "barge runic").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "barre runic").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "runic badge").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "quack belch").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "belch quack").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "belie quack").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "comic quack").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "quack runic").ProcessDoublet()));
//Console.WriteLine((new DoubletProcessor(dictionary, "belch jowly").ProcessDoublet()));

int test = 1;
while (!reader.EndOfStream)
{
    Console.WriteLine($"Test #{test++}:");
    Console.WriteLine((new DoubletProcessor(dictionary, reader.ReadLine()).ProcessDoublet()));
}