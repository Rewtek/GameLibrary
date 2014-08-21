#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemHelper.cs" company="RewTek Network">
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

namespace Rewtek.GameLibrary.Common
{
    #region Using directives

    using global::System;
    using global::System.Diagnostics;
    using global::System.Threading;

    #endregion

    public static class SystemHelper
    {
        // Properties
        /// <summary>
        /// Gets a value indicating whether the game library is currently running on mac.
        /// </summary>
        public static bool IsMac
        {
            get { return Environment.OSVersion.Platform == PlatformID.MacOSX; }
        }

        /// <summary>
        /// Gets a value indicating whether the game library is currently running on linux.
        /// </summary>
        public static bool IsLinux
        {
            get { return Environment.OSVersion.Platform == PlatformID.Unix; }
        }

        /// <summary>
        /// Gets a value indicating whether the game library is currently running on windows.
        /// </summary>
        public static bool IsWindows
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Win32NT ||
                       Environment.OSVersion.Platform == PlatformID.Win32S ||
                       Environment.OSVersion.Platform == PlatformID.Win32Windows ||
                       Environment.OSVersion.Platform == PlatformID.WinCE;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the game library is currently running on Xbox.
        /// </summary>
        public static bool IsXbox
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Xbox;
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether a debugger is attached.
        /// </summary>
        public static bool IsDebuggerAttached
        {
            get { return Debugger.IsAttached; }
        }

        /// <summary>
        /// Gets a <see cref="System.PlatformID"/> enumeration value that identifies the operating system platform.
        /// </summary>
        public static PlatformID Platform 
        { 
            get { return Environment.OSVersion.Platform; } 
        }

        /// <summary>
        /// Gets the current culture name.
        /// </summary>
        public static string CultureName 
        {
            get { return Thread.CurrentThread.CurrentUICulture.Name; }
        }

        /// <summary>
        /// Gets the current native culture name.
        /// </summary>
        public static string CultureNativeName
        {
            get { return Thread.CurrentThread.CurrentUICulture.NativeName.Split(' ')[0]; }
        }
    }
}
