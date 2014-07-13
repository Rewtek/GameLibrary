namespace Rewtek.GameLibrary.Network
{
    using System;

    public class PacketEventArgs : EventArgs
    {
        // Properties
        public Packet Packet { get; private set; }

        // Constructor
        public PacketEventArgs(Packet packet)
            : this(packet, PacketEventType.None)
        {
        }

        public PacketEventArgs(Packet packet, PacketEventType type)
        {
            Packet = packet;

            if (type == PacketEventType.Send)
            {
                Logger.Log("-> " + packet.GetString());
            }
            else if (type == PacketEventType.Receive)
            {
                Logger.Log("<- " + packet.GetString());
            }
        }
    }

    public enum PacketEventType { None, Send, Receive }
}
