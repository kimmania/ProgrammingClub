
namespace CryptKicker2
{
    internal static class StaticUtilities
    {
        public static IEnumerable<EncryptedMessage> LoadCasesFromFile(string fileName)
        {
            using var reader = new StreamReader(fileName);
            var temp = reader.ReadLine();
            reader.ReadLine(); //empty line

            var numberOfCases = int.Parse(temp ?? "0");
            var loadedCases = 0;

            while (loadedCases < numberOfCases && !reader.EndOfStream)
            {
                loadedCases++;
                var set = new List<string>();
                do
                {
                    temp = reader.ReadLine();
                    if (string.IsNullOrEmpty(temp))
                        break;
                    else
                        set.Add(temp);
                } while (true);

                yield return new EncryptedMessage(set);
            }
        }
    }
}
