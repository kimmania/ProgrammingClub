using System;
using System.Collections.Generic;

namespace AussieVoting.v1
{
    public class Candidate
    {

        private List<Ballot> ballots = new List<Ballot>(1000);

        public int CandidateID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public Candidate(int id, string name)
        {
            CandidateID = id;
            Name = name;
            Active = true;
        }  // end constructor

        public int VoteCount
        {
            get
            {
                return ballots.Count;
            }
        }

        public void AddVote(Ballot ballot)
        {
            ballots.Add(ballot);
        }

        public void MoveVotesToNextSelection(Candidate[] candidates)
        {
            int targetCandidateID;
            Candidate targetCandidate;
            foreach (Ballot ballot in ballots)
            {
                //
                // find next active candidate on the ballot
                //
                do
                {
                    targetCandidateID = ballot.MoveToNextSelectedCandidate(candidates);
                    targetCandidate = candidates[targetCandidateID];
                } while (!targetCandidate.Active);
                //
                // move this ballot to the voter's next selection
                //
                targetCandidate.AddVote(ballot);
            }  // end ballot
            //
            // this candidate is a loser
            //
            Active = false;
        }

    } // end class
}  // end namespace
