using System;
using System.Diagnostics;

namespace DecemberChallengeEulersSquare
{
    internal class Program
    {
        private static void Main()
        {
            {
                Console.WriteLine("Please enter number of iterations to run with a 1001 x 1001 square");
                int j;
                uint iterations = 501;
                object[] sums = new object[3];
                bool isAnInteger = Int32.TryParse(Console.ReadLine(), out j);
                if (isAnInteger)
                {
                    long ticksAggregate = 0;

                    for (int i = 0; i < j; i++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        sums[0] = CalculateSum();
                        stopwatch.Stop();
                        long ticks = stopwatch.ElapsedTicks;
                        ticksAggregate += ticks;
                    }
                    long method1 = ticksAggregate;
                    ticksAggregate = 0;
                    for (int i = 0; i < j; i++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        sums[1] = CalculateSecondarySum();
                        stopwatch.Stop();
                        long ticks = stopwatch.ElapsedTicks;
                        ticksAggregate += ticks;
                    }
                    long method2 = ticksAggregate;
                    ticksAggregate = 0;
                    for (int i = 0; i < j; i++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        sums[2] = Cheat(iterations);
                        stopwatch.Stop();
                        long ticks = stopwatch.ElapsedTicks;
                        ticksAggregate += ticks;
                    }
                    long method3 = ticksAggregate;
                    Console.WriteLine("Total: {0} \n\tLoop with multiplication: {1} \n\tTicks averaged over {2} \n\titerations\n", sums[0], (double)method1 / j, j);
                    Console.WriteLine("Total: {0} \n\tLoop with addition: {1} \n\tTicks averaged over {2} iterations\n", sums[1], (double)method2 / j, j);
                    Console.WriteLine("Total: {0} \n\tThird Order Polynomial (cheating): {1} \n\tTicks averaged over {2} iterations\n", sums[2], (double)method3 / j, j);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("The value supplied is not an integer, Exiting. ");
                    Console.ReadLine();
                }
            }
        }

        private static uint CalculateSum()
        {
            uint sum = 1;
            uint odds = 1;
            uint evens = 0;
            for (uint k = 0; k < 500; k++)
            {
                odds += 2;
                evens += 2;
                sum = sum + ((odds * odds) * 4) - ((evens) * 6);
            }
            return sum;
        }

        private static uint CalculateSecondarySum()
        {
            uint sum = 1;
            uint seededDifference = 24;
            uint currentDifference = 52;
            const uint slope = 32;
            for (uint k = 0; k < 500; k++)
            {
                sum += seededDifference;
                seededDifference += currentDifference;
                currentDifference += slope;
            }
            return sum;
        }

        private static double Cheat(double iterations)
        {
            return (5.33333333333333) * (iterations * iterations * iterations) - 
                    6 * (iterations * iterations) + (4.666666666666667) * (iterations) - 3;
        }
    }
}