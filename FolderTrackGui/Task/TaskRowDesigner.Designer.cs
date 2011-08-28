namespace FolderTrackGuiTest1.Task
{
    partial class TaskRowDesigner
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DetailLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.DetailDiscripLabel = new System.Windows.Forms.Label();
            this.PercentCompleteProgressBar = new System.Windows.Forms.ProgressBar();
            this.ActionDiscripLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(134, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 145);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.DetailDiscripLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.PercentCompleteProgressBar, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ActionDiscripLabel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 117);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.DetailLabel);
            this.panel3.Location = new System.Drawing.Point(43, 45);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(373, 45);
            this.panel3.TabIndex = 5;
            // 
            // DetailLabel
            // 
            this.DetailLabel.AutoSize = true;
            this.DetailLabel.BackColor = System.Drawing.Color.Silver;
            this.DetailLabel.ForeColor = System.Drawing.Color.White;
            this.DetailLabel.Location = new System.Drawing.Point(3, 0);
            this.DetailLabel.Name = "DetailLabel";
            this.DetailLabel.Size = new System.Drawing.Size(35, 13);
            this.DetailLabel.TabIndex = 4;
            this.DetailLabel.Text = "label1";
            this.DetailLabel.Click += new System.EventHandler(this.DetailLabel_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.ActionLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(43, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(373, 45);
            this.panel2.TabIndex = 2;
            // 
            // ActionLabel
            // 
            this.ActionLabel.AutoSize = true;
            this.ActionLabel.ForeColor = System.Drawing.Color.White;
            this.ActionLabel.Location = new System.Drawing.Point(0, 0);
            this.ActionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(35, 13);
            this.ActionLabel.TabIndex = 1;
            this.ActionLabel.Text = "label1";
            // 
            // DetailDiscripLabel
            // 
            this.DetailDiscripLabel.AutoSize = true;
            this.DetailDiscripLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailDiscripLabel.ForeColor = System.Drawing.Color.White;
            this.DetailDiscripLabel.Location = new System.Drawing.Point(3, 45);
            this.DetailDiscripLabel.Name = "DetailDiscripLabel";
            this.DetailDiscripLabel.Size = new System.Drawing.Size(34, 13);
            this.DetailDiscripLabel.TabIndex = 2;
            this.DetailDiscripLabel.Text = "Detail";
            // 
            // PercentCompleteProgressBar
            // 
            this.PercentCompleteProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.SetColumnSpan(this.PercentCompleteProgressBar, 2);
            this.PercentCompleteProgressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PercentCompleteProgressBar.Location = new System.Drawing.Point(3, 93);
            this.PercentCompleteProgressBar.Name = "PercentCompleteProgressBar";
            this.PercentCompleteProgressBar.Size = new System.Drawing.Size(410, 19);
            this.PercentCompleteProgressBar.TabIndex = 3;
            // 
            // ActionDiscripLabel
            // 
            this.ActionDiscripLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ActionDiscripLabel.AutoSize = true;
            this.ActionDiscripLabel.ForeColor = System.Drawing.Color.White;
            this.ActionDiscripLabel.Location = new System.Drawing.Point(3, 0);
            this.ActionDiscripLabel.Name = "ActionDiscripLabel";
            this.ActionDiscripLabel.Size = new System.Drawing.Size(37, 13);
            this.ActionDiscripLabel.TabIndex = 0;
            this.ActionDiscripLabel.Text = "Action";
            // 
            // TaskRowDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 266);
            this.Controls.Add(this.panel1);
            this.Name = "TaskRowDesigner";
            this.Text = "TaskRowDesigner";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label ActionDiscripLabel;
        private System.Windows.Forms.Label ActionLabel;
        private System.Windows.Forms.Label DetailDiscripLabel;
        private System.Windows.Forms.ProgressBar PercentCompleteProgressBar;
        private System.Windows.Forms.Label DetailLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}