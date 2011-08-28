using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FolderTrackGuiTest1
{
    class VersionRow : TableLayoutPanel
    {
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.ListBox UserVersionListBox;
        private System.Windows.Forms.ListBox ChangeFileListBox;
        private System.Windows.Forms.Button ExplorerButton;
        private System.Windows.Forms.Button UseButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.TextBox DiscriptionTextBox;
        private System.Windows.Forms.Label RevisionLabel;

        private void InitializeComponent()
        {
            this.VersionLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.UserVersionListBox = new System.Windows.Forms.ListBox();
            this.ChangeFileListBox = new System.Windows.Forms.ListBox();
            this.ExplorerButton = new System.Windows.Forms.Button();
            this.UseButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.DiscriptionTextBox = new System.Windows.Forms.TextBox();
            this.RevisionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.SuspendLayout();
            // 
            // VersionTableLayoutPanel
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ColumnCount = 6;
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.Controls.Add(this.VersionLabel, 0, 0);
            this.Controls.Add(this.DateLabel, 3, 0);
            this.Controls.Add(this.UserVersionListBox, 0, 2);
            this.Controls.Add(this.ChangeFileListBox, 1, 1);
            this.Controls.Add(this.DiscriptionTextBox, 5, 1);
            this.Controls.Add(this.RevisionLabel, 0, 1);
            this.Controls.Add(this.ExplorerButton, 1, 3);
            this.Controls.Add(this.UseButton, 2, 3);
            this.Controls.Add(this.CopyButton, 3, 3);
            this.Location = new System.Drawing.Point(25, 63);
            this.Name = "VersionTableLayoutPanel";
            this.RowCount = 4;
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.Size = new System.Drawing.Size(941, 207);
            this.TabIndex = 0;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.SetColumnSpan(this.VersionLabel, 3);
            this.VersionLabel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.Location = new System.Drawing.Point(3, 0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(33, 35);
            this.VersionLabel.TabIndex = 0;
            this.VersionLabel.Text = "1";
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.SetColumnSpan(this.DateLabel, 3);
            this.DateLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLabel.Location = new System.Drawing.Point(353, 0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(447, 27);
            this.DateLabel.TabIndex = 1;
            this.DateLabel.Text = "18:08PM Saturday,   December 12, 2009";
            // 
            // UserVersionListBox
            // 
            this.UserVersionListBox.BackColor = System.Drawing.Color.SteelBlue;
            this.UserVersionListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserVersionListBox.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserVersionListBox.FormattingEnabled = true;
            this.UserVersionListBox.HorizontalScrollbar = true;
            this.UserVersionListBox.ItemHeight = 24;
            this.UserVersionListBox.Items.AddRange(new object[] {
            "Debug Filter.3",
            "Dodge Menu.2",
            "RedoCustemer.57"});
            this.UserVersionListBox.Location = new System.Drawing.Point(3, 58);
            this.UserVersionListBox.Name = "UserVersionListBox";
            this.SetRowSpan(this.UserVersionListBox, 2);
            this.UserVersionListBox.Size = new System.Drawing.Size(194, 146);
            this.UserVersionListBox.TabIndex = 2;
            // 
            // ChangeFileListBox
            // 
            this.ChangeFileListBox.BackColor = System.Drawing.Color.SteelBlue;
            this.ChangeFileListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SetColumnSpan(this.ChangeFileListBox, 4);
            this.ChangeFileListBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeFileListBox.FormattingEnabled = true;
            this.ChangeFileListBox.HorizontalScrollbar = true;
            this.ChangeFileListBox.ItemHeight = 14;
            this.ChangeFileListBox.Items.AddRange(new object[] {
            "Add: C:\\Projects\\Soluse\\BackOpperation.cpp",
            "Add: C:\\Projects\\Soluse\\BackOpperation.h",
            "Add: C:\\Projects\\Soluse\\FrontOpperation.cpp",
            "Add: C:\\Projects\\Soluse\\FrontOpperation.h"});
            this.ChangeFileListBox.Location = new System.Drawing.Point(203, 38);
            this.ChangeFileListBox.Name = "ChangeFileListBox";
            this.SetRowSpan(this.ChangeFileListBox, 2);
            this.ChangeFileListBox.Size = new System.Drawing.Size(519, 128);
            this.ChangeFileListBox.TabIndex = 3;
            // 
            // ExplorerButton
            // 
            this.ExplorerButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExplorerButton.Location = new System.Drawing.Point(203, 180);
            this.ExplorerButton.Name = "ExplorerButton";
            this.ExplorerButton.Size = new System.Drawing.Size(69, 23);
            this.ExplorerButton.TabIndex = 4;
            this.ExplorerButton.Text = "Explorer";
            this.ExplorerButton.UseVisualStyleBackColor = true;
            // 
            // UseButton
            // 
            this.UseButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UseButton.Location = new System.Drawing.Point(278, 180);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(69, 23);
            this.UseButton.TabIndex = 5;
            this.UseButton.Text = "Use";
            this.UseButton.UseVisualStyleBackColor = true;
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CopyButton.Location = new System.Drawing.Point(353, 180);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(69, 23);
            this.CopyButton.TabIndex = 6;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            // 
            // DiscriptionTextBox
            // 
            this.DiscriptionTextBox.BackColor = System.Drawing.Color.SteelBlue;
            this.DiscriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DiscriptionTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscriptionTextBox.Location = new System.Drawing.Point(728, 38);
            this.DiscriptionTextBox.Multiline = true;
            this.DiscriptionTextBox.Name = "DiscriptionTextBox";
            this.SetRowSpan(this.DiscriptionTextBox, 3);
            this.DiscriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DiscriptionTextBox.Size = new System.Drawing.Size(204, 166);
            this.DiscriptionTextBox.TabIndex = 7;
            this.DiscriptionTextBox.Text = "This version has everything working. There is a small issue with the statup proce" +
                "dure";
            // 
            // RevisionLabel
            // 
            this.RevisionLabel.AutoSize = true;
            this.RevisionLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RevisionLabel.Location = new System.Drawing.Point(3, 35);
            this.RevisionLabel.Name = "RevisionLabel";
            this.RevisionLabel.Size = new System.Drawing.Size(13, 14);
            this.RevisionLabel.TabIndex = 8;
            this.RevisionLabel.Text = "1";
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(1023, 346);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
