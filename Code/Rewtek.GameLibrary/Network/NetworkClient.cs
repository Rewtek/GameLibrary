namespace Rewtek.GameLibrary.Network
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class NetworkClient
    {
        // Variables
        private readonly Socket _socket;
        private readonly Thread _thread;
        private PacketHandler _packetHandler;

        // Properties
        public bool IsConnected { get { return _socket.Connected; } }
        
        public string Address { get; set; }
        public int Port { get; set; }

        // Events
        public event EventHandler<PacketEventArgs> PacketSent;
        public event EventHandler<PacketEventArgs> PacketReceived;

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Network.NetworkClient"/> class.
        /// </summary>
        public NetworkClient()
            : this(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Network.NetworkClient"/> class.
        /// <param name="socket">The <see cref="System.Net.Sockets.Socket"/> to be connected with.</param>
        /// </summary>
        public NetworkClient(Socket socket)
        {
            _socket = socket;
            _thread = new Thread(Receive);
        }

        // Methods
        /// <summary>
        ///  Establishes a connection to a remote host. The host is specified by a host
        ///  name and a port number.
        /// </summary>
        /// <param name="host">The name of the remote host.</param>
        /// <param name="port">The port number of the remote host.</param>
        /// <returns>Returns true if the client is connected. Otherwise, false.</returns>
        public bool Connect(string host, int port)
        {
            try
            {
                _socket.Connect(host, port);

                TryStartReceive();
            }
            catch (Exception ex)
            {
                Logger.Log("Error connecting to {0}:{1}", host, port);
                Logger.Log("NetworkClient Error: {0}", ex.Message);
                Logger.Log("NetworkClient Stack: {0}", ex.StackTrace);
                MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "Error connecting to {0}:{1}", host, port); 
            }

            return IsConnected;
        }

        /// <summary>
        /// Starts listening.
        /// </summary>
        public void Listen()
        {
            TryStartReceive();
        }

        /// <summary>
        /// Sends the specified number of bytes of data to a connected <see cref="System.Net.Sockets.Socket"/>,
        /// using the specified <see cref="System.Net.Sockets.SocketFlags"/>.
        /// </summary>
        /// <param name="buffer">An array of type <see cref="System.Byte"/> that contains the data to be sent.</param>
        /// <returns>The number of bytes sent to the <see cref="System.Net.Sockets.Socket"/>.</returns>
        public int Send(byte[] buffer)
        {
            if (!IsConnected) return 0;
            
            return _socket.Send(buffer, buffer.Length, SocketFlags.None);
        }

        /// <summary>
        /// Sends the specified <see cref="Rewtek.GameLibrary.Network.Packet"/>.
        /// </summary>
        /// <param name="packet">The <see cref="Rewtek.GameLibrary.Network.Packet"/> to send.</param>
        /// <returns>The number of bytes sent to the <see cref="System.Net.Sockets.Socket"/>.</returns>
        public int Send(Packet packet)
        {
            PacketSent.SafeInvoke(this, new PacketEventArgs(packet, PacketEventType.Send));
            return Send(packet.GetBytes());
        }

        /// <summary>
        /// Closes the <see cref="System.Net.Sockets.Socket"/> connection, releases all associated resources
        /// and terminates the connection.
        /// </summary>
        public void Disconnect()
        {
            _socket.Close();

            Logger.Log("NetworkClient disconnected ({0}:{1})", Address, Port);
        }

        /// <summary>
        /// Subscribes a <see cref="Rewtek.GameLibrary.Network.PacketHandler"/>.
        /// </summary>
        /// <param name="packetHandler">The <see cref="Rewtek.GameLibrary.Network.PacketHandler"/>.</param>
        public void SubscribePacketHandler(PacketHandler packetHandler)
        {
            _packetHandler = packetHandler;
        }

        #region Private Members

        private void TryStartReceive()
        {
            if (!IsConnected) return;

            Address = ((IPEndPoint)_socket.RemoteEndPoint).Address.ToString();
            Port = ((IPEndPoint)_socket.RemoteEndPoint).Port;

            Logger.Log("NetworkClient connected ({0}:{1})", Address, Port);

            // Start the receive thread
            _thread.Name = string.Format("<RewtekNetworkClient-{0}>", _socket.RemoteEndPoint);
            _thread.IsBackground = true;
            _thread.Start();
        }

        private void Receive()
        {
            while (IsConnected)
            {
                try
                {
                    var buffer = new byte[2048];
                    var len = _socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

                    if (len < 1) break;

                    var packet = new Packet(buffer);

                    if (_packetHandler != null) _packetHandler.InvokePacket(this, packet);

                    PacketReceived.SafeInvoke(this, new PacketEventArgs(packet, PacketEventType.Receive));
                }
                catch (SocketException ex)
                {
                    if (ex.ErrorCode == 10053 || ex.ErrorCode == 10054) continue;

                    Logger.Log("NetworkClient Error: {0} Code: {1}", ex.Message, ex.ErrorCode);
                    Logger.Log("NetworkClient Stack: {0}", ex.StackTrace);
                }
                catch (Exception ex)
                {
                    Logger.Log("NetworkClient Error: {0}", ex.Message);
                    Logger.Log("NetworkClient Stack: {0}", ex.StackTrace);
                }
            }
        }

        #endregion
    }
}
