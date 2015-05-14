using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace ProgrammingChallenge2_Speed
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid arguments!  Please provide\nProgrammingChallenge2_Speed <input file path> <output file path>");
                return;
            }
            
            //@"C:\Users\kimmania\Documents\testdata2.txt"
            StreamReader file = new StreamReader(args[0]);
            //(@"C:\Users\kimmania\Documents\testdata2a_result.txt"
            StreamWriter output = new StreamWriter(args[1]);

            int grids = 0;
            int rows, columns;
            string line;

            Stopwatch numberOfTicks = Stopwatch.StartNew();  	
            while ((line = file.ReadLine()) != null)	//do we have more
            {
                string[] numbers = line.Split(' ');
                rows = int.Parse(numbers[0]);
                columns = int.Parse(numbers[1]);
                int rightedge = columns - 1;
                int bottomedge = rows - 1;
                // read in grid
                int[,] grid = new int[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    //build each row
                    for (int j = 0; j < columns; j++)
                    {
                        //grab each field
                        if ((char)file.Read() == '*')
                        {
                            grid[i, j] = -1; //this is a bomb
                            //update surrounding
                            //left upper
                            if ((i > 0) && (j > 0) && (grid[i - 1, j - 1] != -1))
                                grid[i - 1, j - 1]++;
                            //above
                            if ((i > 0) && (grid[i - 1, j] != -1))
                                grid[i - 1, j]++;
                            //right upper
                            if ((i > 0) && (j < rightedge) && (grid[i - 1, j + 1] != -1))
                                grid[i - 1, j + 1]++;
                            //left
                            if ((j > 0) && (grid[i, j - 1] != -1))
                                grid[i, j - 1]++;
                            //right
                            if ((j < rightedge) && (grid[i, j + 1] != -1))
                                grid[i, j + 1]++;
                            //left lower
                            if ((i < bottomedge) && (j > 0) && (grid[i + 1, j - 1] != -1))
                                grid[i + 1, j - 1]++;
                            //lower
                            if ((i < bottomedge) && (grid[i + 1, j] != -1))
                                grid[i + 1, j]++;
                            //right lower
                            if ((i < bottomedge) && (j < rightedge) && (grid[i + 1, j + 1] != -1))
                                grid[i + 1, j + 1]++;
                        }//end bomb
                    }//end field
                    file.Read();//new line
                    file.Read();
                }//end row
                output.WriteLine("Field #" + ++grids + ":");
                for (int i = 0; i < rows; i++)
                {
                    //build each row
                    for (int j = 0; j < columns; j++)
                    {
                        output.Write(grid[i, j] == -1 ? "*" : grid[i, j].ToString());
                    }
                    output.Write("\n");
                }
                output.WriteLine();
            }//end file
            output.Flush();
            numberOfTicks.Stop();
            output.WriteLine("Total Milliseconds: " + numberOfTicks.ElapsedMilliseconds);
            output.WriteLine("Average Milliseconds: " + ((float)numberOfTicks.ElapsedMilliseconds/grids));
            output.WriteLine("Total Ticks: " + numberOfTicks.ElapsedTicks);
            output.WriteLine("Average Ticks: " + (numberOfTicks.ElapsedTicks/grids));
            output.Flush();
            output.Close();

        }
    }
}
