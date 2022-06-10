using Yahtzee.Models;

namespace Yahtzee
{
    internal class StaticUtilities
    {
        public static IEnumerable<Game> LoadGamesFromFile(string fileName, int setLen = 13)
        {
            using var reader = new StreamReader(fileName);
            while (!reader.EndOfStream)
            {
                var set = new List<string>();
                for (var i = 0; i < setLen && !reader.EndOfStream; i++)
                {
                    set.Add(reader.ReadLine());
                }
                yield return new Game(set);
            }
        }
    }
}
