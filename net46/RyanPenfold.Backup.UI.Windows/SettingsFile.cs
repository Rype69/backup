// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsFile.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace RyanPenfold.Backup.UI.Windows
{
    using System;
    using System.IO;

    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// A settings file
    /// </summary>
    public class SettingsFile : ISettingsFile
    {
        /// <summary>
        /// The path to a settings file
        /// </summary>
        private static string settingsFilePath;

        /// <summary>
        /// Initializes static members of the <see cref="SettingsFile"/> class. 
        /// </summary>
        static SettingsFile()
        {
            Load();
        }

        /// <summary>
        /// Gets or sets the data contained within a settings file
        /// </summary>
        public static SettingsInfo Data { get; set; }

        /// <summary>
        /// Gets the path to a settings file
        /// </summary>
        public static string SettingsFilePath
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(settingsFilePath))
                {
                    return settingsFilePath;
                }

                var settingsfileSwitchFound = false;
                var commandLineArgs = Environment.GetCommandLineArgs();
                foreach (var commandLineArg in commandLineArgs)
                {
                    if (settingsfileSwitchFound)
                    {
                        settingsFilePath = commandLineArg;
                    }

                    if (string.Equals(commandLineArg, "/settingsfile", StringComparison.InvariantCultureIgnoreCase))
                    {
                        settingsfileSwitchFound = true;
                    }
                }

                if (!File.Exists(settingsFilePath) && settingsfileSwitchFound)
                {
                    settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settingsFilePath);
                }

                if (!File.Exists(settingsFilePath) && !settingsfileSwitchFound)
                {
                    settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"settings.{Environment.MachineName}.dat");
                }

                if (!File.Exists(settingsFilePath))
                {
                    settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.dat");
                }

                return settingsFilePath;
            }
        }

        /// <summary>
        /// Loads the settings file
        /// </summary>
        public static void Load()
        {
            // The default contents of the file is a fresh one
            Data = new SettingsInfo();

            // Only attempt to read the file if it exists
            if (!File.Exists(SettingsFilePath))
            {
                return;
            }

            // Read the contents of the file as a string
            var serialised = File.ReadAllText(SettingsFilePath);

            // If the contents is null, empty, or whitespace, set the settings to default
            if (string.IsNullOrWhiteSpace(serialised))
            {
                return;
            }

            // Attempt to deserialise the settings instance
            try
            {
                Data = JsonConvert.DeserializeObject<SettingsInfo>(serialised);
            }
            catch (Exception exception)
            {
                MainForm.Instance.Invoke(new MainForm.ToDoDelegate(() => MainForm.Instance.BackgroundWorker_Log(new object(), new LoggingEventArgs { Exception = exception })));
                Data = new SettingsInfo();
            }
        }

        /// <summary>
        /// Saves the settings file
        /// </summary>
        public static void Save()
        {
            // If the file if it exists, delete it
            if (File.Exists(SettingsFilePath))
            {
                File.Delete(SettingsFilePath);
            }

            // Write the data
            File.WriteAllText(SettingsFilePath, JsonConvert.SerializeObject(Data));
        }
    }
}
