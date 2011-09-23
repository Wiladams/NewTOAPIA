#region Copyright
//-----------------------------------------------------------------------------
//					        FAT32 File IO Library
//								    V2.5
// 	  							 Rob Riglar
//						    Copyright 2003 - 2010
//
//   					  Email: rob@robriglar.com
//
//								License: GPL
//   If you would like a version with a more permissive license for use in
//   closed source commercial applications please contact me for details.
//-----------------------------------------------------------------------------
//
// This file is part of FAT32 File IO Library.
//
// FAT32 File IO Library is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// FAT32 File IO Library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with FAT32 File IO Library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//-----------------------------------------------------------------------------
#endregion

namespace FAT32
{
    public partial class FileAccess : fatlib
    {
        //-----------------------------------------------------------------------------
        // Locals
        //-----------------------------------------------------------------------------
        static FL_FILE[] Files = new FL_FILE[FATFS_MAX_OPEN_FILES];
        static int Filelib_Init = 0;
        static int Filelib_Valid = 0;
        static fatfs Fs;
        static FL_FILE Filelib_open_files = null;
        static FL_FILE Filelib_free_files = null;


        //-----------------------------------------------------------------------------
        // fatfs_lfn_entries_required: Calculate number of 13 characters entries
        //-----------------------------------------------------------------------------
        int fatfs_lfn_entries_required(string filename)
        {
            int length = (int)strlen(filename);

            if (length)
                return (length + MAX_SFN_ENTRY_LENGTH - 1) / MAX_SFN_ENTRY_LENGTH;
            else
                return 0;
        }

        //-----------------------------------------------------------------------------
        // fatfs_filename_to_lfn:
        //-----------------------------------------------------------------------------
        void fatfs_filename_to_lfn(string filename, byte[] buffer, int entry, byte sfnChk)
{
	int i;
	int[] nameIndexes = {1,3,5,7,9,0x0E,0x10,0x12,0x14,0x16,0x18,0x1C,0x1E};

	// 13 characters entries
	int length = (int)strlen(filename);
	int entriesRequired = fatfs_lfn_entries_required(filename);

	// Filename offset
	int start = entry * MAX_SFN_ENTRY_LENGTH;

	// Initialise to zeros
	memset(buffer, 0x00, 32);

	// LFN entry number
	buffer[0] = (byte)(((entriesRequired-1)==entry)?(0x40|(entry+1)):(entry+1));

	// LFN flag
	buffer[11] = 0x0F;

	// Checksum of short filename
	buffer[13] = sfnChk;

	// Copy to buffer
	for (i=0;i<13;i++)
	{
		if ( (start+i) < length )
			buffer[nameIndexes[i]] = filename[start+i];
		else if ( (start+i) == length )
			buffer[nameIndexes[i]] = 0x00;
		else
		{
			buffer[nameIndexes[i]] = 0xFF;
			buffer[nameIndexes[i]+1] = 0xFF;
		}
	}
}

        //-----------------------------------------------------------------------------
        // _allocate_file: Find a slot in the open files buffer for a new file
        //-----------------------------------------------------------------------------
        static FL_FILE _allocate_file()
        {
            // Allocate free file
            FL_FILE file = Filelib_free_files;

            if (file != null)
            {
                Filelib_free_files = file.next;

                // Add to open list
                file.next = Filelib_open_files;
                Filelib_open_files = file;
            }

            return file;
        }

        //-----------------------------------------------------------------------------
        // _check_file_open: Returns true if the file is already open
        //-----------------------------------------------------------------------------
        static int _check_file_open(FL_FILE file)
        {
            FL_FILE openFile = Filelib_open_files;

            // Compare open files
            while (openFile != null)
            {
                // If not the current file 
                if (openFile != file)
                {
                    // Compare path and name
                    if ((fatfs_compare_names(openFile.path, file.path)) && (fatfs_compare_names(openFile.filename, file.filename)))
                        return 1;
                }

                openFile = openFile.next;
            }

            return 0;
        }

