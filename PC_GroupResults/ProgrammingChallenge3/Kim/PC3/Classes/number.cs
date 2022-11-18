using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC3.Classes
{
    enum sides
    {
        leftonly,
        rightonly,
        both,
        empty
    }

    public struct number
    {
        char digit;
        bool top;
        bool bottom;
        bool middle;
        sides topsides;
        sides bottomsides;

        public number(char digit, bool top, bool bottom, bool middle, sides topsides, sides bottomsides)
        {
            this.digit = digit;
            this.top = top;
            this.bottom = bottom;
            this.middle = middle;
            this.topsides = topsides;
            this.bottomsides = bottomsides;
        }
    }

    struct numbers
    {
        public Dictionary<char,number> digits = new Dictionary<char,number>();
        public numbers()
        {
            digits.Add('1', new number('1',false,false,false,sides.rightonly, sides.rightonly));
            digits.Add('2', new number('2', true, true, true, sides.rightonly, sides.leftonly));
            digits.Add('3', new number('3', true, true, true, sides.rightonly, sides.rightonly));
            digits.Add('4', new number('4', true, true, false, sides.both, sides.rightonly));
            digits.Add('5', new number('5', true,true,true, sides.leftonly, sides.rightonly));
            digits.Add('6', new number('6', true, true, true, sides.leftonly, sides.both));
            digits.Add('7', new number('7', true, false, false, sides.rightonly, sides.rightonly));
            digits.Add('8', new number('8', true, true, true, sides.both, sides.both));
            digits.Add('9', new number('9', true, true, true, sides.both, sides.rightonly));
            digits.Add('0', new number('0', false, false, false, sides.rightonly, sides.rightonly));
        }
    }
}
