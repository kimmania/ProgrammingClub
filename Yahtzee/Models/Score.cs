
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

        public Score() { } //this will be used to create the final scoring

        /// <summary>
        /// Copy an existing score that has already been calculated
        /// </summary>
        /// <param name="scoreToCopy"></param>
        public Score(Score scoreToCopy) 
        {
            this.Ones = scoreToCopy.Ones;
            this.Twos = scoreToCopy.Twos;
            this.Threes = scoreToCopy.Threes;
            this.Fours = scoreToCopy.Fours;
            this.Fives = scoreToCopy.Fives;
            this.Sixes = scoreToCopy.Sixes;
            this.Chance = scoreToCopy.Chance;
            this.ThreeOfKind = scoreToCopy.ThreeOfKind;
            this.FourOfKind = scoreToCopy.FourOfKind;
            this.FiveOfKind = scoreToCopy.FiveOfKind;
            this.ShortStraight = scoreToCopy.ShortStraight;
            this.LongStraight = scoreToCopy.LongStraight;
            this.FullHouse = scoreToCopy.FullHouse;
        }

        /// <summary>
        /// Evaluate a single throw's possible scores
        /// </summary>
        /// <param name="dice"></param>
        public Score(List<int> dice)
        {
            //going to evaluate a single throw
            int sumOfAllDice = 0; // used for chance, 3 of kind, 4 of kind -- calculating once
            //counting, not summing
            int[] counts = new int[7]; // ignoring the zero entry

            //loop over the dice and track values that we have
            foreach (var die in dice)
            {
                sumOfAllDice += die;
                counts[die]++;
            }

            Chance = sumOfAllDice;
            Ones = counts[1];
            Twos = counts[2] * 2;
            Threes = counts[3] * 3;
            Fours = counts[4] * 4;
            Fives = counts[5] * 5;
            Sixes = counts[6] * 6;

            //check for 5 of a kind, that automatically means we fill in 4 and 3 of kind
            if (
                counts[1] == 5 ||
                counts[2] == 5 ||
                counts[3] == 5 ||
                counts[4] == 5 ||
                counts[5] == 5 ||
                counts[6] == 5
            )
            {
                FiveOfKind = 50;
                FourOfKind = sumOfAllDice;
                ThreeOfKind = sumOfAllDice;
            }
            //not 5 of a kind, so check for 4 of kind, automatically 3 of a kind 
            else if (
                counts[1] > 3 ||
                counts[2] > 3 ||
                counts[3] > 3 ||
                counts[4] > 3 ||
                counts[5] > 3 ||
                counts[6] > 3
            )
            {
                FourOfKind = sumOfAllDice;
                ThreeOfKind = sumOfAllDice;
            }
            //not 4 of a kind, so evaluate for three of a kind
            else if (
                counts[1] > 2 ||
                counts[2] > 2 ||
                counts[3] > 2 ||
                counts[4] > 2 ||
                counts[5] > 2 ||
                counts[6] > 2
            )
            {
                ThreeOfKind = sumOfAllDice;

                if (
                    counts[1] == 2 ||
                    counts[2] == 2 ||
                    counts[3] == 2 ||
                    counts[4] == 2 ||
                    counts[5] == 2 ||
                    counts[6] == 2
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
                        counts[1] == 1 &&
                        counts[2] == 1 &&
                        counts[3] == 1 &&
                        counts[4] == 1 &&
                        counts[5] == 1
                    )
                    ||
                    (
                        counts[2] == 1 &&
                        counts[3] == 1 &&
                        counts[4] == 1 &&
                        counts[5] == 1 &&
                        counts[6] == 1
                    )
                )
                {
                    ShortStraight = 25;
                    LongStraight = 35;
                } else if //not a long straight so now checking for small straight
                (
                    (counts[1] > 0 && counts[2] > 0 && counts[3] > 0 && counts[4] > 0)
                    ||
                    (counts[2] > 0 && counts[3] > 0 && counts[4] > 0 && counts[5] > 0)
                    ||
                    (counts[3] > 0 && counts[4] > 0 && counts[5] > 0 && counts[6] > 0)
                )
                    ShortStraight = 25;
            }
        }

        public override string ToString()
            => $"{Ones} {Twos} {Threes} {Fours} {Fives} {Sixes} {Chance} {ThreeOfKind} {FourOfKind} {FiveOfKind} {ShortStraight} {LongStraight} {FullHouse} {Bonus} {TotalScore}";

    }
}