        //-----------------------------------------------------------------------------
        // _free_file: Free open file handle
        //-----------------------------------------------------------------------------
        static void _free_file(FL_FILE file)
        {
            FL_FILE openFile = Filelib_open_files;
            FL_FILE lastFile = null;

            // Remove from open list
            while (openFile != null)
            {
                // If the current file 
                if (openFile == file)
                {
                    if (lastFile)
                        lastFile.next = openFile.next;
                    else
                        Filelib_open_files = openFile.next;

                    break;
                }

                lastFile = openFile;
                openFile = openFile.next;
            }

            // Add to free list
            file.next = Filelib_free_files;
            Filelib_free_files = file;
        }

        //-----------------------------------------------------------------------------
        //								Low Level
        //-----------------------------------------------------------------------------

        //-----------------------------------------------------------------------------
        // _open_directory: Cycle through path string to find the start cluster
        // address of the highest subdir.
        //-----------------------------------------------------------------------------
        static int _open_directory(string path, ref ulong pathCluster)
        {
            int levels;
            int sublevel;
            char[] currentfolder = new char[FATFS_MAX_LONG_FILENAME];
            FAT32_ShortEntry sfEntry;
            ulong startcluster;

            // Set starting cluster to root cluster
            startcluster = fatfs_get_root_cluster(&Fs);

            // Find number of levels
            levels = fatfs_total_path_levels(path);

            // Cycle through each level and get the start sector
            for (sublevel = 0; sublevel < (levels + 1); sublevel++)
            {
                if (fatfs_get_substring(path, sublevel, currentfolder, sizeof(currentfolder)) == -1)
                    return 0;

                // Find clusteraddress for folder (currentfolder) 
                if (fatfs_get_file_entry(&Fs, startcluster, currentfolder, &sfEntry))
                    startcluster = (((ulong)sfEntry.FstClusHI) << 16) + sfEntry.FstClusLO;
                else
                    return 0;
            }

            pathCluster = startcluster;

            return 1;
        }

