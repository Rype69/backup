// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RichTextBoxExtensions.cs" company="Ryan Penfold">
//     Copyright © Ryan Penfold. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace RyanPenfold.Backup.UI.Windows
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Provides extension methods for <see cref="RichTextBox"/> instances
    /// </summary>
    public static class RichTextBoxExtensions
    {
        /// <summary>
        /// Appends some text to a <see cref="RichTextBox"/> instance
        /// </summary>
        /// <param name="box">A <see cref="RichTextBox"/> instance</param>
        /// <param name="text">Some text</param>
        /// <param name="color">A <see cref="Color"/></param>
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
