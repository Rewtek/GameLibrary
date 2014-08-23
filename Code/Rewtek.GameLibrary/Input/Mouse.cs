namespace Rewtek.GameLibrary.Input
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Rewtek.GameLibrary.Components;
    using Rewtek.GameLibrary.Rendering.Surfaces;

    public class Mouse : Component
    {
        // Variables
        private bool _initialized;
        private bool _disposed;

        // Properties
        /// <summary>
        /// Gets the position of mouses cursor.
        /// </summary>
        public Point Position { get { return ComputeLocation(); } }
        /// <summary>
        /// Gets x-coordinates of mouses cursor.
        /// </summary>
        public int X { get { return Position.X; } }
        /// <summary>
        /// Gets y-coordinates of mouses cursor.
        /// </summary>
        public int Y { get { return Position.Y; } }

        public MouseButton PressedButton { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Input.Mouse"/> class.
        /// </summary>
        public Mouse()
        {
        }

        // Destructor
        ~Mouse()
        {
            Dispose(false);
        }

        // Methods
        #region Public Member

        /// <summary>
        /// Initializes the <see cref="Rewtek.GameLibrary.Components.Component"/>.
        /// </summary>
        public override void Initialize()
        {
            if (_initialized) return;

            Logger.Log(Messages.COMPONENT_INITIALIZING, GetType().Name);

            Attach();

            _initialized = true;
        }

        /// <summary>
        /// Attaches the <see cref="Rewtek.GameLibrary.Input.Mouse"/> to the <see cref="Rewtek.GameLibrary.Rendering.Surfaces.WindowSurface"/>.
        /// </summary>
        public void Attach()
        {
            var surface = Core.Components.Require<WindowSurface>();
            surface.Control.MouseMove += OnMouseMove;
            surface.Control.MouseDown += OnMouseDown;
            surface.Control.MouseUp += OnMouseUp;

            Logger.Log("Mouse attached to surface");
        }

        /// <summary>
        /// Detaches the <see cref="Rewtek.GameLibrary.Input.Mouse"/> from the <see cref="Rewtek.GameLibrary.Rendering.Surfaces.WindowSurface"/>.
        /// </summary>
        public void Detach()
        {
            var surface = Core.Components.Require<WindowSurface>();
            if (surface.Control == null) return;
            surface.Control.MouseMove -= OnMouseMove;
            surface.Control.MouseDown -= OnMouseDown;
            surface.Control.MouseUp -= OnMouseUp;

            Logger.Log("Mouse detached from surface");
        }
        
        #endregion

        #region Private Member

        /// <summary>
        /// Computes the location of the specified screen point into client coordinates.
        /// </summary>
        /// <returns>The screen coordinate <see cref="System.Drawing.Point"/> to convert.</returns>
        private Point ComputeLocation()
        {
            var surface = Core.Components.Require<WindowSurface>();
            var point = Point.Empty;

            try
            {
                if (surface.Control.InvokeRequired)
                {
                    surface.Control.Invoke((Action)(() =>
                    {
                        point = surface.Control.PointToClient(Cursor.Position);
                    }));
                }
                else
                {
                    point = surface.Control.PointToClient(Cursor.Position);
                }
            }
            catch
            {
                // NOTHING TO DO HERE :)
            }

            return point;
        }

        #endregion

        #region Event Handler

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            OnMouseDown(sender, e);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PressedButton = MouseButton.Left;
            }
            else if (e.Button == MouseButtons.Right)
            {
                PressedButton = MouseButton.Right;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                PressedButton = MouseButton.Wheel;
            }
            else
            {
                PressedButton = MouseButton.None;
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            PressedButton = MouseButton.None;
        }

        #endregion

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
        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            Logger.Log(Messages.COMPONENT_DESTROYING, GetType().Name);

            if (disposing)
            {
                // Free other state (managed objects).
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.

            Detach();

            _disposed = true;
        }

        #endregion
    }
}