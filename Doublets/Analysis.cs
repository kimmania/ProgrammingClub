namespace Doublets
{
    /// <summary>
    /// This class exists to keep track of state on the stack including relevant iterators for the recursive functionality
    /// </summary>
    internal class Analysis
    {
        /// <summary>
        /// Indicator of the number of words we have evaluated at this level within the stack
        /// </summary>
        public int Depth { get; private set; }
        /// <summary>
        /// Possible solution relative to the current depth of analysis. May be thrown out when the processed patterns turn out to not yield anything
        /// </summary>
        public List<string> CurrentWordStack { get; private set; }
        /// <summary>
        /// List of patterns to evaluate next
        /// </summary>
        private List<string> Patterns { get; set; }
        /// <summary>
        /// Current list of words related to the current iterator pattern
        /// </summary>
        private List<string> WordsToProcess { get; set; }

        /// <summary>
        /// Indicator of where were are within the stack as far as processing goes...currently iterating over the patterns or over the words
        /// </summary>
        public bool IsLoopingWords { get; private set; }

        /// <summary>
        /// Initial record to start the stack
        /// </summary>
        /// <param name="currentWord"></param>
        /// <param name="patterns"></param>
        public Analysis(string currentWord, List<string> patterns)
        {
            Depth = 1;
            CurrentWordStack = new List<string> { currentWord };
            Patterns = new List<string>(patterns);
        }

        /// <summary>
        /// Additional records that utilize information from previous records in the stack (ie, the current possible solution)
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="newWord"></param>
        /// <param name="patterns"></param>
        public Analysis(Analysis existing, string newWord, List<string> patterns)
        {
            Depth = existing.Depth + 1;
            CurrentWordStack = new List<string>(existing.CurrentWordStack);
            CurrentWordStack.Add(newWord);
            Patterns = new List<string>(patterns);
        }

        /// <summary>
        /// Iterator of the patterns on the class itself so that was we walk the stack, we don't lose our previous iteration
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetNextPattern()
        {
            foreach (var item in Patterns)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Simple method to load the possible words to evaluate. It naturally excludes any words that already exist in the possible solution so that we do not naturally cause an infinite loop
        /// </summary>
        /// <param name="words"></param>
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

        /// <summary>
        /// Iterator of the patterns on the class itself so that was we walk the stack, we don't lose our previous iteration
        /// </summary>
        /// <returns></returns>
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
