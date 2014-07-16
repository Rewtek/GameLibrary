namespace Rewtek.IntegrityGen.Core
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Auto)]
    public struct FileIntegrityEntry
    {
        public short FileNameLen;
        public string FileName;
        public int Size;
        public int Checksum;
    }
}
