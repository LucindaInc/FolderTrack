namespace FolderTrackGuiTest1.SearchTab
{
    partial class ChangeRowDesigner
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ChangeLabel = new System.Windows.Forms.Label();
            this.ChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(47, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 56);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Cornsilk;
            this.panel2.Controls.Add(this.ChangeLabel);
            this.panel2.Location = new System.Drawing.Point(35, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(381, 45);
            this.panel2.TabIndex = 1;
            // 
            // ChangeLabel
            // 
            this.ChangeLabel.AutoSize = true;
            this.ChangeLabel.Location = new System.Drawing.Point(3, 7);
            this.ChangeLabel.Name = "ChangeLabel";
            this.ChangeLabel.Size = new System.Drawing.Size(35, 13);
            this.ChangeLabel.TabIndex = 1;
            this.ChangeLabel.Text = "label1";
            // 
            // ChangeCheckBox
            // 
            this.ChangeCheckBox.BackColor = System.Drawing.Color.Cornsilk;
            this.ChangeCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ChangeCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ChangeCheckBox.Location = new System.Drawing.Point(0, 0);
            this.ChangeCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.ChangeCheckBox.Name = "ChangeCheckBox";
            this.ChangeCheckBox.Size = new System.Drawing.Size(35, 45);
            this.ChangeCheckBox.TabIndex = 0;
            this.ChangeCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChangeCheckBox.UseVisualStyleBackColor = false;
            this.ChangeCheckBox.CheckedChanged += new System.EventHandler(this.ChangeCheckBox_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ChangeCheckBox, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 48);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ChangeRowDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 266);
            this.Controls.Add(this.panel1);
            this.Name = "ChangeRowDesigner";
            this.Text = "ChangeRowDesigner";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ChangeCheckBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label ChangeLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}