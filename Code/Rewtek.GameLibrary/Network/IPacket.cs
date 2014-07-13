namespace Rewtek.GameLibrary.Network
{
    public interface IPacket
    {
        void Handle(NetworkClient client, Packet packet);
        Packet Depart();
    }
}
