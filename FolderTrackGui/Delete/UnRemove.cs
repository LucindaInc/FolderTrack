using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using FolderTrack.Types;
using FolderTrackGuiTest1.AllVersionsTab;

namespace FolderTrackGuiTest1.Delete
{
    public class UnRemove : Panel, Util.MonGroRet
    {

        private FTObjects m_FTObjects;
        PanelList<VersionInfo> UnRempvePanelList;
        private MainForm m_form;
        delegate void VoidNoArgDelegate();

        public UnRemove(MainForm mainform)
        {
            InitializeComponent();
            UnRempvePanelList = new PanelList<VersionInfo>();
            this.tableLayoutPanel1.Controls.Add(UnRempvePanelList,0,1);
            m_form = mainform;
            
        }

        public FTObjects P_FTObjects
        {
            set
            {
                m_FTObjects = value;
                new Thread(tSetPandel).Start();
            }
        }
        private void tSetPandel()
        {
            UnRempvePanelList.Invoke(new VoidNoArgDelegate(iSetPanel));
        }

        private void iSetPanel()
        {
            UnRempvePanelList.PanelFromData = new PanelFromVersionInfo(m_FTObjects, this);
        }

        private void CloseButton2_Click(object sender, EventArgs e)
        {
            m_form.HideRemove();
        }

        private void CloseButton1_Click(object sender, EventArgs e)
        {
            m_form.HideRemove();
        }

        public void RemoveVersion(VersionInfo version)
           {
            UnRempvePanelList.RemoveData(version);
        }
		
        public void CallDelete()
        {
            UnRempvePanelList.ClearAllData();
            m_FTObjects.GetVersionInfoFromMonitor(FolderTrack.Delete.DeleteRules.DeleteReturn.REMOVED, this);
        }

        public void VerInfoCal(List<VersionInfo> data, bool done)
        {
            UnRempvePanelList.AddDataTop(data);
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
            this.CloseButton1 = new System.Windows.Forms.Button();
            this.CloseButton2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.CloseButton1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CloseButton2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(423, 244);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // CloseButton1
            // 
            this.CloseButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CloseButton1.Location = new System.Drawing.Point(174, 3);
            this.CloseButton1.Name = "CloseButton1";
            this.CloseButton1.Size = new System.Drawing.Size(75, 23);
            this.CloseButton1.TabIndex = 0;
            this.CloseButton1.Text = "Close";
            this.CloseButton1.UseVisualStyleBackColor = true;
            this.CloseButton1.Click += new System.EventHandler(this.CloseButton1_Click);
            // 
            // CloseButton2
            // 
            this.CloseButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CloseButton2.Location = new System.Drawing.Point(174, 218);
            this.CloseButton2.Name = "CloseButton2";
            this.CloseButton2.Size = new System.Drawing.Size(75, 23);
            this.CloseButton2.TabIndex = 1;
            this.CloseButton2.Text = "Close";
            this.CloseButton2.UseVisualStyleBackColor = true;
            this.CloseButton2.Click += new System.EventHandler(this.CloseButton2_Click);
            // 
            // Undelete
            // 
            this.Name = "Undelete";
            this.Text = "Undelete";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button CloseButton1;
        private System.Windows.Forms.Button CloseButton2;

    }
}
