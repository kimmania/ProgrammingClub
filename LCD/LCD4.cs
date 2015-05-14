using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace PC3
{
    /// <summary>
    /// Reads in all of the text and uses a regular expression to split the lines for processing
    /// Writes everything to disk instead of storing in temporary variables
    /// </summary>
    class LCD4
    { 
        StreamWriter output;
        char[,] lookupChar;

        public long Run(string fileToProcess, string fileToWrite)
        {
            string line;
            Stopwatch numberOfTicks = Stopwatch.StartNew();
            output = new StreamWriter(fileToWrite);

            /*
             * LED representation for each number, according to
             * the following convention:
             *
             *  -0-
             * |   |
             * 2   1
             * |   |
             *  -3-
             * |   |
             * 5   4
             * |   |
             *  -6-
             *
             */

            lookupChar = new char[,]{
		            /* 0   1   2   3   4   5   6 */
	        /* 0 */ {'-','|','|',' ','|','|','-'},  
	        /* 1 */ {' ','|',' ',' ','|',' ',' '},
	        /* 2 */ {'-','|',' ','-',' ','|','-'},
	        /* 3 */ {'-','|',' ','-','|',' ','-'},
	        /* 4 */ {' ','|','|','-','|',' ',' '},
	        /* 5 */ {'-',' ','|','-','|',' ','-'},
	        /* 6 */ {'-',' ','|','-','|','|','-'},
	        /* 7 */ {'-','|',' ',' ','|',' ',' '},
	        /* 8 */ {'-','|','|','-','|','|','-'},
	        /* 9 */ {'-','|','|','-','|',' ','-'}
           };
            string formatBar = " {0}  {1}";
            string formatSides = "{0}{1}{2} {3}";
            string centerSpaces;
            int dig, size;
            string lines, number;

            using (var input = System.IO.File.OpenText(fileToProcess))
            {
                while ((line = input.ReadLine()) != null)
                {
                    string[] numbers = line.Split(' ');
                    size = int.Parse(numbers[0]);
                    number = numbers[1];
                    if (size == 0)
                        continue;
                    
                    //setup my initial format with built in repeating lines
                    lines = string.Format("{0}\n{1}{2}\n{3}{4}\n", "{0}", string.Concat(Enumerable.Repeat("{1}\n", size)), "{2}", string.Concat(Enumerable.Repeat("{3}\n", size)), "{4}");
                    centerSpaces = new string(' ', size);

                    //loop the digits
                    foreach (char digit in number)
                    {
                        dig = digit - '0'; //translate to a number
                        lines = string.Format(lines,
                            string.Format(formatBar, new string(lookupChar[dig, 0], size), "{0}"), //look up '-' for top
                            string.Format(formatSides, lookupChar[dig, 2], centerSpaces, lookupChar[dig, 1], "{1}"),//conditionally set each '|'
                            string.Format(formatBar, new string(lookupChar[dig, 3], size), "{2}"), //look up '-' for top
                            string.Format(formatSides, lookupChar[dig, 5], centerSpaces, lookupChar[dig, 4], "{3}"),//conditionally set each '|'
                            string.Format(formatBar, new string(lookupChar[dig, 6], size), "{4}")); //look up '-' for top
                    }
                    output.WriteLine(Regex.Replace(lines, "{.}", string.Empty, RegexOptions.Compiled));
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
    }
}
