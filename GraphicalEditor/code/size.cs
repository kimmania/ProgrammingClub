using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC4
{
    public struct size
    {
        public int sz;
        public string bar;
        public string leftonly;
        public string rightonly;
        public string empty;
        public string both;

        public size(int sz, string bar, string lo, string ro, string empty, string both)
        {
            this.sz = sz;
            this.bar = bar;
            this.leftonly = lo;
            this.rightonly = ro;
            this.empty = empty;
            this.both = both;
        }
    }


    class sizes
    {
        public Dictionary<string,size> szs = new Dictionary<string,size>();

        public sizes()
        {
            szs.Add("1", new size(1," -  ","|   ","  | ","    ","| | "));
            szs.Add("2", new size(2," --  ","|    ","   | ","     ","|  | "));
            szs.Add("3", new size(3," ---  ","|     ","    | ","      ","|   | "));
            szs.Add("4", new size(4," ----  ","|      ","     | ","       ","|    | "));
            szs.Add("5", new size(5," -----  ","|       ","      | ","        ","|     | "));
            szs.Add("6", new size(6," ------  ","|        ","       | ","         ","|      | "));
            szs.Add("7", new size(7," -------  ","|         ","        | ","          ","|       | "));
            szs.Add("8", new size(8," --------  ","|          ","         | ","           ","|        | "));
            szs.Add("9", new size(9," ---------  ","|           ","          | ","            ","|         | "));
            szs.Add("10", new size(10, " ----------  ", "|            ", "           | ", "             ", "|          | "));
        }
    }



}
