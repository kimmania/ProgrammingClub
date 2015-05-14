using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PC5
{
    /// Reads one line in at a time, and splits using the string.split function
    /// writes the results as generated
    class Interpreter1
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

                int dig1 = instruction / 100;
                if (dig1.Equals(1))
                    return executions; //halt

                int dig2 = (instruction / 10) % 10;
                int dig3 = instruction % 10;

                switch (dig1)
                {
                    case 2:
                        registers[dig2] = dig3;
                        break;
                    case 3:
                        registers[dig2] = (registers[dig2] + dig3) % 1000;
                        break;
                    case 4:
                        registers[dig2] = (registers[dig2] * dig3) % 1000;
                        break;
                    case 5:
                        registers[dig2] = registers[dig3];
                        break;
                    case 6:
                        registers[dig2] = (registers[dig2] + registers[dig3]) % 1000;
                        break;
                    case 7:
                        registers[dig2] = (registers[dig2] * registers[dig3]) % 1000;
                        break;
                    case 8:
                        registers[dig2] = ram[registers[dig3]];
                        break;
                    case 9:
                        ram[registers[dig3]] = registers[dig2];
                        break;
                    case 0:
                        if (registers[dig3] != 0)
                        {
                            ramLocation = registers[dig2];
                            continue;
                        }
                        break;
                }

                ++ramLocation;
            }

            return executions;
        }
    }
}
