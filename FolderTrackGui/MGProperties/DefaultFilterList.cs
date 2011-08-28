using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.ExclusionRules;
using System.Threading;
using System.Drawing;

namespace FolderTrackGuiTest1.MGProperties
{
    class DefaultFilterList : Panel
    {
        PanelList<GuiInfoMGProperties> ExcluPanLis;
        MGProRowFromMGObject fromM;
        private delegate void VoidObject(List<GuiInfoMGProperties> Rule);

        public DefaultFilterList()
        {
            InitializeComponent();
        }

        public void SetRules(List<GuiInfoMGProperties> LRules)
        {
         //   new Thread(new ParameterizedThreadStart(SetRulesNTre)).Start(LRules);
            SetRulesNTre(LRules);
        }

        private void SetRulesNTre(Object RulesO)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidObject(SetRulesNTre), new object[] { RulesO });
                return;
            }
            List<GuiInfoMGProperties> Rules = (List<GuiInfoMGProperties>)RulesO;
            ExcluPanLis = new PanelList<GuiInfoMGProperties>();
            this.SuspendLayout();
            this.Controls.Add(ExcluPanLis);
            ExcluPanLis.Dock = DockStyle.Fill;
            ExcluPanLis.Size = this.Size;
            fromM = new MGProRowFromMGObject();
            ExcluPanLis.PanelFromData = fromM;

            
            ExcluPanLis.AddData(Rules);
            this.ResumeLayout(false);
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
            this.Location = new System.Drawing.Point(51, 74);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(440, 242);
            this.TabIndex = 0;
            // 
            // DefaultFilterListDesignerForm
            // 
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
    }
}
