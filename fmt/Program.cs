using fmt;

using var reader = new StreamReader(@".\Data\Input.txt");

var content = new List<string>();
do
{
    content.Add(reader.ReadLine());    

} while (!reader.EndOfStream);


Console.Write((new FmtDocument(content)).Format());