namespace Rewtek.GameLibrary
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class Global
    {
        // Properties
        public static bool Initialized { get; private set; }

        public static string Version { get; private set; }
        public static string BuildDate { get; private set; }

        // Constructor
        static Global()
        {
            Initialize();
        }

        // Methods
        public static void Initialize()
        {
            if (Initialized) return;

            // set the version
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // set the build date
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * version.Build + // days since 1 January 2000
                TimeSpan.TicksPerSecond * 2 * version.Revision)); // seconds since midnight, (multiply by 2 to get original)
            // a valid date-string can now be constructed like this
            BuildDate = buildDateTime.ToShortDateString();

            Initialized = true;
        }
    }
}
