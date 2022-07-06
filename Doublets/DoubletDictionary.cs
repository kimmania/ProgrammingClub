namespace Doublets
{
    internal class DoubletDictionary
    {
        private Dictionary<string, List<string>> DictionaryOfWords = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> DictionaryOfPatterns = new Dictionary<string, List<string>>();

        public DoubletDictionary() { }

        public void LoadDictionary(StreamReader reader)
        {
            var shouldExit = false;
            while(shouldExit == false && !reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    shouldExit = true;
                    break;
                }
                List<string> patterns = new List<string>();
                //ensure we haven't see the word yet
                if (DictionaryOfWords.TryAdd(line, patterns))
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        string pattern = $"{(i == 0 ? "" : line.Substring(0, i))}.{line.Substring(i + 1)}";
                        patterns.Add(pattern);
                        if (!DictionaryOfPatterns.TryAdd(pattern, new List<string>() { line }))
                            DictionaryOfPatterns[pattern].Add(line);
                    }
                }
            }

            //now let's get rid of patterns that have exactly one match as they aren't useful for linking anything
            foreach (var item in DictionaryOfPatterns.Where(k => k.Value.Count == 1).ToList())
            {
                DictionaryOfPatterns.Remove(item.Key);
            }
        }

        public string DetermineShortestSequenceToDoublet(string doubletToParse)
        {
            if (string.IsNullOrWhiteSpace(doubletToParse))
                return "";

            var split = doubletToParse.Split(' ');
            return DetermineShortestSequenceToDoublet(split[0], split[1]);
        }

        public string DetermineShortestSequenceToDoublet(string start, string finish)
        {
            //need to find the shortest sequence, not just the first sequence
            if (start == null || finish == null || start.Length != finish.Length)
                return "No solution.";

            return "TBD";
        }
    }
}
