namespace AutomatedJudgeScript
{
    internal static class StaticUtilities
    {
        public static IEnumerable<EvalationData> LoadCasesFromFile(string fileName)
        {
            int excutionNumber = 0;
            using var reader = new StreamReader(fileName);

            do
            {
                var expected = ReadDataSet(reader);
                //only have something to process if the first grouping has something to process
                if (expected.Count > 0)
                {
                    List<string> received = ReadDataSet(reader);
                    yield return new EvalationData(++excutionNumber, expected, received);
                }
            } while (!reader.EndOfStream);
        }

        private static List<string> ReadDataSet(StreamReader reader)
        {
            int.TryParse(reader.ReadLine(), out int numberOfInputLines);
            var set = new List<string>();
            if (numberOfInputLines > 0)
            {
                for (int i = 0; i < numberOfInputLines; i++)
                {
                    var line = reader.ReadLine();
                    if (line !=  null)
                        set.Add(line);
                }
            }
            return set;
        }
    }
}
