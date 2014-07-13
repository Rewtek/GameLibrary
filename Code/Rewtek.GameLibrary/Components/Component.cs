#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Component.cs" company="RewTek Network">
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

namespace Rewtek.GameLibrary.Components
{
    #region Using directives

    using global::System;
    using global::System.Linq;
    using global::System.Collections.Generic;

    #endregion

    /// <summary>
    /// Provides the base class for a component.
    /// </summary>
    public abstract class Component : IDisposable, IComponent
    {
        // Variables
        private bool _initialized;
        private bool _disposed;

        // Properties

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Components.Component"/> class.
        /// </summary>
        public Component()
        {
        }

        // Destructor
        ~Component()
        {
            Dispose(false);
        }

        // Methods
        #region Component Implementation

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public virtual void Initialize()
        {
            if (_initialized) return;

            Logger.Log(Messages.COMPONENT_INITIALIZING, GetType().Name);

            _initialized = true;
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            Logger.Log(Messages.COMPONENT_DESTROYING, GetType().Name);

            if (disposing)
            {
                // Free other state (managed objects).
            }

            // Free your own state (unmanaged objects).
            // Set large fields to null.

            _disposed = true;
        }

        #endregion
    }
}
