namespace FolderTrackGuiTest1.NewMonitorGroup
{
    partial class MonitorLocationRowDesigner
    {
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
            this.DeleteButton = new System.Windows.Forms.Button();
            this.MonitorLocationPanel = new System.Windows.Forms.Panel();
            this.NotificationTextBox = new System.Windows.Forms.TextBox();
            this.MonitorLocationLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.MonitorLocationPanel.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.NotificationTextBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(51, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(479, 82);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // MonitorLocationRowDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 199);
            this.Controls.Add(this.panel1);
            this.Name = "MonitorLocationRowDesigner";
            this.Text = "MonitorLocationRow";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.MonitorLocationPanel.ResumeLayout(false);
            this.MonitorLocationPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
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