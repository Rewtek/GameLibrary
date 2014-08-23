namespace Rewtek.GameLibrary.UI.Widgets
{
    using System;
    using System.Drawing;

    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.Rendering.Fonts;
    using Rewtek.GameLibrary.UI;
    using Rewtek.GameLibrary.UI.Widgets;
    using Rewtek.GameLibrary.UI.Widgets.Base;

    using Font = Rewtek.GameLibrary.Rendering.Fonts.Font;

    public class Label : Widget
    {
        // Properties
        public bool AutoSize { get; set; }
        public bool Shadow { get; set; }
        
        public string Text { get; set; }

        public Font Font { get; set; }

        public Color FontColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ShadowColor { get; set; }

        public TextAlignment TextAlignment { get; set; }

        // Constructor
        public Label(IWidgetContainer container)
            : base(container)
        {
            AutoSize = true;

            Text = Name;

            Font = Font.Default;

            FontColor = Color.White;
            BackgroundColor = Color.Transparent;
            ShadowColor = Color.Gray;

            TextAlignment = TextAlignment.None;

            Width = Font.MeasureString(Text).Width;
            Height = Font.MeasureString(Text).Height;
        }

        // Methods
        protected override void OnPaint()
        {
            if (string.IsNullOrEmpty(Text) || Font == null) return;

            var position = Position;

            if (!AutoSize)
            {
                var measure = Font.MeasureString(Text);

                if (TextAlignment == TextAlignment.Left)
                {
                    position.Y = position.Y + (Height / 2) - (measure.Height / 2);
                }
                else if (TextAlignment == TextAlignment.Right)
                {
                    position.X = position.X + (Width - measure.Width);
                    position.Y = position.Y + (Height / 2) - (measure.Height / 2);
                }
                else if (TextAlignment == TextAlignment.Center)
                {
                    position.X = position.X + (Width / 2) - (measure.Width / 2);
                    position.Y = position.Y + (Height / 2) - (measure.Height / 2);
                }
            }

            if (Shadow)
            {
                GraphicsDevice.Gemoetry.DrawRectangle(Position.X, Position.Y, Size.X + 1, Size.Y, BackgroundColor);
                Font.Draw(Text, (int)position.X + 1, (int)position.Y + 1, ShadowColor);
                Font.Draw(Text, (int)position.X, (int)position.Y, FontColor);
            }
            else
            {
                GraphicsDevice.Gemoetry.DrawRectangle(Position.X, Position.Y, Size.X, Size.Y, BackgroundColor);
                Font.Draw(Text, (int)position.X, (int)position.Y, FontColor);
            }            
        }
    }
}