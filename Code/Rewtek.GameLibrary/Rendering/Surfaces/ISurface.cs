using Rewtek.GameLibrary.Components;

namespace Rewtek.GameLibrary.Rendering.Surfaces
{
    public interface ISurface : IComponent
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Gets or sets the title of the surface.
        /// </summary>
        string Title { get; set; }
    }
}
