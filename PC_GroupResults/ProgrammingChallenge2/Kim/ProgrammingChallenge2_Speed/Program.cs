using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace ProgrammingChallenge2_Speed
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Invalid arguments!  Please provide\nProgrammingChallenge2_Speed <input file path> <output file path>");
               return;
               // minesweeper(@"C:\Users\kimmania\Documents\testdata2a.txt", @"C:\Users\kimmania\Documents\testdata2aa_result.txt");
            }
            //want to add some stuff here to generate the test data
            minesweeper(args[0], args[1]);

        }

        /// <summary>
        /// First line is used to determine the size of the array.  For performance reasons, I add two to the grid size for both dimensions.
        /// As each character is read in, I check to see if it is a bomb.  If it is, then add one to each of the surrounding fields (unless that field is a bomb).  
        /// By having the array bigger than the grid itself, I can refrain from having to check that the field isn't a boundary.  Given the rarety of boundary fields
        /// and the likelihood that such a field will have a bomb, it is faster to use the additional memory and not have the additional criteria on the neighbor checks.
        /// </summary>
        /// <param name="input">source to evaluate</param>
        /// <param name="outfile">where to send the results</param>
        static void minesweeper(string input, string outfile)
        {
            StreamReader file = new StreamReader(input);
            StreamWriter output = new StreamWriter(outfile);
            int grids = 0;
            int rows, columns;
            string line;
            Stopwatch numberOfTicks = Stopwatch.StartNew();
            while ((line = file.ReadLine()) != null)	//do we have more to process
            {
                string[] numbers = line.Split(' ');
                rows = int.Parse(numbers[0]);
                columns = int.Parse(numbers[1]);
                // read in grid
                int[,] grid = new int[rows + 2, columns + 2];
                for (int i = 1; i <= rows; i++)
                {
                    //build each row
                    for (int j = 1; j <= columns; j++)
                    {
                        //grab each field
                        if ((char)file.Read() == '*')
                        {
                            grid[i, j] = -1; //this is a bomb
                            //update surrounding

                            //left upper
                            if (grid[i - 1, j - 1] != -1)
                                grid[i - 1, j - 1]++;
                            //above
                            if (grid[i - 1, j] != -1)
                                grid[i - 1, j]++;
                            //right upper
                            if (grid[i - 1, j + 1] != -1)
                                grid[i - 1, j + 1]++;
                            //left
                            if (grid[i, j - 1] != -1)
                                grid[i, j - 1]++;
                            //right
                            if (grid[i, j + 1] != -1)
                                grid[i, j + 1]++;
                            //left lower
                            if (grid[i + 1, j - 1] != -1)
                                grid[i + 1, j - 1]++;
                            //lower
                            if (grid[i + 1, j] != -1)
                                grid[i + 1, j]++;
                            //right lower
                            if (grid[i + 1, j + 1] != -1)
                                grid[i + 1, j + 1]++;
                        }//end bomb
                    }//end field
                    file.Read();//new line
                    file.Read();
                }//end row
                //write the results
                output.WriteLine("Field #" + ++grids + ":");
                for (int i = 1; i <= rows; i++)
                {
                    //build each row
                    for (int j = 1; j <= columns; j++)
                    {
                        //output.Write(grid[i, j] == -1 ? "*" : grid[i, j].ToString());
                        //ToString is slower than below
                        switch (grid[i,j])
                        {
                            case 0:
                                output.Write("0");
                                break;
                            case 1:
                                output.Write("1");
                                break;
                            case 2:
                                output.Write("2");
                                break;
                            case 3:
                                output.Write("3");
                                break;
                            case -1:
                                output.Write("*");
                                break;
                            case 4:
                                output.Write("4");
                                break;
                            case 5:
                                output.Write("5");
                                break;
                            case 6:
                                output.Write("6");
                                break;
                            case 7:
                                output.Write("7");
                                break;
                            case 8:
                                output.Write("8");
                                break;
                        }
                    }
                    output.Write("\n");
                }
                output.WriteLine();
            }//end file
            output.Flush();
            numberOfTicks.Stop();
            output.WriteLine("Total Milliseconds: " + numberOfTicks.ElapsedMilliseconds);
            output.WriteLine("Average Milliseconds: " + ((float)numberOfTicks.ElapsedMilliseconds / grids));
            output.WriteLine("Total Ticks: " + numberOfTicks.ElapsedTicks);
            output.WriteLine("Average Ticks: " + (numberOfTicks.ElapsedTicks / grids));
            output.Flush();
            output.Close();
        }

        static void GenerateRandomData(int grids, int length, int width, int bombs, char field1, char field2, string outputFile)
        {
            StreamWriter output = new StreamWriter(outputFile,true);
            Random random = new Random();
            int totalFields = length * width;
            string str = string.Empty;
            str = str.PadRight(totalFields, field1);
            HashSet<int> hash = new HashSet<int>();
            for (int i = 0; i < grids; i++)
            {
                char[] array = str.ToCharArray();
                hash.Clear();
                while (hash.Count < bombs)
                {
                    int number = random.Next(1, totalFields);
                    hash.Add(number);
                    array[number - 1] = field2;
                }

                string result = new string(array);
                output.WriteLine(length + " " + width);
                for (int j = 0; j < totalFields; j = j + width)
                {
                    output.WriteLine(result.Substring(j, width));
                }
            }
            output.Flush();
            output.Close();
        }
    
    }
}
