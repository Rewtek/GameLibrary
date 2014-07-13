namespace Rewtek.GameLibrary.Testing
{
    public abstract class Test : System.IDisposable
    {
        // Properties
        public bool IsClosed { get; private set; }
        public bool IsExecuting { get; private set; }

        // Methods
        /// <summary>
        /// Runs the test.
        /// </summary>
        public void Run()
        {
            Logger.Log("Running {0} ...", GetType().Name);

            IsExecuting = true;
            Execute();
        }

        /// <summary>
        /// Executes the test.
        /// </summary>
        public virtual void Execute()
        {
            Close();
        }

        /// <summary>
        /// Closes the test.
        /// </summary>
        public void Close()
        {
            if (IsClosed) return;

            Logger.Log("Closeing {0} ...", GetType().Name);

            IsClosed = true;
            IsExecuting = false;
        }

        /// <summary>
        /// Disposes the test.
        /// </summary>
        public void Dispose()
        {
            Close();
        }
    }
}
