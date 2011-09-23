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
    public class disk_if
    {
        // User supplied function pointers for disk IO
        fn_diskio_read read_sector;
        fn_diskio_write write_sector;
    }
}