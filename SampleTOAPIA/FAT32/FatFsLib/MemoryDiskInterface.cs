namespace FAT32
{
    using System;

    public class MemoryDiskInterface : DiskInterface
    {
        byte[] Storage { get; set; }
        long Size { get; set; }

        #region Constructors
        MemoryDiskInterface(int numberOfBytes)
        {
            Storage = new byte[numberOfBytes];
        }
        #endregion

        int GetSectorByteOffset(uint sector)
        {
            return (int)(sector * FAT_SECTOR_SIZE);
        }

        public override bool ReadSector(uint sector, byte[] buffer)
        {
            int sectorOffset = GetSectorByteOffset(sector);

            Array.Copy(Storage, sectorOffset, buffer, 0, FAT_SECTOR_SIZE);

            return true;
        }

        public override bool WriteSector(uint sector, byte[] buffer)
        {
            int sectorOffset = GetSectorByteOffset(sector);

            Array.Copy(buffer, 0, Storage, sectorOffset, FAT_SECTOR_SIZE);

            return true;
        }

        #region Static Helpers
        public static MemoryDiskInterface CreateFromBytes(int numberOfBytes)
        {
            MemoryDiskInterface di = new MemoryDiskInterface(numberOfBytes);
            return di;
        }
        #endregion
    }
}