namespace Rewtek.GameLibrary
{
    #region Using directives

    using System;
    using System.Linq;
    using System.Diagnostics;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Logging;

    #endregion

    /// <summary>
    /// Provides a global logger instance.
    /// </summary>
    public static class Logger
    {
        // Variables
        private static readonly List<ILogger> _loggers;
        private static readonly object _locker;

        // Properties
        /// <summary>
        /// Gets or sets a value indicating whether the default log should be logged into a file.
        /// </summary>
        public static bool LogToFile { get; set; }

        // Constructor
        /// <summary>
        /// Initializes the <see cref="Rewtek.GameLibrary.Loggin.Logger"/> class.
        /// </summary>
        static Logger()
        {
            _loggers = new List<ILogger>();
            _locker = new object();
        }

        // Methods
        #region File Logger Implementation

        public static void AddFileLogger(string name, string fileName)
        {
            if (!Contains(name))
            {
                _loggers.Add(new FileLogger(name, fileName));
            }
        }

        public static ILogger GetFileLogger(string name)
        {
            return _loggers.FirstOrDefault(logger => logger.Name == name);
        }

        public static ILogger GetFileLogger(string name, bool require)
        {
            var log = _loggers.FirstOrDefault(logger => logger.Name == name);

            if (log == null) throw new NullReferenceException("Logger " + name + " has not been found.");

            return log;
        }

        public static void RemoveFileLogger(string name)
        {
            foreach (var logger in _loggers.Where(logger => logger.Name == name))
            {
                logger.Close();
                _loggers.Remove(logger);
            }
        }

        public static bool Contains(string name)
        {
            if (_loggers.Any(logger => logger.Name == name))
            {
                return true;
            }

            return false;
        }

        public static void CloseAll()
        {
            _loggers.ForEach(logger => logger.Close());
            _loggers.Clear();
        }

        #endregion

        #region Console Logger Implementation

        public static void Print(LogEvent logEvent, string value)
        {
            lock (_locker)
            {
                switch (logEvent)
                {
                    case LogEvent.Info:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case LogEvent.Success:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case LogEvent.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogEvent.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case LogEvent.Critical:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case LogEvent.Debug:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                }

                //Console.Write("[{0}, {1}]> ", DateTime.Now.ToShortTimeString(), logEvent);
                Console.WriteLine(value);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public static void Print(LogEvent logEvent, string format, params object[] param)
        {
            Print(logEvent, string.Format(format, param));
        }

        #endregion

        #region Default Logger Implementation

        public static void Log(string value)
        {
            Log(LogLevel.Always, value);
        }

        public static void Log(string format, params object[] param)
        {
            Log(LogLevel.Always, string.Format(format, param));
        }

        public static void Log(LogLevel level, string value)
        {
            lock (_locker)
            {
                if (level.HasFlag(LogLevel.Debug))
                {
                    GetFileLogger("DEBUG", true).Log(value);
                }
                else
                {
                    Console.WriteLine(value);
                    GetFileLogger("DEFAULT", true).Log(value);
                }
            }
        }

        public static void Log(LogLevel level, string format, params object[] param)
        {
            Log(level, string.Format(format, param));
        }
        
        #endregion

        // -- TEST
        public static void TraceMessage(int level = 1)
        {
            StackTrace trace = new StackTrace();
            string callerMethodName = trace.GetFrame(level).GetMethod().Name;
            string callerClassName = trace.GetFrame(level).GetMethod().ReflectedType.Name;

            Console.WriteLine("Caller method: " + callerMethodName);
            Console.WriteLine("Caller class: " + callerClassName);
        }

        public static void TraceOut(int level = 1)
        {
            var trace = new StackTrace();
            var callerMethodName = trace.GetFrame(level).GetMethod().Name;
            var callerClassName = trace.GetFrame(level).GetMethod().ReflectedType.Name;

            //WriteToConsole("{0}::{1} > ", callerClassName, callerMethodName);
        }
        // -- TEST END
    }
}
