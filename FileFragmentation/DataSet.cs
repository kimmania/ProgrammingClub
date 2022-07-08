
namespace FileFragmentation
{
    internal class DataSet
    {
        private readonly List<string> Data;
        public DataSet(List<string> data) => Data = data;

        public string Defrag()
        {
            var groupedBySize = Data.ToLookup(x => x.Length);
            return string.Empty;
        }
    }
}
