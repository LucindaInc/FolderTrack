using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Drawing;
using System.Threading;

namespace FolderTrackGuiTest1.Task
{
    class TaskGroupRow : Panel, PanelList<TaskGroup>.PanelFunction
    {
        PanelList<FolderTrack.Types.Task> TaskPanelList;

        private FTObjects m_FTObjects;

        public class CallButton : Button
        {
            private FTObjects m_FTObjects;
            int TyeNumber;

            public CallButton(FolderTrack.Types.TaskCaller caller, FTObjects ftob)
            {
                this.Text = caller.text;
                this.AutoSize = true;
                this.BackColor = System.Drawing.Color.BurlyWood;
                this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.Location = new System.Drawing.Point(29, 4);
                this.Name = "button1";
                this.Size = new System.Drawing.Size(75, 23);
                this.TabIndex = 0;
                this.UseVisualStyleBackColor = false;
                m_FTObjects = ftob;
                TyeNumber = caller.TyesNumber;
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                try
                {
                    new Thread(Call).Start();
                }
                catch
                {
                    //
                }
            }

            public void Call()
            {
                m_FTObjects.TaskAnswer(TyeNumber);
            }


        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            Pen pe;
            Color clear;

                pe = new Pen(Color.Black, 3);
                clear = Color.White;


            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(clear);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(3, 3);
            pt[1] = new Point(this.Width -3, 3);
            pt[2] = new Point(this.Width -3, this.Height -3);
            pt[3] = new Point(3, this.Height -3);
            pt[4] = new Point(3, 3);

            e.Graphics.DrawLines(pe, pt);
        }

        public TaskGroupRow(TaskGroup group, FTObjects ftobj)
        {
            InitializeComponent();
            this.ResizeRedraw = true;
            this.label1.Text = group.TaskName;
            TaskPanelList = new PanelList<FolderTrack.Types.Task>();
            panel2.Controls.Add(TaskPanelList);
            TaskPanelList.Dock = DockStyle.Fill;
            TaskPanelList.PanelFromData = new PanelFromTask();
            List<FolderTrack.Types.Task> taskList = new List<FolderTrack.Types.Task>();
            int failedTask = 0;
            int compleTask = 0;
            CallButton cabu = null;
            int nextx=-1;
            int nexty=-1;
            m_FTObjects = ftobj;
            if (group.caller != null)
            {
                foreach (FolderTrack.Types.TaskCaller calr in group.caller)
                {
                    if (cabu != null)
                    {
                        nextx = cabu.Location.X + cabu.Size.Width + 10;
                        nexty = cabu.Location.Y;
                    }
                    cabu = new CallButton(calr, m_FTObjects);
                    if (nextx != -1)
                    {
                        cabu.Location = new Point(nextx, nexty);
                    }
                    this.Callpanel.Controls.Add(cabu);
                }
            }

            foreach(FolderTrack.Types.Task tas in group.TaskList.Values)
            {
                if ((tas.percent > 0 && tas.percent < 100 ))
                {
                    taskList.Insert(0, tas);
                    compleTask++;
                }
                else if (tas.Status == FolderTrack.Types.Task.FAILED)
                {
                    taskList.Add(tas);
                    failedTask++;
                }
                else if (tas.Status != FolderTrack.Types.Task.COMPLETE)
                {
                    taskList.Insert(compleTask, tas);
                    
                }
            }
            TaskPanelList.SynchData(taskList);

        }

        #region PanelFunction Members

        public void DoFunction(object data)
        {
            TaskGroup group = (TaskGroup)data;

            foreach(FolderTrack.Types.Task t in group.TaskList.Values)
            {
                Util.DBug2("TaskGroupRow", t.Action + " " + t.Detail + " " + t.percent);
            }


            List<FolderTrack.Types.Task> taskList = new List<FolderTrack.Types.Task>();
            Dictionary<FolderTrack.Types.Task, object> taskTask;
            taskTask = new Dictionary<FolderTrack.Types.Task,object>();
            int failedTask = 0;
            int compleTask = 0;
            foreach (FolderTrack.Types.Task tas in group.TaskList.Values)
            {
                if ((tas.percent > 0 && tas.percent < 100))
                {
                    taskList.Insert(0, tas);
                    compleTask++;
                }
                else if (tas.Status == FolderTrack.Types.Task.FAILED)
                {
                    taskList.Add(tas);
                    failedTask++;
                }
                else if (tas.Status != FolderTrack.Types.Task.COMPLETE)
                {
                    taskList.Insert(compleTask, tas);

                }
                taskTask[tas] = tas;
            }
            TaskPanelList.SynchData(taskList);
            TaskPanelList.AddFunctionCallToData(taskTask);

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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CallerBackPanel = new System.Windows.Forms.Panel();
            this.Callpanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.CallerBackPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(12, 59);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(624, 352);
            this.TabIndex = 0;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.CallerBackPanel, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(598, 335);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(273, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 259);
            this.panel2.TabIndex = 1;
            // 
            // CallerBackPanel
            // 
            this.CallerBackPanel.AutoScroll = true;
            this.CallerBackPanel.Controls.Add(this.Callpanel);
            this.CallerBackPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CallerBackPanel.Location = new System.Drawing.Point(0, 20);
            this.CallerBackPanel.Margin = new System.Windows.Forms.Padding(0);
            this.CallerBackPanel.Name = "CallerBackPanel";
            this.CallerBackPanel.Size = new System.Drawing.Size(598, 50);
            this.CallerBackPanel.TabIndex = 2;
            // 
            // Callpanel
            // 
            this.Callpanel.AutoSize = true;
            this.Callpanel.Location = new System.Drawing.Point(3, 0);
            this.Callpanel.Name = "Callpanel";
            this.Callpanel.Size = new System.Drawing.Size(526, 33);
            this.Callpanel.TabIndex = 0;
            // 
            // TaskGroupRowDesigner
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.CallerBackPanel.ResumeLayout(false);
            this.CallerBackPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel CallerBackPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel Callpanel;
    }
}
