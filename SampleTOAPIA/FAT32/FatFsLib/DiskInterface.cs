namespace FAT32
{
    //-----------------------------------------------------------------------------
    // Function Pointers
    //-----------------------------------------------------------------------------
    public delegate int fn_diskio_read(uint sector, byte[] buffer);
    public delegate int fn_diskio_write(uint sector, byte[] buffer);

    //-----------------------------------------------------------------------------
    // Structures
    //-----------------------------------------------------------------------------
    abstract public class DiskInterface : fatlib
    {
        public abstract bool ReadSector(uint sector, byte[] buffer);
        public abstract bool WriteSector(uint sector, byte[] buffer);
    }
}