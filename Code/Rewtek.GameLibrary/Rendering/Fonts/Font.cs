namespace Rewtek.GameLibrary.Rendering.Fonts
{
    using System;
    using System.Drawing;

    using Microsoft.DirectX;
    using Microsoft.DirectX.Direct3D;

    using SysFont = System.Drawing.Font;
    using D3DFont = Microsoft.DirectX.Direct3D.Font;

    public class Font
    {
        // Variables
        private D3DFont _font;

        // Properties
        public static Font Default;

        // Constructor
        public Font(string familyName, float emSize)
        {
            _font = new D3DFont(Core.Components.Require<GraphicsDevice>().Device, new SysFont(familyName, emSize));
        }

        static Font()
        {
            Default = new Font(new D3DFont(Core.Components.Require<GraphicsDevice>().Device, new SysFont("Dotum", 10)));
        }

        public Font(D3DFont font)
        {
            _font = font;
        }

        // Methods
        public void Draw(string value, int x, int y)
        {
            _font.DrawText(null, value, new Point(x, y), Color.White);
        }
    }
}
