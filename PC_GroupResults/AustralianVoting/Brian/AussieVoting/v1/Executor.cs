using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Reflection;

using AussieVoting.Objects;

namespace AussieVoting.v1
{
    public class Executor : IExecute
    {

        public string ResultsFileName { get; set; }

        private int raceCount;
        private int candidateCount;
        private Candidate[] candidates;
        private int ballotCount;

        public ExecuteResults Execute(string inputFileName)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ExecuteResults results = new ExecuteResults();

            try
            {
                string outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                ResultsFileName = Path.Combine(outputDirectory, "results-Brian.txt");

                using (TextWriter wtr = File.CreateText(ResultsFileName))
                {
                    using (TextReader rdr = File.OpenText(inputFileName))
                    {
                        //
                        // first line is the number of election races in the input file followed by a blank line
                        //
                        raceCount = Parsers.IntParseFast(rdr.ReadLine());
                        rdr.ReadLine();
#if CREATE_DEBUG_FILE
                        DebugLogger.WriteMessage(String.Format(
                            @"File contains {0:#,0} election races",
                            raceCount));
#endif
                        //
                        // process each race in sequence
                        //
                        for (int race = 0; race < raceCount; race++)
                        {
                            ProcessRace(wtr, rdr, race);
                        }  // next race
                    }  // end using rdr
                    //
                    // calculate run-time stats
                    //
                    wtr.Flush();
                    sw.Stop();
                    wtr.WriteLine(String.Format(
                        "Elapsed (ms): {0}",
                        sw.ElapsedMilliseconds));
                    wtr.WriteLine(string.Format(
                        "Elapsed (ticks): {0}",
                        sw.ElapsedTicks));
                    UpdateResults(sw, results);
                }  // end using wtr
            }
            catch (Exception ex)
            {
                results.ErrorMessage = "Exception: " + ex.ToString();
#if CREATE_DEBUG_FILE
                DebugLogger.WriteMessage(String.Format(
                    "Exception:{0}\r\n{1}",
                    ex.Message,
                    ex.ToString()));
#endif
            }
#if CREATE_DEBUG_FILE
            finally
            {
                DebugLogger.WriteMessage("Terminating....");
                DebugLogger.Close();
            }
#endif

            return results;
        }  // end Execute

        private void ProcessRace(TextWriter wtr, TextReader rdr, int raceNumber)
        {
            //
            // first line is number of candidates in this race
            //
            candidateCount = Parsers.IntParseFast(rdr.ReadLine());
#if CREATE_DEBUG_FILE
            DebugLogger.WriteMessage(" ");
            DebugLogger.WriteMessage(String.Format(
                "Race #{0:#,0}: candidates: {1}",
                raceNumber + 1,
                candidateCount));
#endif
            ReadCandidates(rdr);
            //
            // read ballots and assign each one to the voter's first selected candidate
            //
            ReadBallots(rdr);

            bool winnerFound = false;
            bool allCandidatesLost = false;
            int winnerCandidateID;

            while (!winnerFound && !allCandidatesLost)
            {
                CheckForWinner(out winnerFound, out winnerCandidateID);
                if (winnerFound)
                    wtr.WriteLine(candidates[winnerCandidateID].Name);
                else
                    DropLosers(wtr, out allCandidatesLost);
            }  // end while
            wtr.WriteLine("");
        }  // end ProcessRace

        private void ReadCandidates(TextReader rdr)
        {
            string record;
            candidates = new Candidate[candidateCount];
            //
            // read the candidates' names
            //
            for (int c = 0; c < candidateCount; c++)
            {
                record = rdr.ReadLine();
                candidates[c] = new Candidate(c, record);
#if CREATE_DEBUG_FILE
                DebugLogger.WriteMessage(String.Format(
                    "Candidate #{0}: {1}",
                    c + 1,
                    record));
#endif
            }  // next c
        }  // end ReadCandidates

        private void ReadBallots(TextReader rdr)
        {
            Ballot ballot;
            bool moreRecords = true;
            string record;
            ballotCount = 0;
            Candidate firstSelection;
            while (moreRecords)
            {
                if ((record = rdr.ReadLine()) != null)
                {
                    if (String.IsNullOrEmpty(record))
                        moreRecords = false;
                    else
                    {
                        ballotCount++;
#if CREATE_DEBUG_FILE
                        DebugLogger.WriteMessage(String.Format(
                            "Ballot #{0}: {1}",
                            ballotCount,
                            record));
#endif
                        ballot = new Ballot(ballotCount, record);
                        firstSelection = candidates[ballot.SelectedCandidate];
                        firstSelection.AddVote(ballot);
                    }
                }
                else
                    moreRecords = false;
            }
        }  // end ReadBallots

