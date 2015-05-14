using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace PC3
{
    /// <summary>
    /// Reads in all of the text and uses a regular expression to split the lines for processing
    /// writes the results as generated
    /// </summary>
    class LCD2
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
            string regFind = @"(?'size'\d{1,2}) (?'number'\d{1,10})";
            Stopwatch numberOfTicks = Stopwatch.StartNew();
            output = new StreamWriter(fileToWrite);
            characters = new sizes();
           
            foreach (Match m in Regex.Matches(File.ReadAllText(fileToProcess), regFind, RegexOptions.Compiled))
            {
                ProcessNumber(m.Groups["size"].Value, m.Groups["number"].Value);
            }

            output.Flush();
            numberOfTicks.Stop();
            output.WriteLine("Total Milliseconds: " + numberOfTicks.ElapsedMilliseconds);
            output.WriteLine("Total Ticks: " + numberOfTicks.ElapsedTicks);
            output.Flush();
            output.Close();
            output.Dispose();

            //http://stackoverflow.com/questions/10788972/matchcollection-parallel-foreach
            //http://social.msdn.microsoft.com/Forums/en-US/parallelextensions/thread/47c060dc-d8fa-44d7-8a53-6d85535bc063/
            //http://stackoverflow.com/questions/1271225/c-sharp-reading-a-file-line-by-line

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
