#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Components\MissingComponentException.cs" company="Xemio Network">
//      Copyright (c) 2013 Xemio Network (xemio.net)
//      
//      Permission is hereby granted, free of charge, to any person obtaining a copy of
//      this software and associated documentation files (the "Software"), to deal in
//      the Software without restriction, including without limitation the rights to
//      use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//      the Software, and to permit persons to whom the Software is furnished to do so,
//      subject to the following conditions:
//      
//      The above copyright notice and this permission notice shall be included in all
//      copies or substantial portions of the Software.
//      
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//      FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//      COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//      IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//      CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
#endregion

namespace Rewtek.GameLibrary.Components
{
    #region Using directives

    using System;

    #endregion

    [Serializable]
    public class MissingComponentException : Exception
    {
        // Properties
        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        public Type SourceType { get; private set; }
        /// <summary>
        /// Gets the type of the component.
        /// </summary>
        public Type ComponentType { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Components.MissingComponentException"/> class.
        /// </summary>
        /// <param name="componentType">Type of the component.</param>
        public MissingComponentException(Type componentType)
            : base(string.Format("Required component {0} doesn't exist inside the component registry.", componentType))
        {
            ComponentType = componentType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Components.MissingComponentException"/> class.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="componentType">Type of the component.</param>
        public MissingComponentException(Type sourceType, Type componentType)
            : base(string.Format("{0} requires a {1} component registered inside the component registry.", sourceType.Name, componentType.Name))
        {
            SourceType = sourceType;
            ComponentType = componentType;
        }
    }
}
