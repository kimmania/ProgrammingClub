using System.Text;

namespace WheresWaldorf
{
    internal class Point
    {
        public int Row;
        public int Column;
        
        public Point(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

    internal class Case
    {
        List<string> RawData;
        int maxRows;
        int maxCols;
        int numberofWords;
        char[,] Grid; //converted to lowercase of the provided grid
        
        //processing shortcuts
        Dictionary<char,List<Point>> LetterLocations = new Dictionary<char,List<Point>>();

        StringBuilder Output = new StringBuilder("");

        public Case(List<string> data)
        {
            //load the data for use, but initialize the key properties here
            this.RawData = data;
            //first line is size of the grid
            var sizes = data[0].Split(' ');
            maxRows = int.Parse(sizes[0]);
            maxCols = int.Parse(sizes[1]);
            //then the grid
            Grid = new char[maxRows + 1, maxCols + 1]; // adding one so we can work without having to subtract to accommodate zero based array
            //number of words
            numberofWords = int.Parse(data[maxRows+1]);
        }

        public string SolveCase()
        {
            LoadGridAndLetterLocations();
            ProcessWords();
            return Output.ToString();
        }

        private void ProcessWords()
        {
            for (int wordIndex = maxRows + 2; wordIndex < RawData.Count; wordIndex++)
            {
                var word = RawData[wordIndex];
                //we need the following info:
                //first letter formated for comparison
                char first = char.ToLower(word[0]);
                //last letter formated for comparison
                char last = char.ToLower(word[word.Length - 1]);
                //length
                int length = word.Length - 1;

                //find possible matches
                if (LetterLocations.ContainsKey(first))
                {
                    foreach (var point in LetterLocations[first])
                    {
                        //calculate the four outer most directions
                        int left = point.Column - length;
                        int right = point.Column + length;
                        int up = point.Row - length;
                        int down = point.Row + length;

                        //instructions indicate we are guaranteed to find the word

                        //we have something to even look at, now evaluate in the order that matters for recording
                        //and stop when we have a matching word                       
                        if (down <= maxRows)
                        {
                            if (Grid[down, point.Column] == last && IsFullWord(word, point, 1, 0))
                                //Down,
                                break;

                            //RightDown,
                            if (right <= maxCols && Grid[down, right] == last && IsFullWord(word, point, 1, 1))
                                break;

                            //LeftDown,
                            if (left > 0 && Grid[down, left] == last && IsFullWord(word, point, 1, -1))
                                break;
                        }

                        //Right,
                        if (right <= maxCols && Grid[point.Row, right] == last && IsFullWord(word, point, 0, 1))
                            break ;
                        
                        if (up > 0)
                        {
                            //RightUp,
                            if (right <= maxCols && Grid[up, right] == last && IsFullWord(word, point, -1, 1))
                                break;

                            if (Grid[up, point.Column] == last && IsFullWord(word, point, -1, 0))
                                //Up,
                                break ;

                            //LeftUp,
                            if (left > 0 && Grid[up, left] == last && IsFullWord(word, point, -1, -1))
                                break;
                        }

                        //Left,
                        if (left > 0 && Grid[point.Row, left] == last && IsFullWord(word, point, 0, -1))
                            break;
                    }
                }
            }
        }

        private bool IsFullWord(string word, Point gridStart, int verticalStep, int horizontalStep)
        {
            //we know beginning and end match, check the middle -- exit as soon as we find a mismatch
            int letterIndex = 1;
            int maxExecutions = word.Length - 2;
            int row = gridStart.Row;
            int column = gridStart.Column;
            do
            {
                //increment
                row = row + verticalStep;
                column = column + horizontalStep;
                if (word[letterIndex++] != Grid[row, column])
                    return false;
            } while (letterIndex < maxExecutions);

            Output.AppendLine($"{gridStart.Row} {gridStart.Column}");
            return true;
        }

        private void LoadGridAndLetterLocations()
        {
            for (int row = 1; row <= maxRows; row++)
            {
                var line = RawData[row];
                for (int j = 0; j < maxCols; j++)
                {
                    var letter = char.ToLower(line[j]);
                    int column = j + 1;
                    Grid[row, column] = letter; //add the letter to the grid

                    //store the letter locations
                    if (LetterLocations.ContainsKey(letter))
                        LetterLocations[letter].Add(new Point(row, column));
                    else
                        LetterLocations.Add(letter, new List<Point>() { new Point(row, column) });    
                }
            }
        }
    }
}
