using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AussieVoting.Objects
{
    public class Parsers
    {

        public static int IntParseFast(string value)
        {
            int result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                result = 10 * result + (value[i] - 48);
            }
            return result;
        }

    }  // end class
}  // end namespace
