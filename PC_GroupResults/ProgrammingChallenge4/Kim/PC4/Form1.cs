using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PC4
{
    public partial class Form1 : Form
    {
        enum Algorithm
        {
            first = 0,
            second = 1,
            third = 2
            #region hide
            /*,
            fourth = 3,
            fifth = 4,
            sixth = 5*/
            #endregion
        }

        Stat[] stats = new Stat[6];

        Algorithm aToRun = Algorithm.first;

        public Form1()
        {
            InitializeComponent();
            foreach (Algorithm a in Enum.GetValues(typeof(Algorithm)))
            {
                stats[(int)a] = new Stat();
            }

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox2.Text = openFileDialog1.FileName;
        }

        private void WriteToLog(Algorithm test, long time)
        {
            textBox1.AppendText(string.Format("{0}\t{1}\n",test.ToString(),time));
            UpdateStat(test, time);
        }
        private void genericButtonClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Select a file to process first!");
                return;
            }
            
            runOne(aToRun);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Please select a file";
            openFileDialog1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //first
            aToRun = Algorithm.first;
            genericButtonClick(sender, e);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //clear stats
            ClearStats();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //run all
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Select a file to process first!");
                return;
            }
            ClearStats();
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                if (checkBox1.Checked)
                    runOne(Algorithm.first);
                if (checkBox2.Checked)
                     runOne(Algorithm.second);
                if (checkBox3.Checked)
                     runOne(Algorithm.third);
                #region hide
                /* 
                
                 if (checkBox4.Checked)
                     runOne(Algorithm.fourth);
                 if (checkBox5.Checked)
                     runOne(Algorithm.fifth);
                 if (checkBox6.Checked)
                     runOne(Algorithm.sixth);
                 */
                #endregion
            }
            DisplayStats();
        }

        private void runOne(Algorithm toRun)
        {
            long result = 0;

            switch (toRun)
            {
                case Algorithm.first:
                    GE1 first = new GE1();
                    result = first.Run(textBox2.Text, textBox2.Text + "result-ge1.txt");
                    break;
                case Algorithm.second:
                    GE2 second = new GE2();
                    result = second.Run(textBox2.Text, textBox2.Text + "result-ge2.txt");
                    break;
                case Algorithm.third:
                    GE3 third = new GE3();
                    result = third.Run(textBox2.Text, textBox2.Text + "result-ge3.txt");
                    break;
                #region hide
                /*
                 
                case Algorithm.fourth:
                    LCD4 fourth = new LCD4();
                    result = fourth.Run(textBox2.Text, textBox2.Text + "result-ge4.txt");
                    break;
                case Algorithm.fifth:
                    LCD5 fifth = new LCD5();
                    result = fifth.Run(textBox2.Text, textBox2.Text + "result-ge5.txt");
                    break;
                case Algorithm.sixth:
                    LCD6 sixth = new LCD6();
                    result = sixth.Run(textBox2.Text, textBox2.Text + "result-ge6.txt");
                    break;
                 */
                #endregion

            }
            WriteToLog(toRun, result);
        }

        private void UpdateStat(Algorithm toRun, long time)
        {
            stats[(int)toRun].AddRun(time);
        }

        private void ClearStats()
        {
            textBox1.Clear();
            foreach (Algorithm a in Enum.GetValues(typeof(Algorithm)))
            {
                stats[(int)a].ClearStat();
            }
        }

        private void DisplayStats()
        {
            textBox1.Clear();
            textBox1.Text = string.Format("{0}\t{1}", "Algorithm", stats[0].ToStringHeader());
            foreach (Algorithm a in Enum.GetValues(typeof(Algorithm)))
            {
                if (stats[(int)a].average > 0)
                    textBox1.AppendText(string.Format("{0}\t{1}", a.ToString(), stats[(int)a].ToString()));
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            //display current stats
            DisplayStats();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //second
            aToRun = Algorithm.second;
            genericButtonClick(sender, e);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //third
            aToRun = Algorithm.third;
            genericButtonClick(sender, e);
        }
        #region unused




        private void button5_Click(object sender, EventArgs e)
        {
            //fourth
            //aToRun = Algorithm.fourth;
            //genericButtonClick(sender, e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //fifth
            //aToRun = Algorithm.fifth;
            //genericButtonClick(sender, e);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //sixth
            //aToRun = Algorithm.sixth;
            //genericButtonClick(sender, e);
        }
        #endregion

    }
}
