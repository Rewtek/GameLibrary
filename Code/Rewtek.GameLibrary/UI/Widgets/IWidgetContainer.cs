namespace Rewtek.GameLibrary.UI.Widgets
{
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Math;
    using Rewtek.GameLibrary.Rendering;

    public interface IWidgetContainer
    {
        List<Widget> Widgets { get; }

        GraphicsDevice GraphicsDevice { get; }

        int Width { get; }
        int Height { get; }

        Vector2 Position { get; }
        Vector2 ScreenPosition { get; }

        void SetFocus(Widget widget);

        void Next();
        void Next(IWidgetContainer container);
    }
}