#region Copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common\ResourceSystem.cs" company="RewTek Network">
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
    using global::System.IO;
    using global::System.Linq;
    using global::System.Collections.Generic;
    using global::System.Security.AccessControl;

    using BaseEnvironment = global::System.Environment;

    #endregion

    /// <summary>
    /// Implementation of the default resource and file system.
    /// </summary>
    public static class ResourceSystem
    {
        // Variables
        private static bool _initialized;

        // Properties
        /// <summary>
        /// Gets the base path.
        /// </summary>
        public static string BasePath { get; private set; }

        // Constructor
        /// <summary>
        /// Initializes the <see cref="Rewtek.GameLibrary.Common.ResourceSystem"/> class.
        /// </summary>
        static ResourceSystem()
        {
            Initialize();
        }

        // Methods
        #region Public Members

        /// <summary>
        /// Initializes the <see cref="Rewtek.GameLibrary.Common.ResourceSystem"/>.
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            Logger.Log(Messages.COMPONENT_INITIALIZING, "ResourceSystem");

            BasePath = BaseEnvironment.CurrentDirectory + "\\";

            _initialized = true;
        }

        #region Directory Members

        /// <summary>
        /// Creates all directories and subdirectories as specified by path.
        /// </summary>
        /// <param name="path">The directory to create.</param>
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(GetFullPath(path));
            Logger.Log("Directory {0} has been created", path);
        }

        /// <summary>
        /// Creates all the directories in the specified path, applying the specified Windows security.
        /// </summary>
        /// <param name="path">The directory to create.</param>
        /// <param name="directorySecurity">The access control to apply to the directory.</param>
        public static void CreateDirectory(string path, DirectorySecurity directorySecurity)
        {
            Directory.CreateDirectory(GetFullPath(path), directorySecurity);
            Logger.Log("Directory {0} has been created", path);
        }

        /// <summary>
        /// Deletes an empty directory from a specified path.
        /// </summary>
        /// <param name="path">The name of the directory to remove.</param>
        public static void DeleteDirectory(string path)
        {
            Directory.Delete(GetFullPath(path));
        }

        /// <summary>
        /// Deletes an empty directory and, if indicated, any subdirectories and files in the directory.
        /// </summary>
        /// <param name="path">The name of the directory to remove.</param>
        /// <param name="recursive">true to remove directories, subdirectories, and files in path; otherwise, false.</param>
        public static void DeleteDirectory(string path, bool recursive)
        {
            Directory.Delete(GetFullPath(path), recursive);
        }

        /// <summary>
        /// Determines whether the given path refers to an existing directory on disk.
        /// </summary>
        /// <param name="path">The path to test.</param>
        /// <returns>true if path refers to an existing directory; otherwise, false.</returns>
        public static bool ExistsDirectory(string path)
        {
            var directory = GetFullPath(path); // Path.GetDirectoryName(GetFullPath(path));
            if (directory == null) return false;
            if (directory.Contains('/') || directory.Contains('\\'))
            {
                if (!Directory.Exists(directory)) return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether the given path refers to an existing directory on disk and throws an error message if not.
        /// </summary>
        /// <param name="path">The path to test.</param>
        /// <returns>true if path refers to an existing directory; otherwise, false.</returns>
        public static bool CheckDirectory(string path)
        {
            if (ExistsDirectory(path))
            {
                return true;
            }
            else
            {
                MsgBox.Show(MsgBoxIcon.Error, "ResourceSystem::DirectoryExists", "Directory Not Found - " + path);
                return false;
            }
        }

        #endregion

        #region File Members

        /// <summary>
        /// Determines whether the given file refers to an existing file on disk.
        /// </summary>
        /// <param name="path">The file to test.</param>
        /// <returns>true if path refers to an existing file; otherwise, false.</returns>
        public static bool ExistsFile(string path)
        {
            var directory = Path.GetDirectoryName(GetFullPath(path));
            if (directory == null) return false;
            if (directory.Contains('/') || directory.Contains('\\'))
            {
                if (!Directory.Exists(directory)) return false;
            }

            if (!File.Exists(path)) return false;

            return true;
        }

        /// <summary>
        /// Determines whether the file path refers to an existing file on disk and throws an error message if not.
        /// </summary>
        /// <param name="path">The file to test.</param>
        /// <returns>true if path refers to an existing file; otherwise, false.</returns>
        public static bool CheckFile(string path)
        {
            if (ExistsFile(path))
            {
                return true;
            }
            else
            {
                Logger.Log("File Not Found - {0}", path);
                MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "File Not Found - {0}", path);
                return false;
            }
        }

        #endregion File Members

        #endregion Public Members

        #region Private Members

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="part">The part.</param>
        private static string GetFullPath(string part)
        {
            return Path.Combine(BasePath, part);
        }

        #endregion
    }
}
