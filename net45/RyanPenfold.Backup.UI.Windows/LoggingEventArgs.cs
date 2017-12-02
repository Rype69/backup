namespace RyanPenfold.Backup.UI.Windows
{
    using System;

    public class LoggingEventArgs : EventArgs
    {
        public Exception Exception { get; set; }

        public string Message { get; set; }
    }
}
