using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_PokerHands
{
	static class TieSettler
	{
		internal static Enums.Winner HighCard(List<Card> blackCards, List<Card> whiteCards)
		{
			// For HighCard, sort each players hand and compare each card until a winner is determined

			// Sort and compare the rest of each player's hand
			return SortAndCompareHand(blackCards, whiteCards);
		}

		internal static Enums.Winner Pair(List<Card> blackCards, List<Card> whiteCards)
		{
			// For Pair, compare the pair cards. If there's still no clear winner, compare the rest of the hand

			// Identify the pair
			Enums.FaceValue _blackPairValue = GetPair(ref blackCards);
			Enums.FaceValue _whitePairValue = GetPair(ref whiteCards);

			// Compare each player's pair
			if (_blackPairValue > _whitePairValue)
			{
				return Enums.Winner.Black;
			}
			else if (_blackPairValue < _whitePairValue)
			{
				return Enums.Winner.White;
			}

			// Sort and compare the rest of each player's hand
			return SortAndCompareHand(blackCards, whiteCards);
		}

		internal static Enums.Winner TwoPair(List<Card> blackCards, List<Card> whiteCards)
		{
			// For TwoPair, compare each of the pair cards. If there's still no clear winner, compare the rest of the hand

			// Identify the pairs
			List<Enums.FaceValue> _blackPairsValue = new List<Enums.FaceValue>();
			List<Enums.FaceValue> _whitePairsValue = new List<Enums.FaceValue>();
			_blackPairsValue.Add(GetPair(ref blackCards));
			_blackPairsValue.Add(GetPair(ref blackCards));
			_whitePairsValue.Add(GetPair(ref whiteCards));
			_whitePairsValue.Add(GetPair(ref whiteCards));

			// Sort the pairs
			_blackPairsValue = _blackPairsValue.OrderByDescending(x => x).ToList();
			_whitePairsValue = _whitePairsValue.OrderByDescending(x => x).ToList();

			// Compare each player's pairs
			for (int _comparisonCount = 0; _comparisonCount < 2; _comparisonCount++)		// 2: once for each pair
			{
				if (_blackPairsValue[_comparisonCount] > _whitePairsValue[_comparisonCount])
				{
					return Enums.Winner.Black;
				}
				else if (_blackPairsValue[_comparisonCount] < _whitePairsValue[_comparisonCount])
				{
					return Enums.Winner.White;
				}
			}

			// Sort and compare the rest of each player's hand
			return SortAndCompareHand(blackCards, whiteCards);
		}

		internal static Enums.Winner ThreeOfAKind(List<Card> blackCards, List<Card> whiteCards)
		{
			// For ThreeOfAKind, compare the matching cards. If there's still no clear winner, compare the rest of the hand

			// Identify the ThreeOfAKind cards
			Enums.FaceValue _blackMatchValue = GetThreeOfAKind(ref blackCards);
			Enums.FaceValue _whiteMatchValue = GetThreeOfAKind(ref whiteCards);

			// Compare each player's ThreeOfAKind cards
			if (_blackMatchValue > _whiteMatchValue)
			{
				return Enums.Winner.Black;
			}
			else if (_blackMatchValue < _whiteMatchValue)
			{
				return Enums.Winner.White;
			}

			// Sort and compare the rest of each player's hand
			return SortAndCompareHand(blackCards, whiteCards);
		}

		internal static Enums.Winner Straight(List<Card> blackCards, List<Card> whiteCards)
		{
			// For a Straight, compare the highest card. If it's a tie, it's a tie

			// Sort each player's hand
			blackCards = blackCards.OrderByDescending(x => x.FaceValue).ToList();
			whiteCards = whiteCards.OrderByDescending(x => x.FaceValue).ToList();

			// Compare the value of the high card
			if(blackCards.ElementAt(0).FaceValue > whiteCards.ElementAt(0).FaceValue)
			{
				return Enums.Winner.Black;
			}
			else if(blackCards.ElementAt(0).FaceValue < whiteCards.ElementAt(0).FaceValue)
			{
				return Enums.Winner.White;
			}
			else
			{
				return Enums.Winner.Tie;
			}
		}

		internal static Enums.Winner Flush(List<Card> blackCards, List<Card> whiteCards)
		{
			// Sort and compare each player's hand
			return SortAndCompareHand(blackCards, whiteCards);
		}

		internal static Enums.Winner FullHouse(List<Card> blackCards, List<Card> whiteCards)
		{
			// For FullHouse, compare the ThreeOfAKind cards. If there's still no clear winner, compare the Pair cards

			// Identify the ThreeOfAKind cards
			Enums.FaceValue _blackMatchValue = GetThreeOfAKind(ref blackCards);
			Enums.FaceValue _whiteMatchValue = GetThreeOfAKind(ref whiteCards);

			// Compare each player's ThreeOfAKind cards
			if (_blackMatchValue > _whiteMatchValue)
			{
				return Enums.Winner.Black;
			}
			else if (_blackMatchValue < _whiteMatchValue)
			{
				return Enums.Winner.White;
			}

			// Identify the pair
			Enums.FaceValue _blackPairValue = GetPair(ref blackCards);
			Enums.FaceValue _whitePairValue = GetPair(ref whiteCards);

			// Compare each player's pair
			if (_blackPairValue > _whitePairValue)
			{
				return Enums.Winner.Black;
			}
			else if (_blackPairValue < _whitePairValue)
			{
				return Enums.Winner.White;
			}
			else
			{
				return Enums.Winner.Tie;
			}
		}

		internal static Enums.Winner FourOfAKind(List<Card> blackCards, List<Card> whiteCards)
		{
			// For FourOfAKind, compare the matching cards

			// Identify the ThreeOfAKind cards
			Enums.FaceValue _blackMatchValue = GetFourOfAKind(ref blackCards);
			Enums.FaceValue _whiteMatchValue = GetFourOfAKind(ref whiteCards);

			// Compare each player's ThreeOfAKind cards
			if (_blackMatchValue > _whiteMatchValue)
			{
				return Enums.Winner.Black;
			}
			else if (_blackMatchValue < _whiteMatchValue)
			{
				return Enums.Winner.White;
			}
			else
			{
				return Enums.Winner.Tie;
			}
		}

		internal static Enums.Winner StraightFlush(List<Card> blackCards, List<Card> whiteCards)
		{
			// For a StraightFlush, compare the highest card. If it's a tie, it's a tie

			// Sort each player's hand
			blackCards = blackCards.OrderByDescending(x => x.FaceValue).ToList();
			whiteCards = whiteCards.OrderByDescending(x => x.FaceValue).ToList();

			// Compare the value of the high card
			if (blackCards.ElementAt(0).FaceValue > whiteCards.ElementAt(0).FaceValue)
			{
				return Enums.Winner.Black;
			}
			else if (blackCards.ElementAt(0).FaceValue < whiteCards.ElementAt(0).FaceValue)
			{
				return Enums.Winner.White;
			}
			else
			{
				return Enums.Winner.Tie;
			}
		}

		/*********************************************************************/

		private static Enums.FaceValue GetPair(ref List<Card> cards)
		{
			Enums.FaceValue _pair = Enums.FaceValue.Two;
			bool _pairFound = false;

			for (int _baseCard = 0; _baseCard < cards.Count(); _baseCard++)
			{
				for (int _compareTo = _baseCard + 1; _compareTo < cards.Count(); _compareTo++)
				{
					if (cards[_baseCard].FaceValue == cards[_compareTo].FaceValue)
					{
						_pair = cards[_baseCard].FaceValue;

						cards.RemoveAt(_compareTo);
						cards.RemoveAt(_baseCard);

						_pairFound = true;
						break;
					}
				}

				if (_pairFound)
				{
					break;
				}
			}
			return _pair;
		}

		private static Enums.FaceValue GetThreeOfAKind(ref List<Card> cards)
		{
			Enums.FaceValue _match = Enums.FaceValue.Two;
			bool _matchFound = false;

			for (int _baseCard = 0; _baseCard < cards.Count(); _baseCard++)
			{
				for (int _compareTo1 = _baseCard + 1; _compareTo1 < cards.Count(); _compareTo1++)
				{
					if (cards[_baseCard].FaceValue == cards[_compareTo1].FaceValue)
					{
						for (int _compareTo2 = _compareTo1 + 1; _compareTo2 < cards.Count(); _compareTo2++)
						{
							if (cards[_compareTo1].FaceValue == cards[_compareTo2].FaceValue)
							{
								_match = cards[_baseCard].FaceValue;

								cards.RemoveAt(_compareTo2);
								cards.RemoveAt(_compareTo1);
								cards.RemoveAt(_baseCard);

								_matchFound = true;
								break;
							}
						}
					}

					if (_matchFound)
					{
						break;
					}
				}

				if (_matchFound)
				{
					break;
				}
			}
			return _match;
		}

		private static Enums.FaceValue GetFourOfAKind(ref List<Card> cards)
		{
			// Sort of a hack, but if you know you have a hand with a FourOfAKind, then you only need to find a pair in it
			return GetPair(ref cards);
		}

		private static Enums.Winner SortAndCompareHand(List<Card> blackCards, List<Card> whiteCards)
		{
			blackCards = blackCards.OrderByDescending(x => x.FaceValue).ToList();
			whiteCards = whiteCards.OrderByDescending(x => x.FaceValue).ToList();

			for (int _comparisonCount = 0; _comparisonCount < blackCards.Count(); _comparisonCount++)
			{
				if (blackCards[_comparisonCount].FaceValue > whiteCards[_comparisonCount].FaceValue)
				{
					return Enums.Winner.Black;
				}
				else if (blackCards[_comparisonCount].FaceValue < whiteCards[_comparisonCount].FaceValue)
				{
					return Enums.Winner.White;
				}
			}
			return Enums.Winner.Tie;
		}
	}


}
