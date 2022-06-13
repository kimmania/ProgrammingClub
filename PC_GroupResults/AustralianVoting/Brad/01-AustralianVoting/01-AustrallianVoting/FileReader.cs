using System;
using System.Collections.Generic;
using System.IO;

namespace AustralianVoting
{
	class FileReader
	{
		private static StreamReader _streamReader;

		/// <summary>
		/// Processes a standard input file. This function assumes that the input file is formatted properly.
		/// </summary>
		/// <returns>A list of BallotSet objects from the file.</returns>
		public static List<BallotSet> ProcessFile(string path)
		{
			_streamReader = new StreamReader(path);

			List<BallotSet> _ballotSets = new List<BallotSet>();

			// Read the number of sets
			int _numberOfSets = 0;
			int.TryParse(_streamReader.ReadLine(), out _numberOfSets);

			// Throw away a line
			_streamReader.ReadLine();

			// Read the sets into memory
			for (int _setCounter = 0; _setCounter < _numberOfSets; _setCounter++)
			{
				BallotSet _currentBallotSet = new BallotSet();
				
				// Read number of candidates
				int _numberOfCandidates;
				int.TryParse(_streamReader.ReadLine(), out _numberOfCandidates);

				// Read in candidate names
				Dictionary<int, string> _candidates = new Dictionary<int, string>();
				for(int _candidateCounter = 0; _candidateCounter < _numberOfCandidates; _candidateCounter++)
				{
					_candidates.Add(
						_candidateCounter + 1,
						_streamReader.ReadLine()
					);
				}
				_currentBallotSet.Candidates = _candidates;

				// Read in all the ballots until you hit an empty line (indicating the end of the BallotSet)
				_currentBallotSet.Ballots = new List<Ballot>();
				string _line = _streamReader.ReadLine();
				do
				{
					List<string> _currentBallot_string = new List<string>(_line.Split(' '));
					List<int> _currentBallot_int = new List<int>();
					foreach (string _vote_string in _currentBallot_string)
					{
						int _vote_int = 0;
						int.TryParse(_vote_string, out _vote_int);
						_currentBallot_int.Add(_vote_int);
					}

					_currentBallotSet.Ballots.Add(new Ballot(_currentBallot_int));
					_line = _streamReader.ReadLine();
				} while (_line != "" && _line != null);

				_ballotSets.Add(_currentBallotSet);
			}

			_streamReader = null;
			return _ballotSets;
		}
	}
}
