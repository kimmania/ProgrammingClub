
namespace Yahtzee.Models
{
    internal class Score
    {
        public int Ones { get; set; } = 0;
        public int Twos { get; set; } = 0;
        public int Threes { get; set; } = 0;
        public int Fours { get; set; } = 0;
        public int Fives { get; set; } = 0;
        public int Sixes { get; set; } = 0;
        public int Chance { get; set; } = 0;
        public int ThreeOfKind { get; set; } = 0;
        public int FourOfKind { get; set; } = 0;
        public int FiveOfKind { get; set; } = 0;
        public int ShortStraight { get; set; } = 0;
        public int LongStraight { get; set; } = 0;
        public int FullHouse { get; set; } = 0;

        //used for post score
        public int Bonus => (Ones + Twos + Threes + Fours + Fives + Sixes) > 62 ? 35 : 0;
        public int TotalScore => Ones + Twos + Threes + Fours + Fives + Sixes + Chance + ThreeOfKind + FourOfKind + FiveOfKind + ShortStraight + LongStraight + FullHouse + Bonus;

        /// <summary>
        /// Evaluate a single throw's possible scores
        /// </summary>
        /// <param name="dice"></param>
        public Score(List<int> dice)
        {
            //going to evaluate a single throw
            int sumOfAllDice = 0; // used for chance, 3 of kind, 4 of kind -- calculating once
            //counting, not summing
            int ones = 0;
            int twos = 0;
            int threes = 0;
            int fours = 0;
            int fives = 0;
            int sixes = 0;

            //loop over the dice and track values that we have
            foreach (var die in dice)
            {
                sumOfAllDice += die;
                switch (die)
                {
                    case 1:
                        ones++;
                        break;
                    case 2:
                        twos++;
                        break;
                    case 3:
                        threes++;
                        break;
                    case 4:
                        fours++;
                        break;
                    case 5:
                        fives++;
                        break;
                    case 6:
                        sixes++;
                        break;
                }
            }

            Chance = sumOfAllDice;

            Ones = ones;
            Twos = twos * 2;
            Threes = threes * 3;
            Fours = fours * 4;
            Fives = fives * 5;
            Sixes = sixes * 6;

            //check for 5 of a kind, that automatically means we fill in 4 and 3 of kind
            if (
                ones == 5 ||
                twos == 5 ||
                threes == 5 ||
                fours == 5 ||
                fives == 5 ||
                sixes == 5
            )
            {
                FiveOfKind = 50;
                FourOfKind = sumOfAllDice;
                ThreeOfKind = sumOfAllDice;
            }
            //not 5 of a kind, so check for 4 of kind, automatically 3 of a kind 
            else if (
                ones > 3 ||
                twos > 3 ||
                threes > 3 ||
                fours > 3 ||
                fives > 3 ||
                sixes > 3
            )
            {
                FourOfKind = sumOfAllDice;
                ThreeOfKind = sumOfAllDice;
            }
            //not 4 of a kind, so evaluate for three of a kind
            else if (
                ones > 2 ||
                twos > 2 ||
                threes > 2 ||
                fours > 2 ||
                fives > 2 ||
                sixes > 2
            )
            {
                ThreeOfKind = sumOfAllDice;

                if (
                    ones == 2 ||
                    twos == 2 ||
                    threes == 2 ||
                    fours == 2 ||
                    fives == 2 ||
                    sixes == 2
                )
                    //checking for full house
                    FullHouse = 40;
            }

            //straights - only possible if we do not have a three of a kind
            if (ThreeOfKind == 0)
            {
                //check for long straight, and if not matching, then check for short
                if (
                    (
                        ones == 1 &&
                        twos == 1 &&
                        threes == 1 &&
                        fours == 1 &&
                        fives == 1
                    )
                    ||
                    (
                        twos == 1 &&
                        threes == 1 &&
                        fours == 1 &&
                        fives == 1 &&
                        sixes == 1
                    )
                )
                {
                    ShortStraight = 25;
                    LongStraight = 35;
                } else if //not a long straight so now checking for small straight
                (
                    (ones > 0 && twos > 0 && threes > 0 && fours > 0)
                    ||
                    (twos > 0 && threes > 0 && fours > 0 && fives > 0)
                    ||
                    (threes > 0 && fours > 0 && fives > 0 && sixes > 0)
                )
                    ShortStraight = 25;
            }
        }

        public override string ToString()
            => $"{Ones} {Twos} {Threes} {Fours} {Fives} {Sixes} {Chance} {ThreeOfKind} {FourOfKind} {FiveOfKind} {ShortStraight} {LongStraight} {FullHouse} {Bonus} {TotalScore}";

        public string PrintPossibleScores()
            =>$"{Ones.ToString("00")} {Twos.ToString("00")} {Threes.ToString("00")} {Fours.ToString("00")} {Fives.ToString("00")} {Sixes.ToString("00")} {Chance.ToString("00")} {ThreeOfKind.ToString("00")} {FourOfKind.ToString("00")} {FiveOfKind.ToString("00")} {ShortStraight.ToString("00")} {LongStraight.ToString("00")} {FullHouse.ToString("00")}";
        
    }
}
