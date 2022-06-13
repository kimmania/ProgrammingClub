using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

using AussieVoting.Objects;

namespace AussieVoting
{
    public partial class Form1 : Form
    {

        private StringBuilder errorMessages = new StringBuilder();

        private long executionCount = 0;
        private long executionTotalMilliseconds = 0;
        private long executionTotalTicks = 0;
        private long highExecutionMilliseconds = 0;
        private long highExecutionTicks = 0;
        private long lowExecutionMilliseconds = long.MaxValue;
        private long lowExecutionTicks = long.MaxValue;
        private string lastFileNameCreated = String.Empty;
        private bool autoexec = false;
        private string[] args;

        public Form1()
        {
            InitializeComponent();

            args = Environment.GetCommandLineArgs();
            if ((args != null) && (args.Length > 1))
            {
                autoexec = true;
                btnBrowse_Click(this, new EventArgs());
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (autoexec)
            {
                txtInputFile.Text = args[1];
                openFileDialog1.FileName = txtInputFile.Text;
            }
            else
            {
                if (String.IsNullOrEmpty(txtInputFile.Text))
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    openFileDialog1.InitialDirectory = Path.GetDirectoryName(assembly.Location);
                }
                else
                    openFileDialog1.InitialDirectory = Path.GetDirectoryName(txtInputFile.Text);

                openFileDialog1.FileName = "testdata.txt";
                openFileDialog1.Title = "Select input data file";
                openFileDialog1.Multiselect = false;
            }

            if ((autoexec) || (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                txtInputFile.Text = openFileDialog1.FileName;
                txtErrorMsgs.Text = String.Empty;
                lnkRunV1.Visible = true;
            }
            else
            {
                lnkRunV1.Visible = false;
            }
            Application.DoEvents();
        }

        private void RunExecutor(IExecute executor)
        {
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            try
            {
                ExecuteResults results = executor.Execute(txtInputFile.Text);
                UpdateExecutionStats(results, executor);
                DisplayResultsMessages(errorMessages, results);
                lastFileNameCreated = executor.ResultsFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred while running vote counter:\r\n" + ex.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = oldCursor;
                Application.DoEvents();
            }
        }

        private void DisplayResultsMessages(StringBuilder sb, ExecuteResults resultsOfLastRun)
        {
            if (!String.IsNullOrEmpty(resultsOfLastRun.ErrorMessage))
                sb.AppendLine(resultsOfLastRun.ErrorMessage);
            txtErrorMsgs.Text = sb.ToString();
            txtErrorMsgs.Visible = (!String.IsNullOrEmpty(txtErrorMsgs.Text));
        }

        private void lnkRunV1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtInputFile.Text))
            {
                for (int run = 0; run < 10; run++)
                {
                    v1.Executor executor = new v1.Executor();
                    RunExecutor(executor);
                }  // next run
            }
        }

        private void lnkResetStats_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            executionCount = 0;
            executionTotalMilliseconds = 0;
            executionTotalTicks = 0;
            highExecutionMilliseconds = 0;
            highExecutionTicks = 0;
            lowExecutionMilliseconds = long.MaxValue;
            lowExecutionTicks = long.MaxValue;

            UpdateStatsControls(null);
        }

        private void UpdateExecutionStats(ExecuteResults resultsOfLastRun, IExecute executor)
        {
            executionCount++;
            executionTotalMilliseconds += resultsOfLastRun.ExecutionTimeMilliseconds;
            executionTotalTicks += resultsOfLastRun.ExecutionTimeTicks;
            
            lblExecLastMS.Text = resultsOfLastRun.ExecutionTimeMilliseconds.ToString();
            lblExecLastTicks.Text = resultsOfLastRun.ExecutionTimeTicks.ToString();
            
            if (resultsOfLastRun.ExecutionTimeMilliseconds < lowExecutionMilliseconds)
                lowExecutionMilliseconds = resultsOfLastRun.ExecutionTimeMilliseconds;
            
            if (resultsOfLastRun.ExecutionTimeTicks < lowExecutionTicks)
                lowExecutionTicks = resultsOfLastRun.ExecutionTimeTicks;
            
            if (resultsOfLastRun.ExecutionTimeMilliseconds > highExecutionMilliseconds)
                highExecutionMilliseconds = resultsOfLastRun.ExecutionTimeMilliseconds;
            
            if (resultsOfLastRun.ExecutionTimeTicks > highExecutionTicks)
                highExecutionTicks = resultsOfLastRun.ExecutionTimeTicks;

            UpdateStatsControls(executor);
        }

        private void UpdateStatsControls(IExecute executor)
        {
            if (executionCount == 0)
            {
                lblExecLowMS.Text = "";
                lblExecLowTicks.Text = "";
                lblExecHighMS.Text = "";
                lblExecHighTicks.Text = "";
                lblExecAvgMS.Text = "";
                lblExecAvgTicks.Text = "";
                lblExecLastMS.Text = "";
                lblExecLastTicks.Text = "";
                txtErrorMsgs.Text = "";
            }
            else
            {
                lblExecLowMS.Text = lowExecutionMilliseconds.ToString();
                lblExecLowTicks.Text = lowExecutionTicks.ToString();
                lblExecHighMS.Text = highExecutionMilliseconds.ToString();
                lblExecHighTicks.Text = highExecutionTicks.ToString();
                lblExecAvgMS.Text = (executionTotalMilliseconds / executionCount).ToString();
                lblExecAvgTicks.Text = (executionTotalTicks / executionCount).ToString();
            }
            //
            grpStatistics.Visible = true;

            Application.DoEvents();
        }

        private void lnkViewFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process notePad = new Process();
                notePad.StartInfo.FileName = "notepad.exe";
                notePad.StartInfo.Arguments = lastFileNameCreated;
                notePad.Start();
            }
            catch (Exception ex)
            {
                // ignore this error - the editor may not have created an output file
            }
        }
 
    }
}
