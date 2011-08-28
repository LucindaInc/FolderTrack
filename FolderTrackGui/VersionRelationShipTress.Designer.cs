namespace FolderTrackGuiTest1
{
    partial class VersionRelationShipTress
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
            this.VersionTreePanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.VersionTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // VersionTreePanel
            // 
            this.VersionTreePanel.AutoSize = true;
            this.VersionTreePanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.VersionTreePanel.Location = new System.Drawing.Point(94, 75);
            this.VersionTreePanel.Name = "VersionTreePanel";
            this.VersionTreePanel.Size = new System.Drawing.Size(363, 243);
            this.VersionTreePanel.TabIndex = 0;
            this.VersionTreePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.VersionTreePanel_Paint);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.VersionTreePanel);
            this.panel1.Location = new System.Drawing.Point(21, 154);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 364);
            this.panel1.TabIndex = 1;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 615);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // VersionTextBox
            // 
            this.VersionTextBox.Location = new System.Drawing.Point(313, 26);
            this.VersionTextBox.Name = "VersionTextBox";
            this.VersionTextBox.Size = new System.Drawing.Size(100, 20);
            this.VersionTextBox.TabIndex = 3;
            // 
            // VersionRelationShipTress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(690, 685);
            this.Controls.Add(this.VersionTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "VersionRelationShipTress";
            this.Text = "VersionRelationShipTress";
            this.ResizeEnd += new System.EventHandler(this.VersionRelationShipTress_ResizeEnd);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel VersionTreePanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox VersionTextBox;






    }
}