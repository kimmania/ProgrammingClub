using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Voting
{
    class Program
    {
        static void Main(string[] args)
        {
            string allLines = string.Empty;
            string line = string.Empty;
            string winner = string.Empty;
            int lineNum = 0;
            int numberOfElections = 0;
            bool newElection = false;
            int numberOfCandidates = 0;
            string[] candidates = null;
            int nameCounter = 0;
            int numberOfVoters = 0;
            int electionNumber = 1;
            LinkedList<Queue<int>>[] electionResults = null;
            Stopwatch stopWatch = new Stopwatch();
            Console.Write("Enter Path:");
            //string path = @"D:\OLD\Ed\Projects\PC\AustralianVoting-input.txt";
            string path = Console.ReadLine();

            stopWatch.Start();

            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    //Very first line, number of elections
                    if (lineNum == 0)
                        numberOfElections = int.Parse(line);

                    //process the actual results
                    if (!newElection && nameCounter == numberOfCandidates && numberOfCandidates > 0 && !string.IsNullOrEmpty(line))
                    {
                        string[] preferencesStr = line.Split(' ');
                        Queue<int> preferences = new Queue<int>();

                        foreach (string vote in preferencesStr)
                            preferences.Enqueue(int.Parse(vote));

                        LinkedList<Queue<int>> theList = electionResults[preferences.Peek() - 1];
                        if (theList == null)
                            theList = new LinkedList<Queue<int>>();

                        theList.AddLast(preferences);

                        electionResults[preferences.Peek() - 1] = theList;
                        numberOfVoters++;
                    }

                    //is this the list of candidates? If so, put it into an array
                    if (nameCounter < numberOfCandidates)
                    {
                        candidates[nameCounter] = line;
                        nameCounter++;
                    }



                    //If this a brand new election? initialize
                    if (newElection)
                    {
                        numberOfCandidates = int.Parse(line);
                        candidates = new string[numberOfCandidates];
                        electionResults = new LinkedList<Queue<int>>[numberOfCandidates];
                        nameCounter = 0;
                    }

                    if (string.IsNullOrEmpty(line))
                    {
                        newElection = true;
                        electionNumber++;
                        if (electionResults != null)
                        {
                            int votesToWin = (int)Math.Floor((double)numberOfVoters / 2) + 1;
                            ProcessResults(electionResults, votesToWin, candidates);
                            numberOfCandidates = 0;
                            candidates = null;
                            nameCounter = 0;
                            numberOfVoters = 0;
                            electionResults = null;
                        }

                    }

                    else
                    {
                        newElection = false;
                    }
                    lineNum++;
                }
            }

            //Last Election
            if (electionResults != null)
            {
                int votesToWin = (int)Math.Floor((double)numberOfVoters / 2) + 1;
                ProcessResults(electionResults, votesToWin, candidates);
            }
            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedMilliseconds);
            Console.ReadLine();
        }

        private static void ProcessResults(LinkedList<Queue<int>>[] results, int votesToWin, string[] candidates)
        {
            int winner = -1;
            //  List<int> theWinners = new List<int>();


            while (winner == -1)
            {
                int lowestVotes = int.MaxValue;
                int activeCandidates = results.Length;
                // int lowestCandidate = results.Length + 1;
                List<int> lowestCandiates = new List<int>();

                for (int i = 0; i < results.Length; i++)
                {

                    if (results[i] == null)
                    {
                        //skip this dead-beat loser...
                        activeCandidates--;
                        continue;
                    }

                    if (results[i].Count >= votesToWin)
                    {
                        //theWinners.Add(i + 1);
                        Console.WriteLine(candidates[i]);
                        return;
                        //return theWinners;
                    }

                    if (results[i].Count == lowestVotes)
                    {
                        lowestCandiates.Add(i);
                    }

                    if (results[i].Count < lowestVotes)
                    {
                        lowestVotes = results[i].Count;
                        //  lowestCandidate = i;
                        lowestCandiates.Clear();
                        lowestCandiates.Add(i);
                    }
                }

                if (lowestCandiates.Count == activeCandidates)
                {
                    //Everyone's a winner!!!
                    //Well, its a tie. 
                    foreach (int lowest in lowestCandiates)
                    {
                        Console.WriteLine(candidates[lowest]);
                        // theWinners.Add(lowest + 1);
                    }

                    return;
                    // return theWinners;
                }

                //find lowest and pop the queue. reassign votes to next choice and do it again
                // LinkedList<Queue<int>> loser = results[lowestCandidate];

                foreach (int lowest in lowestCandiates)
                {
                    Parallel.ForEach (results[lowest], votes =>
                    {
                        votes.Dequeue();
                        int nextChoice = votes.Peek();
                        while (results[nextChoice - 1] == null)
                        {
                            votes.Dequeue();
                            //No one is left
                            if (votes.Count == 0)
                            {
                                Console.WriteLine(System.Environment.NewLine);
                                return;
                            }
                            nextChoice = votes.Peek();
                        }
                        results[nextChoice - 1].AddLast(votes);
                    });

                    //Take this loser out of the list.
                    results[lowest] = null;
                }

            }

            Console.WriteLine(System.Environment.NewLine);
        }

    }
}
