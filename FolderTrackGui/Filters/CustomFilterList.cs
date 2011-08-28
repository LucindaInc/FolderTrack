using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using FolderTrack.Types;

namespace FolderTrackGuiTest1.Filters
{
    class CustomFilterList : Panel
    {
        PanelList<FilterObject> ExcluPanLis;
        FilterRowFromFilterObject fromM;
        private delegate void VoidObject(Object Rule);
        List<FilterObject> CusL;
        List<string> monitor;
        List<string> filterString;

        public CustomFilterList()
        {
            InitializeComponent();
            
        }

        public void SetMonitor(List<string> monitor, List<string> filterString)
        {
            this.monitor = monitor;
            this.filterString = filterString;
        }

        public void SetCus(List<FilterObject> LRules)
        {
          //  new Thread(new ParameterizedThreadStart(SetCustomNTre)).Start(LRules);
            SetCustomNTre(LRules);
        }

        private void SetCustomNTre(Object CusO)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidObject(SetCustomNTre), new object[] { CusO });
                return;
            }
            CusL = (List<FilterObject>)CusO;
            ExcluPanLis = new PanelList<FilterObject>();
            this.SuspendLayout();
            this.Controls.Add(ExcluPanLis);
            ExcluPanLis.Dock = DockStyle.Fill;
            ExcluPanLis.Size = this.Size;
            fromM = new FilterRowFromFilterObject();
            fromM.monitor = monitor;
            fromM.FilterStrings = filterString;
            ExcluPanLis.PanelFromData = fromM;

            
            ExcluPanLis.AddData(CusL);
            this.ResumeLayout(false);
        }

        public void AddFil(Object filtO)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidObject(AddFil), new object[] { filtO });
                return;
            }
            List<FilterObject> filt = (List<FilterObject>)filtO;
            this.SuspendLayout();
            this.Controls.Add(ExcluPanLis);
            ExcluPanLis.AddData(filt);
            CusL.AddRange(filt);
            this.ResumeLayout(false);
        }

        public FilterChangeOb GetLists()
        {
            return getFilterObFrom(CusL);
        }

        public static FilterChangeOb getFilterObFrom(List<FilterObject> CusL)
        {
            FilterChangeOb filCg = new FilterChangeOb();

            filCg.addFilfer = new List<string>();
            filCg.removeFilter = new List<string>();

            foreach (FilterObject fil in CusL)
            {
                if (fil.use)
                {
                    filCg.addFilfer.Add(fil.filter);
                }
                else
                {
                    if (fil.mode == FilterObject.FilterObjectMode.KEEP)
                    {
                        filCg.removeFilter.Add(fil.filter);
                    }
                }
            }
            return filCg;
        }

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
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.BackColor = System.Drawing.Color.Linen;
            this.Location = new System.Drawing.Point(37, 55);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(223, 113);
            this.TabIndex = 0;
            // 
            // CustomFilterListDesignerForm
            // 
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
    }
}
