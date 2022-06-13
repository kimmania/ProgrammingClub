using System;
using System.Collections.Generic;

using AussieVoting.Objects;

namespace AussieVoting.v1
{
    public class Ballot
    {
        private static char[] votesSeparators = new char[] { ' ' };

        private int currentSelection = 0;
        private int VoterID;
        private int[] CandidateRankings;

        public Ballot(int voterID, string votesRecord)
        {
            VoterID = voterID;
            currentSelection = 0;
            //
            // parse votes into int array
            //
            string[] voteSegments = votesRecord.Split(votesSeparators);
            int voteCount = voteSegments.Length;
            CandidateRankings = new int[voteCount];
            for (int i = 0; i < voteCount; i++)
            {
                CandidateRankings[i] = Parsers.IntParseFast(voteSegments[i]);
            }  // next i
        }  // end constructor

        public int SelectedCandidate
        {
            get
            {
                return CandidateRankings[currentSelection] - 1;
            }
        }

        public int MoveToNextSelectedCandidate(Candidate[] candidates)
        {
#if CREATE_DEBUG_FILE
            int previousSelection = currentSelection;
#endif
            //
            // select the next active candidate
            //
            int candidateIndex;
            while (true)
            {
                currentSelection++;
                candidateIndex = CandidateRankings[currentSelection];
                if (candidates[candidateIndex - 1].Active)
                    break;
            }  // end while
#if CREATE_DEBUG_FILE
            DebugLogger.WriteMessage(String.Format(
                "... Ballot #{0}: Selection moved from candidate #{1} to candidate #{2}",
                VoterID,
                CandidateRankings[previousSelection],
                CandidateRankings[currentSelection]));
#endif
            //return CandidateRankings[currentSelection] - 1;
            return SelectedCandidate;
        }

    }  // end class
}  // end namespace
