using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_PokerHands;

namespace _02_PokerHands
{
	static class HandRecognizer
	{
		public static Enums.Hand Recognize(List<Card> cards)
		{
			int _uniqueCards = getUniqueFaceValueCount(cards);

			Enums.Hand _hand = Enums.Hand.HighCard;

			switch(_uniqueCards)
			{
				case 5:
					// Five unique cards narrows it down to either HighCard, Straight, Flush, or StraightFlush
					bool _isFlush = isFlush(cards);
					bool _isStraight = isStraight(cards);

					if(_isStraight && _isFlush)
					{
						_hand = Enums.Hand.StraightFlush;
					}
					else if (_isStraight && !_isFlush)
					{
						_hand = Enums.Hand.Straight;
					}
					else if (!_isStraight && _isFlush)
					{
						_hand = Enums.Hand.Flush;
					}
					else
					{
						_hand = Enums.Hand.HighCard;
					}
					break;
				case 4:
					// Four unique cards narrows it down to a Pair
					_hand = Enums.Hand.Pair;
					break;
				case 3:
					// Three unique cards narrows it down to TwoPair or ThreeOfAKind
					
					// Examine the first card, and count the number of other cards in the hand with the same facevalue
					bool _noClearWinner = true;
					int _cardToExamine = 0;
					while(_noClearWinner)
					{
						int _faceValueToMatch = CountCardsMatchingFaceValue(cards.ElementAt(_cardToExamine).FaceValue, cards);
						switch(_faceValueToMatch)
						{
							case 1:
								// Unclear winner, examine next card
								_cardToExamine++;
								break;
							case 2:
								// Having two of a card means the hand has at least one pair, making it a TwoPair
								_hand = Enums.Hand.TwoPair;
								_noClearWinner = false;
								break;
							case 3:
								// Having three of a card is a ThreeOfAKind
								_hand = Enums.Hand.ThreeOfAKind;
								_noClearWinner = false;
								break;
						}
					}
					
					break;
				case 2:
					// Two unique cards narrows it down to either a FullHouse or FourOfAKind
					
					// Examine the first card, and count the number of other cards in the hand with the same facevalue
					int _FaceValueToMatch = CountCardsMatchingFaceValue(cards.ElementAt(0).FaceValue, cards);
					switch(_FaceValueToMatch)
					{
						case 1:
							// Any unique card means that the hand is a FourOfAKind
							_hand = Enums.Hand.FourOfAKind;
							break;
						case 2:
							// Having two of a card means the hand is a FullHouse
							_hand = Enums.Hand.FullHouse;
							break;
						case 3:
							// Having three of a card means the hand is a FullHouse
							_hand = Enums.Hand.FullHouse;
							break;
						case 4:
							// Having four of a card is a FourOfAKind
							_hand = Enums.Hand.FourOfAKind;
							break;
					}
					break;
			}

			return _hand;
		}

		private static int getUniqueFaceValueCount(List<Card> cards)
		{
			int _faceValuesBinary =
				(int)cards[0].FaceValue
				| (int)cards[1].FaceValue
				| (int)cards[2].FaceValue
				| (int)cards[3].FaceValue
				| (int)cards[4].FaceValue;
			int _uniqueFaceValueCount = 0;

			for (int _faceCount = 0; _faceCount < 13; _faceCount++ )
			{
				// Check the 1s digit of the binary representation of the poker hand's values. If it's a 1, that's a unique card
				if ((_faceValuesBinary & 1) == 1)
				{
					_uniqueFaceValueCount++;
				}

				_faceValuesBinary = _faceValuesBinary >> 1;
			}

			return _uniqueFaceValueCount;
		}

		private static int CountCardsMatchingFaceValue(Enums.FaceValue faceValueToMatch, List<Card> cards)
		{
			int _count = 0;
			foreach (Card _card in cards)
			{
				if(_card.FaceValue == faceValueToMatch)
				{
					_count++;
				}
			}
			return _count;
		}

		private static bool isFlush(List<Card> cards)
		{
			//return (getUniqueSuitCount(cards) == 1);
			int _SuitsBinary =
				(int)cards[0].Suit
				& (int)cards[1].Suit
				& (int)cards[2].Suit
				& (int)cards[3].Suit
				& (int)cards[4].Suit;

			for (int _suitCount = 0; _suitCount < 4; _suitCount++)
			{
				// Check the 1s digit of the binary representation of the poker hand's suits. If it's a 1, that's a unique suit
				if ((_SuitsBinary & 1) == 1)
				{
					return true;
				}

				_SuitsBinary = _SuitsBinary >> 1;
			}

			return false;
		}

		private static bool isStraight(List<Card> cards)
		{
			int _faceValuesBinary =
				(int)cards[0].FaceValue
				| (int)cards[1].FaceValue
				| (int)cards[2].FaceValue
				| (int)cards[3].FaceValue
				| (int)cards[4].FaceValue;

			for (int _faceCount = 0; _faceCount < 13; _faceCount++)
			{
				// Check the 1s digit of the binary representation of the poker hand's values. If it's a 1, 
				if ((_faceValuesBinary & 31) == 31)		// 31(DEC) == 11111(BIN)
				{
					return true;
				}

				_faceValuesBinary = _faceValuesBinary >> 1;
			}

			return false;
		}
	}
}
