namespace FolderTrackGuiTest1.AllVersionsTab
{
    partial class RelationshipDesigner
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
            this.RelationshipPanel = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.RelationshipPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RelationshipPanel
            // 
            this.RelationshipPanel.BackColor = System.Drawing.SystemColors.Control;
            this.RelationshipPanel.Controls.Add(this.vScrollBar1);
            this.RelationshipPanel.Controls.Add(this.hScrollBar1);
            this.RelationshipPanel.Location = new System.Drawing.Point(12, 24);
            this.RelationshipPanel.Name = "RelationshipPanel";
            this.RelationshipPanel.Size = new System.Drawing.Size(554, 304);
            this.RelationshipPanel.TabIndex = 0;
            this.RelationshipPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.RelationshipPanel_Paint);
            this.RelationshipPanel.Resize += new System.EventHandler(this.RelationshipPanel_Resize);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(537, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 287);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 287);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(554, 17);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // RelationshipDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aquamarine;
            this.ClientSize = new System.Drawing.Size(578, 482);
            this.Controls.Add(this.RelationshipPanel);
            this.Name = "RelationshipDesigner";
            this.Text = "RelationshipDesigner";
            this.RelationshipPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel RelationshipPanel;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}