        //-----------------------------------------------------------------------------
        // _create_directory: Cycle through path string and create the end directory
        //-----------------------------------------------------------------------------
        public int _create_directory(string path)
{
	FL_FILE file; 
	FAT32_ShortEntry sfEntry;
	char[] shortFilename = new char[11];
	int tailNum;
	int i;

	// Allocate a new file handle
	file = _allocate_file();
	if (!file)
		return 0;

	// Clear filename
	memset(file.path, '\0', sizeof(file.path));
	memset(file.filename, '\0', sizeof(file.filename));

	// Split full path into filename and directory path
	if (fatfs_split_path((char*)path, file.path, sizeof(file.path), file.filename, sizeof(file.filename)) == -1)
	{
		_free_file(file);
		return 0;
	}

	// Check if file already open
	if (_check_file_open(file))
	{
		_free_file(file);
		return 0;
	}

	// If file is in the root dir
	if (file.path[0] == 0)
		file.parentcluster = fatfs_get_root_cluster(&Fs);
	else
	{
		// Find parent directory start cluster
		if (!_open_directory(file.path, &file.parentcluster))
		{
			_free_file(file);
			return 0;
		}
	}

	// Check if same filename exists in directory
	if (fatfs_get_file_entry(&Fs, file.parentcluster, file.filename,&sfEntry) == 1)
	{
		_free_file(file);
		return 0;
	}

	file.startcluster = 0;

	// Create the file space for the folder (at least one clusters worth!)
	if (!fatfs_allocate_free_space(&Fs, 1, &file.startcluster, 1))
	{
		_free_file(file);
		return 0;
	}

	// Erase new directory cluster
	memset(file.file_data.sector, 0x00, FAT_SECTOR_SIZE);
	for (i=0;i<Fs.sectors_per_cluster;i++)
	{
		if (!fatfs_sector_writer(&Fs, file.startcluster, i, file.file_data.sector))
		{
			_free_file(file);
			return 0;
		}
	}

	// Generate a short filename & tail
	tailNum = 0;
	do 
	{
		// Create a standard short filename (without tail)
		fatfs_lfn_create_sfn(shortFilename, file.filename);

        // If second hit or more, generate a ~n tail		
		if (tailNum != 0)
			fatfs_lfn_generate_tail((char*)file.shortfilename, shortFilename, tailNum);
		// Try with no tail if first entry
		else
			memcpy(file.shortfilename, shortFilename, 11);

		// Check if entry exists already or not
		if (fatfs_sfn_exists(&Fs, file.parentcluster, (char*)file.shortfilename) == 0)
			break;

		tailNum++;
	}
	while (tailNum < 9999);

	// We reached the max number of duplicate short file names (unlikely!)
	if (tailNum == 9999)
	{
		// Delete allocated space
		fatfs_free_cluster_chain(&Fs, file.startcluster);

		_free_file(file);
		return 0;
	}

	// Add file to disk
	if (!fatfs_add_file_entry(&Fs, file.parentcluster, (char*)file.filename, (char*)file.shortfilename, file.startcluster, 0, 1))
	{
		// Delete allocated space
		fatfs_free_cluster_chain(&Fs, file.startcluster);

		_free_file(file);
		return 0;
	}

	// General
	file.filelength = 0;
	file.bytenum = 0;
	file.file_data.address = 0xFFFFFFFF;
	file.file_data.dirty = false;
	file.filelength_changed = 0;
	
	fatfs_fat_purge(&Fs);

	_free_file(file);
	return 1;
}
        //-----------------------------------------------------------------------------
        // _open_file: Open a file for reading
        //-----------------------------------------------------------------------------
        public static FL_FILE _open_file(string path)
{
	FL_FILE file; 
	FAT32_ShortEntry sfEntry;

	// Allocate a new file handle
	file = _allocate_file();
	if (!file)
		return null;

	// Clear filename
	memset(file.path, '\0', sizeof(file.path));
	memset(file.filename, '\0', sizeof(file.filename));

	// Split full path into filename and directory path
	if (fatfs_split_path((char*)path, file.path, sizeof(file.path), file.filename, sizeof(file.filename)) == -1)
	{
		_free_file(file);
		return null;
	}

	// Check if file already open
	if (_check_file_open(file))
	{
		_free_file(file);
		return null;
	}

	// If file is in the root dir
	if (file.path[0]==0)
		file.parentcluster = fatfs_get_root_cluster(&Fs);
	else
	{
		// Find parent directory start cluster
		if (!_open_directory(file.path, &file.parentcluster))
		{
			_free_file(file);
			return null;
		}
	}

	// Using dir cluster address search for filename
	if (fatfs_get_file_entry(&Fs, file.parentcluster, file.filename,&sfEntry))
	{
		// Initialise file details
		memcpy(file.shortfilename, sfEntry.Name, 11);
		file.filelength = sfEntry.FileSize;
		file.bytenum = 0;
		file.startcluster = (((ulong)sfEntry.FstClusHI)<<16) + sfEntry.FstClusLO;
		file.file_data.address = 0xFFFFFFFF;
		file.file_data.dirty = false;
		file.filelength_changed = 0;

		fatfs_fat_purge(&Fs);

		return file;
	}
	else
	{
		_free_file(file);
		return null;
	}
}
        //-----------------------------------------------------------------------------
        // _create_file: Create a new file
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
static FL_FILE* _create_file(const char *filename)
{
	FL_FILE* file; 
	FAT32_ShortEntry sfEntry;
	char shortFilename[11];
	int tailNum;

	// Allocate a new file handle
	file = _allocate_file();
	if (!file)
		return null;

	// Clear filename
	memset(file.path, '\0', sizeof(file.path));
	memset(file.filename, '\0', sizeof(file.filename));

	// Split full path into filename and directory path
	if (fatfs_split_path((char*)filename, file.path, sizeof(file.path), file.filename, sizeof(file.filename)) == -1)
	{
		_free_file(file);
		return null;
	}

	// Check if file already open
	if (_check_file_open(file))
	{
		_free_file(file);
		return null;
	}

	// If file is in the root dir
	if (file.path[0] == 0)
		file.parentcluster = fatfs_get_root_cluster(&Fs);
	else
	{
		// Find parent directory start cluster
		if (!_open_directory(file.path, &file.parentcluster))
		{
			_free_file(file);
			return null;
		}
	}

	// Check if same filename exists in directory
	if (fatfs_get_file_entry(&Fs, file.parentcluster, file.filename,&sfEntry) == 1)
	{
		_free_file(file);
		return null;
	}

	file.startcluster = 0;

	// Create the file space for the file (at least one clusters worth!)
	if (!fatfs_allocate_free_space(&Fs, 1, &file.startcluster, 1))
	{
		_free_file(file);
		return null;
	}

	// Generate a short filename & tail
	tailNum = 0;
	do 
	{
		// Create a standard short filename (without tail)
		fatfs_lfn_create_sfn(shortFilename, file.filename);

        // If second hit or more, generate a ~n tail		
		if (tailNum != 0)
			fatfs_lfn_generate_tail((char*)file.shortfilename, shortFilename, tailNum);
		// Try with no tail if first entry
		else
			memcpy(file.shortfilename, shortFilename, 11);

		// Check if entry exists already or not
		if (fatfs_sfn_exists(&Fs, file.parentcluster, (char*)file.shortfilename) == 0)
			break;

		tailNum++;
	}
	while (tailNum < 9999);

	// We reached the max number of duplicate short file names (unlikely!)
	if (tailNum == 9999)
	{
		// Delete allocated space
		fatfs_free_cluster_chain(&Fs, file.startcluster);

		_free_file(file);
		return null;
	}

	// Add file to disk
	if (!fatfs_add_file_entry(&Fs, file.parentcluster, (char*)file.filename, (char*)file.shortfilename, file.startcluster, 0, 0))
	{
		// Delete allocated space
		fatfs_free_cluster_chain(&Fs, file.startcluster);

		_free_file(file);
		return null;
	}

	// General
	file.filelength = 0;
	file.bytenum = 0;
	file.file_data.address = 0xFFFFFFFF;
	file.file_data.dirty = false;
	file.filelength_changed = 0;
	
	fatfs_fat_purge(&Fs);

	return file;
}
#endif

