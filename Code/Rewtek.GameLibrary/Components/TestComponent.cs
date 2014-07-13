namespace Rewtek.GameLibrary.Components
{
    #region Using directives

    using global::System;
    using global::System.Linq;
    using global::System.Collections.Generic;

    #endregion

    public class TestComponent : IComponent
    {
        // Variables
		private bool _initialized;
        private bool _disposed;

        // Properties

        // Constructor
		/// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Components.TestComponent"/> class.
        /// </summary>
        public TestComponent()
        {
            
        }
		
		// Destructor
        ~TestComponent()
        {
            Dispose(false);
        }

        // Methods
		public void Initialize()
		{
			if (_initialized) return;
			
			Logger.Log(Messages.COMPONENT_INITIALIZING, GetType().Name);
			
			_initialized = true;
		}

        #region IDisposable Implementation

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the object.
        /// <param name="disposing"></param>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            Logger.Log(Messages.COMPONENT_DESTROYING, GetType().Name);

            if (disposing)
            {
                // Free other state (managed objects).
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
            _disposed = true;
        }

        #endregion
    }
}
