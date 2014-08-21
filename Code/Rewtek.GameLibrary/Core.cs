#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.cs" company="RewTek Network">
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
    using global::System.Linq;
    using global::System.Reflection;
    using global::System.Diagnostics;
    using global::System.Windows.Forms;

    using Rewtek.GameLibrary;
    using Rewtek.GameLibrary.Common;
    using Rewtek.GameLibrary.Components;
    using Rewtek.GameLibrary.Game;
    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.Rendering.Surfaces;
    
    #endregion

    /// <summary>
    /// Provides the system class of the game library.
    /// </summary>
    public static class Core
    {
        // Properties
        /// <summary>
        /// Gets a value indicating whether the game library has been initialized.
        /// </summary>
        public static bool Initialized { get; private set; }

        /// <summary>
        /// Gets the assembly version of the game library.
        /// </summary>
        public static string Version 
        { 
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } 
        }
        /// <summary>
        /// Gets the build date of the game library.
        /// </summary>
        public static string BuildDate 
        {
            get 
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * version.Build + // days since 1 January 2000
                    TimeSpan.TicksPerSecond * 2 * version.Revision)); // seconds since midnight, (multiply by 2 to get original)
                
                // a valid date-string can now be constructed like this
                return buildDateTime.ToShortDateString();
            }
        }

        /// <summary>
        /// Gets the <see cref="Rewtek.GameLibrary.Components.ComponentManager"/>.
        /// </summary>
        public static ComponentManager Components { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes the system.
        /// </summary>
        static Core()
        {
        }

        // Methods
        #region Public Member

        /// <summary>
        /// Initializes the game library.
        /// </summary>
        public static void Initialize()
        {
            if (Initialized) return;

            // Setup
            SetupConfiguration();
            SetDebugCondition();
            SetLoggerConfiguration();

            // Log
            Logger.Log("Initializing Game Library ({0}) on {1}", Version, SystemHelper.Platform);

            // Initialize component manager
            Components = new ComponentManager();

            // Initialize resource system
            ResourceSystem.Initialize();

            // Initialize components
            Components.Install(new MemoryManager());
            Components.Install(new GameLoop());

            Initialized = true;
        }

        
        /// <summary>
        /// Starts the game library with the specified configuration.
        /// </summary>
        public static void Run()
        {
            // Initialize memory manager
            //Components.Require<MemoryManager>().StartMemoryChecker();            

            // Initialize window
            Core.Components.Require<WindowSurface>().Initialize();
            
            // Initialize graphics
            Core.Components.Require<GraphicsDevice>().Initialize(Core.Components.Require<WindowSurface>());

            if (!Core.Components.Require<GraphicsDevice>().Initialized) return;

            // Initialize game loop
            Core.Components.Require<GameLoop>().Initialize();
            Core.Components.Require<GameLoop>().Start();

            Application.Run((Form)Core.Components.Require<WindowSurface>().Control);
        }

        /// <summary>
        /// Destroys all resources.
        /// </summary>
        public static void Destroy()
        {
            Components.Dispose();
        }

        #endregion

        #region Private Member

        private static void SetupConfiguration()
        {
            
        }

        private static void SetLoggerConfiguration()
        {
            Logger.AddFileLogger("DEFAULT", "LogFile.txt");
        }

        [ConditionalAttribute("DEBUG")]
        private static void SetDebugCondition()
        {
            Logger.AddFileLogger("DEBUG", "Debug.txt");
            Logger.Log(LogLevel.Debug, "Log file opened");
        }

        #endregion
    }
}
