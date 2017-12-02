// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using Utilities.ComponentModel;

    /// <summary>
    /// The main Windows form
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The maximum value for the progress bar.
        /// </summary>
        private const int ProgressBarMaximumValue = 100;

        /// <summary>
        /// The minimum value for the progress bar.
        /// </summary>
        private const int ProgressBarMinimumValue = 0;

        /// <summary>
        /// A <see cref="LoggingBackgroundWorker"/>.
        /// </summary>
        private LoggingBackgroundWorker backgroundWorker;

        /// <summary>
        /// Denotes whether or not the application is in running state.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Denotes whether or not the application is in reversing state.
        /// </summary>
        private bool isReversing;

        /// <summary>
        /// Denotes whether all items should be enabled
        /// </summary>
        private bool enableAll;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class. 
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// A delegate to allow cross-thread UI component access
        /// </summary>
        public delegate void ToDoDelegate();

        public string[] CommandLineArgs => Environment.GetCommandLineArgs();

        /// <summary>
        /// Gets the singleton instance of this type
        /// </summary>
        public static MainForm Instance => new MainForm();

        /// <summary>
        /// Gets the log text box
        /// </summary>
        public RichTextBox LogTextBox => this.logTextBox;

        /// <summary>
        /// Appends a value to the log text box
        /// </summary>
        /// <param name="value">A value to append</param>
        /// <param name="isException">Indicates whether the value pertains to an exception</param>
        public void AppendToLogTextBox(string value, bool isException = false)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, bool>(this.AppendToLogTextBox), value, isException);
                return;
            }

            this.LogTextBox.AppendText(value, isException ? Color.Red : Color.Black);

            this.LogTextBox.Focus();
            this.LogTextBox.Select(this.LogTextBox.Text.Length, 0);
        }

        /// <summary>
        /// Occurs when <see cref="LoggingBackgroundWorker.Log"/> is called.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        public void BackgroundWorker_Log(object sender, LoggingEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ToDoDelegate(() => this.BackgroundWorker_Log(sender, e)));
                return;
            }

            if (!string.IsNullOrWhiteSpace(e.Message))
            {
                Logger.Instance.Log(e.Message);
                if (!string.IsNullOrWhiteSpace(this.logTextBox.Text)
                    && !e.Message.StartsWith("\r\n") && !e.Message.StartsWith("\n"))
                {
                    this.AppendToLogTextBox("\r\n");
                }

                this.AppendToLogTextBox(e.Message);
            }

            if (e.Exception != null)
            {
                Logger.Instance.Log(e.Exception);

                if (!string.IsNullOrWhiteSpace(this.logTextBox.Text))
                {
                    this.AppendToLogTextBox("\r\n");
                }

                this.AppendToLogTextBox($"Exception Thrown: \r\n{e.Exception.Message}\r\n{e.Exception.StackTrace}", true);
            }
        }

        /// <summary>
        /// Allows for minimizing to the system tray 
        /// </summary>
        /// <param name="m">A message</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                this.ShowMe();
            }

            // If this is WM_QUERYENDSESSION, the closing event should be fired in the base WndProc
            base.WndProc(ref m);
        }

        /// <summary>
        /// Occurs when <see cref="BackgroundWorker.ReportProgress(int)" /> is called.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="ProgressChangedEventArgs" /> object that contains the event data. </param>
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var progressPercentage = e.ProgressPercentage;
            if (progressPercentage > ProgressBarMaximumValue)
            {
                progressPercentage = ProgressBarMaximumValue;
            }

            // Update progress bar. The progress percentage is a property of e
            this.Invoke(new ToDoDelegate(() => this.progressBar1.Value = progressPercentage));
        }

        /// <summary>
        /// Occurs when the background operation has completed, has been canceled, or has raised an exception.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="RunWorkerCompletedEventArgs" /> object that contains the event data. </param>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Set the progress bar to maximum
            this.Invoke(new ToDoDelegate(() => this.progressBar1.Value = ProgressBarMaximumValue));

            // Re-enable the UI of the Main form
            this.Invoke(new ToDoDelegate(() => this.Enabled = true));

            // Indicate success!
            System.Media.SystemSounds.Asterisk.Play();
            this.Invoke(new ToDoDelegate(() => this.BackgroundWorker_Log(this, new LoggingEventArgs { Message = "Complete!" })));

            // If necessary, restart the Google Drive process
            if (SettingsFile.Data.EndRestartGoogleDriveProcess)
            {
                using (var pr = new Process())
                {
                    pr.StartInfo.FileName = @"C:\Program Files (x86)\Google\Drive\googledrivesync.exe";
                    pr.Start();
                }
            }

            this.isRunning = false;
            this.RefreshStatus();

            if (this.autoClosecheckBox.Checked)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Occurs when the <see cref="TextBox.Text"/> property value changes.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void DestinationPathTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ValidatePaths();
        }

        /// <summary>
        /// Occurs when the <see cref="exitMenuItem"/> is clicked.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Occurs when the <see cref="mainButton"/> is clicked.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void mainButton_Click(object sender, EventArgs e)
        {
            this.isReversing = false;

            Process();
        }

        /// <summary>
        /// Occurs before a form is displayed for the first time.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.RefreshDirectoryPaths();
            this.RefreshStatus();
            this.ValidatePaths();

            this.autoClosecheckBox.Checked = SettingsFile.Data.AutoClose;
            this.endRestartGoogleDriveProcessCheckbox.Checked = SettingsFile.Data.EndRestartGoogleDriveProcess;

            if (CommandLineArgs.Any(a => string.Equals(a, "/autostart", StringComparison.InvariantCultureIgnoreCase)))
            {
                this.mainButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Makes the bound directory paths on the <see cref="pathsDataGridView"/> clickable
        /// </summary>
        private void MakeDirectoryPathsClickable()
        {
            foreach (DataGridViewRow row in this.pathsDataGridView.Rows)
            {
                row.Cells[2] = new DataGridViewLinkCell { Value = row.Cells[1].Value };
                row.Cells[3] = new DataGridViewLinkCell { Value = row.Cells[2].Value };
            }
        }

        /// <summary>
        /// Attempts to open a file explorer window
        /// </summary>
        /// <param name="directoryPath">The path to open</param>
        private void OpenFileExplorerWindow(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                MessageBox.Show("Please specify a directory path.");
                return;
            }

            switch (Directory.Exists(directoryPath))
            {
                case true:
                    using (var pr = new Process())
                    {
                        pr.StartInfo.FileName = "EXPLORER";
                        pr.StartInfo.Arguments = $"/n, /e, \"{directoryPath}\"";
                        pr.Start();
                    }

                    break;
                case false:
                    MessageBox.Show($"Cannot find directory \"{directoryPath}\"");
                    break;
            }
        }

        /// <summary>
        /// Occurs when the content within a cell is clicked.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void PathsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && (this.pathsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewLinkCell)))
            {
                this.OpenFileExplorerWindow((this.pathsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell).Value.ToString());
            }

            if (e.RowIndex > -1 && (this.pathsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewCheckBoxCell)))
            {
                (this.pathsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell).Value =
                    !(bool)(this.pathsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell).Value;
                SettingsFile.Save();
                //SettingsFile.Load();
                //RefreshDirectoryPaths();
            }
        }

        /// <summary>
        /// Occurs when the user deletes a row from the <see cref="DataGridView"/> control.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void PathsDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            foreach (DataGridViewRow item in this.pathsDataGridView.SelectedRows)
            {
                SettingsFile.Data.Paths = SettingsFile.Data.Paths.Except(SettingsFile.Data.Paths.Where(d => d.Id == new Guid(item.Cells[0].Value.ToString()))).ToList();
            }

            SettingsFile.Save();
        }

        /// <summary>
        /// Occurs when the user clicks a column header.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void PathsDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (pathsDataGridView.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn &&
                string.Equals(pathsDataGridView.Columns[e.ColumnIndex].HeaderText, "Enabled", StringComparison.InvariantCultureIgnoreCase))
            {
                this.enableAll = !this.enableAll;
                foreach (DataGridViewRow row in this.pathsDataGridView.Rows)
                {
                    (row.Cells[1] as DataGridViewCheckBoxCell).Value = this.enableAll;
                }

                SettingsFile.Save();
            }

            this.MakeDirectoryPathsClickable();
        }

        /// <summary>
        /// Copy files
        /// </summary>
        private void Process()
        {
            this.isRunning = !this.isRunning;

            if (this.isRunning)
            {
                if (endRestartGoogleDriveProcessCheckbox.Checked)
                {
                    var runningProcesses = System.Diagnostics.Process.GetProcesses();
                    foreach (var process in runningProcesses)
                    {
                        try
                        {
                            if (string.Equals(process.MainModule.FileName,
                                              @"C:\Program Files (x86)\Google\Drive\googledrivesync.exe", 
                                              StringComparison.InvariantCultureIgnoreCase))
                            {
                                process.Kill();
                            }
                        }
                        catch (Exception e)
                        {
                        }

                        // now check the modules of the process
                        //foreach (ProcessModule module in process.Modules)
                        //{
                        //    if (module.FileName.Equals("googledrivesync.exe"))
                        //    {
                        //        process.Kill();
                        //    }
                        //}
                    }
                }

                this.progressBar1.Value = ProgressBarMinimumValue;

                this.backgroundWorker = new LoggingBackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };

                this.backgroundWorker.DoWork += this.BackgroundWorker_DoWork;
                this.backgroundWorker.Log += this.BackgroundWorker_Log;
                this.backgroundWorker.ProgressChanged += this.BackgroundWorker_ProgressChanged;
                this.backgroundWorker.RunWorkerCompleted += this.BackgroundWorker_RunWorkerCompleted;
                this.backgroundWorker.RunWorkerAsync();
            }
            else
            {
                CopyEngine.Instance.CancelRequestReceived = true;
                this.backgroundWorker.CancelAsync();

                // Reload persisted settings
                SettingsFile.Load();
                RefreshDirectoryPaths();
            }

            this.RefreshStatus();
        }

        /// <summary>
        /// Refreshes the directory paths
        /// </summary>
        private void RefreshDirectoryPaths()
        {
            this.pathsDataGridView.DataSource = null;
            var formattedGroupList = new SortableBindingList<ISetting>();
            foreach (var setting in SettingsFile.Data.Paths)
            {
                formattedGroupList.Add(setting);
            }

            this.pathsDataGridView.DataSource = formattedGroupList;
            this.pathsDataGridView.Invalidate();

            this.pathsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Remove ID column
            this.pathsDataGridView.Columns[0].Visible = false;

            // Set column headers
            this.pathsDataGridView.Columns[1].HeaderText = "Enabled";
            this.pathsDataGridView.Columns[2].HeaderText = "Source";
            this.pathsDataGridView.Columns[3].HeaderText = "Destination";

            this.pathsDataGridView.Columns[1].HeaderCell.Style.ForeColor = Color.Blue;

            this.MakeDirectoryPathsClickable();
        }

        /// <summary>
        /// Refresh the running status
        /// </summary>
        private void RefreshStatus()
        {
            this.toolStripStatusLabel1.ForeColor = SystemColors.WindowText;
            this.toolStripStatusLabel1.Text = string.Empty;

            switch (this.isRunning)
            {
                case true:
                    this.toolStripStatusLabel1.Text = "Running";
                    this.mainButton.Text = "Stop";
                    this.reverseButton.Text = "Stop";
                    break;
                case false:
                    this.toolStripStatusLabel1.Text = "Ready";
                    this.mainButton.Text = "GO!";
                    this.reverseButton.Text = "[Reverse]";
                    break;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="saveButton"/> is clicked.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.sourcePathTextBox.Text))
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "Source path text box contents is null or whitespace.";
                return;
            }

            if (string.IsNullOrWhiteSpace(this.destinationPathTextBox.Text))
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "Destination path text box contents is null or whitespace.";
                return;
            }

            if (!Directory.Exists(this.sourcePathTextBox.Text))
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "Source path does not exist.";
                return;
            }

            if (!Directory.Exists(this.destinationPathTextBox.Text))
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "Destination path does not exist.";
                return;
            }

            if (!string.IsNullOrWhiteSpace(this.sourcePathTextBox.Text)
                && !string.IsNullOrWhiteSpace(this.destinationPathTextBox.Text)
                && string.Compare(
                Path.GetFullPath(this.sourcePathTextBox.Text).TrimEnd('\\'),
                Path.GetFullPath(this.destinationPathTextBox.Text).TrimEnd('\\'),
                StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                this.toolStripStatusLabel1.ForeColor = Color.Red;
                this.toolStripStatusLabel1.Text = "The source path is the same as the destination path.";
                return;
            }

            foreach (var directoryPathPair in SettingsFile.Data.Paths)
            {
                if (directoryPathPair.Source == null || directoryPathPair.Destination == null)
                {
                    continue;
                }

                if (string.Compare(
                    Path.GetFullPath(this.sourcePathTextBox.Text).TrimEnd('\\'),
                    Path.GetFullPath(directoryPathPair.Source).TrimEnd('\\'),
                    StringComparison.InvariantCultureIgnoreCase) == 0 &&
                    string.Compare(
                    Path.GetFullPath(this.destinationPathTextBox.Text).TrimEnd('\\'),
                    Path.GetFullPath(directoryPathPair.Destination).TrimEnd('\\'),
                    StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    this.toolStripStatusLabel1.ForeColor = Color.Red;
                    this.toolStripStatusLabel1.Text = "The combination of these two paths has already been added.";
                    return;
                }
            }

            // TODO: Need the ability to select Google drive (etc)
            SettingsFile.Data.Paths.Add(new Setting
            {
                Id = Guid.NewGuid(),
                Enabled = true,
                Source = this.sourcePathTextBox.Text,
                Destination = this.destinationPathTextBox.Text,
                BackupMode = this.backupRadioButton.Checked && !this.synchronizeRadioButton.Checked
                                        ? BackupMode.Backup
                                        : BackupMode.Synchronise
            });

            try
            {
                SettingsFile.Save();
                this.RefreshDirectoryPaths();

                this.sourcePathTextBox.Text = string.Empty;
                this.destinationPathTextBox.Text = string.Empty;
                this.backupRadioButton.Checked = false;
                this.synchronizeRadioButton.Checked = true;
            }
            catch (Exception exception)
            {
                this.BackgroundWorker_Log(this, new LoggingEventArgs { Exception = exception });
                throw;
            }
        }

        /// <summary>
        /// Makes the application UI visible
        /// </summary>
        private void ShowMe()
        {
            if (this.Visible && this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            // get our current "TopMost" value (ours will always be false though)
            var top = this.TopMost;

            // make our form jump to the top of everything
            this.TopMost = true;

            // set it back to whatever it was
            this.TopMost = top;
        }

        /// <summary>
        /// Occurs when the <see cref="sourcePathTextBox"/> text property value changes.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void SourcePathTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ValidatePaths();
        }

        /// <summary>
        /// Validates the paths
        /// </summary>
        private void ValidatePaths()
        {
            this.saveButton.Enabled = true;

            this.sourcePathTextBox.ForeColor = SystemColors.WindowText;

            if (string.IsNullOrWhiteSpace(this.sourcePathTextBox.Text)
                || !Directory.Exists(this.sourcePathTextBox.Text))
            {
                this.sourcePathTextBox.ForeColor = Color.Red;
                this.saveButton.Enabled = false;
            }

            this.destinationPathTextBox.ForeColor = SystemColors.WindowText;

            if (string.IsNullOrWhiteSpace(this.destinationPathTextBox.Text)
                || !Directory.Exists(this.destinationPathTextBox.Text))
            {
                this.destinationPathTextBox.ForeColor = Color.Red;
                this.saveButton.Enabled = false;
            }

            if (!string.IsNullOrWhiteSpace(this.sourcePathTextBox.Text)
                && !string.IsNullOrWhiteSpace(this.destinationPathTextBox.Text)
                && string.Compare(
                Path.GetFullPath(this.sourcePathTextBox.Text).TrimEnd('\\'),
                Path.GetFullPath(this.destinationPathTextBox.Text).TrimEnd('\\'),
                StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                this.sourcePathTextBox.ForeColor = Color.Red;
                this.destinationPathTextBox.ForeColor = Color.Red;
                this.saveButton.Enabled = false;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="autoClosecheckBox"/> CheckedChanged property changes.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void AutoClosecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsFile.Data.AutoClose = this.autoClosecheckBox.Checked;
            SettingsFile.Save();
        }

        /// <summary>
        /// Occurs when <see cref="BackgroundWorker.RunWorkerAsync()"/> is called.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="DoWorkEventArgs" /> object that contains the event data. </param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Clear the log text box
                this.Invoke(new ToDoDelegate(() => this.logTextBox.Text = string.Empty));

                // Log "started" message
                this.Invoke(new ToDoDelegate(() => this.BackgroundWorker_Log(this, new LoggingEventArgs { Message = "Started!" })));

                // Reset the progress bar to the minimum value
                this.progressBar1.Value = ProgressBarMinimumValue;

                if (isReversing)
                {
                    SwapSettings();
                }

                // Start the mapping!
                CopyEngine.Instance.Process(this.backgroundWorker);
            }
            catch (AggregateException ae)
            {
                foreach (var ee in ae.InnerExceptions)
                {
                    this.Invoke(new ToDoDelegate(() => this.BackgroundWorker_Log(this, new LoggingEventArgs { Exception = ee })));
                }
            }
            catch (Exception ee)
            {
                this.Invoke(new ToDoDelegate(() => this.BackgroundWorker_Log(this, new LoggingEventArgs { Exception = ee })));
            }
            finally
            {
                // Revert settings
                isReversing = false;
                SettingsFile.Load();
            }

        }

        /// <summary>
        /// Occurs when the value of the <see cref="endRestartGoogleDriveProcessCheckbox"/> CheckedChanged property changes.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data. </param>
        private void endRestartGoogleDriveProcessCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsFile.Data.EndRestartGoogleDriveProcess = this.endRestartGoogleDriveProcessCheckbox.Checked;
            SettingsFile.Save();
        }

        /// <summary>
        /// Occurs when <see cref="reverseButton"/> is clicked.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">An <see cref="EventArgs" /> object that contains the event data.</param>
        private void reverseButton_Click(object sender, EventArgs e)
        {
            // Confirmation
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to reverse copy?", "Confirm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            // Denote reversing
            isReversing = true;

            // GO!
            this.Process();
        }

        /// <summary>
        /// Swaps the source paths with the destinations.
        /// Doesn't persist to file.
        /// </summary>
        private void SwapSettings()
        {
            var oldSettings = SettingsFile.Data.Paths.ToList();
            SettingsFile.Data.Paths.Clear();
            foreach (var setting in oldSettings)
            {
                SettingsFile.Data.Paths.Add(new Setting
                {
                    BackupMode = setting.BackupMode,
                    Destination = setting.Source,
                    Source = setting.Destination,
                    Id = setting.Id,
                    Enabled = setting.Enabled
                });
            }
        }
    }
}
