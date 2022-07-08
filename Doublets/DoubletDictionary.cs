namespace Doublets
{
    internal class DoubletDictionary
    {
        private Dictionary<string, List<string>> DictionaryOfWords = new Dictionary<string, List<string>>();  // word, list of patterns
        private Dictionary<string, List<string>> DictionaryOfPatterns = new Dictionary<string, List<string>>(); //pattern, list of words

        //empty class instantiator
        public DoubletDictionary() { }

        /// <summary>
        /// Initialize the dictionary using a stream reader
        /// </summary>
        /// <param name="reader"></param>
        public void InitializeDictionary(StreamReader reader)
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
                //ensure we haven't see the word yet in case there are duplicates
                if (DictionaryOfWords.TryAdd(line, patterns))
                {
                    //then for the word, create a pattern for the number of letters in the word, with each one replacing a single letter with a period
                    //example: try
                    //becomes: .ry    t.y     tr.
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

        public List<string> GetPatternsForWord(string value)
        {
            if (DictionaryOfWords.TryGetValue(value, out List<string> result))
                return result;

            return new List<string>();
        }

        public List<string> GetWordsForPattern(string value)
        {
            if (DictionaryOfPatterns.TryGetValue(value, out List<string> result))
                return result;

            return new List<string>();
        }
    }
}
