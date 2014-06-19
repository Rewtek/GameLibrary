namespace Rewtek.GameLibrary
{
    using System;
    using System.Linq;
    using System.Diagnostics;

    public static class Logger
    {
        public static void Log(string value)
        {
            Console.WriteLine(value);
        }

        public static void Log(string format, params object[] param)
        {
            Log(string.Format(format, param));
        }

        [ConditionalAttribute("DEBUG")]
        public static void Debug(string value)
        {
            Console.WriteLine(value);
        }

        [ConditionalAttribute("DEBUG")]
        public static void Debug(string format, params object[] param)
        {
            Debug(string.Format(format, param));
        }
    }
}
