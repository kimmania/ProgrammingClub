<Query Kind="Program" />

void Main()
{
	/*string[] hands = {"9D 6S QS 9H JH AC 2S QC KC 2C",
					"JH 3S 9C 4D 9S TC QD KC TD KS",
					"3S 5S 7C TH 9H KH 7S AS 5H 7H",
					"QS 4C JS 3S AC TS 6D 4D 3D KD",
					"4S 5H 9S 5C 2S JH 6S 9D TS TH",
					"QD 6S QS 7D JH 7C 4S KD JC 4C",
					"3D 2S 2H 3H 7C 8H 2C 6D 7D KH",
					"8H 4S AS JS QC 3C 6S TS 8C 2S",
					"6D 9D 6H JH TC 3H 6C 5H 2H 8D"};
	*/
	
	Hand hand= new Hand("9D 6S QS 9H JH AC 2S QC KC 2C");
	
	hand= new Hand("9D 6D QD 9D JD AC 2S QC KC 2C");
	
}
// Define other methods and classes here

public class Hand
{
	Hand()
	{
	}
	
	public Hand(string hand)
	{
		// need to parse a line into two players
		PlayerHand.GradeHand(hand.Substring(0,14));
		PlayerHand.GradeHand(hand.Substring(15));
	}
}

public static class PlayerHand
{
	public static int GradeHand(string playerHand)
	{
		int handGrade = 0;
		bool IsFlush = (playerHand[1] == playerHand[4] && playerHand[1] == playerHand[7] && playerHand[1] == playerHand[10] && playerHand[1] == playerHand[13]);
		int[] valueOfCards = {CharToRank(playerHand[0]),CharToRank(playerHand[3]),CharToRank(playerHand[6]),CharToRank(playerHand[9]),CharToRank(playerHand[12])};
		playerHand.Dump();
		Array.Sort(valueOfCards);
		
		//
		if (valueOfCards[0] == valueOfCards[3] || valueOfCards[1] == valueOfCards[4])
		{
			handGrade = 10000;
		}
		
		
		/*
		var evalueCards = valueOfCards.GroupBy (oc => oc).Select(c => new {Key = c.Key, total = c.Count()}).OrderByDescending (c => c.total).ThenByDescending (c => c.Key);

		var valueCounts = evalueCards.Select (c => c.total);
		var valueRanking = evalueCards.Select (c => c.Key);
		
		evalueCards.Dump();	
		valueCounts.Dump();
		valueRanking.Dump();
		*/
		/*switch (valueCounts.Count())
		{
			case 2:
				// 4 of a kind
				if ()
				//full house
				break;
			case 3:
				//three of a kind
				//two pair
				break;
			case 4:
				//pair
				break;
			case 5:
				//straight
				
				break;
		}
		*/
		IsFlush.Dump();
		
		return handGrade;
	}
	
	static int CharToRank(char card)
	{
		switch (card)
		{
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case 'T':
				return 10;
			case 'J':
				return 11;
			case 'Q':
				return 12;
			case 'K':
				return 13;
			default: //A
				return 14;
		}
	}
}