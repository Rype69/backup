namespace RyanPenfold.Backup.UI.Windows
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pathsDataGridView = new System.Windows.Forms.DataGridView();
            this.pathsLabel = new System.Windows.Forms.Label();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.synchronizeRadioButton = new System.Windows.Forms.RadioButton();
            this.backupRadioButton = new System.Windows.Forms.RadioButton();
            this.saveButton = new System.Windows.Forms.Button();
            this.destinationPathTextBox = new System.Windows.Forms.TextBox();
            this.sourcePathTextBox = new System.Windows.Forms.TextBox();
            this.destinationPathLabel = new System.Windows.Forms.Label();
            this.sourcePathLabel = new System.Windows.Forms.Label();
            this.mainButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.logLabel = new System.Windows.Forms.Label();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.autoClosecheckBox = new System.Windows.Forms.CheckBox();
            this.reverseButton = new System.Windows.Forms.Button();
            this.endRestartGoogleDriveProcessCheckbox = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pathsDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.statusStrip1.Size = new System.Drawing.Size(854, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // pathsDataGridView
            // 
            this.pathsDataGridView.AllowUserToAddRows = false;
            this.pathsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pathsDataGridView.Location = new System.Drawing.Point(8, 24);
            this.pathsDataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.pathsDataGridView.MultiSelect = false;
            this.pathsDataGridView.Name = "pathsDataGridView";
            this.pathsDataGridView.ReadOnly = true;
            this.pathsDataGridView.RowTemplate.Height = 28;
            this.pathsDataGridView.Size = new System.Drawing.Size(834, 227);
            this.pathsDataGridView.TabIndex = 10;
            this.pathsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PathsDataGridView_CellContentClick);
            this.pathsDataGridView.ColumnHeaderMouseClick += this.PathsDataGridView_ColumnHeaderMouseClick;
            // 
            // pathsLabel
            // 
            this.pathsLabel.AutoSize = true;
            this.pathsLabel.Location = new System.Drawing.Point(4, 9);
            this.pathsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pathsLabel.Name = "pathsLabel";
            this.pathsLabel.Size = new System.Drawing.Size(37, 13);
            this.pathsLabel.TabIndex = 11;
            this.pathsLabel.Text = "Paths:";
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.exitMenuItem});
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 0;
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.synchronizeRadioButton);
            this.groupBox1.Controls.Add(this.backupRadioButton);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.destinationPathTextBox);
            this.groupBox1.Controls.Add(this.sourcePathTextBox);
            this.groupBox1.Controls.Add(this.destinationPathLabel);
            this.groupBox1.Controls.Add(this.sourcePathLabel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(438, 260);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(404, 117);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Paths";
            // 
            // synchronizeRadioButton
            // 
            this.synchronizeRadioButton.AutoSize = true;
            this.synchronizeRadioButton.Checked = true;
            this.synchronizeRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.synchronizeRadioButton.Location = new System.Drawing.Point(95, 91);
            this.synchronizeRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.synchronizeRadioButton.Name = "synchronizeRadioButton";
            this.synchronizeRadioButton.Size = new System.Drawing.Size(83, 17);
            this.synchronizeRadioButton.TabIndex = 23;
            this.synchronizeRadioButton.TabStop = true;
            this.synchronizeRadioButton.Text = "Synchronize";
            this.synchronizeRadioButton.UseVisualStyleBackColor = true;
            // 
            // backupRadioButton
            // 
            this.backupRadioButton.AutoSize = true;
            this.backupRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backupRadioButton.Location = new System.Drawing.Point(7, 91);
            this.backupRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.backupRadioButton.Name = "backupRadioButton";
            this.backupRadioButton.Size = new System.Drawing.Size(86, 17);
            this.backupRadioButton.TabIndex = 22;
            this.backupRadioButton.Text = "Backup Only";
            this.backupRadioButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(180, 91);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(50, 20);
            this.saveButton.TabIndex = 19;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // destinationPathTextBox
            // 
            this.destinationPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationPathTextBox.Location = new System.Drawing.Point(7, 70);
            this.destinationPathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.destinationPathTextBox.Name = "destinationPathTextBox";
            this.destinationPathTextBox.Size = new System.Drawing.Size(395, 20);
            this.destinationPathTextBox.TabIndex = 18;
            this.destinationPathTextBox.TextChanged += new System.EventHandler(this.DestinationPathTextBox_TextChanged);
            // 
            // sourcePathTextBox
            // 
            this.sourcePathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourcePathTextBox.Location = new System.Drawing.Point(7, 36);
            this.sourcePathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.sourcePathTextBox.Name = "sourcePathTextBox";
            this.sourcePathTextBox.Size = new System.Drawing.Size(395, 20);
            this.sourcePathTextBox.TabIndex = 17;
            this.sourcePathTextBox.TextChanged += new System.EventHandler(this.SourcePathTextBox_TextChanged);
            // 
            // destinationPathLabel
            // 
            this.destinationPathLabel.AutoSize = true;
            this.destinationPathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationPathLabel.Location = new System.Drawing.Point(4, 55);
            this.destinationPathLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.destinationPathLabel.Name = "destinationPathLabel";
            this.destinationPathLabel.Size = new System.Drawing.Size(88, 13);
            this.destinationPathLabel.TabIndex = 16;
            this.destinationPathLabel.Text = "Destination Path:";
            // 
            // sourcePathLabel
            // 
            this.sourcePathLabel.AutoSize = true;
            this.sourcePathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourcePathLabel.Location = new System.Drawing.Point(4, 21);
            this.sourcePathLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.sourcePathLabel.Name = "sourcePathLabel";
            this.sourcePathLabel.Size = new System.Drawing.Size(69, 13);
            this.sourcePathLabel.TabIndex = 15;
            this.sourcePathLabel.Text = "Source Path:";
            // 
            // mainButton
            // 
            this.mainButton.Location = new System.Drawing.Point(427, 423);
            this.mainButton.Margin = new System.Windows.Forms.Padding(2);
            this.mainButton.Name = "mainButton";
            this.mainButton.Size = new System.Drawing.Size(415, 35);
            this.mainButton.TabIndex = 23;
            this.mainButton.Text = "GO!";
            this.mainButton.UseVisualStyleBackColor = true;
            this.mainButton.Click += new System.EventHandler(this.mainButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(7, 383);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(835, 31);
            this.progressBar1.TabIndex = 22;
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(8, 260);
            this.logLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(25, 13);
            this.logLabel.TabIndex = 24;
            this.logLabel.Text = "Log";
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(8, 275);
            this.logTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(427, 98);
            this.logTextBox.TabIndex = 25;
            this.logTextBox.Text = "";
            // 
            // autoClosecheckBox
            // 
            this.autoClosecheckBox.AutoSize = true;
            this.autoClosecheckBox.Location = new System.Drawing.Point(612, 5);
            this.autoClosecheckBox.Name = "autoClosecheckBox";
            this.autoClosecheckBox.Size = new System.Drawing.Size(230, 17);
            this.autoClosecheckBox.TabIndex = 26;
            this.autoClosecheckBox.Text = "Automatically close window when complete";
            this.autoClosecheckBox.UseVisualStyleBackColor = true;
            this.autoClosecheckBox.CheckedChanged += new System.EventHandler(this.AutoClosecheckBox_CheckedChanged);
            // 
            // reverseButton
            // 
            this.reverseButton.Location = new System.Drawing.Point(7, 423);
            this.reverseButton.Margin = new System.Windows.Forms.Padding(2);
            this.reverseButton.Name = "reverseButton";
            this.reverseButton.Size = new System.Drawing.Size(415, 35);
            this.reverseButton.TabIndex = 27;
            this.reverseButton.Text = "[Reverse]";
            this.reverseButton.UseVisualStyleBackColor = true;
            this.reverseButton.Click += new System.EventHandler(this.reverseButton_Click);
            // 
            // endRestartGoogleDriveProcessCheckbox
            // 
            this.endRestartGoogleDriveProcessCheckbox.AutoSize = true;
            this.endRestartGoogleDriveProcessCheckbox.Location = new System.Drawing.Point(416, 5);
            this.endRestartGoogleDriveProcessCheckbox.Name = "endRestartGoogleDriveProcessCheckbox";
            this.endRestartGoogleDriveProcessCheckbox.Size = new System.Drawing.Size(195, 17);
            this.endRestartGoogleDriveProcessCheckbox.TabIndex = 28;
            this.endRestartGoogleDriveProcessCheckbox.Text = "End / Restart Google Drive process";
            this.endRestartGoogleDriveProcessCheckbox.UseVisualStyleBackColor = true;
            this.endRestartGoogleDriveProcessCheckbox.CheckedChanged += new System.EventHandler(this.endRestartGoogleDriveProcessCheckbox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 482);
            this.Controls.Add(this.endRestartGoogleDriveProcessCheckbox);
            this.Controls.Add(this.reverseButton);
            this.Controls.Add(this.autoClosecheckBox);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.mainButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pathsLabel);
            this.Controls.Add(this.pathsDataGridView);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(870, 521);
            this.MinimumSize = new System.Drawing.Size(870, 521);
            this.Name = "MainForm";
            this.Text = "Rype Backup";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pathsDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridView pathsDataGridView;
        private System.Windows.Forms.Label pathsLabel;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton synchronizeRadioButton;
        private System.Windows.Forms.RadioButton backupRadioButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox destinationPathTextBox;
        private System.Windows.Forms.TextBox sourcePathTextBox;
        private System.Windows.Forms.Label destinationPathLabel;
        private System.Windows.Forms.Label sourcePathLabel;
        private System.Windows.Forms.Button mainButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.CheckBox autoClosecheckBox;
        private System.Windows.Forms.Button reverseButton;
        private System.Windows.Forms.CheckBox endRestartGoogleDriveProcessCheckbox;
    }
}

