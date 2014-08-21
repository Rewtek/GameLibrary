namespace Rewtek.GameLibrary.Rendering.Surfaces
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;
    
    using Rewtek.GameLibrary.Components;

    public class WindowSurface : Component, ISurface
    {
        // Properties
        /// <summary>
        /// Gets the handle.
        /// </summary>
        public IntPtr Handle { get; set; }
        /// <summary>
        /// Gets the control.
        /// </summary>
        public Control Control { get { return Control.FromHandle(Handle); } }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get { return Control.ClientSize.Width; }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get { return Control.ClientSize.Height; }
        }
        /// <summary>
        /// Gets or sets the title of the surface.
        /// </summary>
        public string Title
        {
            get { return Control.Text; }
            set { Control.Text = value; }
        }

        // Constructor
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowSurface"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public WindowSurface(int width, int height)
            : this(width, height, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowSurface"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="title">The title.</param>
        public WindowSurface(int width, int height, string title)
        {
            var form = new Form
            {
                ClientSize = new Size(width, height),
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                StartPosition = FormStartPosition.CenterScreen,
                Text = title
            };

            Handle = form.Handle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowSurface"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public WindowSurface(IntPtr handle)
        {
            Handle = handle;
        }

        #endregion

        // Methods
        #region Public Methods

        /// <summary>
        /// Closes the surface.
        /// </summary>
        public void Close()
        {
            if (Control != null) Control.Invoke((Action)(() => { ((Form)Control).Close(); })); 
        }

        #endregion
    }
}