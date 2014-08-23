namespace Rewtek.GameLibrary.UI.Widgets
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Math;
    using Rewtek.GameLibrary.Rendering;
    using Rewtek.GameLibrary.UI;
    using Rewtek.GameLibrary.UI.Widgets;
    using Rewtek.GameLibrary.UI.Widgets.Base;

    public class WidgetContainer : IWidgetContainer
    {
        // Properties
        public GraphicsDevice GraphicsDevice { get; private set; }
        public Vector2 ScreenPosition { get; set; }
        public Vector2 Position { get; set; }
        public List<Widget> Widgets { get; set; }
        public int Width { get { return GraphicsDevice.Viewport.Width; } }
        public int Height { get { return GraphicsDevice.Viewport.Height; } }

        // Constructor
        public WidgetContainer()
        {
            GraphicsDevice = Core.Components.Require<GraphicsDevice>();
            Widgets = new List<Widget>();
        }

        // Methods
        public void SetFocus(Widget focusedWidget)
        {
            focusedWidget.Container.Widgets.ForEach(widget =>
            {
                if (widget != focusedWidget)
                {
                    widget.Focused = false;
                }
            });
        }

        public void Next()
        {
            Next(this);
        }

        public void Next(IWidgetContainer container)
        {
            Widget focusedWidget = container.Widgets.FirstOrDefault(widget => widget.Focused == true);

            if (focusedWidget != null)
            {
                int currentIndex = container.Widgets.IndexOf(focusedWidget);
                if (currentIndex >= container.Widgets.Count)
                {
                    currentIndex = 0;
                }

                currentIndex++;
                if (currentIndex >= container.Widgets.Count)
                {
                    currentIndex = 0;
                }

                while (!container.Widgets[currentIndex].Focusable)
                {
                    currentIndex++;
                    if (currentIndex >= container.Widgets.Count)
                    {
                        currentIndex = 0;
                    }
                }
                container.Widgets[currentIndex].Focused = true;
            }
        }

        public void Tick(float elapsed)
        {
            Widgets.ForEach(widget => widget.Update(elapsed));
        }

        public void Render()
        {
            Widgets.ForEach(widget => widget.Draw());
        }
    }
}