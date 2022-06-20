// See https://aka.ms/new-console-template for more information
using WheresWaldorf;

var cases = StaticUtilities.LoadCasesFromFile("./SampleInput.txt");
foreach (var instance in cases)
{
    Console.WriteLine(instance.SolveCase());
}