namespace Rewtek.GameLibrary.UI.Widgets
{
    using System;
    using System.Drawing;

    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.Rendering.Fonts;
    using Rewtek.GameLibrary.UI;
    using Rewtek.GameLibrary.UI.Widgets;
    using Rewtek.GameLibrary.UI.Widgets.Base;

    public class Box : Widget
    {
        // Properties
        public Color BackgroundColor { get; set; }
        public Color BorderColor { get; set; }
        public BorderStyle BorderStyle { get; set; }
        public int BorderSize { get; set; }

        // Constructor
        public Box(IWidgetContainer container)
            : base(container)
        {
            BackgroundColor = Color.Black;
            BorderColor = Color.Gray;
            BorderStyle = BorderStyle.None;
            BorderSize = 1;
        }

        // Methods
        protected override void OnPaint()
        {
            if (BorderStyle == BorderStyle.Single)
            {
                GraphicsDevice.Gemoetry.DrawRectangle(Position.X, Position.Y, Size.X, Size.Y, BorderColor);
                GraphicsDevice.Gemoetry.DrawRectangle(Position.X + 1, Position.Y + 1, Size.X - 2, Size.Y - 2, BackgroundColor);
            }
            else if (BorderStyle == BorderStyle.Custom)
            {
                GraphicsDevice.Gemoetry.DrawRectangle(Position.X, Position.Y, Size.X, Size.Y, BorderColor);
                GraphicsDevice.Gemoetry.DrawRectangle(Position.X + BorderSize, Position.Y + BorderSize, Size.X - (BorderSize * 2), Size.Y - (BorderSize * 2), BackgroundColor);
            }
            else
            {
                GraphicsDevice.Gemoetry.DrawRectangle(Position.X, Position.Y, Size.X, Size.Y, BackgroundColor);
            }
        }
    }
}
