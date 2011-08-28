using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace FolderTrackGuiTest1.UserVersionDialog
{
    public class StopUserVersionRow : Panel, PanelList<String>.PanelFunction
    {



        private System.Windows.Forms.Label UserVersionLabel;
        private System.Windows.Forms.Button RemoveNewButton;
        private System.Windows.Forms.Button LastButton;
        private System.Windows.Forms.Panel UserVersionPanel;

        private UserVersionForm m_UserVersionForm;
        private string m_UserVersionName;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public StopUserVersionRow()
        {

        }

        public StopUserVersionRow(String UserVersionName, UserVersionForm userversionfrom)
        {
            InitializeComponent();
            m_UserVersionName = UserVersionName;
            this.UserVersionLabel.Text = UserVersionName;
            this.m_UserVersionForm = userversionfrom;
            this.LastButton.Click += new EventHandler(LastButton_Click);
            this.RemoveNewButton.Click += new EventHandler(RemoveNewButton_Click);
            
        }

        void RemoveNewButton_Click(object sender, EventArgs e)
        {
            m_UserVersionForm.RemoveVersionNow(m_UserVersionName);
        }

        void LastButton_Click(object sender, EventArgs e)
        {
            m_UserVersionForm.SetLastUserVersion(m_UserVersionName);
        }

        

        public void SetAsLast()
        {
            this.UserVersionPanel.BackColor = System.Drawing.Color.PaleVioletRed;
            this.LastButton.Text = "Continue";
        }

        public void UnSetAsLast()
        {
            this.UserVersionPanel.BackColor = System.Drawing.Color.LightBlue;
            this.LastButton.Text = "Last Use";
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            Pen pe = new Pen(Color.BurlyWood, 17);
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(Color.Peru);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(9, 9);
            pt[1] = new Point(this.Width - 9, 9);
            pt[2] = new Point(this.Width - 9, this.Height - 9);
            pt[3] = new Point(9, this.Height - 9);
            pt[4] = new Point(9, 9);

            e.Graphics.DrawLines(pe, pt);
        }



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
            this.UserVersionPanel = new System.Windows.Forms.Panel();
            this.UserVersionLabel = new System.Windows.Forms.Label();
            this.RemoveNewButton = new System.Windows.Forms.Button();
            this.LastButton = new System.Windows.Forms.Button();
            this.UserVersionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserVersionPanel
            // 
            this.UserVersionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UserVersionPanel.BackColor = System.Drawing.Color.LightBlue;
            this.UserVersionPanel.Controls.Add(this.UserVersionLabel);
            this.UserVersionPanel.Controls.Add(this.RemoveNewButton);
            this.UserVersionPanel.Controls.Add(this.LastButton);
            this.UserVersionPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UserVersionPanel.Location = new System.Drawing.Point(10, 7);
            this.UserVersionPanel.Name = "UserVersionPanel";
            this.UserVersionPanel.Size = new System.Drawing.Size(300, 36);
            this.UserVersionPanel.TabIndex = 0;
            // 
            // UserVersionLabel
            // 
            this.UserVersionLabel.AutoSize = true;
            this.UserVersionLabel.Location = new System.Drawing.Point(15, 12);
            this.UserVersionLabel.Name = "UserVersionLabel";
            this.UserVersionLabel.Size = new System.Drawing.Size(35, 13);
            this.UserVersionLabel.TabIndex = 2;
            this.UserVersionLabel.Text = "label1";
            // 
            // RemoveNewButton
            // 
            this.RemoveNewButton.Location = new System.Drawing.Point(202, 6);
            this.RemoveNewButton.Name = "RemoveNewButton";
            this.RemoveNewButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveNewButton.TabIndex = 1;
            this.RemoveNewButton.Text = "Remove Now";
            this.RemoveNewButton.UseVisualStyleBackColor = true;
            // 
            // LastButton
            // 
            this.LastButton.Location = new System.Drawing.Point(114, 6);
            this.LastButton.Name = "LastButton";
            this.LastButton.Size = new System.Drawing.Size(82, 23);
            this.LastButton.TabIndex = 0;
            this.LastButton.Text = "Last Use";
            this.LastButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.UserVersionPanel);
            this.Location = new System.Drawing.Point(22, 156);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(320, 50);
            this.TabIndex = 1;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this_Paint);
            // 
            // StopUserVersionRowDesignerForm
            // 
            this.UserVersionPanel.ResumeLayout(false);
            this.UserVersionPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion





        #region PanelFunction Members

        public void DoFunction(object data)
        {
            if (data is Boolean)
            {
                if (((Boolean)data).Equals(true))
                {
                    SetAsLast();
                }
                else
                {
                    UnSetAsLast();
                }
            }
        }

        #endregion
    }
}
