namespace CommonPermutation
{
    internal class StaticUtilities
    {
        public static IEnumerable<WordPair> LoadContentFromFile(string fileName)
        {
            using var reader = new StreamReader(fileName);
            while (!reader.EndOfStream)
            {
                string a = reader.ReadLine();
                string b = reader.ReadLine();
                yield return new WordPair(a, b);
            }
        }
    }
}
