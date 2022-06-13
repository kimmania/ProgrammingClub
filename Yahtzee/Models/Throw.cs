using System.Security.Cryptography;

namespace Yahtzee.Models
{
    internal class Throw
    {
        List<int> Dice;
        public Score Score { get; private set; }
        public Throw()
        {
            Dice = new List<int> { ThrowDie(), ThrowDie(), ThrowDie(), ThrowDie(), ThrowDie() };
            Score = new Score(Dice);
        }

        //we should only be getting 5, but forcing it with the take
        public Throw(string Throw)
        {
            Dice = Throw.Split(' ').Select(x => TextToInt(x)).Take(5).ToList();
            Score = new Score(Dice);
        }

        public override string ToString() => String.Join(" ", Dice);

        private int ThrowDie() => RandomNumberGenerator.GetInt32(1, 7); // ending is not inclusive

        private static int TextToInt(string dieValue)
            //faster than parsing
            => dieValue switch
            {
                "1" => 1,
                "2" => 2,
                "3" => 3,
                "4" => 4,
                "5" => 5,
                "6" => 6,
                _ => throw new Exception(),
            };
    }
}
