namespace Rewtek.GameLibrary.UI.Widgets
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Math;
    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.Rendering.Fonts;
    using Rewtek.GameLibrary.UI;
    using Rewtek.GameLibrary.UI.Widgets;
    using Rewtek.GameLibrary.UI.Widgets.Base;

    using Font = Rewtek.GameLibrary.Rendering.Fonts.Font;

    public class ContextMenu : Widget
    {
        // Properties
        public bool Shadow { get; set; }

        public Font Font { get; set; }

        public Color FontColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Color ShadowColor { get; set; }

        public string Text { get; set; }

        public TextAlignment TextAlignment { get; set; }

        public List<Label> ContextMenuItems { get; set; }

        // Constructor
        public ContextMenu(IWidgetContainer container)
            : base(container)
        {
            Font = Font.Default;

            Text = "Context Menu Item";

            FontColor = Color.White;
            BackgroundColor = Color.Black;
            ShadowColor = Color.Gray;

            TextAlignment = TextAlignment.Center;
            
            ContextMenuItems = new List<Label>();
            Widgets.Add(new Label(this));
            Widgets.Add(new Label(this));

            foreach (var contextMenuItem in Widgets)
            {
                contextMenuItem.MouseEnter += (sender, e) => { ((Label)sender).BackgroundColor = Color.FromArgb(75, 0, 0, 0); };
                contextMenuItem.MouseLeave += (sender, e) => { ((Label)sender).BackgroundColor = Color.FromArgb(50, 0, 0, 0); };
                //contextMenuItem.Paint += (sender, e) => { System.Windows.Forms.MessageBox.Show("Test"); };
            }
        }

        // Methods
        protected override void OnUpdate()
        {
            var lastPosition = Position;
            foreach (Label contextMenuItem in Widgets)
            {
                contextMenuItem.AutoSize = false;
                contextMenuItem.BackgroundColor = BackgroundColor;
                contextMenuItem.Position = new Vector2(lastPosition.X, lastPosition.Y + Size.Y + 1);
                contextMenuItem.Size = Size;
                contextMenuItem.TextAlignment = TextAlignment.Center;
                contextMenuItem.MouseEnter += (sender, e) => { ((Label)sender).BackgroundColor = Color.FromArgb(75, 0, 0, 0); };
                contextMenuItem.MouseLeave += (sender, e) => { ((Label)sender).BackgroundColor = Color.FromArgb(50, 0, 0, 0); };
                contextMenuItem.Paint += (sender, e) => { System.Windows.Forms.MessageBox.Show("Test"); };
                contextMenuItem.Update(0f);

                lastPosition = contextMenuItem.Position;
            }
        }

        protected override void OnPaint()
        {
            //if (string.IsNullOrEmpty(Text) || Font == null) return;
            
            foreach (var contextMenuItem in ContextMenuItems)
            {
               //contextMenuItem.Draw();
            }
        }
    }
}