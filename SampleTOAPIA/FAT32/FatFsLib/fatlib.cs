namespace FAT32
{
    using stdlib;

    public class fatlib : libc
    {
        public const int null = 0;

        //-----------------------------------------------------------------------------
// Macros
//-----------------------------------------------------------------------------
// Little Endian
#if FATFS_IS_LITTLE_ENDIAN
	uint GET_32BIT_WORD(byte[] buffer, location)	( ((uint)buffer[location+3]<<24) + ((uint)buffer[location+2]<<16) + ((uint)buffer[location+1]<<8) + (uint)buffer[location+0] )
	ushort GET_16BIT_WORD(buffer, location)	( ((ushort)buffer[location+1]<<8) + (ushort)buffer[location+0] )

	void SET_32BIT_WORD(buffer, location, value)	{ buffer[location+0] = (byte)((value)&0xFF); \
													  buffer[location+1] = (byte)((value>>8)&0xFF); \
													  buffer[location+2] = (byte)((value>>16)&0xFF); \
													  buffer[location+3] = (byte)((value>>24)&0xFF); }

	void SET_16BIT_WORD(buffer, location, value)	{ buffer[location+0] = (byte)((value)&0xFF); \
													  buffer[location+1] = (byte)((value>>8)&0xFF); }
// Big Endian
#else
	static public uint GET_32BIT_WORD(byte[] buffer, int location)	
    {
        return ((uint)buffer[location+0]<<24) + ((uint)buffer[location+1]<<16) + ((uint)buffer[location+2]<<8) + (uint)buffer[location+3];
    }

    static public ushort GET_16BIT_WORD(byte[] buffer, int location)
    {
        return (ushort)((ushort)(buffer[location + 0] << 8) + (ushort)buffer[location + 1]);
    }

	static public void SET_32BIT_WORD(byte[] buffer, int location, uint value)	
    { 
        buffer[location+3] = (byte)((value)&0xFF);
		buffer[location+2] = (byte)((value>>8)&0xFF); 
		buffer[location+1] = (byte)((value>>16)&0xFF); 
		buffer[location+0] = (byte)((value>>24)&0xFF); 
    }

	static public void SET_16BIT_WORD(byte[] buffer, int location, ushort value)	
    { 
        buffer[location+1] = (byte)((value)&0xFF);
	    buffer[location+0] = (byte)((value>>8)&0xFF); 
    }
#endif

        //-------------------------------------------------------------
        // Configuration
        //-------------------------------------------------------------

        // Is the system little endian (1) or big endian (0)
        public const int FATFS_IS_LITTLE_ENDIAN = 1;

        // Max filename Length 
        public const int FATFS_MAX_LONG_FILENAME = 260;

        // Max open files (reduce to lower memory requirements)
        public const int FATFS_MAX_OPEN_FILES = 2;

        // Max FAT sectors to buffer (min 1)
        // (mem used is FAT_BUFFERED_SECTORS * FAT_SECTOR_SIZE)
        public const int FAT_BUFFERED_SECTORS = 1;

        // Include support for writing files
        public const int FATFS_INC_WRITE_SUPPORT = 1;

        // Sector size used
        public const int FAT_SECTOR_SIZE = 512;

        public static void FAT_PRINTF(object a)
        {
            //printf a
        }

        public const int MAX_LONGFILENAME_ENTRIES = 20;
        public const int MAX_SFN_ENTRY_LENGTH = 13;

        public const int FAT_INIT_OK = 0;
        public const int FAT_INIT_MEDIA_ACCESS_ERROR = (-1);
        public const int FAT_INIT_INVALID_SECTOR_SIZE = (-2);
        public const int FAT_INIT_INVALID_SIGNATURE = (-3);
        public const int FAT_INIT_ENDIAN_ERROR = (-4);
        public const int FAT_INIT_WRONG_FILESYS_TYPE = (-5);
        public const int FAT_INIT_WRONG_PARTITION_TYPE = (-6);

        //-----------------------------------------------------------------------------
        //			FAT32 Offsets
        //		Name				Offset
        //-----------------------------------------------------------------------------

        // Boot Sector
        public const int BS_JMPBOOT = 0;	// Length = 3
        public const int BS_OEMNAME = 3;	// Length = 8
        public const int BPB_BYTSPERSEC = 11;	// Length = 2
        public const int BPB_SECPERCLUS = 13;	// Length = 1
        public const int BPB_RSVDSECCNT = 14;	// Length = 2
        public const int BPB_NUMFATS = 16;	// Length = 1
        public const int BPB_ROOTENTCNT = 17;	// Length = 2
        public const int BPB_TOTSEC16 = 19;	// Length = 2
        public const int BPB_MEDIA = 21;	// Length = 1
        public const int BPB_FATSZ16 = 22;	// Length = 2
        public const int BPB_SECPERTRK = 24;	// Length = 2
        public const int BPB_NUMHEADS = 26;	// Length = 2
        public const int BPB_HIDDSEC = 28;	// Length = 4
        public const int BPB_TOTSEC32 = 32;	// Length = 4

        // FAT 12/16
        public const int BS_FAT_DRVNUM = 36;	// Length = 1
        public const int BS_FAT_BOOTSIG = 38;	// Length = 1
        public const int BS_FAT_VOLID = 39;	// Length = 4
        public const int BS_FAT_VOLLAB = 43;	// Length = 11
        public const int BS_FAT_FILSYSTYPE = 54;	// Length = 8

        // FAT 32
        public const int BPB_FAT32_FATSZ32 = 36;	// Length = 4
        public const int BPB_FAT32_EXTFLAGS = 40;	// Length = 2
        public const int BPB_FAT32_FSVER = 42;	// Length = 2
        public const int BPB_FAT32_ROOTCLUS = 44;	// Length = 4
        public const int BPB_FAT32_FSINFO = 48;	// Length = 2
        public const int BPB_FAT32_BKBOOTSEC = 50;	// Length = 2
        public const int BS_FAT32_DRVNUM = 64;	// Length = 1
        public const int BS_FAT32_BOOTSIG = 66;	// Length = 1
        public const int BS_FAT32_VOLID = 67;	// Length = 4
        public const int BS_FAT32_VOLLAB = 71;	// Length = 11
        public const int BS_FAT32_FILSYSTYPE = 82;	// Length = 8

        //-----------------------------------------------------------------------------
        // FAT Types
        //-----------------------------------------------------------------------------
        public const int FAT_TYPE_FAT12 = 1;
        public const int FAT_TYPE_FAT16 = 2;
        public const int FAT_TYPE_FAT32 = 3;

        //-----------------------------------------------------------------------------
        // FAT32 Specific Statics
        //-----------------------------------------------------------------------------
        public const int SIGNATURE_POSITION = 510;
        public const int SIGNATURE_VALUE = 0xAA55;
        public const int PARTITION1_TYPECODE_LOCATION = 450;
        public const int FAT32_TYPECODE1 = 0x0B;
        public const int FAT32_TYPECODE2 = 0x0C;
        public const int PARTITION1_LBA_BEGIN_LOCATION = 454;
        public const int PARTITION1_SIZE_LOCATION = 458;

        //-----------------------------------------------------------------------------
        // FAT32 File Attributes and Types
        //-----------------------------------------------------------------------------
        public const int FILE_ATTR_READ_ONLY = 0x01;
        public const int FILE_ATTR_HIDDEN = 0x02;
        public const int FILE_ATTR_SYSTEM = 0x04;
        public const int FILE_ATTR_SYSHID = 0x06;
        public const int FILE_ATTR_VOLUME_ID = 0x08;
        public const int FILE_ATTR_DIRECTORY = 0x10;
        public const int FILE_ATTR_ARCHIVE = 0x20;
        public const int FILE_ATTR_LFN_TEXT = 0x0F;
        public const int FILE_HEADER_BLANK = 0x00;
        public const int FILE_HEADER_DELETED = 0xE5;
        public const int FILE_TYPE_DIR = 0x10;
        public const int FILE_TYPE_FILE = 0x20;

        //-----------------------------------------------------------------------------
        // Other Defines
        //-----------------------------------------------------------------------------
        public const uint FAT32_LAST_CLUSTER = 0xFFFFFFFF;
        public const uint FAT32_INVALID_CLUSTER = 0xFFFFFFFF;


    }
}