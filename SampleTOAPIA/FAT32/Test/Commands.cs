//-----------------------------------------------------------------------------
// fl_listdirectory: List a directory based on a path
//-----------------------------------------------------------------------------
void fl_listdirectory(const char *path)
{
	int levels;
	uint cluster = FAT32_INVALID_CLUSTER;

	// If first call to library, initialise
	CHECK_FL_INIT();

	FL_LOCK(&Fs);

	levels = fatfs_total_path_levels((char*)path) + 1;

	// If path is in the root dir
	if (levels == 0)
		cluster = fatfs_get_root_cluster(&Fs);
	// Find parent directory start cluster
	else
		_open_directory((char*)path, &cluster);

	if (cluster != FAT32_INVALID_CLUSTER)
		fatfs_list_directory(&Fs, cluster);

	FL_UNLOCK(&Fs);
}

//-----------------------------------------------------------------------------
        // ListDirectory: Using starting cluster number of a directory and the FAT,
        //				  list all directories and files 
        //-----------------------------------------------------------------------------
        void fatfs_list_directory(uint StartCluster)
{
	byte i,item;
	ushort recordoffset;
	byte LFNIndex=0;
	uint x=0;
	FAT32_ShortEntry directoryEntry;
	CharPtr LongFilename;
	char[] ShortFilename = new char[13];
	lfn_cache lfn;
	int dotRequired = 0;
 
	this.filenumber=0;
	FAT_PRINTF("\r\nNo.             Filename\r\n");

	fatfs_lfn_cache_init(&lfn, TRUE);
	
	while (true)
	{
		// If data read OK
		if (fatfs_sector_reader(fs, StartCluster, x++, FALSE))
		{
			LFNIndex=0;

			// Maximum of 16 directory entries
			for (item = 0; item < 16; item++)
			{
				// Increase directory offset 
				recordoffset = (32*item);

				// Overlay directory entry over buffer
				directoryEntry = (FAT32_ShortEntry*)(fs.currentsector.sector+recordoffset);
		 
				// Long File Name Text Found
				if ( fatfs_entry_lfn_text(directoryEntry) )   
					fatfs_lfn_cache_entry(&lfn, fs.currentsector.sector+recordoffset);
				 	 
				// If Invalid record found delete any long file name information collated
				else if ( fatfs_entry_lfn_invalid(directoryEntry) ) 	
					fatfs_lfn_cache_init(&lfn, FALSE);

				// Normal SFN Entry and Long text exists 
				else if (fatfs_entry_lfn_exists(&lfn, directoryEntry) ) 
				{
					fs.filenumber++; //File / Dir Count

					// Get text
					LongFilename = fatfs_lfn_cache_get(&lfn);

		 			if (fatfs_entry_is_dir(directoryEntry)) FAT_PRINTF(("\r\nDirectory "));
    				if (fatfs_entry_is_file(directoryEntry)) FAT_PRINTF(("\r\nFile "));

					// Print Filename
					FAT_PRINTF(("%d - %s [%d bytes] (0x%08lx)",fs.filenumber, LongFilename, directoryEntry.FileSize, (directoryEntry.FstClusHI<<16)|directoryEntry.FstClusLO));

		 			fatfs_lfn_cache_init(&lfn, FALSE);
				}
				 
				// Normal Entry, only 8.3 Text		 
				else if ( fatfs_entry_sfn_only(directoryEntry) )
				{
       				fatfs_lfn_cache_init(&lfn, FALSE);
					fs.filenumber++; //File / Dir Count
					
		 			if (fatfs_entry_is_dir(directoryEntry)) FAT_PRINTF(("\r\nDirectory "));
    				if (fatfs_entry_is_file(directoryEntry)) FAT_PRINTF(("\r\nFile "));

					memset(ShortFilename, 0, sizeof(ShortFilename));

					// Copy name to string
					for (i=0; i<8; i++) 
						ShortFilename[i] = directoryEntry.Name[i];

					// Extension
					dotRequired = 0;
					for (i=8; i<11; i++) 
					{
						ShortFilename[i+1] = directoryEntry.Name[i];
						if (directoryEntry.Name[i] != ' ')
							dotRequired = 1;
					}

					// Dot only required if extension present
					if (dotRequired)
					{
						// If not . or .. entry
						if (ShortFilename[0]!='.')
							ShortFilename[8] = '.';
						else
							ShortFilename[8] = ' ';
					}
					else
						ShortFilename[8] = ' ';
		  			
					// Print Filename
					FAT_PRINTF("%d - %s",fs.filenumber, ShortFilename);
					 					
				}
			}// end of for
		}
		else
			break;
	}
}
