using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_PokerHands
{
	class Enums
	{
		public enum Winner
		{
			Black,
			White,
			Tie
		}

		public enum Suit
		{
			Spades		= 1,
			Hearts		= 2,
			Clubs		= 4,
			Diamonds	= 8
		}

		public enum FaceValue
		{
			Two			= 1,
			Three		= 2,
			Four		= 4,
			Five		= 8,
			Six			= 16,
			Seven		= 32,
			Eight		= 64,
			Nine		= 128,
			Ten			= 256,
			Jack		= 512,
			Queen		= 1024,
			King		= 2048,
			Ace			= 4096
		}

		public enum Hand
		{
			HighCard,
			Pair,
			TwoPair,
			ThreeOfAKind,
			Straight,
			Flush,
			FullHouse,
			FourOfAKind,
			StraightFlush
		}
	}
}
