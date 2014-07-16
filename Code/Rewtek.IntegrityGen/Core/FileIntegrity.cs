namespace Rewtek.IntegrityGen.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    public class FileIntegrity
    {
        // Variables
        public List<FileIntegrityEntry> _entries;

        // Properties
        public int EntryCount { get { return _entries.Count; } }

        // Constants
        public const float FILE_VERSION = 1.0f;
        public const byte FILE_PADDING_1 = 0xE7;
        public const byte FILE_PADDING_2 = 0x94;
        public const byte FILE_PADDING_3 = 0x52;
        public const string FILE_TAG = "RFI\0\0";
        public const string FILE_NAME = "FILE_INTEGRITY.BIN";

        // Constructor
        /// <summary>
        /// Intitializes a new instance of the <see cref="Rewtek.Tools.FileIntegrityMaker.Core.FileIntegrity"/> class.
        /// </summary>
        public FileIntegrity()
        {
            _entries = new List<FileIntegrityEntry>();
        }

        // Methods
        /// <summary>
        /// Creates the file integrity.
        /// </summary>
        public void Create()
        {
            var writer = new BinaryWriter(File.Open(FILE_NAME, FileMode.Create, FileAccess.Write));
            var header = new FileIntegrityHeader();

            CreateIntegrity();

            header.Tag = FILE_TAG;
            header.Padding1 = FILE_PADDING_1;
            header.Padding2 = FILE_PADDING_2;
            header.Padding3 = FILE_PADDING_3;
            header.Version = FILE_VERSION;
            header.EntryCount = EntryCount;

            writer.Write(ResourceSystem.MarshalStructure<FileIntegrityHeader>(header));            

            foreach (var entry in _entries)
            {
                writer.Write(entry.FileNameLen);
                writer.Write(entry.FileName.ToCharArray());
                writer.Write(entry.Size);
                writer.Write(entry.Checksum);
            }

            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// Reads the file integrity.
        /// </summary>
        public void Read()
        {
            var reader = new BinaryReader(File.Open(FILE_NAME, FileMode.Open, FileAccess.Read));
            var header = new FileIntegrityHeader();

            header.Tag = new string(reader.ReadChars(5));
            header.Padding1 = reader.ReadByte();
            header.Padding2 = reader.ReadByte();
            header.Padding3 = reader.ReadByte();
            header.Version = reader.ReadSingle();
            header.EntryCount = reader.ReadInt32();

            if (header.Tag != FILE_TAG)
            {
                Console.WriteLine("File Format Missmatch" + FILE_NAME);
                Console.WriteLine("Press any key to exit ...");
                Console.ReadLine();
                return;
            }
            else if (header.Version != FILE_VERSION)
            {
                Console.WriteLine("File Version Missmatch - " + FILE_NAME);
                Console.WriteLine("Press any key to exit ...");
                Console.ReadLine();
                return;
            }
            else
            {
                _entries.Clear();

                for (int i = 0; i < header.EntryCount; i++)
                {
                    var entry = new FileIntegrityEntry();
                    entry.FileNameLen = reader.ReadInt16();
                    entry.FileName = new string(reader.ReadChars(entry.FileNameLen));
                    entry.Size = reader.ReadInt32();
                    entry.Checksum = reader.ReadInt32();

                    Console.WriteLine("{0} 0x{1:X} 0x{2:X}", entry.FileName, entry.Size, entry.Checksum);
                }
            }
        }

        /// <summary>
        /// Creates the file entries.
        /// </summary>
        private void CreateIntegrity()
        {
            _entries.Clear();

            foreach (string fileName in Directory.GetFiles(Environment.CurrentDirectory, "*.*", SearchOption.AllDirectories))
            {
                var file = new FileInfo(fileName);
                var name = ResourceSystem.CombinePath(file.FullName);
                var entry = new FileIntegrityEntry();
                entry.FileName = name;
                entry.FileNameLen = (short)name.Length;
                entry.Size = (int)file.Length;
                entry.Checksum = ResourceSystem.ComputeChecksum(file.FullName);

                _entries.Add(entry);

                Console.WriteLine("{0} 0x{1:X} 0x{2:X}", entry.FileName, entry.Size, entry.Checksum);
            }
        }
    }
}