        private void CheckForWinner(out bool winnerFound, out int winnerCandidateID)
        {
#if CREATE_DEBUG_FILE
            DebugLogger.WriteMessage("Checking for winner in current tally");
#endif
            float candidatePercent;
            float fBallotCount = (float) ballotCount;
            Candidate candidate;
            for (int c = 0; c < candidateCount; c++)
            {
                candidate = candidates[c];
                if (candidate.Active)
                {
                    candidatePercent = (float)candidate.VoteCount / fBallotCount;
#if CREATE_DEBUG_FILE
                    DebugLogger.WriteMessage(String.Format(
                        "Candidate #{0}: Votes: {1:#,0} = {2:0.000%}",
                        c + 1,
                        candidate.VoteCount,
                        candidatePercent));
#endif
                    if (candidatePercent > 0.50f)
                    {
#if CREATE_DEBUG_FILE
                        DebugLogger.WriteMessage("... Winner has >50% of votes");
                        DebugLogger.WriteMessage("... Winner is " + candidate.Name);
#endif
                        winnerFound = true;
                        winnerCandidateID = c;
                        return;
                    }
                }
            }  // next c
            //
            // no winner found
            //
            winnerFound = false;
            winnerCandidateID = -1;
        }

        private int DropLosers(TextWriter wtr, out bool allCandidatesLost)
        {
#if CREATE_DEBUG_FILE
            DebugLogger.WriteMessage("No winner found. Dropping losers.");
#endif
            List<Candidate> losers = new List<Candidate>();
            int lowestCount = int.MaxValue;
            int candidatesChecked = 0;
            //
            // find losers with lowest number of votes
            //
            foreach (Candidate candidate in candidates)
            {
                if (candidate.Active)
                {
                    candidatesChecked++;
                    if (candidate.VoteCount == lowestCount)
                    {
                        //
                        // tied with current loser
                        //
                        losers.Add(candidate);
                    }
                    else if (candidate.VoteCount < lowestCount)
                    {
                        //
                        // lost by more votes than current loser
                        //
                        losers.Clear();
                        lowestCount = candidate.VoteCount;
                        losers.Add(candidate);
                    }
                }
            }  // next candidate
            //
            // if all candidates have the same low score, we have a tie
            //
            if (losers.Count == candidatesChecked)
            {
                RecordTie(wtr, losers);
                allCandidatesLost = true;
                return 0;
            }
            //
            // if losers were found, first mark all losers inactive and then move their votes to the
            // voter's next selection. this must be done as multiple steps to properly handle 
            // moving votes in a round where there are multiple losers (turns out this is not true,
            // but it makes the debug log look better)
            //
#if CREATE_DEBUG_FILE
            foreach (Candidate loser in losers)
            {
                loser.Active = false;
                DebugLogger.WriteMessage(String.Format(
                    "Loser candidate #{0} flagged as loser",
                    loser.CandidateID + 1));
            }
#endif
            foreach (Candidate loser in losers)
            {
#if CREATE_DEBUG_FILE
                DebugLogger.WriteMessage(String.Format(
                    "Dropping loser candidate #{0}",
                    loser.CandidateID + 1));
#endif
                loser.MoveVotesToNextSelection(candidates);
            }  // next loser

            allCandidatesLost = false;
            return losers.Count;
        }  // end DropLosers

        private void RecordTie(TextWriter wtr, List<Candidate> activeCandidates)
        {
#if CREATE_DEBUG_FILE
            DebugLogger.WriteMessage("Race is a tie");
#endif
            foreach (Candidate candidate in activeCandidates)
            {
                wtr.WriteLine(candidate.Name);
            }  // next candidate
        }  // end RecordTie

        private void UpdateResults(Stopwatch sw, ExecuteResults results)
        {
            results.ExecutionTimeMilliseconds = sw.ElapsedMilliseconds;
            results.ExecutionTimeTicks = sw.ElapsedTicks;
        }  // end UpdateResults

    }  // end class
}  // end namespace
