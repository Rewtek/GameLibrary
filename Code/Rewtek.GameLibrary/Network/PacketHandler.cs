namespace Rewtek.GameLibrary.Network
{
    using System;
    using System.Collections.Generic;

    public delegate void OnPacket(NetworkClient Client, Packet Packet);

    public class PacketHandler
    {
        // Variables
        private readonly Dictionary<int, OnPacket> _packets;

        // Properties
        /// <summary>
        /// Gets count of all registered packets.
        /// </summary>
        public int PacketCount { get { return _packets.Count; } }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Network.PacketHandler"/> class.
        /// </summary>
        public PacketHandler()
        {
            _packets = new Dictionary<int, OnPacket>();
            //_packets.Add(0x1234, EXAMPLE_PACKET.Handle);
        }

        // Methods
        /// <summary>
        /// Adds a packet to the <see cref="Rewtek.GameLibrary.Network.PacketHandler"/>.
        /// </summary>
        /// <param name="header">The header of the packet.</param>
        /// <param name="packet">The packet.</param>
        /// <returns>True if the packet has been added. Otherwise, false.</returns>
        public bool AddPacket(int header, OnPacket packet)
        {
            if (!_packets.ContainsKey(header))
            {
                _packets.Add(header, packet);
                return true;
            }
            else
            {
                Logger.Log("Packet {0} has already been added!", header);
                return false;
            }
        }

        /// <summary>
        /// Removes a packet from the <see cref="Rewtek.GameLibrary.Network.PacketHandler"/>.
        /// </summary>
        /// <param name="header">The header of the packet.</param>
        /// <returns>True if the packet has been removed. Otherwise, false.</returns>
        public bool RemovePacket(int header)
        {
            if (_packets.ContainsKey(header))
            {
                _packets.Remove(header);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Invokes a <see cref="Rewtek.GameLibrary.Network.Packet"/>.
        /// </summary>
        /// <param name="client">The <see cref="Rewtek.GameLibrary.Network.NetworkClient"/>.</param>
        /// <param name="packet">The <see cref="Rewtek.GameLibrary.Network.Packet"/>.</param>
        /// <returns>True if the packet has been invoked.</returns>
        public bool InvokePacket(NetworkClient client, Packet packet)
        {
            if (_packets.ContainsKey(packet.Header))
            {
                try
                {
                    _packets[packet.Header].Invoke(client, packet);
                }
                catch (Exception ex)
                {
                    Logger.Log("PacketHandler Error : " + ex.Message);
                    Logger.Log("PacketHandler Stack : " + ex.StackTrace);
                }
                return true;
            }
            else
            {
                Logger.Log("Warning: Packet {0} has no handler!", packet.Header);
                return false;
            }
        }
    }
}
