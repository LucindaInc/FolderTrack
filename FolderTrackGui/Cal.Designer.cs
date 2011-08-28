namespace FolderTrackGuiTest1
{
    partial class Cal
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
            this.MainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.MonthTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.VersionPanel = new System.Windows.Forms.Panel();
            this.CalDataGridView = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VersionDataCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserVersions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotesDataCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalendarTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.YearPanel = new System.Windows.Forms.Panel();
            this.hourLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PMLabel = new System.Windows.Forms.Label();
            this.AMLabel = new System.Windows.Forms.Label();
            this.calDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MainTableLayoutPanel.SuspendLayout();
            this.VersionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalDataGridView)).BeginInit();
            this.hourLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calDataTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            this.MainTableLayoutPanel.AutoScroll = true;
            this.MainTableLayoutPanel.AutoSize = true;
            this.MainTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.MainTableLayoutPanel.ColumnCount = 3;
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 800F));
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 915F));
            this.MainTableLayoutPanel.Controls.Add(this.textBox, 1, 0);
            this.MainTableLayoutPanel.Controls.Add(this.MonthTableLayoutPanel, 2, 2);
            this.MainTableLayoutPanel.Controls.Add(this.VersionPanel, 0, 0);
            this.MainTableLayoutPanel.Controls.Add(this.CalendarTableLayoutPanel, 1, 2);
            this.MainTableLayoutPanel.Controls.Add(this.YearPanel, 1, 1);
            this.MainTableLayoutPanel.Controls.Add(this.hourLayoutPanel, 1, 4);
            this.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            this.MainTableLayoutPanel.RowCount = 5;
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.MainTableLayoutPanel.Size = new System.Drawing.Size(983, 638);
            this.MainTableLayoutPanel.TabIndex = 0;
            // 
            // textBox
            // 
            this.MainTableLayoutPanel.SetColumnSpan(this.textBox, 2);
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Location = new System.Drawing.Point(805, 4);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(1609, 20);
            this.textBox.TabIndex = 2;
            // 
            // MonthTableLayoutPanel
            // 
            this.MonthTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MonthTableLayoutPanel.AutoSize = true;
            this.MonthTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.MonthTableLayoutPanel.ColumnCount = 1;
            this.MonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 323F));
            this.MonthTableLayoutPanel.Location = new System.Drawing.Point(1797, 81);
            this.MonthTableLayoutPanel.Name = "MonthTableLayoutPanel";
            this.MonthTableLayoutPanel.RowCount = 11;
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.Size = new System.Drawing.Size(325, 12);
            this.MonthTableLayoutPanel.TabIndex = 1;
            // 
            // VersionPanel
            // 
            this.VersionPanel.AutoScroll = true;
            this.VersionPanel.AutoSize = true;
            this.VersionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionPanel.Controls.Add(this.CalDataGridView);
            this.VersionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionPanel.Location = new System.Drawing.Point(4, 4);
            this.VersionPanel.Name = "VersionPanel";
            this.MainTableLayoutPanel.SetRowSpan(this.VersionPanel, 5);
            this.VersionPanel.Size = new System.Drawing.Size(794, 613);
            this.VersionPanel.TabIndex = 4;
            // 
            // CalDataGridView
            // 
            this.CalDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CalDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.VersionDataCol,
            this.UserVersions,
            this.NotesDataCol,
            this.ChangeColumn});
            this.CalDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalDataGridView.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CalDataGridView.Location = new System.Drawing.Point(0, 0);
            this.CalDataGridView.Name = "CalDataGridView";
            this.CalDataGridView.Size = new System.Drawing.Size(792, 611);
            this.CalDataGridView.TabIndex = 0;
            this.CalDataGridView.VirtualMode = true;
            this.CalDataGridView.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.CalDataGridView_CellValueNeeded);
            this.CalDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CalDataGridView_CellContentClick);
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // VersionDataCol
            // 
            this.VersionDataCol.HeaderText = "Version";
            this.VersionDataCol.Name = "VersionDataCol";
            this.VersionDataCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // UserVersions
            // 
            this.UserVersions.HeaderText = "UserVersions";
            this.UserVersions.Name = "UserVersions";
            this.UserVersions.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UserVersions.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NotesDataCol
            // 
            this.NotesDataCol.HeaderText = "Notes";
            this.NotesDataCol.Name = "NotesDataCol";
            // 
            // ChangeColumn
            // 
            this.ChangeColumn.DataPropertyName = "Date";
            this.ChangeColumn.HeaderText = "Changes";
            this.ChangeColumn.Name = "ChangeColumn";
            this.ChangeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ChangeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CalendarTableLayoutPanel
            // 
            this.CalendarTableLayoutPanel.AutoSize = true;
            this.CalendarTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.CalendarTableLayoutPanel.ColumnCount = 7;
            this.CalendarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.CalendarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.CalendarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.CalendarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.CalendarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.CalendarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.CalendarTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.CalendarTableLayoutPanel.Location = new System.Drawing.Point(805, 81);
            this.CalendarTableLayoutPanel.Name = "CalendarTableLayoutPanel";
            this.CalendarTableLayoutPanel.RowCount = 7;
            this.CalendarTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CalendarTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CalendarTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CalendarTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CalendarTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CalendarTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CalendarTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CalendarTableLayoutPanel.Size = new System.Drawing.Size(8, 8);
            this.CalendarTableLayoutPanel.TabIndex = 0;
            // 
            // YearPanel
            // 
            this.YearPanel.AutoScroll = true;
            this.MainTableLayoutPanel.SetColumnSpan(this.YearPanel, 2);
            this.YearPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YearPanel.Location = new System.Drawing.Point(805, 31);
            this.YearPanel.Name = "YearPanel";
            this.YearPanel.Size = new System.Drawing.Size(1609, 43);
            this.YearPanel.TabIndex = 5;
            // 
            // hourLayoutPanel
            // 
            this.hourLayoutPanel.ColumnCount = 7;
            this.hourLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.hourLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.hourLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.hourLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.hourLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.hourLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.hourLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.hourLayoutPanel.Controls.Add(this.PMLabel, 1, 0);
            this.hourLayoutPanel.Controls.Add(this.AMLabel, 0, 0);
            this.hourLayoutPanel.Location = new System.Drawing.Point(805, 101);
            this.hourLayoutPanel.Name = "hourLayoutPanel";
            this.hourLayoutPanel.RowCount = 13;
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.hourLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.hourLayoutPanel.Size = new System.Drawing.Size(693, 490);
            this.hourLayoutPanel.TabIndex = 6;
            // 
            // PMLabel
            // 
            this.PMLabel.AutoSize = true;
            this.PMLabel.Location = new System.Drawing.Point(9, 0);
            this.PMLabel.Name = "PMLabel";
            this.PMLabel.Size = new System.Drawing.Size(0, 13);
            this.PMLabel.TabIndex = 1;
            // 
            // AMLabel
            // 
            this.AMLabel.AutoSize = true;
            this.AMLabel.Location = new System.Drawing.Point(3, 0);
            this.AMLabel.Name = "AMLabel";
            this.AMLabel.Size = new System.Drawing.Size(0, 13);
            this.AMLabel.TabIndex = 0;
            // 
            // calDataTableBindingSource
            // 
            this.calDataTableBindingSource.DataSource = typeof(FolderTrackGuiTest1.CalDataTable);
            // 
            // Cal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(983, 638);
            this.Controls.Add(this.MainTableLayoutPanel);
            this.Name = "Cal";
            this.Text = "Cal";
            this.MainTableLayoutPanel.ResumeLayout(false);
            this.MainTableLayoutPanel.PerformLayout();
            this.VersionPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CalDataGridView)).EndInit();
            this.hourLayoutPanel.ResumeLayout(false);
            this.hourLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calDataTableBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel CalendarTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel MonthTableLayoutPanel;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Panel VersionPanel;
        private System.Windows.Forms.Panel YearPanel;
        private System.Windows.Forms.DataGridView CalDataGridView;
        private System.Windows.Forms.BindingSource calDataTableBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn VersionDataCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserVersions;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotesDataCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeColumn;
        private System.Windows.Forms.TableLayoutPanel hourLayoutPanel;
        private System.Windows.Forms.Label PMLabel;
        private System.Windows.Forms.Label AMLabel;

    }
}