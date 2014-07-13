namespace Rewtek.GameLibrary
{
    using System;

    public static class EventHandlerExtensions
    {
        public static void SafeInvoke(this EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler.Invoke(sender, e);
            }
        }

        public static void SafeInvoke<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            if (handler != null)
            {
                handler.Invoke(sender, e);
            }
        }
    }
}
