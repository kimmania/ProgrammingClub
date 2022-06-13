using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace MaxPathSum.v1
{
    public partial class Form1 : Form
    {

        private char[] separators = new char[] { ' ' };

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data files");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFile.Text = openFileDialog1.FileName;
                ProcessFile(openFileDialog1.FileName);
            }
        }

        private void ProcessFile(string fileName)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<int[]> parsedLines = new List<int[]>();
            string line;
            using (var rdr = File.OpenText(fileName))
            {
                while ((line = rdr.ReadLine()) != null)
                {
                    parsedLines.Add(ParseLine(line));
                }
            }  // end using rdr

            int sum = CalculateSum(parsedLines);

            sw.Stop();

            lblSum.Text = sum.ToString();
            lblElapsed.Text = String.Format("{0} ms, {1} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);
            gbResults.Visible = true;
        }

        private int CalculateSum(List<int[]> parsedLines)
        {
            int rowNumber = parsedLines.Count - 2;
            while (rowNumber >= 0)
            {
                CalculateMiniTriangleMaxSums(parsedLines[rowNumber], parsedLines[rowNumber + 1]);
                rowNumber--;
            }  // end while

            return parsedLines[0][0];
        }

        private void CalculateMiniTriangleMaxSums(int[] apexRow, int[] nextRow)
        {
            int apexValue;
            int sumLeft;
            int sumRight;
            for (int i = 0; i < apexRow.Length; i++)
            {
                apexValue = apexRow[i];
                sumLeft = apexValue + nextRow[i];
                sumRight = apexValue + nextRow[i + 1];
                apexRow[i] = ((sumLeft > sumRight) ? sumLeft : sumRight);
            }
        }

        private int[] ParseLine(string line)
        {
            string[] values = line.Split(separators);
            int[] numbers = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                numbers[i] = IntParseFast(values[i]);
            }
            return numbers;
        }

        private int IntParseFast(string value)
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
