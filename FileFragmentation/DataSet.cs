
namespace FileFragmentation
{
    internal class DataSet
    {
        private readonly List<string> Data;
        private Dictionary<string, int> possibleResults = new Dictionary<string, int>();

        public DataSet(List<string> data) => Data = data;

        public string Defrag()
        {
            var groupedBySize = Data.ToLookup(x => x.Length);
            var numberOfPairings = Data.Count / 2;
            
            //special case
            if (numberOfPairings == 1)
                return (string.Join("", Data));

            var minLength = groupedBySize.Min(x => x.Key);
            var maxLength = groupedBySize.Max(x => x.Key);

            //add all combinations for the shortest and the longest
            foreach (var min in groupedBySize[minLength])
            {
                foreach (var max in groupedBySize[maxLength])
                {
                    AddToPossibleSolutions(min, max);
                    AddToPossibleSolutions(max, min);
                }
            }

            if (possibleResults.Count == 1)
                return possibleResults.First().Key;

            //based on loading the above, those are the only possible solutions,
            //so really, below, we just need to track the new ones matching the existing sets
            for (int groupingIndex = ++minLength; groupingIndex <= --maxLength; groupingIndex++)
            {
                if (groupedBySize[groupingIndex] == null)
                    //this presumed bucket doesn't exist, so move on to the next pairing to evaluate
                    continue;

                var lowerValues = groupedBySize[groupingIndex].ToList();
                var upperValues = groupedBySize[maxLength].ToList();

                //looping over the groups
                for (int lowerValueIndex = 0; lowerValueIndex < lowerValues.Count; lowerValueIndex++)
                {
                    for (int upperValueIndex = 0; upperValueIndex < upperValues.Count; upperValueIndex++)
                    {
                        var min = lowerValues[lowerValueIndex];
                        var max = upperValues[upperValueIndex];
                        if (groupingIndex == maxLength && lowerValueIndex == upperValueIndex)
                        {
                            //skip adding itself to itself into the dictionary (skews the numbers)
                            continue;
                        }

                        var first = Match(min, max);
                        if (possibleResults.ContainsKey(first))
                            possibleResults[first]++;

                        var second = Match(max, min);
                        if (first != second &&possibleResults.ContainsKey(second))
                            possibleResults[second]++;   
                    }
                }
            }

            return possibleResults.OrderByDescending(x => x.Value).First().Key;
        }

        private void AddToPossibleSolutions(string min, string max)
        {
            var value = Match(min, max);
            if (possibleResults.ContainsKey(value) == false)
                possibleResults.Add(value, 1);
        }

        private string Match(string first, string last) => $"{first}{last}";
    }
}
