using System.Net.Sockets;
using System.Text;

namespace fmt
{
    internal class FmtDocument
    {
        /*
            Reads lines of text, combining and breaking lines so as to 
            1. create an output file
            2. with lines as close to without exceeding 72 characters long as possible. 
            3. The rules for combining and breaking lines are as follows.
                • A new line may be started anywhere there is a space in the input. If a new line is started, there
                    will be no trailing blanks at the end of the previous line or at the beginning of the new line.
                • A line break in the input may be eliminated in the output, provided it is not followed by a space
                    or another line break. If a line break is eliminated, it is replaced by a space.
                • Spaces never appear at the end of a line.
                • If a sequence of characters longer than 72 characters appears without a space or line break, it
                    appears by itself on a line.         
         */

        private List<string> Content;
        private int LineLength = 72;
        private StringBuilder Output = new StringBuilder();
        public FmtDocument(List<string> content)
        {
            Content = content;
        }

        public string Format()
        {
            //ensure we have something to process
            if (Content.Count > 0)
            {
                StringBuilder lineToWrite = new StringBuilder(72);
                int currentLine = 0;
                int maxLine = Content.Count - 1;

                while (currentLine <= maxLine)
                {
                    
                }
            }
            return Output.ToString();
        }
    }
}
