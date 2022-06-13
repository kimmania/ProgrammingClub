<Query Kind="FSharpProgram">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

type HandRank =
	| HighCard of int * int * int * int * int
	| OnePair of int * int * int * int
	| TwoPair of int * int * int
	| ThreeOfKind of int
	| Straight of int
	| Flush of int * int * int * int * int
	| FullHouse of int
	| FourOfKind of int
	| StraightFlush of int
	
type Card (card:string) = 
	member val Suit = card.[1] with get
	// translate card to numeric value with ace high
	member val Value =
		match card.[0] with
			| '2' -> 2
			| '3' -> 3
			| '4' -> 4
			| '5' -> 5
			| '6' -> 6
			| '7' -> 7
			| '8' -> 8
			| '9' -> 9
			| 'T' -> 10
			| 'J' -> 11
			| 'Q' -> 12
			| 'K' -> 13
			| 'A' -> 14
		with get
	
//Generate a rank of a single player's hand
let PlayerHand(cards:Card list) =
	//group cards by numeric value, sort by count desc then by numeric value desc
	//to give us the hightest nubmer of duplicate cards and highest numeric at the top -- tie breakers
	let OfKinds =
		cards 
		|> Seq.groupBy (fun card -> card.Value)
		|> Seq.map (fun (k, seq) -> (k, Seq.length seq))
		|> Seq.toList
		|> List.sortBy (fun (k, l) -> -(l) , -(k))
		
	let GetFirstCardValue = OfKinds.Head |> (fun c -> fst c) 	// shortcut to get the first card's value
	let NoDupes = OfKinds.Head |> (fun a -> snd a) = 1 //shortcut for possible straight or highest card
	let IsFlush = cards |> List.forall(fun c -> c.Suit = cards.Head.Suit) // check if all cards are the same suit
	
	//can be a straight if there are no duplicates, then check first and last to ensure they are 4 apart
	let IsStraight = 
		if NoDupes && (GetFirstCardValue = (fst OfKinds.[4]) + 4) then true 
		else false
		
	let FirstCardDuplicateCount = OfKinds.Head |> (fun c -> snd c) //shortcut to get the highest grouping count
	let secondCardGrouping = OfKinds.[1] // shortcut to get the second ranked grouping
	
	//calculate the hand's rank based on the rules provided
	let handRank =
		if IsStraight && IsFlush then StraightFlush( GetFirstCardValue )
		else if IsStraight then Straight( GetFirstCardValue )
		else if IsFlush then Flush( fst OfKinds.[0], fst OfKinds.[1], fst OfKinds.[2], fst OfKinds.[3], fst OfKinds.[4] )
		else if NoDupes then HighCard(fst OfKinds.[0], fst OfKinds.[1], fst OfKinds.[2], fst OfKinds.[3], fst OfKinds.[4])
		else if FirstCardDuplicateCount = 4 then FourOfKind( GetFirstCardValue )
		else if FirstCardDuplicateCount = 3 && (snd secondCardGrouping) = 2 then FullHouse( GetFirstCardValue )
		else if FirstCardDuplicateCount = 3 then ThreeOfKind( GetFirstCardValue ) 
		else if (snd secondCardGrouping) = 2 then TwoPair (GetFirstCardValue, (fst secondCardGrouping), (fst OfKinds.[2]))
		else OnePair(GetFirstCardValue, (fst secondCardGrouping), (fst OfKinds.[2]), (fst OfKinds.[3]))
	handRank //return the resulting rank
		
//process a hand of poker
let PokerHand(hand:string) =
	let cards = hand.Split(' ') |> Seq.map (fun c -> Card (c))
	let black = (PlayerHand(cards |> Seq.take 5 |> Seq.toList))
	let white = (PlayerHand(cards |> Seq.skip 5 |> Seq.toList))
	let result =
		if black > white then "black"
		else if black < white then "white"
		else "tie"
	result

//process all hands within the array
let run(rounds) =
	//need to Work with timing
	let numberOfTicks = System.Diagnostics.Stopwatch.StartNew()
	let result = 
		rounds 
		|> Array.Parallel.map(fun str -> (PokerHand(str)))
		|> Array.toSeq
		|> Seq.countBy id
	//result.Dump()
	numberOfTicks.Stop()
	numberOfTicks.Elapsed.TotalMilliseconds		

let hands = File.ReadAllLines(@"C:\Users\Kim\Documents\LINQPad Queries\PC\Poker\PokerHands-input.txt") 



let runs = seq {for i in 1..100 do yield run(hands)} |> Seq.toList

(Seq.max runs).Dump("max")
(Seq.min runs).Dump("min")
(Seq.averageBy float runs).Dump("average")



//need to look into pSeq

//PokerHand("9D 8S 7S 6H TH AC 2S QC KC 2C").Dump("straight highcard") 
//PokerHand("9D 9S QS 9H 9C AC 8C QC KC 2C").Dump("4 kind vs flush") 
//PokerHand("6D 6S QS 9H 9C AC 8C QC KC 2C").Dump("2 pair vs flush") 
//PokerHand("6D 6S QS 9H 9C AC AH QC QH 2C").Dump("2 pair 9 vs 2 pair A")
//PokerHand("2H 3D 5S 9C KD 2D 3H 5C 9S KH").Dump("tie")
//PokerHand("2H 3D 5S 9C KD 4D 3H 5C 9S KH").Dump("white")