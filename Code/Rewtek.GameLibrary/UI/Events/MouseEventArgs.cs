namespace Rewtek.GameLibrary.UI.Events
{
    using System;

    using Rewtek.GameLibrary.Input;
    using Rewtek.GameLibrary.Math;

    public class MouseEventArgs : EventArgs
    {
        // Properties
        public Vector2 Position { get; private set; }
        public MouseButton PressedButton { get; private set; }

        // Constructor
        public MouseEventArgs(Mouse mouse)
        {
            Position = new Vector2(mouse.X, mouse.Y);
            PressedButton = mouse.PressedButton;
        }
    }
}
