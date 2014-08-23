namespace Rewtek.GameLibrary.Rendering
{
    using D3DViewport = Microsoft.DirectX.Direct3D.Viewport;

    /// <summary>
    /// Defines the window dimensions of a render target surface onto which a 3-D
    /// volume projects.
    /// </summary>
    public struct Viewport
    {
        // Properties
        /// <summary>
        /// Retrieves or sets the pixel coordinate of the upper-left corner of the viewport
        /// on the render target surface.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Retrieves or sets the pixel coordinate of the upper-left corner of the viewport
        /// on the render target surface.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Retrieves or sets the width dimension of the viewport on the render target
        /// surface, in pixels.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Retrieves or sets the height dimension of the viewport on the render target
        /// surface, in pixels.
        /// </summary>
        public int Height { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Hatwell.GameLibrary.Rendering.Viewport"/> class.
        /// <param name="viewport">A reference to the <see cref="Microsoft.DirectX.Direct3D.Viewport"/> class.</param>
        /// </summary>
        public Viewport(D3DViewport viewport)
        {
            this = new Viewport();
            X = viewport.X;
            Y = viewport.Y;
            Width = viewport.Width;
            Height = viewport.Height;
        }

        // Methods
        /// <summary>
        /// Obtains a string representation of the current instance.
        /// </summary>
        /// <returns>String that represents the object.</returns>
        public override string ToString()
        {
            return string.Format("{{ {0}, {1}, {2}, {3} }}", X, Y, Width, Height);
        }
    }
}
