#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileLogger.cs" company="RewTek Network">
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

namespace Rewtek.GameLibrary.Logging
{
    #region Using directives

    using global::System;
    using global::System.IO;

    #endregion

    public class FileLogger : ILogger
    {
        // Variables
        private readonly LoggerType _type;
        private readonly object _lock;

        private readonly string _name;
        private readonly string _filename;
        private StreamWriter _stream;

        // Properties
        public string Name { get { return _name; } }
        public string FileName { get { return _filename; } }
        public LoggerType Type { get { return _type; } }

        // Constructor
        public FileLogger(string name, string fileName)
        {
            _type = LoggerType.File;
            _lock = new object();
            _name = name;
            _filename = fileName;

            CreateFileLogger();
        }

        // Methods
        private void CreateFileLogger()
        {
            if (_filename.Contains("/") || _filename.Contains("\\"))
            {
                ResourceSystem.CreateDirectory(_filename);
            }

            _stream = new StreamWriter(_filename);
        }

        public void Log(object value)
        {
            lock (_lock)
            {
                if (_stream == null) CreateFileLogger();
                //if (_stream != null && _stream.BaseStream.CanWrite)
                //{
                //    _stream.Close();
                //    _stream = null;

                //    CreateFileLogger();
                //}

                if (_stream != null && _stream.BaseStream.CanWrite)
                {
                    _stream.WriteLine("[{0}] > {1}", DateTime.Now.ToString("dd/MM HH:mm:ss"), value);
                    _stream.Flush();
                }
            }
        }

        public void Log(string value)
        {
            Log((object)value);
        }

        public void Log(string format, params object[] param)
        {
            Log(string.Format(format, param));
        }

        public void Close()
        {
            if (_stream != null)
            {
                _stream.Flush();
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }

            GC.Collect();
        }
    }
}
