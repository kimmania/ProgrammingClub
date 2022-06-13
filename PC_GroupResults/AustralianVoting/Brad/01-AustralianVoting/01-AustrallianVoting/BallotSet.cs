using System;
using System.Collections.Generic;

namespace AustralianVoting
{
	public class BallotSet
	{
		private List<Ballot> _ballots;
		public List<Ballot> Ballots
		{
			get { return _ballots; }
			set { _ballots = value; }
		}

		private Dictionary<int, string> _candidates;
		public Dictionary<int, string> Candidates
		{
			get { return _candidates; }
			set { _candidates = value; }
		}

		private List<int> _eleminatedCandidates;
		public List<int> EleminatedCandidates
		{
			get { return _eleminatedCandidates; }
			set { _eleminatedCandidates = value; }
		}

		public BallotSet()
		{
			_ballots = new List<Ballot>();
			_candidates = new Dictionary<int, string>();
			_eleminatedCandidates = new List<int>();
		}

		public Dictionary<int, int> Tally()
		{
			Dictionary<int, int> _tally = new Dictionary<int, int>();
			int _totalVotes = 0;

			// Add each candidate into _tally
			foreach (KeyValuePair<int, string> _candidate in _candidates)
			{
				_tally.Add(_candidate.Key, 0);
			}

			// Count the votes
			foreach (Ballot _ballot in _ballots)
			{
				int _vote = _ballot.Votes[0];		// assume that the first vote isn't for an eliminated candidate (it shouldn't be)
				_tally[_vote] = _tally[_vote] + 1;
				_totalVotes++;
			}

			// Convert to percentages
			for (int _candidateCount = 0; _candidateCount < _candidates.Count; _candidateCount++)
			{
				_tally[_candidateCount + 1] = (int)(((double)_tally[_candidateCount + 1] / (double) _totalVotes) * 100) ;
			}

			return _tally;
		}

		public void ProcessEliminatedCandidates()
		{
			foreach (Ballot _ballot in _ballots)
			{
				bool _restartLoop;
				do
				{
					_restartLoop = false;
					
					// Make sure the current ballot's vote isn't for an eleminated candidate
					foreach (int _eliminatedCandidate in _eleminatedCandidates)
					{
						if (_ballot.Votes[0] == _eliminatedCandidate)
						{
							// If it is, remove that vote and restart the loop to check the next vote
							_ballot.Votes.RemoveAt(0);
							_restartLoop = true;
							break;
						}
					}
				} while (_restartLoop);
			}
		}
	}
}
