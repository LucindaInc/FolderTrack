using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.IO;


namespace FolderTrackGuiTest1.Filters
{
    class FolderRow : Panel
    {

        private delegate void VoidFilterObjectDelegate(FilterObject fi);
        private delegate void VoidNoArgDelegate();
        private System.Windows.Forms.TextBox CustText;
        private System.Windows.Forms.Button CustButton;
        private System.Windows.Forms.Label FiltLab;
        private System.Windows.Forms.CheckBox ApplyCheckBox;
        private System.Windows.Forms.Button KeepButton;
        private System.Windows.Forms.TextBox PROText;
        private System.Windows.Forms.Label PROLabel;
        private System.Windows.Forms.Button PROButton;


        FilterObject fi;
        bool init = false;

        public FolderRow(FilterObject fi)
        {
            init = false;
            InitializeComponent();
            this.fi = fi;
            CustText = new TextBox();
            CustButton = new Button();
            FiltLab = new Label();
            if (this.fi.mode == FilterObject.FilterObjectMode.KEEP)
            {
                this.KeepButton = new System.Windows.Forms.Button();
                this.KeepButton.AutoSize = true;
                this.KeepButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                this.KeepButton.BackColor = System.Drawing.Color.Gainsboro;
                this.KeepButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.KeepButton.Location = new System.Drawing.Point(1, 13);
                this.KeepButton.Name = "KeepButton";
                this.KeepButton.Size = new System.Drawing.Size(42, 23);
                this.KeepButton.TabIndex = 4;
                this.KeepButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.KeepButton.BackColor = Color.Transparent;
                this.KeepButton.UseVisualStyleBackColor = false;
                this.KeepButton.Click += new System.EventHandler(this.KeepButton_Click);
                this.panel2.Controls.Add(this.KeepButton);
            }
            else if (this.fi.mode == FilterObject.FilterObjectMode.SELECT)
            {
                this.ApplyCheckBox = new System.Windows.Forms.CheckBox();
                this.ApplyCheckBox.AutoSize = true;
                this.ApplyCheckBox.Location = new System.Drawing.Point(1, 13);
                this.ApplyCheckBox.Name = "ApplyCheckBox";
                this.ApplyCheckBox.Size = new System.Drawing.Size(52, 17);
                this.ApplyCheckBox.TabIndex = 4;
                this.ApplyCheckBox.Font = Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ApplyCheckBox.BackColor = Color.Transparent;
                this.ApplyCheckBox.Text = "Apply";
                this.ApplyCheckBox.UseVisualStyleBackColor = true;
                this.ApplyCheckBox.CheckedChanged += new System.EventHandler(this.ApplyCheckBox_CheckedChanged);
                this.panel2.Controls.Add(this.ApplyCheckBox);
            }
            else if (this.fi.mode == FilterObject.FilterObjectMode.PROFILT)
            {
                this.PROButton = new System.Windows.Forms.Button();
                this.PROButton.AutoSize = true;
                this.PROButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                this.PROButton.BackColor = System.Drawing.Color.Gainsboro;
                this.PROButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.PROButton.Location = new System.Drawing.Point(1, 13);
                this.PROButton.Name = "PROButton";
                this.PROButton.Size = new System.Drawing.Size(42, 23);
                this.PROButton.TabIndex = 4;
                this.PROButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.PROButton.BackColor = Color.Transparent;
                this.PROButton.UseVisualStyleBackColor = false;
                this.PROButton.Click += new EventHandler(PROButton_Click);
                this.panel2.Controls.Add(this.PROButton);
            }

            SetUpFilterObject();
            init = true;
        }

        void PROButton_Click(object sender, EventArgs e)
        {
            fi.use = !fi.use;
            if (init == true)
            {
                new Thread(HandleUse).Start();
            }

        }

