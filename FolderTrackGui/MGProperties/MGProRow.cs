using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.ExclusionRules;
using System.Drawing;

namespace FolderTrackGuiTest1.MGProperties
{
    class MGProRow : Panel
    {
        GuiInfoMGProperties exru;

        public MGProRow(GuiInfoMGProperties ru)
        {
            InitializeComponent();
            this.exru = ru;
            this.TitleCheckBox.Text = ru.title;
            this.DescriptionTextBox.Text = ru.description;
            this.TitleCheckBox.Checked = ru.active;
        }

        private void TitleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
                exru.active = this.TitleCheckBox.Checked;          
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
            this.TitleCheckBox = new System.Windows.Forms.CheckBox();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(12, 97);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(347, 121);
            this.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.TitleCheckBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DescriptionTextBox, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(19, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(310, 99);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // TitleCheckBox
            // 
            this.TitleCheckBox.AutoSize = true;
            this.TitleCheckBox.Location = new System.Drawing.Point(3, 3);
            this.TitleCheckBox.Name = "TitleCheckBox";
            this.TitleCheckBox.Size = new System.Drawing.Size(15, 14);
            this.TitleCheckBox.TabIndex = 0;
            this.TitleCheckBox.UseVisualStyleBackColor = true;
            this.TitleCheckBox.CheckedChanged += new System.EventHandler(this.TitleCheckBox_CheckedChanged);
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DescriptionTextBox.Location = new System.Drawing.Point(3, 23);
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DescriptionTextBox.Size = new System.Drawing.Size(304, 73);
            this.DescriptionTextBox.TabIndex = 1;
            // 
            // MGProRowDesigner
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox TitleCheckBox;
        private System.Windows.Forms.TextBox DescriptionTextBox;
    }
}
