using System;
using System.Collections.Generic;

namespace AustralianVoting
{
	public class Ballot
	{
		private List<int> _votes;
		public List<int> Votes
		{
			get { return _votes; }
			set { _votes = value; }
		}

		public Ballot()
		{
			_votes = new List<int>();
		}

		public Ballot(List<int> votes)
		{
			_votes = votes;
		}
	}
}
