// See https://aka.ms/new-console-template for more information
using WheresWaldorf;

var cases = StaticUtilities.LoadCasesFromFile("./SampleInput.txt");
foreach (var instance in cases)
{
    Console.WriteLine(instance.SolveCase());
}

//todo: evaluate the points for the end letter by using the calculated