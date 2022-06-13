using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace _02_PokerHands
{
	class Program
	{
		public const int TimesToRun = 3;
		public const string FilePath = @"input3.txt";

		static void Main(string[] args)
		{
            List<Stopwatch> _runTimers = new List<Stopwatch>();

			for (int _runCount = 0; _runCount < TimesToRun; _runCount++)
			{
				// Read File
				string[] _lines = File.ReadAllLines(FilePath);


                int _blackCount = 0;
                int _whiteCount = 0;
                int _tieCount = 0;

				// Initialize stopwatch
				Stopwatch _stopwatch = new Stopwatch();
				_stopwatch.Start();

				Console.WriteLine("Run #" + (_runCount + 1));

				foreach(string _line in _lines)
				{
					// Parse the players' hands
					List<Card> _blackCards = HandParser.Parse(_line.Substring(0, 14));		// First half of line
					List<Card> _whiteCards = HandParser.Parse(_line.Substring(15));			// Second half of line

					// Compare hands
					Enums.Winner _winner = HandComparator.Compare(_blackCards, _whiteCards);

					// Announce Winner
					switch(_winner)
					{
						case Enums.Winner.Black:
							//Console.WriteLine("Black wins.");
                            _blackCount++;
							break;
						case Enums.Winner.White:
							//Console.WriteLine("White wins.");
                            _whiteCount++;
							break;
						case Enums.Winner.Tie:
							//Console.WriteLine("Tie.");
                            _tieCount++;
							break;
					}
				}

				_stopwatch.Stop();
                _runTimers.Add(_stopwatch);
                Console.WriteLine("Black wins: " + _blackCount + ", White wins: " + _whiteCount + ", Ties: " + _tieCount);
                Console.WriteLine("Run " + (_runCount + 1) + " elapsed time: " + FormatTimeSpan(_stopwatch.Elapsed));
			}

			// Average run time
            Console.WriteLine();
            TimeSpan _averageRunTime = TimeSpan.FromMilliseconds(_runTimers.Average(x => x.Elapsed.TotalMilliseconds));
			Console.WriteLine("Average run time: " + FormatTimeSpan(_averageRunTime));
			Console.WriteLine();

			// Bye
			System.Console.ReadKey();
		}

		private static string FormatTimeSpan(TimeSpan timespan)
		{
			return String.Format(
				"{0:00}h {1:00}m {2:00}s {3:00}ms",
				timespan.Hours,
				timespan.Minutes,
				timespan.Seconds,
				timespan.Milliseconds / 10
			);
		}
	}
}
