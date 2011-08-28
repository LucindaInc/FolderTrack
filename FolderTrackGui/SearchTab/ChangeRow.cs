using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;

namespace FolderTrackGuiTest1.SearchTab
{
    class ChangeRow : Panel
    {
        public SearchPanel pan;
        string ChangeFile;

        public ChangeRow()
        {
            InitializeComponent();
        }

        public ChangeRow(SearchPanel pan, string changefile)
        {
            InitializeComponent();
            this.ChangeFile = changefile;
            this.pan = pan;
            this.ChangeLabel.Text = this.ChangeFile;
        }

        private void ChangeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(handle);
            thread.Start();
        }

        private void handle()
        {
            pan.HandleChangeFileChan(ChangeFile, this.ChangeCheckBox.Checked);
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            Pen pe = new Pen(Color.BurlyWood, 8);
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(Color.Moccasin);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(3, 5);
            pt[1] = new Point(this.Width - 3, 5);
            pt[2] = new Point(this.Width - 3, this.Height - 5);
            pt[3] = new Point(3, this.Height - 5);
            pt[4] = new Point(3, 5);

            e.Graphics.DrawLines(pe, pt);


        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChangeLabel = new System.Windows.Forms.Label();
            this.ChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(47, 67);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(424, 56);
            this.TabIndex = 0;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this_Paint);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Cornsilk;
            this.panel2.Controls.Add(this.ChangeLabel);
            this.panel2.Location = new System.Drawing.Point(35, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(381, 45);
            this.panel2.TabIndex = 1;
            // 
            // ChangeLabel
            // 
            this.ChangeLabel.AutoSize = true;
            this.ChangeLabel.Location = new System.Drawing.Point(3, 7);
            this.ChangeLabel.Name = "ChangeLabel";
            this.ChangeLabel.Size = new System.Drawing.Size(35, 13);
            this.ChangeLabel.TabIndex = 1;
            this.ChangeLabel.Text = "label1";
            // 
            // ChangeCheckBox
            // 
            this.ChangeCheckBox.BackColor = System.Drawing.Color.Cornsilk;
            this.ChangeCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ChangeCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ChangeCheckBox.Location = new System.Drawing.Point(0, 0);
            this.ChangeCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.ChangeCheckBox.Name = "ChangeCheckBox";
            this.ChangeCheckBox.Size = new System.Drawing.Size(35, 45);
            this.ChangeCheckBox.TabIndex = 0;
            this.ChangeCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChangeCheckBox.UseVisualStyleBackColor = false;
            this.ChangeCheckBox.CheckedChanged += new System.EventHandler(this.ChangeCheckBox_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ChangeCheckBox, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 48);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ChangeRowDesigner
            // 
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ChangeCheckBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label ChangeLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
