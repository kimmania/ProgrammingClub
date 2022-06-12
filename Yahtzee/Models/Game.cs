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

            // loop through each throw and set up the values such that
            // the score values are shifted to so that the highest values appear as the lowest by taking a single max of 50 and subtracting the actual value
            // the kuhn munkres works by calculating the lowest value possible while assigning all tasks...by negating as above, we are allowing it to work for calculating
            // the highest score possible
            for (int rec = 0; rec < 13; rec++)
            {
                for (int field = 0; field < 13; field++)
                {
                    dissimilarity[rec, field] = Throws[rec].Score.NegatedPossibleScores[field];
                }
            }            

            KuhnMunkres kh = new KuhnMunkres(dissimilarity);
            //evaluate using the Kuhn Munkres evaluation method to get a listing of valid scores
            int[] res = kh.Solve();
            //now generate the final score using the results
            for (int i = 0; i < res.Count(); i++)
            {
                //i indicates the throw, res[i] equals which score to utilize
                Final.PossibleScores[res[i]] = Throws[i].Score.PossibleScores[res[i]];
            }
        }
    }
}
