using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Threading;

namespace FolderTrackGuiTest1.Task
{
    class TaskGroupChoose : Panel
    {


        public delegate void VoidTaskGroupArrDelegate(TaskGroup [] taskarr);

        PanelList<FolderTrack.Types.TaskGroup> TaskGroupPanelList = new PanelList<FolderTrack.Types.TaskGroup>();
        MainForm mfo;

        public TaskGroupChoose(MainForm mfor)
        {
            InitializeComponent();
            this.mfo = mfor;
            TaskGroupPanelList.Dock = DockStyle.Fill;
            TaskGroupPanelList.PanelFromData = new PanelFromTaskGroup(m_FTObjeact);
            this.panel2.Controls.Add(TaskGroupPanelList);
        }

        private FTObjects m_FTObjeact;

        public void SetFTOb(FTObjects ftob)
        {
            this.m_FTObjeact = ftob;
            TaskGroupPanelList.PanelFromData = new PanelFromTaskGroup(m_FTObjeact);
        }

        public TaskGroupChoose(TaskGroup[] TaskArr)
        {
            InitializeComponent();
            TaskGroupPanelList.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(TaskGroupPanelList);
            SetGroup(TaskArr);
        }

        public void SetGroup(TaskGroup[] TaskArr)
        {
            List<FolderTrack.Types.TaskGroup> taskList = new List<FolderTrack.Types.TaskGroup>();
            Dictionary<FolderTrack.Types.TaskGroup, object> TaskGroupTas;
            TaskGroupTas = new Dictionary<TaskGroup, object>();
            foreach (FolderTrack.Types.TaskGroup tas in TaskArr)
            {
                Util.DBug2("TaskGroupChoose", tas.TaskName);
                taskList.Add(tas);
                TaskGroupTas[tas] = tas;
                
            }
            foreach (FolderTrack.Types.TaskGroup gr in taskList)
            {
                foreach (FolderTrack.Types.Task t in gr.TaskList.Values)
                {
                    Util.DBug2("TaskGroupChoose", t.Action + " " + t.Detail + " " + t.percent);
                }
            }
            TaskGroupPanelList.SynchData(taskList);
            TaskGroupPanelList.AddFunctionCallToData(TaskGroupTas);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            new Thread(this.mfo.TaskClosed).Start();

        }




        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(33, 85);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(550, 156);
            this.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(550, 156);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(237, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(544, 121);
            this.panel2.TabIndex = 1;
            // 
            // TaskGroupChooserDesigner
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;

    }
}
