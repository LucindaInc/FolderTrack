namespace FolderTrackGuiTest1.AllVersionsTab
{
    partial class ListPanelDesignerForm
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
            this.ListRadioButton = new System.Windows.Forms.RadioButton();
            this.RelationshipRadioButton = new System.Windows.Forms.RadioButton();
            this.VersionPanel = new System.Windows.Forms.Panel();
            this.RelationShipPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.VersionPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ListRadioButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.RelationshipRadioButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.VersionPanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(138, 22);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ListRadioButton
            // 
            this.ListRadioButton.AutoSize = true;
            this.ListRadioButton.Location = new System.Drawing.Point(3, 3);
            this.ListRadioButton.Name = "ListRadioButton";
            this.ListRadioButton.Size = new System.Drawing.Size(41, 17);
            this.ListRadioButton.TabIndex = 1;
            this.ListRadioButton.TabStop = true;
            this.ListRadioButton.Text = "List";
            this.ListRadioButton.UseVisualStyleBackColor = true;
            this.ListRadioButton.CheckedChanged += new System.EventHandler(this.ListRadioButton_CheckedChanged);
            // 
            // RelationshipRadioButton
            // 
            this.RelationshipRadioButton.AutoSize = true;
            this.RelationshipRadioButton.Location = new System.Drawing.Point(50, 3);
            this.RelationshipRadioButton.Name = "RelationshipRadioButton";
            this.RelationshipRadioButton.Size = new System.Drawing.Size(83, 17);
            this.RelationshipRadioButton.TabIndex = 2;
            this.RelationshipRadioButton.TabStop = true;
            this.RelationshipRadioButton.Text = "Relationship";
            this.RelationshipRadioButton.UseVisualStyleBackColor = true;
            this.RelationshipRadioButton.CheckedChanged += new System.EventHandler(this.RelationshipRadioButton_CheckedChanged);
            // 
            // VersionPanel
            // 
            this.VersionPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.VersionPanel, 3);
            this.VersionPanel.Controls.Add(this.RelationShipPanel);
            this.VersionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionPanel.Location = new System.Drawing.Point(3, 26);
            this.VersionPanel.Name = "VersionPanel";
            this.VersionPanel.Size = new System.Drawing.Size(132, 1);
            this.VersionPanel.TabIndex = 3;
            // 
            // RelationShipPanel
            // 
            this.RelationShipPanel.BackColor = System.Drawing.Color.Transparent;
            this.RelationShipPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RelationShipPanel.Location = new System.Drawing.Point(0, 0);
            this.RelationShipPanel.Name = "RelationShipPanel";
            this.RelationShipPanel.Size = new System.Drawing.Size(132, 1);
            this.RelationShipPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 22);
            this.panel1.TabIndex = 1;
            // 
            // ListPanelDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(138, 22);
            this.Controls.Add(this.panel1);
            this.Name = "ListPanelDesignerForm";
            this.Text = "ListPanelDesignerForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.VersionPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton ListRadioButton;
        private System.Windows.Forms.RadioButton RelationshipRadioButton;
        private System.Windows.Forms.Panel VersionPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel RelationShipPanel;

    }
}