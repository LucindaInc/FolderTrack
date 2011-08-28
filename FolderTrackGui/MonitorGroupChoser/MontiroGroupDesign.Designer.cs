namespace FolderTrackGuiTest1.MonitorGroupChoser
{
    partial class EMontiroGroupDesign
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Namelabel = new System.Windows.Forms.Label();
            this.LocationsListBox = new System.Windows.Forms.ListBox();
            this.OkBbutton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(49, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 154);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Cornsilk;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Namelabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LocationsListBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.OkBbutton, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(395, 130);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Namelabel
            // 
            this.Namelabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Namelabel.AutoSize = true;
            this.Namelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Namelabel.ForeColor = System.Drawing.Color.White;
            this.Namelabel.Location = new System.Drawing.Point(172, 0);
            this.Namelabel.Name = "Namelabel";
            this.Namelabel.Size = new System.Drawing.Size(51, 20);
            this.Namelabel.TabIndex = 0;
            this.Namelabel.Text = "label1";
            // 
            // LocationsListBox
            // 
            this.LocationsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocationsListBox.FormattingEnabled = true;
            this.LocationsListBox.Location = new System.Drawing.Point(3, 23);
            this.LocationsListBox.Name = "LocationsListBox";
            this.LocationsListBox.Size = new System.Drawing.Size(389, 69);
            this.LocationsListBox.TabIndex = 1;
            // 
            // OkBbutton
            // 
            this.OkBbutton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OkBbutton.BackColor = System.Drawing.Color.BurlyWood;
            this.OkBbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OkBbutton.Location = new System.Drawing.Point(160, 104);
            this.OkBbutton.Name = "OkBbutton";
            this.OkBbutton.Size = new System.Drawing.Size(75, 23);
            this.OkBbutton.TabIndex = 2;
            this.OkBbutton.Text = "Open";
            this.OkBbutton.UseVisualStyleBackColor = false;
            // 
            // EMontiroGroupDesign
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "EMontiroGroupDesign";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Namelabel;
        private System.Windows.Forms.ListBox LocationsListBox;
        private System.Windows.Forms.Button OkBbutton;
    }
}