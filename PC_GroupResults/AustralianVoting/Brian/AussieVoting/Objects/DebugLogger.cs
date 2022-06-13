#if CREATE_DEBUG_FILE

using System;
using System.IO;
using System.Reflection;

namespace AussieVoting.Objects
{
    public class DebugLogger
    {
        private static TextWriter wtr = null;

        private static void Initialize()
        {
            string outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string debugFileName = Path.Combine(outputDirectory, "debug.txt");
            wtr = File.CreateText(debugFileName);
        }

        public static void WriteMessage(string message)
        {
            if (wtr == null)
                Initialize();
            wtr.WriteLine(String.Format(
                @"{0:HH.mm.ss.fff}   {1}",
                DateTime.Now,
                message));
        }

        public static void Close()
        {
            if (wtr != null)
            {
                wtr.Flush();
                wtr.Close();
                wtr.Dispose();
                wtr = null;
            }
        }

    }  //end class
}  // end namespace

#endif