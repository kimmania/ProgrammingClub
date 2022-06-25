using CryptKicker2;

var messages = StaticUtilities.LoadCasesFromFile("./InputFiles/ProblemSampleInput.txt");
foreach (var message in messages)
{
    Console.WriteLine(message.Decrypt());
}
