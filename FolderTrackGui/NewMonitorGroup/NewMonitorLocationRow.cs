using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace FolderTrackGuiTest1.NewMonitorGroup
{
    public class NewMonitorLocationRow : TableLayoutPanel
    {


        public NewMonitorGroupForm.NewLocationManger NewLocationManger;
        public string location;
        public delegate void VoidNoArgDelegate();
        public delegate void VoidStringDelegate(string s);

        public NewMonitorLocationRow()
        {
            InitializeComponent();
        }

        public NewMonitorLocationRow(string loca, NewMonitorGroupForm.NewLocationManger NewLocationManger)
        {
            InitializeComponent();
            this.location = loca;
            this.MonitorLocationLabel.Text = this.location;
            this.NewLocationManger = NewLocationManger;
            this.NotificationTextBox.Visible = false;
            FolderTrackGuiTest1.NewMonitorGroup.NewMonitorGroupForm.ErrorAc er = new NewMonitorGroupForm.ErrorAc();
            er.location = loca;
            er.ro = this;
            NewLocationManger.AddRowFromString(er);
        }

        public void showError(string error)
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new VoidStringDelegate(showError), new object[] { error });
                return;
            }
            this.MonitorLocationLabel.Visible = false;
            this.NotificationTextBox.Text = error;
            this.NotificationTextBox.Visible = true;
            this.tableLayoutPanel1.BackColor = Color.DarkRed;
            this.NotificationTextBox.BackColor = Color.LightSalmon;
            this.DeleteButton.BackColor = Color.LightSalmon;
            this.NotificationTextBox.Dock = DockStyle.Fill;
        }

        public void removeError()
        {
            if (this.NotificationTextBox.InvokeRequired)
            {
                this.NotificationTextBox.Invoke(new VoidNoArgDelegate(removeError));
                return;
            }
            this.NotificationTextBox.Visible = false;
            this.tableLayoutPanel1.BackColor = Color.Cornsilk;
            this.NotificationTextBox.BackColor = Color.White;
            this.DeleteButton.BackColor = Color.BurlyWood;
            this.MonitorLocationLabel.Visible = true;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            NewLocationManger.RemoveLocation(this.location);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            FolderTrackGuiTest1.NewMonitorGroup.NewMonitorGroupForm.ErrorAc er = new NewMonitorGroupForm.ErrorAc();
            er.location = this.location;
            er.ro = this;
            NewLocationManger.RemoveRowFromString(er);
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            Pen pe = new Pen(Color.BurlyWood, 8);
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(Color.Moccasin);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(1, 8);
            pt[1] = new Point(this.Width - 0, 8);
            pt[2] = new Point(this.Width - 0, this.Height - 11);
            pt[3] = new Point(1, this.Height-11 );
            pt[4] = new Point(1, 8);

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
            this.DeleteButton = new System.Windows.Forms.Button();
            this.MonitorLocationPanel = new System.Windows.Forms.Panel();
            this.NotificationTextBox = new System.Windows.Forms.TextBox();
            this.MonitorLocationLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.MonitorLocationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Cornsilk;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.DeleteButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.MonitorLocationPanel, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(459, 65);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.Color.BurlyWood;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.Location = new System.Drawing.Point(3, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 0;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // MonitorLocationPanel
            // 
            this.MonitorLocationPanel.AutoScroll = true;
            this.MonitorLocationPanel.Controls.Add(this.NotificationTextBox);
            this.MonitorLocationPanel.Controls.Add(this.MonitorLocationLabel);
            this.MonitorLocationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MonitorLocationPanel.Location = new System.Drawing.Point(84, 3);
            this.MonitorLocationPanel.Name = "MonitorLocationPanel";
            this.MonitorLocationPanel.Size = new System.Drawing.Size(372, 59);
            this.MonitorLocationPanel.TabIndex = 1;
            // 
            // NotificationTextBox
            // 
            this.NotificationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NotificationTextBox.Location = new System.Drawing.Point(176, 29);
            this.NotificationTextBox.Multiline = true;
            this.NotificationTextBox.Name = "NotificationTextBox";
            this.NotificationTextBox.ReadOnly = true;
            this.NotificationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.NotificationTextBox.Size = new System.Drawing.Size(100, 20);
            this.NotificationTextBox.TabIndex = 1;
            // 
            // MonitorLocationLabel
            // 
            this.MonitorLocationLabel.AutoSize = true;
            this.MonitorLocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MonitorLocationLabel.Location = new System.Drawing.Point(3, 6);
            this.MonitorLocationLabel.Name = "MonitorLocationLabel";
            this.MonitorLocationLabel.Size = new System.Drawing.Size(41, 15);
            this.MonitorLocationLabel.TabIndex = 0;
            this.MonitorLocationLabel.Text = "label1";
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(51, 93);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(479, 82);
            this.TabIndex = 1;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this_Paint);
            // 
            // MonitorLocationRowDesigner
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.MonitorLocationPanel.ResumeLayout(false);
            this.MonitorLocationPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Panel MonitorLocationPanel;
        private System.Windows.Forms.Label MonitorLocationLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox NotificationTextBox;


       
    }
}
