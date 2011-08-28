using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using FolderTrack.Types;
using System.Drawing;
using FolderTrackGuiTest1.CommonGuiFunctions;

namespace FolderTrackGuiTest1.MonitorGroupChoser
{
    class MonitorGroupRow : Panel
    {

        string name;
        MainForm mainfo;
        bool Error = false;
        MonitorGroupInfo m_mg;

        public MonitorGroupRow(MonitorGroupInfo mg, MainForm mainfor)
        {

            this.name = mg.name;
            this.m_mg = mg;
            Error = mg.error;
            InitializeComponent();
            this.mainfo = mainfor;
            this.ResizeRedraw = true;
            if (Error == false)
            {
                this.Namelabel.Text = name;
                string[] listo = new string[mg.Loca.Count];
                mg.Loca.CopyTo(listo, 0);
                this.LocationsListBox.Items.AddRange(listo);
                
            }
            else
            {

                this.Namelabel.Text = "\""+name+ "\"" + " has a problem!";
                this.Errorlabel.Text = GuiFunctions.ErrorMonitorGroupText();
                
            }
            
        }


        private void OkBbutton_Click(object sender, EventArgs e)
        {
            new Thread(ExploreVersion).Start();
        }

        void DeleteButton_Click(object sender, EventArgs e)
        {
            new Thread(HandleDeleteCli).Start();
        }

        private void HandleDeleteCli()
        {

            ConfirmDeleteOrStop con = new ConfirmDeleteOrStop(ConfirmDeleteOrStop.CONFTY.DELETE);
            DialogResult di = con.ShowDialog();
            if (di == DialogResult.OK)
            {
                mainfo.DeletMonitoringGroup(m_mg.name);
                mainfo.HandleMonitorGroDelOpen(m_mg);
            }
        }

        private void ExploreVersion()
        {
            mainfo.OpenMonitorGroup(name);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pe = new Pen(Color.BurlyWood, 7);
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            
            e.Graphics.Clear(System.Drawing.SystemColors.Control);
            
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(15, 12);
            pt[1] = new Point(this.Width - 15, 12);
            pt[2] = new Point(this.Width - 15, this.Height - 12);
            pt[3] = new Point(15, this.Height - 12);
            pt[4] = new Point(15, 12);

            e.Graphics.DrawLines(pe, pt);
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
            this.Errorlabel = new System.Windows.Forms.Label();
            this.LocationsListBox = new System.Windows.Forms.ListBox();
            this.OkBbutton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(49, 61);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(425, 154);
            this.TabIndex = 0;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            if (Error == false)
            {
                this.tableLayoutPanel1.BackColor = System.Drawing.Color.Cornsilk;
                this.tableLayoutPanel1.Controls.Add(this.Namelabel, 0, 0);
                this.tableLayoutPanel1.Controls.Add(this.LocationsListBox, 0, 1);
                this.tableLayoutPanel1.Controls.Add(this.OkBbutton, 0, 2);
            }
            else
            {
                this.tableLayoutPanel1.BackColor = System.Drawing.Color.LightCoral;
                this.tableLayoutPanel1.Controls.Add(this.Namelabel, 0, 0);
                this.tableLayoutPanel1.Controls.Add(this.Errorlabel, 0, 1);
                this.tableLayoutPanel1.Controls.Add(this.DeleteButton, 0, 2);
            }
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            if (Error == false)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            }
            else
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            }
            this.tableLayoutPanel1.Size = new System.Drawing.Size(395, 130);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Namelabel
            // 
            this.Namelabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Namelabel.AutoSize = true;
            this.Namelabel.Location = new System.Drawing.Point(180, 3);
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
            this.LocationsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocationsListBox.FormattingEnabled = true;
            this.LocationsListBox.Location = new System.Drawing.Point(3, 23);
            this.LocationsListBox.Name = "LocationsListBox";
            this.LocationsListBox.Size = new System.Drawing.Size(389, 69);
            this.LocationsListBox.TabIndex = 1;
            // 
            // OkBbutton
            // 
            this.OkBbutton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OkBbutton.BackColor = System.Drawing.Color.BurlyWood;
            this.OkBbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OkBbutton.Location = new System.Drawing.Point(160, 104);
            this.OkBbutton.Name = "OkBbutton";
            this.OkBbutton.Size = new System.Drawing.Size(75, 23);
            this.OkBbutton.TabIndex = 2;
            this.OkBbutton.Text = "Open";
            this.OkBbutton.UseVisualStyleBackColor = false;
            this.OkBbutton.Click += new System.EventHandler(this.OkBbutton_Click);
            // 
            // Delete Button
            // 
            this.DeleteButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DeleteButton.BackColor = System.Drawing.Color.BurlyWood;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.Location = new System.Drawing.Point(160, 104);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new EventHandler(DeleteButton_Click);
            // 
            // MontiroGroupDesign
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Namelabel;
        private System.Windows.Forms.Label Errorlabel;
        private System.Windows.Forms.ListBox LocationsListBox;
        private System.Windows.Forms.Button OkBbutton;
        private System.Windows.Forms.Button DeleteButton;
    }
}
