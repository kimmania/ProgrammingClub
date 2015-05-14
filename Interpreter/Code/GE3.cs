using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace PC5
{
    /// Reads one line in at a time, and splits using the string.split function
    /// writes the results as generated
    class Interpreter3
    {
        StreamWriter output;
        NewInts[] registers = new NewInts[10];
        NewInts[] ram = new NewInts[1000];

        NewInts[] registersReset = new NewInts[10];
        NewInts[] ramReset = new NewInts[1000];

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

                //initialize the ram and registers
                for (int i = 0; i < 10; i++)
                {
                    registersReset[i] = new NewInts();
                }

                for (int i = 0; i < 1000; i++)
                {
                    ramReset[i] = new NewInts();
                }

                while (setsExecuted++ < numberOfSets)
                {
                    //currently these are not working
                    registers = registersReset;
                    ram = ramReset;

                    //load the values into the RAM
                    linecounter = 0;
                    while (((line = input.ReadLine()) != null) && !string.IsNullOrWhiteSpace(line))
                    {
                        ram[linecounter++].Update(line);
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

            NewInts instruction;
            while (ramLocation < ram.Length)
            {
                ++executions;
                instruction = ram[ramLocation];

                if (instruction.value < 100)
                {
                    if (registers[instruction.ones].value != 0)
                    {
                        ramLocation = registers[instruction.tens].value;
                        continue;
                    }
                }
                else if (instruction.value < 200)
                    return executions; //halt
                else if (instruction.value < 300)
                    registers[instruction.tens].value = instruction.ones;
                else if (instruction.value < 400)
                    //registers[instruction.tens].Add(instruction.ones); 
                     registers[instruction.tens] += instruction.ones;
                else if (instruction.value < 500)
                    //registers[instruction.tens].Multiply(instruction.ones); 
                    registers[instruction.tens] *= instruction.ones;
                else if (instruction.value < 600)
                    registers[instruction.tens] = registers[instruction.ones];
                else if (instruction.value < 700)
                    //registers[instruction.tens].Add(registers[instruction.ones].value);
                    registers[instruction.tens] += registers[instruction.ones];
                else if (instruction.value < 800)
                    //registers[instruction.tens].Multiply(registers[instruction.ones].value);
                    registers[instruction.tens] *= registers[instruction.ones];
                else if (instruction.value < 900)
                    registers[instruction.tens] = ram[registers[instruction.ones].value];
                else
                    ram[registers[instruction.ones].value] = registers[instruction.tens];

                ++ramLocation;
            }

            return executions;
        }


        public class NewInts
        {
            public int value;
            public int ones { get { return value % 10; } set { } }
            public int tens { get { return (value / 10) % 10; } set { } }
            public int hundreds { get { return value / 100; } set { } }

            public NewInts()
            {
                value = 0;
            }

            public NewInts(int v)
            {
                value = v;
            }

            public void Update(string a)
            {
                value = Utilities.ParseInt(ref a);
            }

            public void Add(int b)
            {
                value = (value + b) % 1000;
            }

            public void Multiply(int b)
            {
                value = (value * b) % 1000;
            }
            public static NewInts operator +(NewInts a, NewInts b)
            {
                return new NewInts((a.value + b.value) % 1000);
            }

            public static NewInts operator +(NewInts a, int b)
            {
                return new NewInts((a.value + b) % 1000);
            }

            public static NewInts operator *(NewInts a, NewInts b)
            {
                return new NewInts((a.value * b.value) % 1000);
            }
            public static NewInts operator *(NewInts a, int b)
            {
                return new NewInts((a.value * b) % 1000);
            }

        }
    }
    /*
    static struct pairs
    {
        int ones;
        int tens;

        public pairs(int o, int t)
        {
            ones = o;
            tens = t;
        }
    }

    class multipliers
    {
        public Dictionary<pairs, pairs> mults = new Dictionary<pairs, pairs>();

        public multipliers()
        {
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
            mults.Add(new pairs(0, 0), new pairs(0, 0));
        }
    }
     * */
}
