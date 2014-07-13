#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common\MessageBoxIcon.cs" company="RewTek Network">
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

/// <summary>
/// Specifies constants defining which information to display.
/// </summary>
public enum MsgBoxIcon
{
    /// <summary>
    /// The message box contain no symbols.
    /// </summary>
    None = 0,
    /// <summary>
    /// The message box contains a symbol consisting of white X in a circle with
    /// a red background.
    /// </summary>
    Error = 16,
    /// <summary>
    /// The message box contains a symbol consisting of an exclamation point in a
    /// triangle with a yellow background.
    /// </summary>
    Warning = 48,
    /// <summary>
    /// The message box contains a symbol consisting of a lowercase letter i in a
    /// circle.
    /// </summary>
    Information = 64,
    /// <summary>
    /// The message box contains a symbol consisting of a question mark in a circle.
    /// </summary>
    Question = 32,
}