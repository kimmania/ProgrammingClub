using System.Text;

namespace CryptKicker2
{
    internal class EncryptedMessage
    {
        private readonly List<string> Data;
        private const string NoSolution = "No Solution.\n";
        private const int KeyLength = 43;
        private Dictionary<char, char> Translation = new Dictionary<char, char>();
        public EncryptedMessage(List<string> data) => this.Data = data;

        public string Decrypt()
        {
            if (FindKey())
            {
                //now we just need to translate each line
                StringBuilder result = new StringBuilder();
                foreach (string line in this.Data)
                {
                    foreach (var chr in line)
                    {
                        result.Append(Translation[chr]);
                    }
                    
                    result.Append("\n");
                }
                return result.ToString();
            }
            return NoSolution;
        }

        private bool FindKey()
        {
            foreach (var line in Data)
            {
                //throw out if the wrong length
                if (line.Length != KeyLength)
                    continue;

                //check spaces
                if (
                    line[3] != ' ' ||
                    line[9] != ' ' ||
                    line[15] != ' ' ||
                    line[19] != ' ' ||
                    line[25] != ' ' ||
                    line[30] != ' ' ||
                    line[34] != ' ' ||
                    line[39] != ' '
                )
                    continue;

                Dictionary<char, char> translation = new Dictionary<char, char>();
                //to simplify lookups, just map space to itself
                translation.Add(' ', ' ');

                if (
                    //check for matching letters the
                    line[0] != line[31] ||
                    line[1] != line[32] ||
                    line[2] != line[33] || line[2] != line[28] ||
                    //o
                    line[12] != line[17] || line[12] != line[26] || line[12] != line[41] ||
                    //u
                    line[5] != line[21] ||
                    //r
                    line[11] != line[29] ||

                    //check for uniqueness when adding
                    //'the'
                    !translation.TryAdd(line[0], 't') ||
                    !translation.TryAdd(line[1], 'h') ||
                    !translation.TryAdd(line[2], 'e') ||

                    //quick
                    !translation.TryAdd(line[4], 'q') ||
                    !translation.TryAdd(line[5], 'u') ||
                    !translation.TryAdd(line[6], 'i') ||
                    !translation.TryAdd(line[7], 'c') ||
                    !translation.TryAdd(line[8], 'k') ||

                    //brown
                    !translation.TryAdd(line[10], 'b') ||
                    !translation.TryAdd(line[11], 'r') ||
                    !translation.TryAdd(line[12], 'o') ||
                    !translation.TryAdd(line[13], 'w') ||
                    !translation.TryAdd(line[14], 'n') ||

                    //fox
                    !translation.TryAdd(line[16], 'f') ||
                    //!translation.TryAdd(line[17], 'o') ||
                    !translation.TryAdd(line[18], 'x') ||
                    //jumps
                    !translation.TryAdd(line[20], 'j') ||
                    //!translation.TryAdd(line[21], 'u') ||
                    !translation.TryAdd(line[22], 'm') ||
                    !translation.TryAdd(line[23], 'p') ||
                    !translation.TryAdd(line[24], 's') ||
                    //over
                    //!translation.TryAdd(line[26], 'o') ||
                    !translation.TryAdd(line[27], 'v') ||
                    //!translation.TryAdd(line[28], 'e') ||
                    //!translation.TryAdd(line[29], 'r') ||
                    //lazy
                    !translation.TryAdd(line[35], 'l') ||
                    !translation.TryAdd(line[36], 'a') ||
                    !translation.TryAdd(line[37], 'z') ||
                    !translation.TryAdd(line[38], 'y') ||
                    //dog
                    !translation.TryAdd(line[40], 'd') ||
                    //!translation.TryAdd(line[41], 'o') ||
                    !translation.TryAdd(line[42], 'g')
                )
                    continue;

                //we have a key, so update with our new dictionary and exit
                Translation = translation;
                return true;
            }

            return false;
        }
    }
}
