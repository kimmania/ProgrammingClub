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
                    //becomes: .rt    t.y     tr.
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

        /// <summary>
        /// Simple pass along that parses for two words to process
        /// </summary>
        /// <param name="doubletToParse"></param>
        /// <returns></returns>
        public string DetermineShortestSequenceToDoublet(string doubletToParse)
        {
            if (string.IsNullOrWhiteSpace(doubletToParse))
                return "";

            var split = doubletToParse.Split(' ');
            if (split.Length != 2)
                return "No solution.\n";

            return DetermineShortestSequenceToDoublet(split[0], split[1]);
        }

        /// <summary>
        /// Main algorithm to determine the shorest doublet for the pair of words. More than one solution is possible, this stops with the first shortest found
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public string DetermineShortestSequenceToDoublet(string start, string finish)
        {
            //objective: need to find the shortest sequence, not just the first sequence

            if (start == finish)
                return $"{start}\n";

            //No solution
            //  if either word is null or empty
            //  length of words do not match (have no way to get to a match)
            //  either word not in the dictionary of words
            if (
                string.IsNullOrWhiteSpace(start) ||
                string.IsNullOrWhiteSpace(finish) || 
                start.Length != finish.Length ||
                DictionaryOfWords.ContainsKey(start) == false ||
                DictionaryOfWords.ContainsKey(finish) == false
            )
                return "No solution.\n";

            //test for the simplest case first
            if (DictionaryOfWords[start].Intersect(DictionaryOfWords[finish]).Any())
                return $"{start}\n{finish}\n";

            List<string> solution = new List<string>();
            Dictionary<string, int> wordsAlreadySeen = new Dictionary<string, int>();
            Dictionary<string, int> patternsAlreadyReviewed = new Dictionary<string, int>();
            Stack<Analysis> currentPath = new Stack<Analysis>();
            currentPath.Push(new Analysis(start, DictionaryOfWords[start]));
            wordsAlreadySeen.Add(start, 0);

            do
            {
                var currentAnalysis = currentPath.Peek();

                if (solution.Count > 0 && currentAnalysis.Depth > solution.Count)
                {
                    currentPath.Pop();
                }
                else if (currentAnalysis.IsLoopingWords)
                {
                    foreach (var word in currentAnalysis.GetNextWord())
                    {
                        if (wordsAlreadySeen.ContainsKey(word) && wordsAlreadySeen[word] <= currentAnalysis.Depth)
                            //no point in checking out further, we already know this leads to something wrong
                            continue;

                        if (wordsAlreadySeen.TryAdd(word, currentAnalysis.Depth) == false)
                            wordsAlreadySeen[word] = currentAnalysis.Depth;

                        if (DictionaryOfWords.ContainsKey(word))
                        {
                            currentPath.Push(new Analysis(currentAnalysis, word, DictionaryOfWords[word]));
                            break;
                        }
                    }
                    //when we hit the end of words, the flag automatically gets flipped for IsLoopingWords, so we can continue one with the do loop
                }
                else
                {
                    foreach (var pattern in currentAnalysis.GetNextPattern())
                    {
                        if (patternsAlreadyReviewed.ContainsKey(pattern) && patternsAlreadyReviewed[pattern] <= currentAnalysis.Depth)
                        {
                            //skip as the results will end up being as long or longer, and not a solution we would use
                            continue;
                        }
                        else
                        {
                            if (patternsAlreadyReviewed.TryAdd(pattern, currentAnalysis.Depth) == false)
                                patternsAlreadyReviewed[pattern] = currentAnalysis.Depth;

                            //going to look through the patterns to start processing words
                            if (DictionaryOfPatterns.TryGetValue(pattern, out List<string> wordsToCheck))
                            {
                                if (wordsToCheck.Contains(finish))
                                {
                                    if (solution.Count == 0 || solution.Count > currentAnalysis.CurrentWordStack.Count + 1)
                                    {
                                        //we have found the word, set the solution
                                        solution = new List<string>(currentAnalysis.CurrentWordStack);
                                        solution.Add(finish);
                                    }
                                    currentPath.Pop();
                                }
                                else
                                {
                                    //prepare for the processing of the pattern's words
                                    currentAnalysis.LoadWords(wordsToCheck);
                                }
                                break;
                            }
                        }
                    }

                    if (
                        currentPath.Count > 0 &&
                        currentAnalysis.IsLoopingWords == false &&
                        currentPath.Peek().Depth == currentAnalysis.Depth
                    )
                        //make sure we are still looking at the same level, and we have finished all there is with this level
                        currentPath.Pop();
                }
            } while (currentPath.Count > 0);

            if (solution.Count > 0)
                return $"{string.Join("\n", solution)}\n";
            return "No solution.\n";
        }
    }
}
