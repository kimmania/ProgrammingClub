using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_PokerHands
{
	static class HandParser
	{
		public static List<Card> Parse(string playersCards)
		{
			List<Card> _playerCards = new List<Card>(5);
			string[] _cards = playersCards.Split(' ');
			
			foreach(string _card in _cards)
			{
				Card _newCard = new Card();

				switch(_card[0])
				{
					case '2':
						_newCard.FaceValue = Enums.FaceValue.Two;
						break;
					case '3':
						_newCard.FaceValue = Enums.FaceValue.Three;
						break;
					case '4':
						_newCard.FaceValue = Enums.FaceValue.Four;
						break;
					case '5':
						_newCard.FaceValue = Enums.FaceValue.Five;
						break;
					case '6':
						_newCard.FaceValue = Enums.FaceValue.Six;
						break;
					case '7':
						_newCard.FaceValue = Enums.FaceValue.Seven;
						break;
					case '8':
						_newCard.FaceValue = Enums.FaceValue.Eight;
						break;
					case '9':
						_newCard.FaceValue = Enums.FaceValue.Nine;
						break;
					case 'T':
						_newCard.FaceValue = Enums.FaceValue.Ten;
						break;
					case 'J':
						_newCard.FaceValue = Enums.FaceValue.Jack;
						break;
					case 'Q':
						_newCard.FaceValue = Enums.FaceValue.Queen;
						break;
					case 'K':
						_newCard.FaceValue = Enums.FaceValue.King;
						break;
					case 'A':
						_newCard.FaceValue = Enums.FaceValue.Ace;
						break;
				}

				switch(_card[1])
				{
					case 'S':
						_newCard.Suit = Enums.Suit.Spades;
						break;
					case 'H':
						_newCard.Suit = Enums.Suit.Hearts;
						break;
					case 'C':
						_newCard.Suit = Enums.Suit.Clubs;
						break;
					case 'D':
						_newCard.Suit = Enums.Suit.Diamonds;
						break;
				}

				_playerCards.Add(_newCard);
			}

			return _playerCards;
		}
	}
}
