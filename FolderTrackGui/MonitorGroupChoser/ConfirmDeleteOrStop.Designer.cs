namespace FolderTrackGuiTest1.MonitorGroupChoser
{
    partial class ConfirmDeleteOrStop
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
            this.Discriplabel = new System.Windows.Forms.Label();
            this.ConfirButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Moccasin;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Discriplabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ConfirButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.CancelButton, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(304, 142);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Discriplabel
            // 
            this.Discriplabel.AutoSize = true;
            this.Discriplabel.BackColor = System.Drawing.Color.Moccasin;
            this.tableLayoutPanel1.SetColumnSpan(this.Discriplabel, 2);
            this.Discriplabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Discriplabel.Location = new System.Drawing.Point(3, 0);
            this.Discriplabel.Name = "Discriplabel";
            this.Discriplabel.Size = new System.Drawing.Size(298, 113);
            this.Discriplabel.TabIndex = 0;
            this.Discriplabel.Text = "label1";
            this.Discriplabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ConfirButton
            // 
            this.ConfirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirButton.AutoSize = true;
            this.ConfirButton.BackColor = System.Drawing.Color.BurlyWood;
            this.ConfirButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConfirButton.Location = new System.Drawing.Point(74, 116);
            this.ConfirButton.Name = "ConfirButton";
            this.ConfirButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirButton.TabIndex = 1;
            this.ConfirButton.Text = "button1";
            this.ConfirButton.UseVisualStyleBackColor = false;
            this.ConfirButton.Click += new System.EventHandler(this.ConfirButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.AutoSize = true;
            this.CancelButton.BackColor = System.Drawing.Color.BurlyWood;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CancelButton.Location = new System.Drawing.Point(155, 116);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ConfirmDeleteOrStop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 142);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConfirmDeleteOrStop";
            this.Text = "ConfirmDeleteOrStop";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Discriplabel;
        private System.Windows.Forms.Button ConfirButton;
        private System.Windows.Forms.Button CancelButton;
    }
}