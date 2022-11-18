using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PC3
{
    /// Reads one line in at a time, and splits using the string.split function
    /// writes the results as generated
    class LCD5
    {
        StreamWriter output;
        StringBuilder top = new StringBuilder(150);
        StringBuilder topSides = new StringBuilder(150);
        StringBuilder middle = new StringBuilder(150);
        StringBuilder bottomsides = new StringBuilder(150);
        StringBuilder bottom = new StringBuilder(150);

        public long Run(string fileToProcess, string fileToWrite)
        {
            //string line;
            Stopwatch numberOfTicks = Stopwatch.StartNew();
            output = new StreamWriter(fileToWrite);

            foreach (string line in File.ReadLines(fileToProcess))
                {
                    string[] numbers = line.Split(' ');
                    ProcessNumber(int.Parse(numbers[0]), numbers[1]);
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

        void ProcessNumber(int size, string number)
        {
            if (size == 0)
                return;
            top.Clear();
            topSides.Clear();
            middle.Clear();
            bottomsides.Clear();
            bottom.Clear();

            string sideSpaces = string.Empty.PadLeft(size);
            string emptybar = string.Empty.PadLeft(size + 3);
            string dashes = " " + string.Empty.PadLeft(size, '-') + "  ";
            string leftonly = "|" + sideSpaces + "  ";
            string rightonly = sideSpaces + " | ";
            string both = "|" + sideSpaces + "| ";

            foreach (char digit in number)
            {
                switch (digit)
                {
                    case '0':
                        top.Append(dashes);
                        topSides.Append(both);
                        middle.Append(emptybar);
                        bottomsides.Append(both);
                        bottom.Append(dashes);
                        break;
                    case '1':
                        top.Append(emptybar);
                        topSides.Append(rightonly);
                        middle.Append(emptybar);
                        bottomsides.Append(rightonly);
                        bottom.Append(emptybar);
                        break;
                    case '2':
                        top.Append(dashes);
                        topSides.Append(rightonly);
                        middle.Append(dashes);
                        bottomsides.Append(leftonly);
                        bottom.Append(dashes);
                        break;
                    case '3':
                        top.Append(dashes);
                        topSides.Append(rightonly);
                        middle.Append(dashes);
                        bottomsides.Append(rightonly);
                        bottom.Append(dashes);
                        break;
                    case '4':
                        top.Append(emptybar);
                        topSides.Append(both);
                        middle.Append(dashes);
                        bottomsides.Append(rightonly);
                        bottom.Append(emptybar);
                        break;
                    case '5':
                        top.Append(dashes);
                        topSides.Append(leftonly);
                        middle.Append(dashes);
                        bottomsides.Append(rightonly);
                        bottom.Append(dashes);
                        break;
                    case '6':
                        top.Append(dashes);
                        topSides.Append(leftonly);
                        middle.Append(dashes);
                        bottomsides.Append(both);
                        bottom.Append(dashes);
                        break;
                    case '7':
                        top.Append(dashes);
                        topSides.Append(rightonly);
                        middle.Append(emptybar);
                        bottomsides.Append(rightonly);
                        bottom.Append(emptybar);
                        break;
                    case '8':
                        top.Append(dashes);
                        topSides.Append(both);
                        middle.Append(dashes);
                        bottomsides.Append(both);
                        bottom.Append(dashes);
                        break;
                    default:
                        //9
                        top.Append(dashes);
                        topSides.Append(both);
                        middle.Append(dashes);
                        bottomsides.Append(rightonly);
                        bottom.Append(dashes);
                        break;
                }
            }

            output.WriteLine(top);
            for (int i = 0; i < size; i++)
                output.WriteLine(topSides);
            output.WriteLine(middle);
            for (int i = 0; i < size; i++)
                output.WriteLine(bottomsides);
            output.WriteLine(bottom);
            output.WriteLine();
        }
    }
}
