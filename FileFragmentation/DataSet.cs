
namespace FileFragmentation
{
    internal class DataSet
    {
        private readonly List<string> Data;
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
            var expectedLength = minLength + maxLength;

            Dictionary<string, int> possibleResults = new Dictionary<string, int>();
            //add all combinations for the shortest and the longest
            foreach (var min in groupedBySize[minLength])
            {
                foreach (var max in groupedBySize[maxLength])
                {
                    var first = $"{min}{max}";
                    if (possibleResults.TryAdd(first, 1) == false)
                        possibleResults[first]++;

                    var second = $"{max}{min}";
                    if (possibleResults.TryAdd(second, 1) == false)
                        possibleResults[second]++;
                }
            }

            if (possibleResults.Count == 1)
                return possibleResults.First().Key;

            for (int groupingIndex = ++minLength; groupingIndex <= --maxLength; groupingIndex++)
            {
                var lowerValues = groupedBySize[groupingIndex].ToList();
                var upperValues = groupedBySize[maxLength].ToList();
                //looping over the groups
                for (int lowerValueIndex = 0; lowerValueIndex < lowerValues.Count(); lowerValueIndex++)
                {
                    for (int upperValueIndex = 0; upperValueIndex < upperValues.Count(); upperValueIndex++)
                    {
                        var min = lowerValues[lowerValueIndex];
                        var max = upperValues[upperValueIndex];
                        if (groupingIndex == maxLength && lowerValueIndex == upperValueIndex)
                        {
                            //skip adding itself to itself into the dictionary (skews the numbers)
                            continue;
                        }

                        var first = $"{min}{max}";
                        if (possibleResults.TryAdd(first, 1) == false)
                            possibleResults[first]++;

                        var second = $"{max}{min}";
                        if (first != second)
                            if (possibleResults.TryAdd(second, 1) == false)
                                possibleResults[second]++;
                        
                    }
                }
            }

            return possibleResults.OrderByDescending(x => x.Value).First().Key;
        }
    }
}
