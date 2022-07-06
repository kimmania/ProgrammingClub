using AutomatedJudgeScript;

var input = @"./DataFiles/Input.txt";
foreach (var evaluationSet in StaticUtilities.LoadCasesFromFile(input))
{
    Console.WriteLine(evaluationSet.Process());
}
