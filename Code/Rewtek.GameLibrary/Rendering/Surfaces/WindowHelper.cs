namespace Rewtek.GameLibrary.Rendering.Surfaces
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class WindowHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursorFromFile(string path);

        public static Cursor LoadCursor(string path)
        {
            return new Cursor(LoadCursorFromFile(path));
        }
    }
}
