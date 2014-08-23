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
        public static Font Default { get; set; }
        
        // Constructor
        public Font(string familyName, float emSize)
        {
            _font = new D3DFont(Core.Components.Require<GraphicsDevice>().Device, new SysFont(familyName, emSize));
        }

        public Font(D3DFont font)
        {
            _font = font;
        }

        static Font()
        {
            CreateDefaultFont();
        }

        // Methods
        public void Dispose()
        {
            _font.Dispose();
        }

        public void Draw(string value, int x, int y)
        {
            _font.DrawText(null, value, new Point(x, y), Color.White);
        }

        public void Draw(string value, int x, int y, Color color)
        {
            _font.DrawText(null, value, new Point(x, y), color);
        }

        public Rectangle MeasureString(string value)
        {
            var measure = _font.MeasureString(null, value, DrawTextFormat.None, Color.White);
            return new Rectangle(measure.X, measure.Y, measure.Width, measure.Height);
        }

        public static void CreateDefaultFont()
        {
            Default = new Font(new D3DFont(Core.Components.Require<GraphicsDevice>().Device, new SysFont("Dotum", 10)));
        }
    }
}
