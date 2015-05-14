using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PC5
{
    /// Reads one line in at a time, and splits using the string.split function
    /// writes the results as generated
    class Interpreter2
    {
        StreamWriter output;
        int[] registers = new int[10];
        int[] ram = new int[1000];

        int[] registersReset = new int[10];
        int[] ramReset = new int[1000];

        public long Run(string fileToProcess, string fileToWrite)
        {
            Stopwatch numberOfTicks = Stopwatch.StartNew();
            string line;
            int setsExecuted = 0;
            int linecounter = 0;

            output = new StreamWriter(fileToWrite);

            using (var input = System.IO.File.OpenText(fileToProcess))
            {
                //read in the number of sets
                int numberOfSets = Utilities.ParseInt(input.ReadLine());
                input.ReadLine(); //read the empty line
                

                while (setsExecuted < numberOfSets)
                {
                    if (setsExecuted++ > 0)
                    {
                        registers = registersReset;
                        ram = ramReset;
                    }//endif

                    //load the values into the RAM
                    linecounter = 0;
                    while (((line = input.ReadLine()) != null) && !string.IsNullOrWhiteSpace(line))
                    {
                        ram[linecounter++] = Utilities.ParseInt(ref line);
                    }//while

                    //process instructions
                    output.WriteLine(Solve());
                    output.WriteLine();
                }//while setsExecuted < numberOfSets
            }//using

            output.Flush();
            numberOfTicks.Stop();
            output.WriteLine("Total Milliseconds: " + numberOfTicks.ElapsedMilliseconds);
            output.WriteLine("Total Ticks: " + numberOfTicks.ElapsedTicks);
            output.Flush();
            output.Close();
            output.Dispose();

            return numberOfTicks.ElapsedTicks;
        }

        private int Solve()
        {
            int executions = 0;
            int ramLocation = 0;

            int instruction;
            while (ramLocation < ram.Length)
            {
                ++executions;
                instruction = ram[ramLocation];

                int tens = (instruction / 10) % 10;
                int ones = instruction % 10;

                if (instruction < 100)
                {
                    if (registers[ones] != 0)
                    {
                        ramLocation = registers[tens];
                        continue;
                    }
                }
                else if (instruction < 200)
                    return executions; //halt
                else if (instruction < 300)
                    registers[tens] = ones;
                else if (instruction < 400)
                    registers[tens] = (registers[tens] + ones) % 1000;
                else if (instruction < 500)
                    registers[tens] = (registers[tens] * ones) % 1000;
                else if (instruction < 600)
                    registers[tens] = registers[ones];
                else if (instruction < 700)
                    registers[tens] = (registers[tens] + registers[ones]) % 1000;
                else if (instruction < 800)
                    registers[tens] = (registers[tens] * registers[ones]) % 1000;
                else if (instruction < 900)
                    registers[tens] = ram[registers[ones]];
                else
                    ram[registers[ones]] = registers[tens];

                ++ramLocation;
            }

            return executions;
        }
    }
}
