#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentManager.cs" company="RewTek Network">
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

namespace Rewtek.GameLibrary
{
    #region Using directives

    using global::System;
    using global::System.Diagnostics;
    using global::System.Reflection;

    #endregion

    public static class Reflector
    {
        /// <summary>
        /// Returns a <see cref="System.String"/> that contains the caller class and method name.
        /// </summary>
        /// <returns>A <see cref="System.String"/>, containing the caller class and method name.</returns>
        public static string GetCaller()
        {
            var trace = new StackTrace();
            var callerMethodName = trace.GetFrame(1).GetMethod().Name;
            var callerClassName = trace.GetFrame(1).GetMethod().ReflectedType.Name;

            return callerClassName + "::" + callerMethodName;
        }

        /// <summary>
        /// Returns a <see cref="Rewtek.GameLibrary.Common.CallerInfo"/> that contains the caller information.
        /// </summary>
        /// <returns>A  <see cref="Rewtek.GameLibrary.Common.CallerInfo"/>, containing the caller information.</returns>
        public static CallerInfo GetCallerInfo()
        {
            return new CallerInfo(new StackTrace(true));
        }
    }

    public struct CallerInfo
    {
        // Properties
        public string ClassName { get; private set; }
        public string MethodName { get; private set; }
        public string FileName { get; private set; }
        public int LineNumber { get; private set; }

        // Constructor
        public CallerInfo(StackTrace trace)
        {
            this = new CallerInfo();
            MethodName = trace.GetFrame(1).GetMethod().Name;
            ClassName = trace.GetFrame(1).GetMethod().ReflectedType.Name;
            FileName = trace.GetFrame(1).GetFileName();
            LineNumber = trace.GetFrame(1).GetFileLineNumber();
        }

        // Methods
        public override string ToString()
        {
            return string.Format("Caller {0}::{1} on line {2}\nFile: {3}", ClassName, MethodName, LineNumber, FileName);
        }
    }
}
