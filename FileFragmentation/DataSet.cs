
namespace FileFragmentation
{
    internal class DataSet
    {
        private readonly List<string> Data;
        private Dictionary<string, int> possibleResults = new Dictionary<string, int>();
        //setting the lengths so that we can rank appropriately based on data
        private int minLength { get; set; } = 512;
        private int maxLength { get; set; } = 1;
        public DataSet(List<string> data) => Data = data;

        public string Defrag()
        {
            //special case -- test for it before parsing any of the data further
            if (Data.Count / 2 == 1)
                return (string.Join("", Data));
            
            //was going to use a ToLookup, but I need to interact with the contents as a list instead of an IEnumerable
            Dictionary<int, List<string>> groupedBySize = BuildDataLengthDictionary();

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
                if (groupedBySize.ContainsKey(groupingIndex) == false)
                    //this presumed bucket doesn't exist, so move on to the next pairing to evaluate
                    continue;

                //pointers to the arrays we are going to walk through
                List<string> lowerValues = groupedBySize[groupingIndex];
                List<string> upperValues = groupedBySize[maxLength];

                //looping over the groups
                for (int lowerValueIndex = 0; lowerValueIndex < lowerValues.Count; lowerValueIndex++)
                {
                    for (int upperValueIndex = 0; upperValueIndex < upperValues.Count; upperValueIndex++)
                    {
                        if (groupingIndex == maxLength && lowerValueIndex == upperValueIndex)
                        {
                            //in the situation where the middle ends up being the same from both directions
                            //we want to skip evaluating adding a record to itself
                            continue;
                        }

                        var min = lowerValues[lowerValueIndex];
                        var max = upperValues[upperValueIndex];

                        var first = Match(min, max);
                        if (possibleResults.ContainsKey(first))
                            possibleResults[first]++;

                        var second = Match(max, min);
                        if (first != second && possibleResults.ContainsKey(second))
                            possibleResults[second]++;
                    }
                }
            }

            return possibleResults.OrderByDescending(x => x.Value).First().Key;
        }

        private Dictionary<int, List<string>> BuildDataLengthDictionary()
        {
            Dictionary<int, List<string>> groupedBySize = new Dictionary<int, List<string>>();
            foreach (var item in Data)
            {
                if (groupedBySize.ContainsKey(item.Length))
                    groupedBySize[item.Length].Add(item);
                else
                {
                    groupedBySize.Add(item.Length, new List<string> { item });
                    
                    //update the lengths based on not having seen before
                    if (minLength > item.Length)
                        minLength = item.Length;

                    if (maxLength < item.Length)
                        maxLength = item.Length;
                }
            }

            return groupedBySize;
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
