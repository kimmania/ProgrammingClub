using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PC3
{
    /// Reads one line in at a time, and splits using the string.split function
    /// writes the results as generated
    class LCD1
    {
        StreamWriter output;
        StringBuilder top = new StringBuilder(150);
        StringBuilder topSides = new StringBuilder(150);
        StringBuilder middle = new StringBuilder(150);
        StringBuilder bottomsides = new StringBuilder(150);
        StringBuilder bottom = new StringBuilder(150);
        sizes characters;

        public long Run(string fileToProcess, string fileToWrite)
        {   
            string line;
            Stopwatch numberOfTicks = Stopwatch.StartNew();
            output = new StreamWriter(fileToWrite);
            characters = new sizes();
            using (var input = System.IO.File.OpenText(fileToProcess))
            {
                while ((line = input.ReadLine()) != null)
                {
                    string[] numbers = line.Split(' ');
                    ProcessNumber(numbers[0], numbers[1]);
                }
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

            top.Clear();
            topSides.Clear();
            middle.Clear();
            bottomsides.Clear();
            bottom.Clear();

            size values = characters.szs[sizeToPrint];
            foreach (char digit in number)
            {
                switch (digit)
                {
                    case '0':
                        top.Append(values.bar);
                        topSides.Append(values.both);
                        middle.Append(values.empty);
                        bottomsides.Append(values.both);
                        bottom.Append(values.bar);
                        break;
                    case '1':
                        top.Append(values.empty);
                        topSides.Append(values.rightonly);
                        middle.Append(values.empty);
                        bottomsides.Append(values.rightonly);
                        bottom.Append(values.empty);
                        break;
                    case '2':
                        top.Append(values.bar);
                        topSides.Append(values.rightonly);
                        middle.Append(values.bar);
                        bottomsides.Append(values.leftonly);
                        bottom.Append(values.bar);
                        break;
                    case '3':
                        top.Append(values.bar);
                        topSides.Append(values.rightonly);
                        middle.Append(values.bar);
                        bottomsides.Append(values.rightonly);
                        bottom.Append(values.bar);
                        break;
                    case '4':
                        top.Append(values.empty);
                        topSides.Append(values.both);
                        middle.Append(values.bar);
                        bottomsides.Append(values.rightonly);
                        bottom.Append(values.empty);
                        break;
                    case '5':
                        top.Append(values.bar);
                        topSides.Append(values.leftonly);
                        middle.Append(values.bar);
                        bottomsides.Append(values.rightonly);
                        bottom.Append(values.bar);
                        break;
                    case '6':
                        top.Append(values.bar);
                        topSides.Append(values.leftonly);
                        middle.Append(values.bar);
                        bottomsides.Append(values.both);
                        bottom.Append(values.bar);
                        break;
                    case '7':
                        top.Append(values.bar);
                        topSides.Append(values.rightonly);
                        middle.Append(values.empty);
                        bottomsides.Append(values.rightonly);
                        bottom.Append(values.empty);
                        break;
                    case '8':
                        top.Append(values.bar);
                        topSides.Append(values.both);
                        middle.Append(values.bar);
                        bottomsides.Append(values.both);
                        bottom.Append(values.bar);
                        break;
                    default:
                        //9
                        top.Append(values.bar);
                        topSides.Append(values.both);
                        middle.Append(values.bar);
                        bottomsides.Append(values.rightonly);
                        bottom.Append(values.bar);
                        break;
                }
            }

            output.WriteLine(top);
            for (int i = 0; i < values.sz; i++)
                output.WriteLine(topSides);
            output.WriteLine(middle);
            for (int i = 0; i < values.sz; i++)
                output.WriteLine(bottomsides);
            output.WriteLine(bottom);
            output.WriteLine();
        }
    }
}
