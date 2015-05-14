using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PC3
{
    /// Reads one line in at a time, and splits using the string.split function
    /// writes the results as generated
    class LCD3
    {
        StreamWriter output;
        sizes characters;

        public long Run(string fileToProcess, string fileToWrite)
        {
            //string line;
            Stopwatch numberOfTicks = Stopwatch.StartNew();
            output = new StreamWriter(fileToWrite);
            characters = new sizes();

            foreach (string line in File.ReadLines(fileToProcess))
            {
                string[] numbers = line.Split(' ');
                ProcessNumber(numbers[0], numbers[1]);
            }

            output.Flush();
            numberOfTicks.Stop();
            output.WriteLine("Total Milliseconds: " + numberOfTicks.ElapsedMilliseconds);
            output.WriteLine("Total Ticks: " + numberOfTicks.ElapsedTicks);
            output.Flush();
            output.Close();
            output.Dispose();

            return numberOfTicks.ElapsedTicks;
        }

        void ProcessNumber(string sizeToPrint, string number)
        {
            if (sizeToPrint.Equals("0"))
                return;

            size values = characters.szs[sizeToPrint];
            //top
            foreach (char digit in number)
            {
                switch (digit)
                {
                    case '1':
                    case '4':
                        output.Write(values.empty);
                        break;
                    default:
                        //0,2,3,5,6,7,8,9
                        output.Write(values.bar);
                        break;
                }
            }
            output.WriteLine();
            //top sides
            for (int i = 0; i < values.sz; i++)
            {
                foreach (char digit in number)
                {
                    switch (digit)
                    {
                        case '1':
                        case '2':
                        case '3':
                        case '7':
                            output.Write(values.rightonly);
                            break;
                        case '5':
                        case '6':
                            output.Write(values.leftonly);
                            break;
                        default:
                            //0,4,8,9
                            output.Write(values.both);
                            break;
                    }
                }
                output.WriteLine();
            }
            //middle
            foreach (char digit in number)
            {
                switch (digit)
                {
                    case '0':
                    case '1':
                    case '7':
                        output.Write(values.empty);
                        break;
                    default:
                        //2,3,4,5,6,8,9
                        output.Write(values.bar);
                        break;
                }
            }
            output.WriteLine();
            //bottom sides
            for (int i = 0; i < values.sz; i++)
            {
                foreach (char digit in number)
                {
                    switch (digit)
                    {
                        case '0':
                        case '6':
                        case '8':
                            output.Write(values.both);
                            break;
                        case '2':
                            output.Write(values.leftonly);
                            break;
                        default:
                            //1,3,4,5,7,9
                            output.Write(values.rightonly);
                            break;
                    }
                }

                output.WriteLine();
            }
            //bottom
            foreach (char digit in number)
            {
                switch (digit)
                {
                    case '1':
                    case '4':
                    case '7':
                        output.Write(values.empty);
                        break;
                    default:
                        //0,2,3,5,6,8,9
                        output.Write(values.bar);
                        break;
                }
            }
            output.WriteLine();
        }
    }
}
