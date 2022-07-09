using FileFragmentation;

var sets = StaticUtilities.LoadCasesFromFile("./DataFiles/Test1/Input.txt");

foreach (var set in sets)
{
    Console.WriteLine(set.Defrag());
    Console.WriteLine();
}

//basic outline:
//  1. Group by length of words
//  2. Record smallest length and longest length
//  3. Pair these up to figure out possible solutions
//  4. Loop through other pairings by looking at the next highest from the low starting and the next lowest from the high end
//  5. add to counts of the possible solutions if they match the determined possible solutions
//  6. Take the value with the highest number of matching solution as the answer, or the first if there isn't a highest number