        //-----------------------------------------------------------------------------
        //								External API
        //-----------------------------------------------------------------------------

        //-----------------------------------------------------------------------------
        // fl_init: Initialise library
        //-----------------------------------------------------------------------------
        void fl_init()
        {
            int i;

            // Add all file objects to free list
            for (i = 0; i < FATFS_MAX_OPEN_FILES; i++)
            {
                Files[i].next = Filelib_free_files;
                Filelib_free_files = &Files[i];
            }

            Filelib_Init = 1;
        }

        //-----------------------------------------------------------------------------
        // fl_attach_locks: 
        //-----------------------------------------------------------------------------
        //void fl_attach_locks(fatfs fs, void (*lock)(void), void (*unlock)(void))
        //{
        //    fs.fl_lock = lock;
        //    fs.fl_unlock = unlock;
        //}

        //-----------------------------------------------------------------------------
        // fl_attach_media: 
        //-----------------------------------------------------------------------------
        int fl_attach_media(fn_diskio_read rd, fn_diskio_write wr)
{
	int res;

	// If first call to library, initialise
	CHECK_FL_INIT();

	Fs.disk_io.read_sector = rd;
	Fs.disk_io.write_sector = wr;

	// Initialise FAT parameters
	if ((res = fatfs_init(&Fs)) != FAT_INIT_OK)
	{
		FAT_PRINTF("FAT_FS: Error could not load FAT details (%d)!\r\n", res);
		return res;
	}

	Filelib_Valid = 1;
	return FAT_INIT_OK;
}

