namespace AussieVoting
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtErrorMsgs = new System.Windows.Forms.TextBox();
            this.grpStatistics = new System.Windows.Forms.GroupBox();
            this.lnkViewFile = new System.Windows.Forms.LinkLabel();
            this.lblExecAvgTicks = new System.Windows.Forms.Label();
            this.lblExecAvgMS = new System.Windows.Forms.Label();
            this.lblExecLowTicks = new System.Windows.Forms.Label();
            this.lblExecLowMS = new System.Windows.Forms.Label();
            this.lblExecHighTicks = new System.Windows.Forms.Label();
            this.lblExecHighMS = new System.Windows.Forms.Label();
            this.lblExecLastTicks = new System.Windows.Forms.Label();
            this.lblExecLastMS = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lnkResetStats = new System.Windows.Forms.LinkLabel();
            this.lnkRunV1 = new System.Windows.Forms.LinkLabel();
            this.grpStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input file:";
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(68, 17);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(555, 20);
            this.txtInputFile.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(629, 15);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(37, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtErrorMsgs
            // 
            this.txtErrorMsgs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtErrorMsgs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtErrorMsgs.ForeColor = System.Drawing.Color.Firebrick;
            this.txtErrorMsgs.Location = new System.Drawing.Point(385, 130);
            this.txtErrorMsgs.MaxLength = 1000000000;
            this.txtErrorMsgs.Multiline = true;
            this.txtErrorMsgs.Name = "txtErrorMsgs";
            this.txtErrorMsgs.ReadOnly = true;
            this.txtErrorMsgs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtErrorMsgs.Size = new System.Drawing.Size(281, 413);
            this.txtErrorMsgs.TabIndex = 4;
            this.txtErrorMsgs.TabStop = false;
            this.txtErrorMsgs.Visible = false;
            // 
            // grpStatistics
            // 
            this.grpStatistics.Controls.Add(this.lnkViewFile);
            this.grpStatistics.Controls.Add(this.lblExecAvgTicks);
            this.grpStatistics.Controls.Add(this.lblExecAvgMS);
            this.grpStatistics.Controls.Add(this.lblExecLowTicks);
            this.grpStatistics.Controls.Add(this.lblExecLowMS);
            this.grpStatistics.Controls.Add(this.lblExecHighTicks);
            this.grpStatistics.Controls.Add(this.lblExecHighMS);
            this.grpStatistics.Controls.Add(this.lblExecLastTicks);
            this.grpStatistics.Controls.Add(this.lblExecLastMS);
            this.grpStatistics.Controls.Add(this.label7);
            this.grpStatistics.Controls.Add(this.label6);
            this.grpStatistics.Controls.Add(this.label5);
            this.grpStatistics.Controls.Add(this.label4);
            this.grpStatistics.Controls.Add(this.label3);
            this.grpStatistics.Controls.Add(this.label2);
            this.grpStatistics.Controls.Add(this.lnkResetStats);
            this.grpStatistics.Location = new System.Drawing.Point(12, 120);
            this.grpStatistics.Name = "grpStatistics";
            this.grpStatistics.Size = new System.Drawing.Size(346, 270);
            this.grpStatistics.TabIndex = 5;
            this.grpStatistics.TabStop = false;
            this.grpStatistics.Text = "Execution statistics";
            this.grpStatistics.Visible = false;
            // 
            // lnkViewFile
            // 
            this.lnkViewFile.AutoSize = true;
            this.lnkViewFile.Location = new System.Drawing.Point(158, 31);
            this.lnkViewFile.Name = "lnkViewFile";
            this.lnkViewFile.Size = new System.Drawing.Size(137, 13);
            this.lnkViewFile.TabIndex = 15;
            this.lnkViewFile.TabStop = true;
            this.lnkViewFile.Text = "View last output file created";
            this.lnkViewFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkViewFile_LinkClicked);
            // 
            // lblExecAvgTicks
            // 
            this.lblExecAvgTicks.Location = new System.Drawing.Point(220, 244);
            this.lblExecAvgTicks.Name = "lblExecAvgTicks";
            this.lblExecAvgTicks.Size = new System.Drawing.Size(100, 23);
            this.lblExecAvgTicks.TabIndex = 14;
            this.lblExecAvgTicks.Text = "label8";
            this.lblExecAvgTicks.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExecAvgMS
            // 
            this.lblExecAvgMS.Location = new System.Drawing.Point(116, 244);
            this.lblExecAvgMS.Name = "lblExecAvgMS";
            this.lblExecAvgMS.Size = new System.Drawing.Size(77, 23);
            this.lblExecAvgMS.TabIndex = 13;
            this.lblExecAvgMS.Text = "label8";
            this.lblExecAvgMS.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExecLowTicks
            // 
            this.lblExecLowTicks.Location = new System.Drawing.Point(220, 202);
            this.lblExecLowTicks.Name = "lblExecLowTicks";
            this.lblExecLowTicks.Size = new System.Drawing.Size(100, 23);
            this.lblExecLowTicks.TabIndex = 12;
            this.lblExecLowTicks.Text = "label8";
            this.lblExecLowTicks.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExecLowMS
            // 
            this.lblExecLowMS.Location = new System.Drawing.Point(116, 202);
            this.lblExecLowMS.Name = "lblExecLowMS";
            this.lblExecLowMS.Size = new System.Drawing.Size(77, 23);
            this.lblExecLowMS.TabIndex = 11;
            this.lblExecLowMS.Text = "label8";
            this.lblExecLowMS.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExecHighTicks
            // 
            this.lblExecHighTicks.Location = new System.Drawing.Point(220, 160);
            this.lblExecHighTicks.Name = "lblExecHighTicks";
            this.lblExecHighTicks.Size = new System.Drawing.Size(100, 23);
            this.lblExecHighTicks.TabIndex = 10;
            this.lblExecHighTicks.Text = "label8";
            this.lblExecHighTicks.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExecHighMS
            // 
            this.lblExecHighMS.Location = new System.Drawing.Point(116, 160);
            this.lblExecHighMS.Name = "lblExecHighMS";
            this.lblExecHighMS.Size = new System.Drawing.Size(77, 23);
            this.lblExecHighMS.TabIndex = 9;
            this.lblExecHighMS.Text = "label8";
            this.lblExecHighMS.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExecLastTicks
            // 
            this.lblExecLastTicks.Location = new System.Drawing.Point(220, 118);
            this.lblExecLastTicks.Name = "lblExecLastTicks";
            this.lblExecLastTicks.Size = new System.Drawing.Size(100, 23);
            this.lblExecLastTicks.TabIndex = 8;
            this.lblExecLastTicks.Text = "label8";
            this.lblExecLastTicks.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExecLastMS
            // 
            this.lblExecLastMS.Location = new System.Drawing.Point(116, 118);
            this.lblExecLastMS.Name = "lblExecLastMS";
            this.lblExecLastMS.Size = new System.Drawing.Size(77, 23);
            this.lblExecLastMS.TabIndex = 7;
            this.lblExecLastMS.Text = "label8";
            this.lblExecLastMS.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(220, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 32);
            this.label7.TabIndex = 6;
            this.label7.Text = "EXEC TIME (TICKS)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(116, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 32);
            this.label6.TabIndex = 5;
            this.label6.Text = "EXEC TIME (MS)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "AVERAGE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "LOW";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "HIGH";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "LAST RUN";
            // 
            // lnkResetStats
            // 
            this.lnkResetStats.AutoSize = true;
            this.lnkResetStats.Location = new System.Drawing.Point(47, 31);
            this.lnkResetStats.Name = "lnkResetStats";
            this.lnkResetStats.Size = new System.Drawing.Size(78, 13);
            this.lnkResetStats.TabIndex = 0;
            this.lnkResetStats.TabStop = true;
            this.lnkResetStats.Text = "Reset statistics";
            this.lnkResetStats.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkResetStats_LinkClicked);
            // 
            // lnkRunV1
            // 
            this.lnkRunV1.AutoSize = true;
            this.lnkRunV1.Location = new System.Drawing.Point(65, 64);
            this.lnkRunV1.Name = "lnkRunV1";
            this.lnkRunV1.Size = new System.Drawing.Size(43, 13);
            this.lnkRunV1.TabIndex = 6;
            this.lnkRunV1.TabStop = true;
            this.lnkRunV1.Text = "Run V1";
            this.lnkRunV1.Visible = false;
            this.lnkRunV1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRunV1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(687, 555);
            this.Controls.Add(this.lnkRunV1);
            this.Controls.Add(this.grpStatistics);
            this.Controls.Add(this.txtErrorMsgs);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Programming Challenge #7   .::.   Aussie Voting";
            this.grpStatistics.ResumeLayout(false);
            this.grpStatistics.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtErrorMsgs;
        private System.Windows.Forms.GroupBox grpStatistics;
        private System.Windows.Forms.LinkLabel lnkResetStats;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblExecAvgTicks;
        private System.Windows.Forms.Label lblExecAvgMS;
        private System.Windows.Forms.Label lblExecLowTicks;
        private System.Windows.Forms.Label lblExecLowMS;
        private System.Windows.Forms.Label lblExecHighTicks;
        private System.Windows.Forms.Label lblExecHighMS;
        private System.Windows.Forms.Label lblExecLastTicks;
        private System.Windows.Forms.Label lblExecLastMS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lnkViewFile;
        private System.Windows.Forms.LinkLabel lnkRunV1;
    }
}

