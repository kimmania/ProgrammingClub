using System.Text;

namespace CommonPermutation
{
    internal class WordPair
    {
        private readonly string A;
        private readonly string B;

        public WordPair(string a, string b)
        {
            this.A = a;
            this.B = b;
        }

        public string Evaluate()
        {
            if (string.IsNullOrWhiteSpace(A) || string.IsNullOrWhiteSpace(B))
                return "";

            //we have something to process
            StringBuilder result = new("");//initialize the result
            using (var a = ParseString(A).GetEnumerator())
            using (var b = ParseString(B).GetEnumerator())
            {
                //load up the first letter of each
                a.MoveNext();
                b.MoveNext();
                do
                {
                    //we will simultaneously walk both letter groupings relative to the current alphabetical order,
                    //so move
                    // when letters match, move both enumerators
                    // when one is alphabetically earlier than the other, move the earlier alphabetical enumerator forward
                    if (a.Current.Letter == b.Current.Letter)
                    {
                        //write out the matching letter
                        result.Append(new string(a.Current.Letter, b.Current.Count < a.Current.Count ? b.Current.Count : a.Current.Count));
                        //move both forward
                        if (!a.MoveNext() || !b.MoveNext())
                        {
                            //going to break out as there is nothing left to do
                            break;
                        }
                    }
                    else if (a.Current.Letter < b.Current.Letter)
                    {
                        //move a forward //this inner if cannot be moved up to the outer if as it would cause b to also move forward
                        if (!a.MoveNext())
                        {
                            //trigger exiting if we have hit the end of a
                            break;
                        }
                    }
                    else if (!b.MoveNext())
                    {
                        //will move b forward
                        // but triggers exiting if we have hit the end
                        break;
                    }
                } while (true);
            }

            return result.ToString();
        }

        //group word by letter with the count, and order alphabetical
        private IOrderedEnumerable<Calc> ParseString(string toParse) => toParse.GroupBy(c => c).Select(g => new Calc(g.Key, g.Count())).OrderBy(x => x.Letter);

        //define the important content for grouping and sorting
        public class Calc
        {
            public char Letter { get; set; }
            public int Count { get; set; }

            public Calc(char letter, int count)
            {
                Letter = letter;
                Count = count;
            }
        }
    }
}