        public void SetUpFilterObject()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidNoArgDelegate(SetUpFilterObject));
                return;
            }
            
            if (fi.filter.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
            {
                this.panel2.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources.filterFolder;
                this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            }
            else
            {
                
                this.panel2.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources.filterFile;
                this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            }

            if (fi.Custom)
            {
                if (fi.ShowCustomText || fi.monitor == null)
                {
                    this.TextPanel.Controls.Add(CustText);
                    CustText.Text = fi.filter;
                    CustText.Dock = DockStyle.Fill;
                    CustText.TextChanged += new EventHandler(CustText_TextChanged);
                }
                
            }
            else
            {
                this.TextPanel.Controls.Add(FiltLab);
              //  FiltLab.Dock = DockStyle.Fill;
                FiltLab.Text = fi.filter;
                FiltLab.AutoSize = true;
            }
            if (this.fi.mode == FilterObject.FilterObjectMode.SELECT)
            {
                this.ApplyCheckBox.Checked = fi.use;
            }

            HandleUse();


        }

        private void SetDescription()
        {
            if (this.DiscriptionLabel.InvokeRequired)
            {
                this.DiscriptionLabel.Invoke(new VoidNoArgDelegate(SetDescription));
                return;
            }
            string usr;

            if (fi.use)
            {
                usr = " (using this filter)";
            }
            else
            {
                usr = " (NOT using this filter)";
            }

            this.DiscriptionLabel.Text = fi.discription + usr;
            

            if (fi.filter.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
            {
                if (fi.Custom)
                {
                    this.DiscriptionLabel.Text = fi.discription +" (Folder Filter remove slash at end to make file filter)";
                }
                else if (fi.mode != FilterObject.FilterObjectMode.PROFILT)
                {
                    this.DiscriptionLabel.Text += " (Folder Filter)";
                }

                
            }
            else
            {
                if (fi.Custom)
                {
                    this.DiscriptionLabel.Text = fi.discription + " (File Filter add slash to the end to make folder filter)";
                }
                else if (fi.mode != FilterObject.FilterObjectMode.PROFILT)
                {
                    this.DiscriptionLabel.Text += " (File Filter)";
                }
                
            }

        }


        void CustButton_Click(object sender, EventArgs e)
        {
            
         //   SelectFilterFromMonitorGroup sel = new SelectFilterFromMonitorGroup(fi.monitor,fi.filterStrings);
            
         //   sel.ShowDialog();
       //     if (sel.DialogResult == DialogResult.OK)
       //     {
       //         List<FilterObject> lisfil = sel.GetFilterList();
       //         fi.AddFilterObList(lisfil);
      //      }

        }

        

        void CustText_TextChanged(object sender, EventArgs e)
        {
            fi.filter = CustText.Text;
            if (fi.filter.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
            {
                this.DiscriptionLabel.Text =fi.discription + " (Folder Filter remove slash at end to make file filter)";
                this.panel2.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources.filterFolder;
                this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            }
            else
            {
                this.DiscriptionLabel.Text = fi.discription + " (File Filter add slash to the end to make folder filter)";
                this.panel2.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources.filterFile;
                this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            }
        }

        

        public void HandleUse()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidNoArgDelegate(HandleUse));
                return;
            }
            SetDescription();
            if (fi.mode == FilterObject.FilterObjectMode.KEEP)
            {
                if (fi.use == true)
                {
                    this.KeepButton.Text = "Remove";
                }
                else
                {
                    this.KeepButton.Text = "Keep";
                }
            }
            else if (fi.mode == FilterObject.FilterObjectMode.PROFILT)
            {
                if (fi.use == true)
                {
                    this.PROButton.Text = "Don't Use";
                }
                else
                {
                    this.PROButton.Text = "Use";
                }
            }
            if (fi.use == false)
            {
                
                if (fi.Custom)
                {
                    CustText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    FiltLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
            else
            {
                if (fi.Custom)
                {
                    CustText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    FiltLab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }

            
        }

        private void ApplyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            fi.use = ApplyCheckBox.Checked;
            if (init == true)
            {
                new Thread(HandleUse).Start();
            }
        }

        private void KeepButton_Click(object sender, EventArgs e)
        {
            fi.use = !fi.use;
            if (init == true)
            {
                new Thread(HandleUse).Start();
            }
        }

        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            Pen pe;
            Color clear;

            pe = new Pen(Color.Gainsboro, 7);
            clear = Color.Linen;

            

            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(clear);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(13, 8);
            pt[1] = new Point(this.Width - 13, 8);
            pt[2] = new Point(this.Width - 13, this.Height - 8);
            pt[3] = new Point(13, this.Height - 8);
            pt[4] = new Point(13, 8);

            e.Graphics.DrawLines(pe, pt);
        }




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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.DiscriptionLabel = new System.Windows.Forms.Label();
            this.TextPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(32, 39);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(408, 80);
            this.TabIndex = 0;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Bisque;
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.Controls.Add(this.DiscriptionLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TextPanel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(383, 68);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // DiscriptionLabel
            // 
            this.DiscriptionLabel.AutoSize = true;
            this.DiscriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscriptionLabel.Location = new System.Drawing.Point(3, 0);
            this.DiscriptionLabel.Name = "DiscriptionLabel";
            this.DiscriptionLabel.Size = new System.Drawing.Size(41, 13);
            this.DiscriptionLabel.TabIndex = 1;
            this.DiscriptionLabel.Text = "label1";
            // 
            // TextPanel
            // 
            this.TextPanel.AutoScroll = true;
            this.TextPanel.BackColor = System.Drawing.Color.Transparent;
            this.TextPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TextPanel.Location = new System.Drawing.Point(0, 13);
            this.TextPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TextPanel.Name = "TextPanel";
            this.TextPanel.Size = new System.Drawing.Size(293, 50);
            this.TextPanel.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources.filterFile;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(293, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 2);
            this.panel2.Size = new System.Drawing.Size(90, 90);
            this.panel2.TabIndex = 4;
            // 
            // FolderRowDesigner
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label DiscriptionLabel;
        private System.Windows.Forms.Panel TextPanel;
        private System.Windows.Forms.Panel panel2;
    }
}
