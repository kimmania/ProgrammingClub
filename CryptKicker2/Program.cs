using CryptKicker2;

var messages = 
//StaticUtilities.LoadCasesFromFile("./InputFiles/ProblemSampleInput.txt");
//StaticUtilities.LoadCasesFromFile("./InputFiles/SampleInput1.txt");
//StaticUtilities.LoadCasesFromFile("./InputFiles/SampleInput2.txt");
StaticUtilities.LoadCasesFromFile("./InputFiles/SampleInput3.txt");
foreach (var message in messages)
{
    Console.WriteLine(message.Decrypt());
}


//Look at 850.pdf for the problem
//have set up some sample files, haven't added a dynamic reading of content



//solution also is very much structured around the known key.
//if the key was dynamically supplied, I'd definitely make that block of code more dynamic.