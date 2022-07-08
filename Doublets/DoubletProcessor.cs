namespace Doublets
{
    internal class DoubletProcessor
    {
        private readonly string Start;
        private readonly string Finish;
        private readonly DoubletDictionary DoubletDict;
        
        //Processing properties
        private string Result { get; set; } = "";
        private bool Processed = false;

        List<string> Solution = new List<string>();
        Dictionary<string, int> WordsAlreadySeen = new Dictionary<string, int>();
        Dictionary<string, int> PatternsAlreadyReviewed = new Dictionary<string, int>();
        Stack<Analysis> CurrentPath = new Stack<Analysis>();

        public DoubletProcessor(DoubletDictionary dictionary, string doubletToParse)
        {
            DoubletDict = dictionary;
            if (string.IsNullOrWhiteSpace(doubletToParse))
            {
                SetResult("");
                return;
            }

            var split = doubletToParse.Split(' ');
            if (split.Length != 2)
            {
               SetResult("No Solution.");
                return;
            }

            Start = split[0];
            Finish = split[1];
            PerformInitialAssessmentAndSetup();
        }

        public DoubletProcessor(DoubletDictionary dictionary, string start, string finish)
        {
            DoubletDict = dictionary;
            Start = start;
            Finish = finish;
            PerformInitialAssessmentAndSetup();
        }

        public string ProcessDoublet()
        {
            if (Processed)
                return Result;

            //this is essentially building a recursive method but managing the recursion within the Analysis class instead of passing parameters into recursive methods
            do
            {
                var currentAnalysis = CurrentPath.Peek();

                if (Solution.Count > 0 && currentAnalysis.Depth > Solution.Count)
                {
                    CurrentPath.Pop();
                }
                else if (currentAnalysis.IsLoopingWords)
                {
                    ProcessWords(currentAnalysis);
                    //when we hit the end of words, the flag automatically gets flipped for IsLoopingWords, so we can continue one with the do loop
                }
                else
                {
                    ProcesPatterns(currentAnalysis);

                    if (
                        CurrentPath.Count > 0 &&
                        currentAnalysis.IsLoopingWords == false &&
                        CurrentPath.Peek().Depth == currentAnalysis.Depth
                    )
                        //make sure we are still looking at the same level, and we have finished all there is with this level
                        CurrentPath.Pop();
                }
            } while (CurrentPath.Count > 0);

            if (Solution.Count > 0)
                return $"{string.Join("\n", Solution)}\n";
            return "No Solution.\n";
        }

        private void ProcesPatterns(Analysis currentAnalysis)
        {
            foreach (var pattern in currentAnalysis.GetNextPattern())
            {
                if (PatternsAlreadyReviewed.ContainsKey(pattern) && PatternsAlreadyReviewed[pattern] <= currentAnalysis.Depth)
                {
                    //skip as the results will end up being as long or longer, and not a Solution we would use
                    continue;
                }
                else
                {
                    if (PatternsAlreadyReviewed.TryAdd(pattern, currentAnalysis.Depth) == false)
                        PatternsAlreadyReviewed[pattern] = currentAnalysis.Depth;

                    //going to look through the patterns to start processing words
                    var wordsToCheck = DoubletDict.GetWordsForPattern(pattern);
                    if (wordsToCheck.Count > 0)
                    {
                        if (wordsToCheck.Contains(Finish))
                        {
                            if (Solution.Count == 0 || Solution.Count > currentAnalysis.CurrentWordStack.Count + 1)
                            {
                                //we have found the word, set the Solution
                                Solution = new List<string>(currentAnalysis.CurrentWordStack);
                                Solution.Add(Finish);
                            }
                            CurrentPath.Pop();
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
        }

        private void ProcessWords(Analysis currentAnalysis)
        {
            foreach (var word in currentAnalysis.GetNextWord())
            {
                if (WordsAlreadySeen.ContainsKey(word) && WordsAlreadySeen[word] <= currentAnalysis.Depth)
                    //no point in checking out further, we already know this leads to something wrong
                    continue;

                if (WordsAlreadySeen.TryAdd(word, currentAnalysis.Depth) == false)
                    WordsAlreadySeen[word] = currentAnalysis.Depth;

                var patterns = DoubletDict.GetPatternsForWord(word);
                if (patterns.Count > 0)
                {
                    CurrentPath.Push(new Analysis(currentAnalysis, word, patterns));
                    break;
                }
            }
        }

        private void PerformInitialAssessmentAndSetup()
        {
            if (Start == Finish)
            {
                SetResult(Start);
                return;
            }

            //No Solution
            //  if either word is null or empty
            //  length of words do not match (have no way to get to a match)
            if (
                string.IsNullOrWhiteSpace(Start) ||
                string.IsNullOrWhiteSpace(Finish) ||
                Start.Length != Finish.Length 
            )
            {
                SetResult("No Solution.");
                return;
            }

            //  either word not in the dictionary of words
            var startPatterns = DoubletDict.GetPatternsForWord(Start);
            var finishPatterns = DoubletDict.GetPatternsForWord(Finish);
            if (startPatterns.Count == 0 || finishPatterns.Count == 0)
            {
                SetResult("No Solution.");
                return;
            }

            //test for the simplest case first, do the two words have a shared pattern
            if (startPatterns.Intersect(finishPatterns).Any())
            {
                SetResult($"{Start}\n{Finish}");
                return;
            }

            //initialize our stack
            CurrentPath.Push(new Analysis(Start, startPatterns));
            WordsAlreadySeen.Add(Start, 0);
        }

        private void SetResult(string value)
        {
            Result = $"{value}\n";
            Processed = true;
        }
    }
}
