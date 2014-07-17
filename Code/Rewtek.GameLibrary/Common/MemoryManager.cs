#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryManager.cs" company="RewTek Network">
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
    using global::System.Threading;

    using Rewtek.GameLibrary.Components;

    #endregion

    public class MemoryManager : IComponent, IDisposable
    {
        // Variables
        private static Thread _thread;
        private static long _lastMemory;

        // Properties
        public string MemoryUsage { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Common.MemoryManager"/> class.
        /// </summary>
        public MemoryManager()
        {
        }

        // Destructor
        ~MemoryManager()
        {
            Logger.Log(Messages.COMPONENT_DESTROYING, GetType().Name);
        }

        // Methods
        #region Public Members

        /// <summary>
        /// Starts the Memory Manager.
        /// </summary>
        public void StartMemoryChecker()
        {
            if (_thread != null) return;

            _thread = new Thread(CheckMemory)
            {
                IsBackground = true,
                Name = "<Rewtek:MemoryManager>"
            };
            _thread.Start();
        }

        /// <summary>
        /// Stops the Memory Manager.
        /// </summary>
        public void StopMemoryChecker()
        {
            if (_thread == null) return;
            if (!_thread.IsAlive) return;

            _thread.Abort();
            _thread = null;
        }

        #endregion

        #region Private Members

        private void CheckMemory()
        {
            Logger.Log("MemoryManager has been started");

            while (true)
            {
                try
                {
                    var usedMemory = Environment.WorkingSet;
                    if (usedMemory != _lastMemory)
                    {
                        MemoryUsage = GetReadableSize(usedMemory);

                        _lastMemory = usedMemory;
                    }
                }
                catch (ThreadAbortException)
                {
                    Logger.Log("MemoryManager has been stopped");
                    break;
                }
                catch
                {
                    Logger.Log("MemoryManager stopped because of an unexpected error");
                    break;
                }

                Thread.Sleep(10);
            }
        }

        private string GetReadableSize(long size)
        {
            var memorySizes = new[] { "B", "KB", "MB", "GB", "TB" };
            var index = 0;

            while (size >= 1024)
            {
                size /= 1024;
                index++;
            }

            return string.Format("{0:00} {1}", size, memorySizes[index]);
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            StopMemoryChecker();
        }

        #endregion
    }
}
