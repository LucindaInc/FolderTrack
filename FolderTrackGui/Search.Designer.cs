namespace FolderTrackGuiTest1
{
    partial class Search
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
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.UserVersions = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Changes = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ShowTimeButton1 = new System.Windows.Forms.Button();
            this.ShowDatePickerButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UserVersSearchButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.VersionTextBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.UserVersionsLabel = new System.Windows.Forms.Label();
            this.UserVersionsComboBox = new System.Windows.Forms.ComboBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CalendarClose = new System.Windows.Forms.Label();
            this.CalendarCancelButton = new System.Windows.Forms.Button();
            this.CalendarOkButton = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.RenameCheckBox = new System.Windows.Forms.CheckBox();
            this.DeleteCheckBox = new System.Windows.Forms.CheckBox();
            this.AddCheckBox = new System.Windows.Forms.CheckBox();
            this.ChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ChangeFileButton = new System.Windows.Forms.Button();
            this.ChangeFileTextBox = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.FilterNoteRadioButton = new System.Windows.Forms.RadioButton();
            this.HasNoteRadioButton = new System.Windows.Forms.RadioButton();
            this.NoNotesRadioButton = new System.Windows.Forms.RadioButton();
            this.FreetextTextBox3 = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.SearchFilesLabel = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.UserVersName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchDataTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(312, 20);
            this.textBox1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateDataGridViewTextBoxColumn,
            this.Version,
            this.UserVersions,
            this.Changes,
            this.notesDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.searchDataTableBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(0, 193);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1144, 150);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
            // 
            // UserVersions
            // 
            this.UserVersions.HeaderText = "UserVersions";
            this.UserVersions.Name = "UserVersions";
            // 
            // Changes
            // 
            this.Changes.DataPropertyName = "Changes";
            this.Changes.HeaderText = "Changes";
            this.Changes.Name = "Changes";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.ShowTimeButton1);
            this.panel1.Controls.Add(this.ShowDatePickerButton);
            this.panel1.Location = new System.Drawing.Point(0, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(102, 100);
            this.panel1.TabIndex = 2;
            // 
            // ShowTimeButton1
            // 
            this.ShowTimeButton1.Location = new System.Drawing.Point(18, 60);
            this.ShowTimeButton1.Name = "ShowTimeButton1";
            this.ShowTimeButton1.Size = new System.Drawing.Size(75, 23);
            this.ShowTimeButton1.TabIndex = 1;
            this.ShowTimeButton1.Text = "Show Time";
            this.ShowTimeButton1.UseVisualStyleBackColor = true;
            // 
            // ShowDatePickerButton
            // 
            this.ShowDatePickerButton.Location = new System.Drawing.Point(3, 19);
            this.ShowDatePickerButton.Name = "ShowDatePickerButton";
            this.ShowDatePickerButton.Size = new System.Drawing.Size(75, 23);
            this.ShowDatePickerButton.TabIndex = 0;
            this.ShowDatePickerButton.Text = "Show Date";
            this.ShowDatePickerButton.UseVisualStyleBackColor = true;
            this.ShowDatePickerButton.Click += new System.EventHandler(this.ShowDatePickerButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.UserVersSearchButton);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.VersionTextBox);
            this.panel2.Location = new System.Drawing.Point(99, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(139, 100);
            this.panel2.TabIndex = 3;
            // 
            // UserVersSearchButton
            // 
            this.UserVersSearchButton.Location = new System.Drawing.Point(32, 70);
            this.UserVersSearchButton.Name = "UserVersSearchButton";
            this.UserVersSearchButton.Size = new System.Drawing.Size(75, 23);
            this.UserVersSearchButton.TabIndex = 2;
            this.UserVersSearchButton.Text = "Search";
            this.UserVersSearchButton.UseVisualStyleBackColor = true;
            this.UserVersSearchButton.Click += new System.EventHandler(this.UserVersSearchButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(49, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Version";
            // 
            // VersionTextBox
            // 
            this.VersionTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.VersionTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.VersionTextBox.Location = new System.Drawing.Point(6, 46);
            this.VersionTextBox.Name = "VersionTextBox";
            this.VersionTextBox.Size = new System.Drawing.Size(133, 20);
            this.VersionTextBox.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel3.Controls.Add(this.UserVersionsLabel);
            this.panel3.Controls.Add(this.UserVersionsComboBox);
            this.panel3.Location = new System.Drawing.Point(244, 93);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(211, 100);
            this.panel3.TabIndex = 4;
            // 
            // UserVersionsLabel
            // 
            this.UserVersionsLabel.AutoSize = true;
            this.UserVersionsLabel.Location = new System.Drawing.Point(68, 43);
            this.UserVersionsLabel.Name = "UserVersionsLabel";
            this.UserVersionsLabel.Size = new System.Drawing.Size(72, 13);
            this.UserVersionsLabel.TabIndex = 1;
            this.UserVersionsLabel.Text = "User Versions";
            // 
            // UserVersionsComboBox
            // 
            this.UserVersionsComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.UserVersionsComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.UserVersionsComboBox.FormattingEnabled = true;
            this.UserVersionsComboBox.Location = new System.Drawing.Point(4, 70);
            this.UserVersionsComboBox.Name = "UserVersionsComboBox";
            this.UserVersionsComboBox.Size = new System.Drawing.Size(192, 21);
            this.UserVersionsComboBox.TabIndex = 0;
            this.UserVersionsComboBox.Text = " ";
            this.UserVersionsComboBox.SelectedValueChanged += new System.EventHandler(this.UserVersionsComboBox_SelectedValueChanged);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(36, 33);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.CalendarClose);
            this.panel4.Controls.Add(this.CalendarCancelButton);
            this.panel4.Controls.Add(this.CalendarOkButton);
            this.panel4.Controls.Add(this.monthCalendar1);
            this.panel4.Location = new System.Drawing.Point(151, 375);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(236, 226);
            this.panel4.TabIndex = 6;
            // 
            // CalendarClose
            // 
            this.CalendarClose.AutoSize = true;
            this.CalendarClose.Location = new System.Drawing.Point(219, 0);
            this.CalendarClose.Name = "CalendarClose";
            this.CalendarClose.Size = new System.Drawing.Size(14, 13);
            this.CalendarClose.TabIndex = 8;
            this.CalendarClose.Text = "X";
            // 
            // CalendarCancelButton
            // 
            this.CalendarCancelButton.Location = new System.Drawing.Point(139, 200);
            this.CalendarCancelButton.Name = "CalendarCancelButton";
            this.CalendarCancelButton.Size = new System.Drawing.Size(75, 23);
            this.CalendarCancelButton.TabIndex = 7;
            this.CalendarCancelButton.Text = "Cancel";
            this.CalendarCancelButton.UseVisualStyleBackColor = true;
            // 
            // CalendarOkButton
            // 
            this.CalendarOkButton.Location = new System.Drawing.Point(36, 200);
            this.CalendarOkButton.Name = "CalendarOkButton";
            this.CalendarOkButton.Size = new System.Drawing.Size(75, 23);
            this.CalendarOkButton.TabIndex = 6;
            this.CalendarOkButton.Text = "OK";
            this.CalendarOkButton.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.RenameCheckBox);
            this.panel5.Controls.Add(this.DeleteCheckBox);
            this.panel5.Controls.Add(this.AddCheckBox);
            this.panel5.Controls.Add(this.ChangeCheckBox);
            this.panel5.Location = new System.Drawing.Point(461, 93);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(103, 100);
            this.panel5.TabIndex = 7;
            // 
            // RenameCheckBox
            // 
            this.RenameCheckBox.AutoSize = true;
            this.RenameCheckBox.Location = new System.Drawing.Point(24, 74);
            this.RenameCheckBox.Name = "RenameCheckBox";
            this.RenameCheckBox.Size = new System.Drawing.Size(66, 17);
            this.RenameCheckBox.TabIndex = 3;
            this.RenameCheckBox.Text = "Rename";
            this.RenameCheckBox.UseVisualStyleBackColor = true;
            // 
            // DeleteCheckBox
            // 
            this.DeleteCheckBox.AutoSize = true;
            this.DeleteCheckBox.Location = new System.Drawing.Point(24, 49);
            this.DeleteCheckBox.Name = "DeleteCheckBox";
            this.DeleteCheckBox.Size = new System.Drawing.Size(57, 17);
            this.DeleteCheckBox.TabIndex = 2;
            this.DeleteCheckBox.Text = "Delete";
            this.DeleteCheckBox.UseVisualStyleBackColor = true;
            // 
            // AddCheckBox
            // 
            this.AddCheckBox.AutoSize = true;
            this.AddCheckBox.Location = new System.Drawing.Point(24, 25);
            this.AddCheckBox.Name = "AddCheckBox";
            this.AddCheckBox.Size = new System.Drawing.Size(45, 17);
            this.AddCheckBox.TabIndex = 1;
            this.AddCheckBox.Text = "Add";
            this.AddCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChangeCheckBox
            // 
            this.ChangeCheckBox.AutoSize = true;
            this.ChangeCheckBox.Location = new System.Drawing.Point(24, 4);
            this.ChangeCheckBox.Name = "ChangeCheckBox";
            this.ChangeCheckBox.Size = new System.Drawing.Size(63, 17);
            this.ChangeCheckBox.TabIndex = 0;
            this.ChangeCheckBox.Text = "Change";
            this.ChangeCheckBox.UseVisualStyleBackColor = true;
            this.ChangeCheckBox.CheckedChanged += new System.EventHandler(this.ChangeCheckBox_CheckedChanged);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Maroon;
            this.panel6.Controls.Add(this.ChangeFileButton);
            this.panel6.Controls.Add(this.ChangeFileTextBox);
            this.panel6.Location = new System.Drawing.Point(570, 93);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(161, 100);
            this.panel6.TabIndex = 8;
            // 
            // ChangeFileButton
            // 
            this.ChangeFileButton.Location = new System.Drawing.Point(38, 15);
            this.ChangeFileButton.Name = "ChangeFileButton";
            this.ChangeFileButton.Size = new System.Drawing.Size(75, 23);
            this.ChangeFileButton.TabIndex = 1;
            this.ChangeFileButton.Text = "Browse";
            this.ChangeFileButton.UseVisualStyleBackColor = true;
            this.ChangeFileButton.Click += new System.EventHandler(this.ChangeFileButton_Click);
            // 
            // ChangeFileTextBox
            // 
            this.ChangeFileTextBox.Location = new System.Drawing.Point(21, 70);
            this.ChangeFileTextBox.Name = "ChangeFileTextBox";
            this.ChangeFileTextBox.Size = new System.Drawing.Size(129, 20);
            this.ChangeFileTextBox.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.FilterNoteRadioButton);
            this.panel7.Controls.Add(this.HasNoteRadioButton);
            this.panel7.Controls.Add(this.NoNotesRadioButton);
            this.panel7.Controls.Add(this.FreetextTextBox3);
            this.panel7.Location = new System.Drawing.Point(738, 93);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(200, 100);
            this.panel7.TabIndex = 9;
            // 
            // FilterNoteRadioButton
            // 
            this.FilterNoteRadioButton.AutoSize = true;
            this.FilterNoteRadioButton.Location = new System.Drawing.Point(7, 48);
            this.FilterNoteRadioButton.Name = "FilterNoteRadioButton";
            this.FilterNoteRadioButton.Size = new System.Drawing.Size(142, 17);
            this.FilterNoteRadioButton.TabIndex = 3;
            this.FilterNoteRadioButton.Text = "Has Note That Contains:";
            this.FilterNoteRadioButton.UseVisualStyleBackColor = true;
            this.FilterNoteRadioButton.CheckedChanged += new System.EventHandler(this.FilterNoteRadioButton_CheckedChanged);
            // 
            // HasNoteRadioButton
            // 
            this.HasNoteRadioButton.AutoSize = true;
            this.HasNoteRadioButton.Location = new System.Drawing.Point(7, 25);
            this.HasNoteRadioButton.Name = "HasNoteRadioButton";
            this.HasNoteRadioButton.Size = new System.Drawing.Size(79, 17);
            this.HasNoteRadioButton.TabIndex = 2;
            this.HasNoteRadioButton.Text = "Has a Note";
            this.HasNoteRadioButton.UseVisualStyleBackColor = true;
            this.HasNoteRadioButton.CheckedChanged += new System.EventHandler(this.HasNoteRadioButton_CheckedChanged);
            // 
            // NoNotesRadioButton
            // 
            this.NoNotesRadioButton.AutoSize = true;
            this.NoNotesRadioButton.Location = new System.Drawing.Point(6, 4);
            this.NoNotesRadioButton.Name = "NoNotesRadioButton";
            this.NoNotesRadioButton.Size = new System.Drawing.Size(137, 17);
            this.NoNotesRadioButton.TabIndex = 1;
            this.NoNotesRadioButton.Text = "Does Not Have a  Note";
            this.NoNotesRadioButton.UseVisualStyleBackColor = true;
            this.NoNotesRadioButton.CheckedChanged += new System.EventHandler(this.NoNotesRadioButton_CheckedChanged);
            // 
            // FreetextTextBox3
            // 
            this.FreetextTextBox3.Location = new System.Drawing.Point(7, 70);
            this.FreetextTextBox3.Name = "FreetextTextBox3";
            this.FreetextTextBox3.Size = new System.Drawing.Size(190, 20);
            this.FreetextTextBox3.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.SearchFilesLabel);
            this.panel8.Controls.Add(this.textBox3);
            this.panel8.Location = new System.Drawing.Point(944, 93);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(200, 100);
            this.panel8.TabIndex = 10;
            // 
            // SearchFilesLabel
            // 
            this.SearchFilesLabel.AutoSize = true;
            this.SearchFilesLabel.Location = new System.Drawing.Point(50, 27);
            this.SearchFilesLabel.Name = "SearchFilesLabel";
            this.SearchFilesLabel.Size = new System.Drawing.Size(75, 13);
            this.SearchFilesLabel.TabIndex = 1;
            this.SearchFilesLabel.Text = "Search Labels";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(4, 70);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(193, 20);
            this.textBox3.TabIndex = 0;
            // 
            // UserVersName
            // 
            this.UserVersName.DropDownWidth = 3;
            this.UserVersName.HeaderText = "UserVersName";
            this.UserVersName.Name = "UserVersName";
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // Version
            // 
            this.Version.DataPropertyName = "Version";
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "Notes";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            // 
            // searchDataTableBindingSource
            // 
            this.searchDataTableBindingSource.DataSource = typeof(FolderTrackGuiTest1.SearchDataTable);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 540);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Search";
            this.Text = "Search";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchDataTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label CalendarClose;
        private System.Windows.Forms.Button CalendarCancelButton;
        private System.Windows.Forms.Button CalendarOkButton;
        private System.Windows.Forms.Button ShowTimeButton1;
        private System.Windows.Forms.Button ShowDatePickerButton;
        private System.Windows.Forms.TextBox VersionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox RenameCheckBox;
        private System.Windows.Forms.CheckBox DeleteCheckBox;
        private System.Windows.Forms.CheckBox AddCheckBox;
        private System.Windows.Forms.CheckBox ChangeCheckBox;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button ChangeFileButton;
        private System.Windows.Forms.TextBox ChangeFileTextBox;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox FreetextTextBox3;
        private System.Windows.Forms.RadioButton FilterNoteRadioButton;
        private System.Windows.Forms.RadioButton HasNoteRadioButton;
        private System.Windows.Forms.RadioButton NoNotesRadioButton;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label SearchFilesLabel;
        private System.Windows.Forms.Label UserVersionsLabel;
        private System.Windows.Forms.ComboBox UserVersionsComboBox;
        private System.Windows.Forms.Button UserVersSearchButton;
        private System.Windows.Forms.DataGridViewComboBoxColumn UserVersName;
        private System.Windows.Forms.BindingSource searchDataTableBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewComboBoxColumn UserVersions;
        private System.Windows.Forms.DataGridViewComboBoxColumn Changes;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;

    }
}