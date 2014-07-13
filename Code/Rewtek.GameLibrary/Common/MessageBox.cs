#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common\MessageBox.cs" company="RewTek Network">
//      Copyright (c) 2014 Rewtek Network (www.rewtek.net)
//      
//      Permission is hereby granted, free of charge, to any person obtaining a copy
//      of this software and associated documentation files (the "Software"), to deal
//      in the Software without restriction, including without limitation the rights
//      to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//      copies of the Software, and to permit persons to whom the Software is
//      furnished to do so, subject to the following conditions:
//      
//      The above copyright notice and this permission notice shall be included in all
//      copies or substantial portions of the Software.
//      
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//      FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//      AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//      LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//      OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//      SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
#endregion

#region Using directives

using global::System;
using global::System.Windows.Forms;

#endregion

/// <summary>
/// Displays a message box that can contain text, buttons, and symbols that inform
/// and instruct the user.
/// </summary>
public static class MsgBox
{
    /// <summary>
    /// Displays a message box with specified text.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    public static void Show(string text)
    {
        MessageBox.Show(text);
    }

    /// <summary>
    /// Displays a message box with specified value.
    /// </summary>
    /// <param name="value">The value to be display as text in the message box.</param>
    public static void Show(object value)
    {
        MessageBox.Show(value.ToString());
    }

    /// <summary>
    ///  Displays a message box with specified text.
    /// </summary>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="text">The text to display in the message box.</param>
    public static void Show(string caption, string text)
    {
        MessageBox.Show(text, caption);
    }

    /// <summary>
    ///  Displays a message box with specified text.
    /// </summary>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="format">The formated text to display in the message box.</param>
    /// <param name="args">An array of objects to display using format.</param>
    public static void Show(string caption, string format, params object[] args)
    {
        System.Console.WriteLine();
        MessageBox.Show(string.Format(format, args), caption);
    }

    /// <summary>
    /// Displays a message box with specified text.
    /// </summary>
    /// <param name="icon">One of the MessageBoxIcon values that specifies which
    /// icon to display in the message box.</param>
    /// <param name="text">The text to display in the title bar of the message box.</param>
    public static void Show(MsgBoxIcon icon, string text)
    {
        MessageBox.Show(text);
    }

    /// <summary>
    /// Displays a message box with specified text.
    /// </summary>
    /// <param name="icon">One of the MessageBoxIcon values that specifies which
    /// icon to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="text">The text to display in the title bar of the message box.</param>
    public static void Show(MsgBoxIcon icon, string caption, string text)
    {
        MessageBox.Show(text, caption, MessageBoxButtons.OK, (MessageBoxIcon)icon);
    }

    /// <summary>
    /// Displays a message box with specified text.
    /// </summary>
    /// <param name="icon">One of the MessageBoxIcon values that specifies which
    /// icon to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box.</param>
    /// <param name="format">The formated text to display in the message box.</param>
    /// <param name="args">An array of objects to display using format.</param>
    public static void Show(MsgBoxIcon icon, string caption, string format, params object[] args)
    {
        MessageBox.Show(string.Format(format, args), caption, MessageBoxButtons.OK, (MessageBoxIcon)icon);
    }
}