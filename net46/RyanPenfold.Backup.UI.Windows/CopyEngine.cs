// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyEngine.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// An implementation of a copying engine 
    /// </summary>
    public class CopyEngine : ICopyEngine
    {
        /// <summary>
        /// An instance of a <see cref="ICopyEngine"/>
        /// </summary>
        private static ICopyEngine instance;

        /// <summary>
        /// Keeps a count of the amount of settings completed
        /// </summary>
        private double qtySettingsCompleted;

        /// <summary>
        /// Keeps a count of the amount of objects completed
        /// </summary>
        private double runningObjectCount = -1;

        /// <summary>
        /// A count of the total objects
        /// </summary>
        private double totalObjectCount = -1;

        /// <summary>
        /// Gets or sets the singleton instance of a <see cref="ICopyEngine"/>
        /// </summary>
        public static ICopyEngine Instance => instance ?? (instance = new CopyEngine());

        /// <summary>
        /// Gets or sets a <see cref="BackgroundWorker"/> instance
        /// </summary>
        public BackgroundWorker BackgroundWorker { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a cancel request has been received
        /// </summary>
        public bool CancelRequestReceived { get; set; }
       
        /// <summary>
        /// Copies the contents of a source to a destination based on an individual setting
        /// </summary>
        /// <param name="setting">Contains information on a source and destination pair</param>
        /// <param name="loggingBackgroundWorker">A <see cref="BackgroundWorker"/> instance to provide updates to</param>
        public void Copy(ISetting setting, LoggingBackgroundWorker loggingBackgroundWorker)
        {
            if (setting == null)
            {
                throw new ArgumentNullException(nameof(setting));
            }

            if (loggingBackgroundWorker == null)
            {
                throw new ArgumentNullException(nameof(loggingBackgroundWorker));
            }

            if (string.IsNullOrWhiteSpace(setting.Source))
            {
                try
                {
                    throw new Exception("Source path is null or whitespace.");
                }
                catch (Exception e)
                {
                    loggingBackgroundWorker.OnLog(this, new LoggingEventArgs { Exception = e });
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(setting.Destination))
            {
                try
                {
                    throw new Exception("Destination path is null or whitespace.");
                }
                catch (Exception e)
                {
                    loggingBackgroundWorker.OnLog(this, new LoggingEventArgs { Exception = e });
                    return;
                }
            }

            if (!Directory.Exists(setting.Source))
            {
                try
                {
                    throw new Exception($"Source path {setting.Source} doesn't exist on disk.");
                }
                catch (Exception e)
                {
                    loggingBackgroundWorker.OnLog(this, new LoggingEventArgs { Exception = e });
                    return;
                }
            }

            if (!Directory.Exists(setting.Destination))
            {
                try
                {
                    Utilities.IO.Directory.CreateDirectory(setting.Destination);
                    // throw new Exception($"Destination path {setting.Destination} doesn't exist on disk.");
                }
                catch (Exception e)
                {
                    loggingBackgroundWorker.OnLog(this, new LoggingEventArgs { Exception = e });
                    return;
                }                
            }

            loggingBackgroundWorker.OnLog(this, new LoggingEventArgs { Message = $"Counting objects in {setting.Source}" });

            this.totalObjectCount = Utilities.IO.Directory.CountObjects(setting.Source, true);

            loggingBackgroundWorker.OnLog(this, new LoggingEventArgs { Message = $"Files and folders in source directory: {this.totalObjectCount}." });
            loggingBackgroundWorker.OnLog(this, new LoggingEventArgs { Message = $"Attempting to copy from {setting.Source} \r\nto {setting.Destination}." });

            // Note: if files / subdirs are added to a directory, the lastwritetime reflects the time they were added.
            // However, if files / subdirs are added to a directory's subdir, the lastwritetime is not updated
            // Therefore, only iterate through a directorys subdirs not its files when the lastwritetime isn't more recent than the destination equivalent
            this.CopyIfNewer(setting.Source, setting.Destination, setting.BackupMode, loggingBackgroundWorker);

            loggingBackgroundWorker.OnLog(this, new LoggingEventArgs { Message = "Done!" });
        }

        /// <summary>
        /// Copies a directory if it's been updated more recently than the destination equivalent,
        /// including all files and sub directories.
        /// </summary>
        /// <param name="sourceDirectoryPath">The path to the source directory.</param>
        /// <param name="destinationDirectoryPath">The path to the destination directory.</param>
        /// <param name="mode">The backup mode</param>
        /// <param name="loggingBackgroundWorker">A <see cref="LoggingBackgroundWorker"/></param>
        /// <remarks>
        /// Based on the example @ https://msdn.microsoft.com/en-us/library/bb762914(v=vs.110).aspx
        /// </remarks>
        // ReSharper disable once StyleCop.SA1650
        public void CopyIfNewer(string sourceDirectoryPath, string destinationDirectoryPath, BackupMode mode, LoggingBackgroundWorker loggingBackgroundWorker)
        {
            // Get the subdirectories for the specified directory.
            var sourceDirectory = new DirectoryInfo(sourceDirectoryPath);

            if (!sourceDirectory.Exists)
            {
                loggingBackgroundWorker.OnLog(this, new LoggingEventArgs { Message = $"Source directory does not exist or could not be found: {sourceDirectoryPath}" });
            }

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destinationDirectoryPath))
            {
                Utilities.IO.Directory.CreateDirectory(destinationDirectoryPath);
            }

            // Get the files in the directory and copy them to the new location.
            foreach (var file in sourceDirectory.GetFiles())
            {
                var destinationFilePath = Path.Combine(destinationDirectoryPath, file.Name);

                var sourceFileInfo = file;

                if (File.Exists(destinationFilePath))
                {
                    var destinationFileInfo = new FileInfo(destinationFilePath);
                    if (sourceFileInfo.LastWriteTime > destinationFileInfo.LastWriteTime)
                    {
                        try
                        {
                            Utilities.IO.File.CopyTo(file.FullName, destinationFilePath);
                        }
                        catch (Exception e)
                        {
                            loggingBackgroundWorker?.OnLog(this, new LoggingEventArgs { Message = $"Exception when trying to copy file from {file.FullName} to {destinationFilePath}.", Exception = e });
                        }
                    }
                }
                else
                {
                    try
                    {
                        Utilities.IO.File.CopyTo(file.FullName, destinationFilePath);
                    }
                    catch (Exception e)
                    {
                        loggingBackgroundWorker?.OnLog(this, new LoggingEventArgs { Message = $"Exception when trying to copy file from {file.FullName} to {destinationFilePath}.", Exception = e });
                    }
                }

                this.runningObjectCount++;
                var value = Convert.ToInt32((((this.qtySettingsCompleted * this.totalObjectCount) + this.runningObjectCount) / (SettingsFile.Data.Paths.Count * this.totalObjectCount)) * 100.0);

                loggingBackgroundWorker?.ReportProgress(value);
            }

            if (mode == BackupMode.Synchronise)
            {
                // Delete any files on destination directory that aren't in the source directory
                var destinationDirectory = new DirectoryInfo(destinationDirectoryPath);
                foreach (var destinationFile in destinationDirectory.GetFiles().Where(df => !sourceDirectory.GetFiles().Any(sf => string.Equals(df.Name, sf.Name))))
                {
                    try
                    {
                        // destinationFile.Delete();
                        Utilities.IO.File.Delete(destinationFile.FullName);
                    }
                    catch (Exception e)
                    {
                        loggingBackgroundWorker?.OnLog(this, new LoggingEventArgs { Message = $"Exception when trying to delete file {destinationFile.FullName}.", Exception = e });
                    }
                }

                // Delete any directories on destination directory that aren't in the source directory
                foreach (var destinationDir in destinationDirectory.GetDirectories().Where(dd => !sourceDirectory.GetDirectories().Any(sd => string.Equals(dd.Name, sd.Name))))
                {
                    try
                    {
                        // destinationDir.Delete();
                        Utilities.IO.Directory.Delete(destinationDir.FullName);
                    }
                    catch (Exception e)
                    {
                        loggingBackgroundWorker?.OnLog(this, new LoggingEventArgs { Message = $"Exception when trying to delete directory {destinationDir.FullName}.", Exception = e });
                    }
                }
            }

            // Copy the subdirectories and their contents to new location.
            foreach (var subdirPath in Directory.GetDirectories(sourceDirectoryPath))
            {
                try
                {
                    // Path.GetDirectoryName throws PathTooLongException
                    var subdirectoryInfo = new DirectoryInfo(subdirPath);
                    this.CopyIfNewer(subdirPath, Path.Combine(destinationDirectoryPath, subdirectoryInfo.Name), mode, loggingBackgroundWorker);
                }
                catch (Exception e)
                {
                    loggingBackgroundWorker?.OnLog(this, new LoggingEventArgs { Message = $"Exception when trying to copy from {subdirPath} to {destinationDirectoryPath}.", Exception = e });
                }

                this.runningObjectCount++;
                var value = Convert.ToInt32((((this.qtySettingsCompleted * this.totalObjectCount) + this.runningObjectCount) / (SettingsFile.Data.Paths.Count * this.totalObjectCount)) * 100.0);
                loggingBackgroundWorker?.ReportProgress(value);
            }
        }

        /// <summary>
        /// Invokes the <see cref="ICopyEngine"/>'s primary copy function
        /// </summary>
        public void Process()
        {
            this.Process(null);
        }

        /// <summary>
        /// Invokes the <see cref="ICopyEngine"/>'s primary copy function
        /// </summary>
        /// <param name="backgroundWorker">Receives updates on the method's progress</param>
        public void Process(LoggingBackgroundWorker backgroundWorker)
        {
            this.BackgroundWorker = backgroundWorker;
            backgroundWorker?.ReportProgress(0);

            this.qtySettingsCompleted = 0;
            foreach (var setting in SettingsFile.Data.Paths.Where(p => p.Enabled))
            {
                this.runningObjectCount = 0;
                this.Copy(setting, backgroundWorker);
                this.qtySettingsCompleted++;
                backgroundWorker?.ReportProgress(Convert.ToInt32((this.qtySettingsCompleted / SettingsFile.Data.Paths.Count) * 100.0));
            }
        }
    }
}
