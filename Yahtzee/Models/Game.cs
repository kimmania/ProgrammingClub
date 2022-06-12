using KuhnMunkresCSharp;

namespace Yahtzee.Models
{
    internal class Game
    {
        List<Throw> Throws = new List<Throw>();
        Score Final = new Score();
        public Game()
        {
            for (int i = 0; i < 13; i++)
                Throws.Add(new Throw());   
        }

        public Game(IEnumerable<string> throws) => Throws = throws.Select(x => new Throw(x)).ToList();

        public override string ToString() => String.Join("\n", Throws);

        public string PrintRowScores() => String.Join("\n", Throws.Select(x => x.Score.ToString()));
        public string PrintFinalScore() => Final.ToString();

        public void EvaluateScores()
        {
            //todo: I haven't analyzed whether this sufficiently accommodates getting the highest score when taking into account a bonus being assigned.
            //      need to determine what sample would create a bonus that would increase an overall total score rather than placing scores elsewhere
            int[,] dissimilarity = new int[13, 13];
            int rowCount = 0;
            // loop through each throw and set up the values such that
            // the score values are shifted to so that the highest values appear as the lowest by taking a single max of 50 and subtracting the actual value
            //the kuhn munkres works by calculating the lowest value possible while assigning all tasks...by negating as above, we are allowing it to work for calculating
            //the highest score possible
            foreach (var thrw in Throws)
            {
                dissimilarity[rowCount, 0] = 50 - thrw.Score.Ones;
                dissimilarity[rowCount, 1] = 50 - thrw.Score.Twos;
                dissimilarity[rowCount, 2] = 50 - thrw.Score.Threes;
                dissimilarity[rowCount, 3] = 50 - thrw.Score.Fours;
                dissimilarity[rowCount, 4] = 50 - thrw.Score.Fives;
                dissimilarity[rowCount, 5] = 50 - thrw.Score.Sixes;
                dissimilarity[rowCount, 6] = 50 - thrw.Score.Chance;
                dissimilarity[rowCount, 7] = 50 - thrw.Score.ThreeOfKind;
                dissimilarity[rowCount, 8] = 50 - thrw.Score.FourOfKind;
                dissimilarity[rowCount, 9] = 50 - thrw.Score.FiveOfKind;
                dissimilarity[rowCount, 10] = 50 - thrw.Score.ShortStraight;
                dissimilarity[rowCount, 11] = 50 - thrw.Score.LongStraight;
                dissimilarity[rowCount, 12] = 50 - thrw.Score.FullHouse;
                ++rowCount;
            };
            KuhnMunkres kh = new KuhnMunkres(dissimilarity);
            //evaluate using the Kuhn Munkres evaluation method to get a listing of valid scores
            int[] res = kh.Solve();
            //now generate the final score using the results
            for (int i = 0; i < res.Count(); i++)
            {
                //i indicates the throw, res[i] equals which score to utilize
                switch (res[i])
                {
                    case 0:
                        Final.Ones = Throws[i].Score.Ones;
                        break;
                    case 1:
                        Final.Twos = Throws[i].Score.Twos;
                        break;
                    case 2:
                        Final.Threes = Throws[i].Score.Threes;
                        break;
                    case 3:
                        Final.Fours = Throws[i].Score.Fours;
                        break;
                    case 4:
                        Final.Fives = Throws[i].Score.Fives;
                        break;
                    case 5:
                        Final.Sixes = Throws[i].Score.Sixes;
                        break;
                    case 6:
                        Final.Chance = Throws[i].Score.Chance;
                        break;
                    case 7:
                        Final.ThreeOfKind = Throws[i].Score.ThreeOfKind;
                        break;
                    case 8:
                        Final.FourOfKind = Throws[i].Score.FourOfKind;
                        break;
                    case 9:
                        Final.FiveOfKind = Throws[i].Score.FiveOfKind;
                        break;
                    case 10:
                        Final.ShortStraight = Throws[i].Score.ShortStraight;
                        break;
                    case 11:
                        Final.LongStraight = Throws[i].Score.LongStraight;
                        break;
                    case 12:
                        Final.FullHouse = Throws[i].Score.FullHouse;
                        break;
                }
            }
        }
    }
}
