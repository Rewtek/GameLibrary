using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rewtek.GameLibrary;
using Rewtek.GameLibrary.Network;

namespace Rewtek.Testing.Network
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.Initialize();

            var packetHandler = new PacketHandler();

            var client = new NetworkClient();
            client.Connect("211.130.155.62", 5330);
            client.SubscribePacketHandler(packetHandler);
            client.PacketReceived += new EventHandler<PacketEventArgs>(client_PacketReceived);

            Console.ReadLine();
        }

        static void client_PacketReceived(object sender, PacketEventArgs e)
        {
            var packet = e.Packet;

            Console.WriteLine(packet.GetString());
        }
    }
}
