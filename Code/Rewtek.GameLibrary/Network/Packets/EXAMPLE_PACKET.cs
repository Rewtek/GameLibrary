
namespace Rewtek.GameLibrary.Network.Packets
{
    using Rewtek.GameLibrary.Network;

    public class EXAMPLE_PACKET : Packet, IPacket
    {
        public void Handle(NetworkClient client, Packet packet)
        {
            Logger.Log("Handling EXAMPLE_PACKET ...");
        }

        public Packet Depart()
        {
            Logger.Log("Departing EXAMPLE_PACKET ...");
            return null;
        }
    }
}