        //-----------------------------------------------------------------------------
        // fl_shutdown: Call before shutting down system
        //-----------------------------------------------------------------------------
        public void fl_shutdown()
        {
            // If first call to library, initialise
            CHECK_FL_INIT();

            FL_LOCK(Fs);
            fatfs_fat_purge(Fs);
            FL_UNLOCK(Fs);
        }


        //-----------------------------------------------------------------------------
        // fl_createdirectory: Create a directory based on a path
        //-----------------------------------------------------------------------------
        public int fl_createdirectory(string path)
        {
            int res;

            FL_LOCK(Fs);
            res = _create_directory(path);
            FL_UNLOCK(Fs);

            return res;
        }

        //-----------------------------------------------------------------------------
        // fopen: Open or Create a file for reading or writing
        //-----------------------------------------------------------------------------
        public FL_FILE fl_fopen(string path, string mode)
        {
            int i;
            FL_FILE file;
            byte flags = 0;

            if (!Filelib_Valid)
                return null;

            if (path == null || mode == null)
                return null;

            // Supported Modes:
            // "r" Open a file for reading. The file must exist. 
            // "w" Create an empty file for writing. If a file with the same name already exists its content is erased and the file is treated as a new empty file.  
            // "a" Append to a file. Writing operations append data at the end of the file. The file is created if it does not exist. 
            // "r+" Open a file for update both reading and writing. The file must exist. 
            // "w+" Create an empty file for both reading and writing. If a file with the same name already exists its content is erased and the file is treated as a new empty file. 
            // "a+" Open a file for reading and appending. All writing operations are performed at the end of the file, protecting the previous content to be overwritten. You can reposition (fseek, rewind) the internal pointer to anywhere in the file for reading, but writing operations will move it back to the end of file. The file is created if it does not exist. 

            for (i = 0; i < (int)strlen(mode); i++)
            {
                switch (tolower(mode[i]))
                {
                    case 'r':
                        flags |= FILE_READ;
                        break;
                    case 'w':
                        flags |= FILE_WRITE;
                        flags |= FILE_ERASE;
                        flags |= FILE_CREATE;
                        break;
                    case 'a':
                        flags |= FILE_WRITE;
                        flags |= FILE_APPEND;
                        flags |= FILE_CREATE;
                        break;
                    case '+':
                        if (flags & FILE_READ)
                            flags |= FILE_WRITE;
                        else if (flags & FILE_WRITE)
                        {
                            flags |= FILE_READ;
                            flags |= FILE_ERASE;
                            flags |= FILE_CREATE;
                        }
                        else if (flags & FILE_APPEND)
                        {
                            flags |= FILE_READ;
                            flags |= FILE_WRITE;
                            flags |= FILE_APPEND;
                            flags |= FILE_CREATE;
                        }
                        break;
                    case 'b':
                        flags |= FILE_BINARY;
                        break;
                }
            }

            file = null;

#if FATFS_INC_WRITE_SUPPORT
	// No write support!
	flags &= ~(FILE_CREATE | FILE_WRITE | FILE_APPEND);
#endif

            FL_LOCK(Fs);

            // Read
            if (flags & FILE_READ)
                file = _open_file(path);

            // Create New
            if (!file && (flags & FILE_CREATE))
                file = _create_file(path);

            // Write Existing (and not open due to read or create)
            if (!(flags & FILE_READ))
                if (!(flags & FILE_CREATE))
                    if (flags & (FILE_WRITE | FILE_APPEND))
                        file = _open_file(path);

            if (file)
                file.flags = flags;

            FL_UNLOCK(Fs);

            return file;
        }

    }
}