
namespace Yahtzee.Models
{
    internal class Game
    {
        List<Throw> Throws = new List<Throw>();

        public Game()
        {
            for (int i = 0; i < 13; i++)
                Throws.Add(new Throw());   
        }

        public Game(IEnumerable<string> throws) => Throws = throws.Select(x => new Throw(x)).ToList();

        public override string ToString() => String.Join("\n", Throws);
    }
}
