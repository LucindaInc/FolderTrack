using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Threading;
using System.Drawing;
using FolderTrackGuiTest1.CommonGuiFunctions;

namespace FolderTrackGuiTest1.MonitorGroupChoser
{
    class EMonitorGroupRow : Panel, PanelList<MonitorGroupInfo>.PanelFunction
    {

        string name;
        FTObjects m_ftObjects;
        MainForm mfor;
        MonitorGroupInfo mg;
        bool Error = false;
        private delegate void VoidObjDelegate(object ob);

        public EMonitorGroupRow(MonitorGroupInfo mg, FTObjects ftob, MainForm mfm)
        {
            this.mg = mg;
            this.mfor = mfm;
            Error = mg.error;
            InitializeComponent();
            if (mg.monitoring == false)
            {
                this.StopButton.Visible = false;
                this.RestartButton.Visible = true;
            }
            else
            {
                this.StopButton.Visible = true;
                this.RestartButton.Visible = false;
            }

            if (Error == false)
            {
                this.Namelabel.Text = mg.name;
                string[] listo = new string[mg.Loca.Count];
                mg.Loca.CopyTo(listo, 0);
                this.LocationsListBox.Items.AddRange(listo);
            }
            else
            {
                this.Namelabel.Text = "\"" + name + "\"" + " has a problem!";
                this.Errorlabel.Text = GuiFunctions.ErrorMonitorGroupText();
            }
            
            
            this.ResizeRedraw = true;
            m_ftObjects = ftob;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            new Thread(HandleStopCli).Start();
        }

        private void HandleStopCli()
        {
            ConfirmDeleteOrStop con = new ConfirmDeleteOrStop(ConfirmDeleteOrStop.CONFTY.STOP);
            DialogResult di = con.ShowDialog();
            if (di == DialogResult.OK)
            {
                m_ftObjects.StopMonitoringGroup(mg.name);
                mfor.HandleMonitorGroStp(mg);
                
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            new Thread(HandleRestartCl).Start();
            
        }

        private void HandleRestartCl()
        {
            m_ftObjects.RestartMonitoringGroup(mg.name);
            mfor.HandleMonitorGroRes(mg);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            new Thread(HandleDeleteCli).Start();
        }

        private void HandleDeleteCli()
        {
            
            ConfirmDeleteOrStop con = new ConfirmDeleteOrStop(ConfirmDeleteOrStop.CONFTY.DELETE);
            DialogResult di = con.ShowDialog();
            if (di == DialogResult.OK)
            {
                m_ftObjects.DeletMonitoringGroup(mg.name);
                mfor.HandleMonitorGroDel(mg);
            }
        }

        #region PanelFunction Members

        public void DoFunction(object data)
        {
            if (this.StopButton.InvokeRequired)
            {
                this.StopButton.Invoke(new VoidObjDelegate(DoFunction),new object[] { data });
                return;
            }

            if (data is Boolean)
            {
                bool st = (bool)data;
                if (st == true)
                {
                    this.StopButton.Visible = false;
                    this.RestartButton.Visible = true;
                }
                else
                {
                    this.StopButton.Visible = true;
                    this.RestartButton.Visible = false;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pe = new Pen(Color.BurlyWood, 7);
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(System.Drawing.SystemColors.Control);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(11, 10);
            pt[1] = new Point(this.Width - 12, 10);
            pt[2] = new Point(this.Width - 12, this.Height - 7 );
            pt[3] = new Point(11, this.Height - 7 );
            pt[4] = new Point(11, 10);

            e.Graphics.DrawLines(pe, pt);
        }




        #endregion


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
            this.Namelabel = new System.Windows.Forms.Label();
            this.LocationsListBox = new System.Windows.Forms.ListBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.Errorlabel = new System.Windows.Forms.Label();
            this.RestartButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Cornsilk;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Namelabel, 0, 0);
            if (Error == false)
            {
                this.tableLayoutPanel1.Controls.Add(this.LocationsListBox, 0, 1);
                this.tableLayoutPanel1.Controls.Add(this.DeleteButton, 1, 2);
                this.tableLayoutPanel1.Controls.Add(this.StopButton, 0, 2);
                this.tableLayoutPanel1.Controls.Add(this.RestartButton, 0, 2);

            }
            else
            {
                this.tableLayoutPanel1.Controls.Add(this.Errorlabel, 0, 1);
                this.tableLayoutPanel1.Controls.Add(this.DeleteButton, 0, 2);
            }
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(393, 130);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Namelabel
            // 
            this.Namelabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Namelabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.Namelabel, 2);
            this.Namelabel.Location = new System.Drawing.Point(179, 3);
            this.Namelabel.Name = "Namelabel";
            this.Namelabel.Size = new System.Drawing.Size(35, 13);
            this.Namelabel.TabIndex = 0;
            this.Namelabel.Text = "label1";
            if (Error)
            {
                this.Namelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.Namelabel.ForeColor = System.Drawing.Color.White;
            }
            // 
            // Errorlabel
            // 
            this.Errorlabel.Dock = DockStyle.Fill;
            this.Errorlabel.AutoSize = true;
            this.Errorlabel.Location = new System.Drawing.Point(180, 3);
            this.Errorlabel.Name = "Errorlabel";
            this.Errorlabel.Size = new System.Drawing.Size(35, 13);
            this.Errorlabel.TabIndex = 1;
            this.Errorlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Errorlabel.ForeColor = System.Drawing.Color.White;
            this.Errorlabel.Text = "label1";
            // 
            // LocationsListBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.LocationsListBox, 2);
            this.LocationsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocationsListBox.FormattingEnabled = true;
            this.LocationsListBox.Location = new System.Drawing.Point(3, 23);
            this.LocationsListBox.Name = "LocationsListBox";
            this.LocationsListBox.Size = new System.Drawing.Size(387, 69);
            this.LocationsListBox.TabIndex = 1;
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.Color.BurlyWood;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.Location = new System.Drawing.Point(199, 104);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 3;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopButton.BackColor = System.Drawing.Color.BurlyWood;
            this.StopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StopButton.Location = new System.Drawing.Point(118, 104);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // RestartButton
            // 
            this.RestartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RestartButton.BackColor = System.Drawing.Color.BurlyWood;
            this.RestartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RestartButton.Location = new System.Drawing.Point(118, 104);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(75, 23);
            this.RestartButton.TabIndex = 4;
            this.RestartButton.Text = "Restart";
            this.RestartButton.UseVisualStyleBackColor = false;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // panel1
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(14, 12);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(421, 152);
            this.TabIndex = 2;
            this.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
            // 
            // EMonitorGroupDesign
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Namelabel;
        private System.Windows.Forms.Label Errorlabel;
        private System.Windows.Forms.ListBox LocationsListBox;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button RestartButton;

        
    }
}
