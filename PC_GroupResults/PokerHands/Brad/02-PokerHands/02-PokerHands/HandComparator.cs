using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_PokerHands
{
	static class HandComparator
	{
		public static Enums.Winner Compare(List<Card> blackCards, List<Card> whiteCards)
		{
			Enums.Winner _winner = Enums.Winner.Tie;

			Enums.Hand _blackHand = HandRecognizer.Recognize(blackCards);
			Enums.Hand _whiteHand = HandRecognizer.Recognize(whiteCards);

			if(_blackHand > _whiteHand)
			{
				_winner = Enums.Winner.Black;
			}
			else if(_blackHand < _whiteHand)
			{
				_winner = Enums.Winner.White;
			}
			else // Need to break the tie
			{
				switch(_blackHand)
				{
					case Enums.Hand.HighCard:
						_winner = TieSettler.HighCard(blackCards, whiteCards);
						break;
					case Enums.Hand.Pair:
						_winner = TieSettler.Pair(blackCards, whiteCards);
						break;
					case Enums.Hand.TwoPair:
						_winner = TieSettler.TwoPair(blackCards, whiteCards);
						break;
					case Enums.Hand.ThreeOfAKind:
						_winner = TieSettler.ThreeOfAKind(blackCards, whiteCards);
						break;
					case Enums.Hand.Straight:
						_winner = TieSettler.Straight(blackCards, whiteCards);
						break;
					case Enums.Hand.Flush:
						_winner = TieSettler.Flush(blackCards, whiteCards);
						break;
					case Enums.Hand.FullHouse:
						_winner = TieSettler.FullHouse(blackCards, whiteCards);
						break;
					case Enums.Hand.FourOfAKind:
						_winner = TieSettler.FourOfAKind(blackCards, whiteCards);
						break;
					case Enums.Hand.StraightFlush:
						_winner = TieSettler.StraightFlush(blackCards, whiteCards);
						break;
				}
			}

			return _winner;
		}
	}
}
