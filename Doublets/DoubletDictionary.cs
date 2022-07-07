namespace Doublets
{
    internal class DoubletDictionary
    {
        private Dictionary<string, List<string>> DictionaryOfWords = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> DictionaryOfPatterns = new Dictionary<string, List<string>>();

        public DoubletDictionary() { }

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
            if (split.Length != 2)
                return "No solution.\n";

            return DetermineShortestSequenceToDoublet(split[0], split[1]);
        }

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
                return $"{start}\n{finish}\n\n";

            List<string> solution = new List<string>();
            Dictionary<string, int> wordsAlreadySeen = new Dictionary<string, int>();
            Dictionary<string, int> patternsAlreadyReviewed = new Dictionary<string, int>();
            Stack<Analysis> currentPath = new Stack<Analysis>();
            currentPath.Push(new Analysis(start, DictionaryOfWords[start]));

            do
            {
                var currentAnalysis = currentPath.Peek();
                if (currentAnalysis.IsLoopingWords)
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
                                    //we have found the word, set the solution
                                    solution = new List<string>(currentAnalysis.CurrentWordStack);
                                    solution.Add(finish);
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

                    if(currentAnalysis.IsLoopingWords == false && currentPath.Count > 0 && currentPath.Peek().Depth == currentAnalysis.Depth)
                        //make sure we are still looking at the same level, and we have finished all there is with this level
                        currentPath.Pop();
                }
            } while (currentPath.Count > 0);

            if (solution.Count > 0)
                return $"{string.Join("\n", solution)}\n";
            return "No solution.\n";
        }

        internal class Analysis
        {
            public int Depth { get; private set; }
            public List<string> CurrentWordStack { get; private set; }
            private List<string> Patterns { get; set; }
            private List<string> WordsToProcess { get; set; }

            public bool IsLoopingWords {get; private set; }

            public Analysis(string currentWord, List<string> patterns)
            {
                Depth = 1;
                CurrentWordStack = new List<string> { currentWord };
                Patterns = new List<string>(patterns);
            }

            public Analysis(Analysis existing, string newWord, List<string> patterns)
            {
                Depth = existing.Depth + 1;
                CurrentWordStack = new List<string>(existing.CurrentWordStack);
                CurrentWordStack.Add(newWord);
                Patterns = new List<string>(patterns);
            }

            public IEnumerable<string> GetNextPattern()
            {
                foreach (var item in Patterns)
                {
                    yield return item;
                }
            }

            public void LoadWords(List<string> words)
            {
                WordsToProcess = new List<string>();
                foreach (var word in words)
                {
                    if (CurrentWordStack.Contains(word) == false)
                        //skip the words we have already seen (avoids an infinite loop
                        WordsToProcess.Add(word);
                }

                IsLoopingWords = WordsToProcess.Count > 0 ? true : false;
            }

            public IEnumerable<string> GetNextWord()
            {
                int maxIterations = WordsToProcess.Count - 1;
                for (int i = 0; i < WordsToProcess.Count; i++)
                {
                    if (i == maxIterations)
                        //revert back to looping the patterns
                        IsLoopingWords = false;

                    yield return WordsToProcess[i];
                }
            }
        }
    }
}
