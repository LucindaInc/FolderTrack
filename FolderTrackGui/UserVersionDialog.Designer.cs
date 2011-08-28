namespace FolderTrackGuiTest1
{
    partial class UserVersionDialog
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
            this.UserVersionTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.AddUserVersionLabel = new System.Windows.Forms.Label();
            this.StopUserVersionsListBox = new System.Windows.Forms.ListBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserVersionTextBox
            // 
            this.UserVersionTextBox.Location = new System.Drawing.Point(37, 37);
            this.UserVersionTextBox.Name = "UserVersionTextBox";
            this.UserVersionTextBox.Size = new System.Drawing.Size(218, 20);
            this.UserVersionTextBox.TabIndex = 0;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(37, 231);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.Location = new System.Drawing.Point(167, 231);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(75, 23);
            this.AbortButton.TabIndex = 2;
            this.AbortButton.Text = "Abort";
            this.AbortButton.UseVisualStyleBackColor = true;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // AddUserVersionLabel
            // 
            this.AddUserVersionLabel.AutoSize = true;
            this.AddUserVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddUserVersionLabel.Location = new System.Drawing.Point(89, 16);
            this.AddUserVersionLabel.Name = "AddUserVersionLabel";
            this.AddUserVersionLabel.Size = new System.Drawing.Size(114, 16);
            this.AddUserVersionLabel.TabIndex = 3;
            this.AddUserVersionLabel.Text = "Add User Version";
            // 
            // StopUserVersionsListBox
            // 
            this.StopUserVersionsListBox.FormattingEnabled = true;
            this.StopUserVersionsListBox.Location = new System.Drawing.Point(55, 75);
            this.StopUserVersionsListBox.Name = "StopUserVersionsListBox";
            this.StopUserVersionsListBox.Size = new System.Drawing.Size(183, 108);
            this.StopUserVersionsListBox.TabIndex = 5;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(109, 200);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 6;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // UserVersionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StopUserVersionsListBox);
            this.Controls.Add(this.AddUserVersionLabel);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.UserVersionTextBox);
            this.Name = "UserVersionDialog";
            this.Text = "UserVersionDialog";
     
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UserVersionTextBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button AbortButton;
        private System.Windows.Forms.Label AddUserVersionLabel;
        private System.Windows.Forms.ListBox StopUserVersionsListBox;
        private System.Windows.Forms.Button StopButton;
    }
}