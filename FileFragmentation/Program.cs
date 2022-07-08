using FileFragmentation;

var sets = StaticUtilities.LoadCasesFromFile("./DataFiles/Test1/Input.txt");

foreach (var set in sets)
{
    Console.WriteLine(set.Defrag());
    Console.WriteLine();
}

