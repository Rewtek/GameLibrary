namespace Rewtek.GameLibrary.Network
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    public class Packet
    {
        // Variables
        private readonly List<object> _objects;
        private int _index;

        // Properties
        /// <summary>
        /// Gets the timestamp of the packet.
        /// </summary>
        public long TimeStamp { get; private set; }
        /// <summary>
        /// Gets the parameter count.
        /// </summary>
        public int ParameterCount { get { return _objects.Count; } }
        /// <summary>
        /// Gets or sets the packet header.
        /// </summary>
        public int Header { get; set; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Network.Packet"/> class.
        /// </summary>
        public Packet()
        {
            _objects = new List<object>();
            _index = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rewtek.GameLibrary.Network.Packet"/> class.
        /// <param name="buffer">The packet buffer.</param>
        /// </summary>
        public Packet(byte[] buffer)
            : this()
        {
            FromBuffer(buffer);
        }

        // Methods
        /// <summary>
        /// Clears the packet.
        /// </summary>
        public void Clear()
        {
            _objects.Clear();
            _index = 0;
        }

        /// <summary>
        /// Adds a parameter to the packet.
        /// </summary>
        public void AddParam()
        {
            _objects.Add(string.Empty);
        }

        /// <summary>
        /// Adds a parameter to the packet.
        /// </summary>
        /// <param name="value">The parameter.</param>
        public void AddParam(object value)
        {
            _objects.Add(value);
        }

        /// <summary>
        /// Returns the next parameter.
        /// </summary>
        /// <returns>The next parameter as <see cref="System.Object"/>.</returns>
        public object GetParam()
        {
            return _index < ParameterCount ? (string)_objects[_index++] : string.Empty;
        }

        /// <summary>
        /// Returns the next parameter as a specific type.
        /// </summary>
        /// <typeparam name="T">The specific type of the parameter.</typeparam>
        /// <returns>The next parameter as T.</returns>
        public T GetParamAs<T>()
        {
            var temp = _index < ParameterCount ? _objects[_index++] : null;
            return (T)Convert.ChangeType(temp, typeof(T));
        }

        /// <summary>
        /// Returns the next parameter at the specific index.
        /// </summary>
        /// <param name="index">The index of the parameter.</param>
        /// <returns>The next parameter as <see cref="System.Object"/> at the specific index.</returns>
        public string GetParamByIndex(int index)
        {
            return _index < ParameterCount ? (string)_objects[index] : null;
        }

        /// <summary>
        /// Returns the next parameter as a specific type at the specific index.
        /// </summary>
        /// <typeparam name="T">The specific type of the parameter.</typeparam>
        /// <returns>The next parameter as T at the specific index.</returns>
        public T GetParamByIndexAs<T>(int index)
        {
            var temp = _index < ParameterCount ? _objects[index] : null;
            return (T)Convert.ChangeType(temp, typeof(T));
        }

        /// <summary>
        /// Returns an crypted <see cref="System.Byte"/>-Array of the packet.
        /// </summary>
        /// <returns>An crypted <see cref="System.Byte"/>-Array of the packet.</returns>
        public byte[] GetBytes()
        {
            return GetBytes(true);
        }

        /// <summary>
        /// Returns an crypted <see cref="System.Byte"/>-Array of the packet, if encrypt is true.
        /// </summary>
        /// <param name="encrypt">Crypts the packet, if true.</param>
        /// <returns>An crypted <see cref="System.Byte"/>-Array of the packet, if encrypt is true.</returns>
        public byte[] GetBytes(bool encrypt)
        {
            return encrypt ? Encode(Encoding.Default.GetBytes(GetString())) : Encoding.Default.GetBytes(GetString());
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> of the packet.
        /// </summary>
        /// <returns>A <see cref="System.String"/> of the packet.</returns>
        public string GetString()
        {
            var packetStr = string.Format("{0} {1} ", Environment.TickCount, Header);
            foreach (var param in _objects)
            {
                packetStr += (param.ToString().Replace(Convert.ToChar(0x20), Convert.ToChar(0x1D)) + Convert.ToChar(0x20));
            }
            packetStr += '\n';

            return packetStr;
        }

        /// <summary>
        /// Decodes the packet with a default encryption key.
        /// </summary>
        /// <param name="buffer">The packet buffer.</param>
        /// <returns>The decoded packet buffer.</returns>
        public virtual byte[] Decode(byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] ^= 0x00;
            }

            return buffer;
        }

        /// <summary>
        /// Encodes the packet with a default encryption key.
        /// </summary>
        /// <param name="buffer">The packet buffer.</param>
        /// <returns>The decoded packet buffer.</returns>
        public virtual byte[] Encode(byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] ^= 0x00;
            }

            return buffer;
        }

        private void FromBuffer(byte[] buffer)
        {
            buffer = Decode(buffer);

            var packetStr = Encoding.Default.GetString(buffer);
            var parameter = packetStr.Split(' ');

            // wrong packet length
            if (parameter.Length < 2) throw new ArgumentException("Invalid packet parameter length");

            long timestamp;
            long.TryParse(parameter[0], out timestamp);
            TimeStamp = timestamp;

            int header;
            int.TryParse(parameter[1], out header);
            Header = header;

            for (var i = 2; i < parameter.Length-1; i++)
            {
                AddParam(parameter[i]);
            }
        }
    }
}
