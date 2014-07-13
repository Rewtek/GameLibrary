namespace Rewtek.GameLibrary.Logging
{
    public interface ILogger
    {
        // Properties
        string Name { get; }
        LoggerType Type { get; }

        // Methods
        void Log(object value);
        void Log(string value);
        void Log(string format, params object[] param);
        void Close();
    }
}
