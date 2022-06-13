// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System.IO

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

let GetValue = function
    //match c with
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

//Generate a rank of a single player's hand
let PlayerHand(playerhand:string) =
    let IsFlush = (playerhand.[1] = playerhand.[4] && playerhand.[1] = playerhand.[7] && playerhand.[1] = playerhand.[10] && playerhand.[1] = playerhand.[13]) // check if all cards are the same suit

    //group cards by numeric value, sort by count desc then by numeric value desc
    //to give us the hightest nubmer of duplicate cards and highest numeric at the top -- tie breakers
    let OfKinds = 
        [playerhand.[0];playerhand.[3];playerhand.[6];playerhand.[9];playerhand.[12]]
        |> Seq.countBy (fun card -> GetValue(card))
        |> Seq.toList
        |> List.sortBy (fun k-> -(snd k), -(fst k))

    let GetFirstCardValue = fst OfKinds.Head // shortcut to get the first card's value

    //can be a straight if there are no duplicates, then check first and last to ensure they are 4 apart
    let IsStraight = 
       if ( snd OfKinds.Head ) = 1  && (GetFirstCardValue = (fst OfKinds.[4]) + 4) then true 
        else false

    //calculate the hand's rank based on the rules provided
    let handRank =
        match OfKinds with
            | [ (_,1); (_,1); (_,1); (_,1); (_,1) ] when IsStraight && IsFlush -> StraightFlush( GetFirstCardValue )
            | [ (_,1); (_,1); (_,1); (_,1); (_,1) ] when IsStraight -> Straight( GetFirstCardValue )
            | [ (_,1); (_,1); (_,1); (_,1); (_,1) ] when IsFlush -> Flush( GetFirstCardValue, fst OfKinds.[1], fst OfKinds.[2], fst OfKinds.[3], fst OfKinds.[4] )
            | [ (_,1); (_,1); (_,1); (_,1); (_,1) ] -> HighCard(GetFirstCardValue, fst OfKinds.[1], fst OfKinds.[2], fst OfKinds.[3], fst OfKinds.[4])
            | [ (_,2); (_,1); (_,1); (_,1) ]-> OnePair(GetFirstCardValue, (fst OfKinds.[1]), (fst OfKinds.[2]), (fst OfKinds.[3]))
            | [ (_,2); (_,2); (_,1) ] -> TwoPair (GetFirstCardValue, (fst OfKinds.[1]), (fst OfKinds.[2]))
            | [ (_,3); (_,1); (_,1) ] -> ThreeOfKind( GetFirstCardValue ) 
            | [ (_,3); (_,2) ] -> FullHouse( GetFirstCardValue )
            | [ (_,4); (_,1); ] -> FourOfKind( GetFirstCardValue )
    handRank //return the resulting rank

//process a hand of poker
let PokerHand(hand:string) =
    let black = (PlayerHand(hand.[0..13]))
    let white = (PlayerHand(hand.[15..28]))
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
        |> Seq.toList //so that it yields all at once

    numberOfTicks.Stop()
    numberOfTicks.Elapsed.TotalMilliseconds, result


[<EntryPoint>]
let main argv = 
    //printfn "%A" argv

    let hands = File.ReadAllLines(@"C:\Users\Kim\Documents\LINQPad Queries\PC\PokerHands-input-test.txt") 
    run(hands) //prime the pump, the first exec seemed to be double to triple the averge
    let runs = 
        seq {
            for i in 1..100 
                do 
                yield run(hands)
        } 
        |> Seq.toList
    
    let average =
        runs 
        |> List.map( fun (a, _) -> a)
        |> List.toSeq
        |> Seq.averageBy float

    printf "Min \t%A\nMax \t%A\nAverage\t%A" (Seq.min runs) (Seq.max runs) average
    System.Console.ReadKey() |> ignore
    0 // return an integer exit code