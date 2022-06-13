using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler67
{
    class Program
    {
        static void Main(string[] args)
        {

            StringBuilder sb = new StringBuilder(10);
            Stopwatch stopWatch = new Stopwatch();
            Console.Write("Enter path:");
            string path = Console.ReadLine();
            //   string path = @"D:\OLD\Ed\Projects\PC\p067_triangle.txt";
            string allLines;

            using (StreamReader sr = new StreamReader(path))
            {
                allLines = sr.ReadToEnd();
            }

          
                stopWatch.Start();

                int[,] inputTriangle = readInput(allLines);//lines;

                int numlines = inputTriangle.GetLength(0);

                for (int i = numlines - 2; i >= 0; i--)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        inputTriangle[i, j] += Math.Max(inputTriangle[i + 1, j], inputTriangle[i + 1, j + 1]);
                    }
                }

            stopWatch.Stop();

            Console.WriteLine("Elapsed Miliseconds: " + stopWatch.ElapsedMilliseconds);
            Console.WriteLine("Elapsed Ticks: " + stopWatch.ElapsedTicks);
            //Console.WriteLine("Ticks Per Milisecond: " + TimeSpan.TicksPerMillisecond);
            Console.WriteLine("Answer " + inputTriangle[0, 0]);

            Console.ReadLine();
        }


        static int[,] readInput(string allLines)
        {
            string[] numbers;
            string[] arrLines = allLines.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            int numLines = arrLines.Length;
            int[,] inputTriangle = new int[numLines, numLines];
            int j = 0;
            foreach (string line in arrLines)
            {
                numbers = line.Split(' ');
                if (!string.IsNullOrEmpty(numbers[0]))
                    {
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        inputTriangle[j, i] = int.Parse(numbers[i]);
                    }
                    j++;
                }
             
            }
            return inputTriangle;
        }

    }


   
}
