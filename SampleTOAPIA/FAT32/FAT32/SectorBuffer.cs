namespace FAT32
{
    public class sector_buffer : fatlib
    {
        public byte[] sector = new byte[FAT_SECTOR_SIZE];
        public uint address;
        public bool dirty;

        // Next in chain of sector buffers
        public sector_buffer next;
    }
}