namespace Rewtek.IntegrityGen
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Rewtek.IntegrityGen.Core;

    public static class Program
    {
        // Properties
        /// <summary>
        /// Gets the assembly version of the logger.
        /// </summary>
        public static string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
        /// <summary>
        /// Gets the build date of the logger.
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
        /// Main entry point.
        /// </summary>
        static void Main(string[] args)
        {
            Console.Title = "Integrity Gen";

            Console.WriteLine("Integrity Generator [Version: {0}, Build: {1}]", Version, BuildDate);
            Console.WriteLine("Copyright (c) 2014 Rewtek Network. All rights reserved.");
            Console.WriteLine();

            var integrity = new FileIntegrity();
            integrity.Create();

            //Console.ReadLine();
        }
    }
}
