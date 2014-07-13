#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="$safeitemrootname$.cs" company="RewTek Network">
//      Copyright (c) $year$ Rewtek Network (www.rewtek.net)
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

namespace $rootnamespace$
{
    #region Using directives

	using global::System;
	using global::System.Linq;
	using global::System.Collections.Generic;
	
	using Rewtek.GameLibrary.Components;
	
    #endregion

    public class $safeitemrootname$ : IComponent
    {
        // Variables
		private bool _initialized;
		
        // Properties

        // Constructor
		/// <summary>
        /// Initializes a new instance of the <see cref="$rootnamespace$.$safeitemrootname$"/> class.
        /// </summary>
        public $safeitemrootname$()
        {
            
        }
		
		// Destructor
        ~$safeitemrootname$()
        {
            Logger.Log(Messages.COMPONENT_DESTROYING, GetType().Name);
        }

        // Methods
		public void Initialize()
		{
			if (_initialized) return;
			
			Logger.Log(Messages.COMPONENT_INITIALIZING, GetType().Name);
			
			_initialized = true;
		}
    }
}
