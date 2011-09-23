namespace FAT32
{
    using stdlib;

    public delegate void locker();
    public delegate void unlocker();

    public partial class fatfs : fatlib
    {


        // Filesystem globals
        byte sectors_per_cluster;
        uint cluster_begin_lba;
        uint rootdir_first_cluster;
        uint fat_begin_lba;
        uint filenumber;
        ushort fs_info_sector;
        uint lba_begin;
        uint fat_sectors;
        uint next_free_cluster;

        // Disk/Media API
        DiskInterface disk_io;

        // [Optional] Thread Safety
        locker fl_lock;
        unlocker fl_unlock;

        // Working buffer
        sector_buffer currentsector;

        // FAT Buffer
        sector_buffer fat_buffer_head;
        sector_buffer[] fat_buffers = new sector_buffer[FAT_BUFFERED_SECTORS];

        //-----------------------------------------------------------------------------
        // fatfs_fat_init:
        //-----------------------------------------------------------------------------
        void fatfs_fat_init()
{
	int i;

	// FAT buffer chain head
	this.fat_buffer_head = null;

	for (i=0;i<FAT_BUFFERED_SECTORS;i++)
	{
		// Initialise buffers to invalid
		this.fat_buffers[i].address = FAT32_INVALID_CLUSTER;
		this.fat_buffers[i].dirty = false;
		memset(this.fat_buffers[i].sector, 0x00,this.fat_buffers[i].sector.Length);

		// Add to head of queue
		this.fat_buffers[i].next = this.fat_buffer_head;
		this.fat_buffer_head = &this.fat_buffers[i];
	}
}

        //-----------------------------------------------------------------------------
        // Macros
        //-----------------------------------------------------------------------------

        // Macro for checking if file lib is initialised
        //#define CHECK_FL_INIT()		{ if (Filelib_Init==0) fl_init(); }

        public void FL_LOCK(fatfs a)
        {
            do
            {
                if ((a).fl_lock)
                    (a).fl_lock();
            } while (false);
        }

        public void FL_UNLOCK(fatfs a)
        {
            do
            {
                if ((a).fl_unlock)
                    (a).fl_unlock();
            } while (false);
        }

        #region Access
        //-----------------------------------------------------------------------------
        // fatfs_init: Load FAT32 Parameters
        //-----------------------------------------------------------------------------
        int fatfs_init()
        {
            byte Number_of_FATS;
            ushort Reserved_Sectors;
            uint FATSz;
            uint RootDirSectors;
            uint TotSec;
            uint DataSec;
            uint CountofClusters;
            uint partition_size = 0;
            byte valid_partition = 0;

            this.currentsector.address = FAT32_INVALID_CLUSTER;
            this.currentsector.dirty = false;

            this.next_free_cluster = 0; // Invalid

            fatfs_fat_init(fs);

            // Make sure we have read and write functions
            if (!this.disk_io.read_sector || !this.disk_io.write_sector)
                return FAT_INIT_MEDIA_ACCESS_ERROR;

            // MBR: Sector 0 on the disk
            // NOTE: Some removeable media does not have this.

            // Load MBR (LBA 0) into the 512 byte buffer
            if (!this.disk_io.ReadSector(0, this.currentsector.sector))
                return FAT_INIT_MEDIA_ACCESS_ERROR;

            // Make Sure 0x55 and 0xAA are at end of sector
            // (this should be the case regardless of the MBR or boot sector)
            if (this.currentsector.sector[SIGNATURE_POSITION] != 0x55 || this.currentsector.sector[SIGNATURE_POSITION + 1] != 0xAA)
                return FAT_INIT_INVALID_SIGNATURE;

            // Now check again using the access function to prove endian conversion function
            if (GET_16BIT_WORD(this.currentsector.sector, SIGNATURE_POSITION) != SIGNATURE_VALUE)
                return FAT_INIT_ENDIAN_ERROR;

            // Check the partition type code
            switch (this.currentsector.sector[PARTITION1_TYPECODE_LOCATION])
            {
                case 0x0B:
                case 0x06:
                case 0x0C:
                case 0x0E:
                case 0x0F:
                case 0x05:
                    valid_partition = 1;
                    break;
                case 0x00:
                    valid_partition = 0;
                    break;
                default:
                    if (this.currentsector.sector[PARTITION1_TYPECODE_LOCATION] <= 0x06)
                        valid_partition = 1;
                    break;
            }

            if (valid_partition)
            {
                // Read LBA Begin for the file system
                this.lba_begin = GET_32BIT_WORD(this.currentsector.sector, PARTITION1_LBA_BEGIN_LOCATION);
                partition_size = GET_32BIT_WORD(this.currentsector.sector, PARTITION1_SIZE_LOCATION);
            }
            // Else possibly MBR less disk
            else
                this.lba_begin = 0;

            // Load Volume 1 table into sector buffer
            // (We may already have this in the buffer if MBR less drive!)
            if (!this.disk_io.ReadSector(this.lba_begin, this.currentsector.sector))
                return FAT_INIT_MEDIA_ACCESS_ERROR;

            // Make sure there are 512 bytes per cluster
            if (GET_16BIT_WORD(this.currentsector.sector, 0x0B) != FAT_SECTOR_SIZE)
                return FAT_INIT_INVALID_SECTOR_SIZE;

            // Load Parameters of FAT32	 
            this.sectors_per_cluster = this.currentsector.sector[BPB_SECPERCLUS];
            Reserved_Sectors = GET_16BIT_WORD(this.currentsector.sector, BPB_RSVDSECCNT);
            Number_of_FATS = this.currentsector.sector[BPB_NUMFATS];
            this.fat_sectors = GET_32BIT_WORD(this.currentsector.sector, BPB_FAT32_FATSZ32);
            this.rootdir_first_cluster = GET_32BIT_WORD(this.currentsector.sector, BPB_FAT32_ROOTCLUS);
            this.fs_info_sector = GET_16BIT_WORD(this.currentsector.sector, BPB_FAT32_FSINFO);

            // First FAT LBA address
            this.fat_begin_lba = this.lba_begin + Reserved_Sectors;

            // The address of the first data cluster on this volume
            this.cluster_begin_lba = this.fat_begin_lba + (Number_of_FATS * this.fat_sectors);

            if (GET_16BIT_WORD(this.currentsector.sector, 0x1FE) != 0xAA55) // This signature should be AA55
                return FAT_INIT_INVALID_SIGNATURE;

            // Calculate the root dir sectors
            RootDirSectors = ((GET_16BIT_WORD(this.currentsector.sector, BPB_ROOTENTCNT) * 32) + (GET_16BIT_WORD(this.currentsector.sector, BPB_BYTSPERSEC) - 1)) / GET_16BIT_WORD(this.currentsector.sector, BPB_BYTSPERSEC);

            if (GET_16BIT_WORD(this.currentsector.sector, BPB_FATSZ16) != 0)
                FATSz = GET_16BIT_WORD(this.currentsector.sector, BPB_FATSZ16);
            else
                FATSz = GET_32BIT_WORD(this.currentsector.sector, BPB_FAT32_FATSZ32);

            if (GET_16BIT_WORD(this.currentsector.sector, BPB_TOTSEC16) != 0)
                TotSec = GET_16BIT_WORD(this.currentsector.sector, BPB_TOTSEC16);
            else
                TotSec = GET_32BIT_WORD(this.currentsector.sector, BPB_TOTSEC32);

            DataSec = TotSec - (GET_16BIT_WORD(this.currentsector.sector, BPB_RSVDSECCNT) + (this.currentsector.sector[BPB_NUMFATS] * FATSz) + RootDirSectors);

            if (this.sectors_per_cluster != 0)
            {
                CountofClusters = DataSec / this.sectors_per_cluster;

                if (CountofClusters < 4085)
                    // Volume is FAT12 
                    return FAT_INIT_WRONG_FILESYS_TYPE;
                else if (CountofClusters < 65525)
                    // Volume is FAT16
                    return FAT_INIT_WRONG_FILESYS_TYPE;

                return FAT_INIT_OK;
            }
            else
                return FAT_INIT_WRONG_FILESYS_TYPE;
        }

        //-----------------------------------------------------------------------------
        // fatfs_lba_of_cluster: This function converts a cluster number into a sector / 
        // LBA number.
        //-----------------------------------------------------------------------------
        uint fatfs_lba_of_cluster(uint Cluster_Number)
        {
            return ((this.cluster_begin_lba + ((Cluster_Number - 2) * this.sectors_per_cluster)));
        }

        //-----------------------------------------------------------------------------
        // fatfs_sector_reader: From the provided startcluster and sector offset
        // Returns True if success, returns False if not (including if read out of range)
        //-----------------------------------------------------------------------------
        bool fatfs_sector_reader(uint Startcluster, uint offset, byte[] target)
        {
            uint SectortoRead = 0;
            uint ClustertoRead = 0;
            uint ClusterChain = 0;
            uint i;
            uint lba;

            // Set start of cluster chain to initial value
            ClusterChain = Startcluster;

            // Find parameters
            ClustertoRead = offset / this.sectors_per_cluster;
            SectortoRead = offset - (ClustertoRead * this.sectors_per_cluster);

            // Follow chain to find cluster to read
            for (i = 0; i < ClustertoRead; i++)
                ClusterChain = fatfs_find_next_cluster(fs, ClusterChain);

            // If end of cluster chain then return false
            if (ClusterChain == FAT32_LAST_CLUSTER)
                return 0;

            // Calculate sector address
            lba = fatfs_lba_of_cluster(fs, ClusterChain) + SectortoRead;

            // User provided target array
            if (target)
                return this.disk_io.ReadSector(lba, target);
            // Else read sector if not already loaded
            else if (lba != this.currentsector.address)
            {
                this.currentsector.address = lba;
                return this.disk_io.ReadSector(this.currentsector.address, this.currentsector.sector);
            }
            else
                return 1;
        }

        //-----------------------------------------------------------------------------
        // fatfs_sector_writer: Write to the provided startcluster and sector offset
        // Returns True if success, returns False if not 
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_sector_writer(fatfs fs, uint Startcluster, uint offset, byte *target)
{
 	uint SectortoWrite = 0;
	uint ClustertoWrite = 0;
	uint ClusterChain = 0;
	uint LastClusterChain = FAT32_INVALID_CLUSTER;
	uint i;
	
	// Set start of cluster chain to initial value
	ClusterChain = Startcluster;

	// Find parameters
	ClustertoWrite = offset / fs.sectors_per_cluster;	  
	SectortoWrite = offset - (ClustertoWrite*fs.sectors_per_cluster);

	// Follow chain to find cluster to read
	for (i=0; i<ClustertoWrite; i++)
	{
		// Find next link in the chain
		LastClusterChain = ClusterChain;
	  	ClusterChain = fatfs_find_next_cluster(fs, ClusterChain);

		// Dont keep following a dead end
		if (ClusterChain == FAT32_LAST_CLUSTER)
			break;
	}

	// If end of cluster chain 
	if (ClusterChain == FAT32_LAST_CLUSTER) 
	{
		// Add another cluster to the last good cluster chain
		if (!fatfs_add_free_space(fs, &LastClusterChain))
			return 0;

		ClusterChain = LastClusterChain;
	}

	// User target buffer passed in
	if (target)
	{
		// Calculate write address
		uint lba = fatfs_lba_of_cluster(fs, ClusterChain) + SectortoWrite;

		// Write to disk
		return fs.disk_io.WriteSector(lba, target);
	}
	else
	{
		// Calculate write address
		fs.currentsector.address = fatfs_lba_of_cluster(fs, ClusterChain)+SectortoWrite;

		// Write to disk
		return fs.disk_io.WriteSector(fs.currentsector.address, fs.currentsector.sector);
	}
}
#endif



        //-----------------------------------------------------------------------------
        // fatfs_get_root_cluster: Get the root dir cluster
        //-----------------------------------------------------------------------------
        uint fatfs_get_root_cluster()
        {
            return this.rootdir_first_cluster;
        }

        //-------------------------------------------------------------
        // fatfs_get_file_entry: Find the file entry for a filename
        //-------------------------------------------------------------
        uint fatfs_get_file_entry(uint Cluster, string nametofind, FAT32_ShortEntry sfEntry)
        {
            byte item = 0;
            ushort recordoffset = 0;
            byte i = 0;
            int x = 0;
            string LongFilename;
            char[] ShortFilename = new char[13];
            lfn_cache lfn;
            int dotRequired = 0;
            FAT32_ShortEntry directoryEntry;

            fatfs_lfn_cache_init(&lfn, TRUE);

            // Main cluster following loop
            while (TRUE)
            {
                // Read sector
                if (fatfs_sector_reader(fs, Cluster, x++, FALSE)) // If sector read was successfull
                {
                    // Analyse Sector
                    for (item = 0; item < 16; item++)
                    {
                        // Create the multiplier for sector access
                        recordoffset = (32 * item);

                        // Overlay directory entry over buffer
                        directoryEntry = (FAT32_ShortEntry*)(fs.currentsector.sector + recordoffset);

                        // Long File Name Text Found
                        if (fatfs_entry_lfn_text(directoryEntry))
                            fatfs_lfn_cache_entry(&lfn, fs.currentsector.sector + recordoffset);

                        // If Invalid record found delete any long file name information collated
                        else if (fatfs_entry_lfn_invalid(directoryEntry))
                            fatfs_lfn_cache_init(&lfn, FALSE);

                        // Normal SFN Entry and Long text exists 
                        else if (fatfs_entry_lfn_exists(&lfn, directoryEntry))
                        {
                            LongFilename = fatfs_lfn_cache_get(&lfn);

                            // Compare names to see if they match
                            if (fatfs_compare_names(LongFilename, nametofind))
                            {
                                memcpy(sfEntry, directoryEntry, sizeof(FAT32_ShortEntry));
                                return 1;
                            }

                            fatfs_lfn_cache_init(&lfn, FALSE);
                        }

                        // Normal Entry, only 8.3 Text		 
                        else if (fatfs_entry_sfn_only(directoryEntry))
                        {
                            memset(ShortFilename, 0, sizeof(ShortFilename));

                            // Copy name to string
                            for (i = 0; i < 8; i++)
                                ShortFilename[i] = directoryEntry.Name[i];

                            // Extension
                            dotRequired = 0;
                            for (i = 8; i < 11; i++)
                            {
                                ShortFilename[i + 1] = directoryEntry.Name[i];
                                if (directoryEntry.Name[i] != ' ')
                                    dotRequired = 1;
                            }

                            // Dot only required if extension present
                            if (dotRequired)
                            {
                                // If not . or .. entry
                                if (ShortFilename[0] != '.')
                                    ShortFilename[8] = '.';
                                else
                                    ShortFilename[8] = ' ';
                            }
                            else
                                ShortFilename[8] = ' ';

                            // Compare names to see if they match
                            if (fatfs_compare_names(ShortFilename, nametofind))
                            {
                                memcpy(sfEntry, directoryEntry, sizeof(FAT32_ShortEntry));
                                return 1;
                            }

                            fatfs_lfn_cache_init(&lfn, FALSE);
                        }
                    } // End of if
                }
                else
                    break;
            } // End of while loop

            return 0;
        }

        //-------------------------------------------------------------
        // fatfs_sfn_exists: Check if a short filename exists.
        // NOTE: shortname is XXXXXXXXYYY not XXXXXXXX.YYY
        //-------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_sfn_exists(fatfs fs, uint Cluster, char *shortname)
{
	byte item=0;
	ushort recordoffset = 0;
	int x=0;
	FAT32_ShortEntry *directoryEntry;

	// Main cluster following loop
	while (TRUE)
	{
		// Read sector
		if (fatfs_sector_reader(fs, Cluster, x++, FALSE)) // If sector read was successfull
		{
			// Analyse Sector
			for (item = 0; item < 16; item++)
			{
				// Create the multiplier for sector access
				recordoffset = (32*item);

				// Overlay directory entry over buffer
				directoryEntry = (FAT32_ShortEntry*)(fs.currentsector.sector+recordoffset);

				// Long File Name Text Found
				if (fatfs_entry_lfn_text(directoryEntry) ) 
					;

				// If Invalid record found delete any long file name information collated
				else if (fatfs_entry_lfn_invalid(directoryEntry) ) 
					;

				// Normal Entry, only 8.3 Text		 
				else if (fatfs_entry_sfn_only(directoryEntry) )
				{
					if (strncmp((const char*)directoryEntry.Name, shortname, 11)==0)
						return 1;
				}
			} // End of if
		} 
		else
			break;
	} // End of while loop

	return 0;
}
#endif

        //-------------------------------------------------------------
        // fatfs_update_file_length: Find a SFN entry and update it 
        // NOTE: shortname is XXXXXXXXYYY not XXXXXXXX.YYY
        //-------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_update_file_length(fatfs fs, uint Cluster, char *shortname, uint fileLength)
{
	byte item=0;
	ushort recordoffset = 0;
	int x=0;
	FAT32_ShortEntry *directoryEntry;

	// Main cluster following loop
	while (TRUE)
	{
		// Read sector
		if (fatfs_sector_reader(fs, Cluster, x++, FALSE)) // If sector read was successfull
		{
			// Analyse Sector
			for (item = 0; item < 16; item++)
			{
				// Create the multiplier for sector access
				recordoffset = (32*item);

				// Overlay directory entry over buffer
				directoryEntry = (FAT32_ShortEntry*)(fs.currentsector.sector+recordoffset);

				// Long File Name Text Found
				if (fatfs_entry_lfn_text(directoryEntry) ) 
					;

				// If Invalid record found delete any long file name information collated
				else if (fatfs_entry_lfn_invalid(directoryEntry) ) 
					;

				// Normal Entry, only 8.3 Text		 
				else if (fatfs_entry_sfn_only(directoryEntry) )
				{
					if (strncmp((const char*)directoryEntry.Name, shortname, 11)==0)
					{
						directoryEntry.FileSize = fileLength;
						// TODO: Update last write time

						// Update sfn entry
						memcpy((byte*)(fs.currentsector.sector+recordoffset), (byte*)directoryEntry, sizeof(FAT32_ShortEntry));					

						// Write sector back
						return fs.disk_io.WriteSector(fs.currentsector.address, fs.currentsector.sector);
					}
				}
			} // End of if
		} 
		else
			break;
	} // End of while loop

	return 0;
}
#endif

        //-------------------------------------------------------------
        // fatfs_mark_file_deleted: Find a SFN entry and mark if as deleted 
        // NOTE: shortname is XXXXXXXXYYY not XXXXXXXX.YYY
        //-------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_mark_file_deleted(fatfs fs, uint Cluster, char *shortname)
{
	byte item=0;
	ushort recordoffset = 0;
	int x=0;
	FAT32_ShortEntry *directoryEntry;

	// Main cluster following loop
	while (TRUE)
	{
		// Read sector
		if (fatfs_sector_reader(fs, Cluster, x++, FALSE)) // If sector read was successfull
		{
			// Analyse Sector
			for (item = 0; item < 16; item++)
			{
				// Create the multiplier for sector access
				recordoffset = (32*item);

				// Overlay directory entry over buffer
				directoryEntry = (FAT32_ShortEntry*)(fs.currentsector.sector+recordoffset);

				// Long File Name Text Found
				if (fatfs_entry_lfn_text(directoryEntry) ) 
					;

				// If Invalid record found delete any long file name information collated
				else if (fatfs_entry_lfn_invalid(directoryEntry) ) 
					;

				// Normal Entry, only 8.3 Text		 
				else if (fatfs_entry_sfn_only(directoryEntry) )
				{
					if (strncmp((const char *)directoryEntry.Name, shortname, 11)==0)
					{
						// Mark as deleted
						directoryEntry.Name[0] = 0xE5; 

						// Update sfn entry
						memcpy((byte*)(fs.currentsector.sector+recordoffset), (byte*)directoryEntry, sizeof(FAT32_ShortEntry));					

						// Write sector back
						return fs.disk_io.WriteSector(fs.currentsector.address, fs.currentsector.sector);
					}
				}
			} // End of if
		} 
		else
			break;
	} // End of while loop

	return 0;
}
#endif

        #endregion
    }
}