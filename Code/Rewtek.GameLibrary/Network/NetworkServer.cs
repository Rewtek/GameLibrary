namespace Rewtek.GameLibrary.Network
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class NetworkServer
    {
        // Variables
        private readonly Socket _socket;
        private Thread _thread;

        // Properties
        /// <summary>
        /// Gets or sets the <see cref="Rewtek.GameLibrary.Network.PacketHandler"/>.
        /// </summary>
        public PacketHandler PacketHandler { get; set; }
        /// <summary>
        /// Gets or sets a value indicates whether the <see cref="System.Net.Sockets.Socket"/> 
        /// accepts incoming connections.
        /// </summary>
        public bool AcceptConnections { get; set; }
        /// <summary>
        /// Gets a value indicating whether the <see cref="System.Net.Sockets.Socket"/> is bound.
        /// </summary>
        public bool IsBound { get { return _socket.IsBound; } }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Network.NetworkServer"/> class.
        /// </summary>
        public NetworkServer()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            AcceptConnections = true;
        }

        // Methods
        /// <summary>
        /// Associates a <see cref="System.Net.Sockets.Socket"/> with a local endpoint.
        /// </summary>
        /// <param name="hostAddr">The name of the remote host.</param>
        /// <param name="port">The port number of the remote host.</param>
        /// <returns>true if the <see cref="System.Net.Sockets.Socket"/> is bound to a local port; otherwise, false</returns>
        public bool Bind(string host, int port)
        {
            try
            {
                _socket.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), port));
                _socket.Listen(0);
            }
            catch
            {
                Logger.Print(LogEvent.Error, "Error starting server on {0}:{1}", host, port);
                MsgBox.Show(MsgBoxIcon.Error, Reflector.GetCaller(), "Error starting server on {0}:{1}", host, port);
                return false;
            }

            // Start tcp listener
            if (_socket.IsBound)
            {
                Logger.Print(LogEvent.Success, "Server successfully started ({0}:{1})",
                    ((IPEndPoint)_socket.LocalEndPoint).Address,
                    ((IPEndPoint)_socket.LocalEndPoint).Port);

                if (PacketHandler == null)
                {
                    Logger.Print(LogEvent.Warning, "Warning: PacketHandler is null!");
                }

                _thread = new Thread(Listen)
                {
                    Name = "<RewtekNetworkServer:Listener>",
                    IsBackground = true
                };

                // Start tcp thread
                _thread.Start();
            }

            return _socket.IsBound;
        }

        /// <summary>
        /// Listens for incoming connections.
        /// </summary>
        private void Listen()
        {
            Logger.Print(LogEvent.Info, "Start listening for clients...");

            while (AcceptConnections)
            {
                var client = new NetworkClient(_socket.Accept());
                client.SubscribePacketHandler(PacketHandler);
                client.Listen();

                Thread.Sleep(10);
            }

            Logger.Print(LogEvent.Info, "Stop listening for clients ...");
        }
    }
}
