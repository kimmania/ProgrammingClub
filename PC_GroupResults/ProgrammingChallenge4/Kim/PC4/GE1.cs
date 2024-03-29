﻿using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PC4
{
    /// Reads one line in at a time, and splits using the string.split function
    /// writes the results as generated
    class GE1
    {
        StreamWriter output;
        //image has a maximum size of 300 by 300, so why re-initialize everytime
        char[,] image = new char[300,300];

        //keeps track of current size
        int width = 0;
        int height = 0;
 
        public long Run(string fileToProcess, string fileToWrite)
        {   
            string line;
            Stopwatch numberOfTicks = Stopwatch.StartNew();
            output = new StreamWriter(fileToWrite);

            using (var input = System.IO.File.OpenText(fileToProcess))
            {
                while ((line = input.ReadLine()) != null)
                {
                    string[] parameters = line.Split(' ');
                    switch (parameters[0])
                    {
                        case "L":
                            //L X Y C: Colors the pixel (X, Y ) in color (C).
                            image[IntParseFast(parameters[2]), IntParseFast(parameters[1])] = parameters[3][0];
                            break;
                        case "V":
                            //V X Y1 Y2 C: Draw a vertical segment of color (C) in column X, between the rows Y 1 and Y 2 inclusive.
                            FillRectangle(IntParseFast(parameters[1]),  IntParseFast(parameters[2]), IntParseFast(parameters[1]),IntParseFast(parameters[3]), parameters[4][0]);
                            break;
                        case "H":
                            //H X1 X2 Y C: Draw a horizontal segment of color (C) in the row Y, between the columns X1 and X2 inclusive.
                            FillRectangle(IntParseFast(parameters[1]), IntParseFast(parameters[3]), IntParseFast(parameters[2]), IntParseFast(parameters[3]), parameters[4][0]);
                            break;
                        case "K":
                            //K X1 Y1 X2 Y2 C: Draw a filled rectangle of color C, where (X1, Y 1) is the upper-left and (X2, Y 2) the lower right corner.
                            FillRectangle(IntParseFast(parameters[1]), IntParseFast(parameters[2]),IntParseFast(parameters[3]),IntParseFast(parameters[4]),parameters[5][0]);
                            break;
                        case "F":
                            //F X Y C: Fill the region R with the color C, where R is defined as follows. Pixel (X, Y ) belongs to R. Any other pixel which is the same color as pixel (X, Y ) and shares a common side with any pixel in R also belongs to this region. 
                            //TODO

                            break;
                        case "S":
                            //S Name: Write the file name followed by the contents of the current image.
                            output.WriteLine(parameters[1]);
                            PrintImage();
                            break;
                        case "I":
                            //I M N: Create a new M × N image with all pixels initially colored white (O). 
                            //for my purposes, reset width, height, then fill values the same as clear
                            width = IntParseFast(parameters[1]) - 1;
                            height = IntParseFast(parameters[2]) - 1;
                            FillRectangle(0,0,width,height,'O');
                            break;
                        case "C":
                            //C: Clear the table by setting all pixels white (O). The size remains unchanged.
                            FillRectangle(0, 0, width, height, 'O');
                            break;
                        case "X":
                            //X: Terminate the session.
                            output.Flush();
                            numberOfTicks.Stop();
                            output.WriteLine("Total Milliseconds: " + numberOfTicks.ElapsedMilliseconds);
                            output.WriteLine("Total Ticks: " + numberOfTicks.ElapsedTicks);
                            output.Flush();
                            output.Close();
                            output.Dispose();
                            return numberOfTicks.ElapsedTicks;
                            //break;
                    }
                }
            }
            return numberOfTicks.ElapsedTicks;
        }

        private void FillRectangle(int x1, int y1, int x2, int y2, char c)
        {
            for (int j = y1; j <= y2; j++)
            {
                for (int i = x1; i <= x2; i++)
                {
                    image[j, i] = c;
                }
            }
        }

        private void PrintImage()
        {
            for (int j = 0; j <= height; j++)
            {
                for (int i = 0; i <= width; i++)
                {
                     output.Write(image[j, i]);
                }
                output.WriteLine();
            }
        }

        public static int IntParseFast(string value)
        {
            int result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                result = 10 * result + (value[i] - 48);
            }
            return result;
        }
    }
}
