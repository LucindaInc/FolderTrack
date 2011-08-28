using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FolderTrackGuiTest1
{
    public partial class CalendarPanel : TableLayoutPanel
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

        public CalendarPanel()
        {
            InitializeComponent();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TimeLabel = new System.Windows.Forms.Label();
            this.ChangeListBox = new System.Windows.Forms.ListBox();
            this.VersionsListBox = new System.Windows.Forms.ListBox();
            this.ActivitiesPanel = new System.Windows.Forms.Panel();
            this.ExploreButton = new System.Windows.Forms.Button();
            this.SetButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.DateLabel = new System.Windows.Forms.Label();
            this.DiscriptionTextBox = new System.Windows.Forms.TextBox();
            this.AutoSize = true;
            this.SuspendLayout();
            this.ActivitiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // VersionTableLayoutPanel
            // 
            this.AutoSize = true;
            this.ColumnCount = 6;
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Controls.Add(this.TimeLabel, 1, 0);
            this.Controls.Add(this.ChangeListBox, 2, 0);
            this.Controls.Add(this.VersionsListBox, 3, 0);
            this.Controls.Add(this.ActivitiesPanel, 5, 0);
            this.Controls.Add(this.DateLabel, 0, 0);
            this.Controls.Add(this.DiscriptionTextBox, 4, 0);
            this.Location = new System.Drawing.Point(22, 76);
            this.Name = "VersionTableLayoutPanel";
            this.RowCount = 1;
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Size = new System.Drawing.Size(828, 106);
            this.TabIndex = 0;
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeLabel.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.Location = new System.Drawing.Point(46, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(110, 106);
            this.TimeLabel.TabIndex = 0;
            this.TimeLabel.Text = "12:34am";
            this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ChangeListBox
            // 
            this.ChangeListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeListBox.FormattingEnabled = true;
            this.ChangeListBox.Location = new System.Drawing.Point(162, 3);
            this.ChangeListBox.Name = "ChangeListBox";
            this.ChangeListBox.Size = new System.Drawing.Size(120, 95);
            this.ChangeListBox.TabIndex = 1;
            // 
            // VersionsListBox
            // 
            this.VersionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionsListBox.FormattingEnabled = true;
            this.VersionsListBox.Location = new System.Drawing.Point(288, 3);
            this.VersionsListBox.Name = "VersionsListBox";
            this.VersionsListBox.Size = new System.Drawing.Size(100, 95);
            this.VersionsListBox.TabIndex = 2;
            // 
            // ActivitiesPanel
            // 
            this.ActivitiesPanel.Controls.Add(this.CopyButton);
            this.ActivitiesPanel.Controls.Add(this.SetButton);
            this.ActivitiesPanel.Controls.Add(this.ExploreButton);
            this.ActivitiesPanel.Location = new System.Drawing.Point(694, 3);
            this.ActivitiesPanel.Name = "ActivitiesPanel";
            this.ActivitiesPanel.Size = new System.Drawing.Size(131, 100);
            this.ActivitiesPanel.TabIndex = 3;
            // 
            // ExploreButton
            // 
            this.ExploreButton.Location = new System.Drawing.Point(3, 3);
            this.ExploreButton.Name = "ExploreButton";
            this.ExploreButton.Size = new System.Drawing.Size(75, 23);
            this.ExploreButton.TabIndex = 0;
            this.ExploreButton.Text = "Explore";
            this.ExploreButton.UseVisualStyleBackColor = true;
            // 
            // SetButton
            // 
            this.SetButton.Location = new System.Drawing.Point(3, 37);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(75, 23);
            this.SetButton.TabIndex = 1;
            this.SetButton.Text = "Set";
            this.SetButton.UseVisualStyleBackColor = true;
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(3, 71);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 23);
            this.CopyButton.TabIndex = 2;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLabel.Location = new System.Drawing.Point(3, 0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(37, 42);
            this.DateLabel.TabIndex = 4;
            this.DateLabel.Text = "1";
            // 
            // DiscriptionTextBox
            // 
            this.DiscriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiscriptionTextBox.Location = new System.Drawing.Point(394, 3);
            this.DiscriptionTextBox.Multiline = true;
            this.DiscriptionTextBox.Name = "DiscriptionTextBox";
            this.DiscriptionTextBox.Size = new System.Drawing.Size(294, 100);
            this.DiscriptionTextBox.TabIndex = 5;
            // 
            // Form2
            // 

            this.ResumeLayout(false);
            this.PerformLayout();
            this.ActivitiesPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.ListBox ChangeListBox;
        private System.Windows.Forms.ListBox VersionsListBox;
        private System.Windows.Forms.Panel ActivitiesPanel;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.Button ExploreButton;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.TextBox DiscriptionTextBox;


    }
}
