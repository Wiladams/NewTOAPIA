namespace FAT32
{

    public class FL_FILE : fatlib
    {
        internal fatfs Fs;   // The file system objects

        public ulong parentcluster;
        public ulong startcluster;
        public ulong bytenum;
        public ulong filelength;
        public int filelength_changed;
        public byte[] path = new byte[FATFS_MAX_LONG_FILENAME];
        public byte[] filename = new byte[FATFS_MAX_LONG_FILENAME];
        public byte[] shortfilename = new byte[11];

        // Read/Write sector buffer
        public sector_buffer file_data;

        // File fopen flags
        public byte flags;
        public const int FILE_READ = 0x01;      // (1 << 0);
        public const int FILE_WRITE = 0x02;     // (1 << 1);
        public const int FILE_APPEND = 0x04;    // (1 << 2);
        public const int FILE_BINARY = 0x08;    // (1 << 3);
        public const int FILE_ERASE = 0x10;     // (1 << 4);
        public const int FILE_CREATE = 0x20;    // (1 << 5);

        public FL_FILE next;


        
        //-----------------------------------------------------------------------------
        // fl_fflush: Flush un-written data to the file
        //-----------------------------------------------------------------------------
        public int fl_fflush()
        {
#if FATFS_INC_WRITE_SUPPORT


		FL_LOCK(Fs);

		// If some write data still in buffer
		if (this.file_data.dirty)
		{
			// Write back current sector before loading next
			if (Fs.fatfs_sector_writer(this.startcluster, this.file_data.address, this.file_data.sector)) 
				this.file_data.dirty = false;
		}

		FL_UNLOCK(Fs);
#endif
            return 0;
        }

        //-----------------------------------------------------------------------------
        // fl_fclose: Close an open file
        //-----------------------------------------------------------------------------
        public void fl_fclose()
        {
                FL_LOCK(Fs);

                // Flush un-written data to file
                fl_fflush();

                // File size changed?
                if (this.filelength_changed)
                {
#if FATFS_INC_WRITE_SUPPORT
			// Update filesize in directory
			fatfs_update_file_length(&Fs, file.parentcluster, (char*)file.shortfilename, file.filelength);
#endif
                    this.filelength_changed = 0;
                }

                this.bytenum = 0;
                this.filelength = 0;
                this.startcluster = 0;
                this.file_data.address = 0xFFFFFFFF;
                this.file_data.dirty = false;
                this.filelength_changed = 0;

                // Free file handle
                _free_file(file);

                Fs.fatfs_fat_purge();

                FL_UNLOCK(Fs);
        }

        //-----------------------------------------------------------------------------
        // fl_fgetc: Get a character in the stream
        //-----------------------------------------------------------------------------
        int fl_fgetc()
        {
            int res;
            byte[] data = new byte[1];

            res = fl_fread(data, 1, 1);

            if (res == 1)
                return (int)data;
            else
                return res;
        }

        //-----------------------------------------------------------------------------
        // fl_fread: Read a block of data from the file
        //-----------------------------------------------------------------------------
        public int fl_fread(byte[] buffer, int size, int length)
        {
            ulong sector;
            ulong offset;
            int copyCount;
            int count = size * length;
            int bytesRead = 0;

            if (buffer == null)
                return -1;

            // No read permissions
            if (!(this.flags & FILE_READ))
                return -1;

            // Nothing to be done
            if (!count)
                return 0;

            // Check if read starts past end of file
            if (this.bytenum >= this.filelength)
                return -1;

            // Limit to file size
            if ((this.bytenum + count) > this.filelength)
                count = this.filelength - this.bytenum;

            // Calculate start sector
            sector = this.bytenum / FAT_SECTOR_SIZE;

            // Offset to start copying data from first sector
            offset = this.bytenum % FAT_SECTOR_SIZE;

            while (bytesRead < count)
            {
                // Do we need to re-read the sector?
                if (this.file_data.address != sector)
                {
                    // Flush un-written data to file
                    if (this.file_data.dirty)
                        fl_fflush();

                    // Read sector of file
                    if (!Fs.fatfs_sector_reader(this.startcluster, sector, this.file_data.sector))
                        // Read failed - out of range (probably)
                        break;

                    this.file_data.address = sector;
                    this.file_data.dirty = false;
                }

                // We have upto one sector to copy
                copyCount = FAT_SECTOR_SIZE - offset;

                // Only require some of this sector?
                if (copyCount > (count - bytesRead))
                    copyCount = (count - bytesRead);

                // Copy to application buffer
                memcpy((byte*)((byte*)buffer + bytesRead), (byte*)(tjos.file_data.sector + offset), copyCount);

                // Increase total read count 
                bytesRead += copyCount;

                // Increment file pointer
                this.bytenum += copyCount;

                // Move onto next sector and reset copy offset
                sector++;
                offset = 0;
            }

            return bytesRead;
        }

        //-----------------------------------------------------------------------------
        // fl_fseek: Seek to a specific place in the file
        //-----------------------------------------------------------------------------
        int fl_fseek(long offset, int origin)
        {
            int res = -1;

            if (origin == SEEK_END && offset != 0)
                return -1;

            FL_LOCK(Fs);

            // Invalidate file buffer
            this.file_data.address = 0xFFFFFFFF;
            this.file_data.dirty = false;

            if (origin == SEEK_SET)
            {
                this.bytenum = (ulong)offset;

                if (this.bytenum > this.filelength)
                    this.bytenum = this.filelength;

                res = 0;
            }
            else if (origin == SEEK_CUR)
            {
                // Positive shift
                if (offset >= 0)
                {
                    this.bytenum += offset;

                    if (this.bytenum > this.filelength)
                        this.bytenum = this.filelength;
                }
                // Negative shift
                else
                {
                    // Make shift positive
                    offset = -offset;

                    // Limit to negative shift to start of file
                    if ((ulong)offset > this.bytenum)
                        this.bytenum = 0;
                    else
                        this.bytenum -= offset;
                }

                res = 0;
            }
            else if (origin == SEEK_END)
            {
                this.bytenum = this.filelength;
                res = 0;
            }
            else
                res = -1;

            FL_UNLOCK(Fs);

            return res;
        }

        //-----------------------------------------------------------------------------
        // fl_fgetpos: Get the current file position
        //-----------------------------------------------------------------------------
        public int fl_fgetpos(ref ulong position)
        {
            FL_LOCK(Fs);

            // Get position
            position = file.bytenum;

            FL_UNLOCK(Fs);

            return 0;
        }

        //-----------------------------------------------------------------------------
        // fl_ftell: Get the current file position
        //-----------------------------------------------------------------------------
        public long fl_ftell()
        {
            ulong pos = 0;

            fl_fgetpos(f, ref pos);

            return (long)pos;
        }

        //-----------------------------------------------------------------------------
        // fl_feof: Is the file pointer at the end of the stream?
        //-----------------------------------------------------------------------------
        public int fl_feof()
        {
            int res;

            FL_LOCK(Fs);

            if (this.bytenum == this.filelength)
                res = EOF;
            else
                res = 0;

            FL_UNLOCK(Fs);

            return res;
        }

        //-----------------------------------------------------------------------------
        // fl_fputc: Write a character to the stream
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fl_fputc(int c, void *f)
{
	byte data = (byte)c;
	int res;

	res = fl_fwrite(&data, 1, 1, f);
	if (res == 1)
		return c;
	else
		return res;
}
#endif

        //-----------------------------------------------------------------------------
        // fl_fwrite: Write a block of data to the stream
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fl_fwrite(const void * data, int size, int count, void *f )
{
	FL_FILE *file = (FL_FILE *)f;
	unsigned long sector;
	unsigned long offset;	
	unsigned long length = (size*count);
	byte *buffer = (byte *)data;
	int dirtySector = 0;
	unsigned long bytesWritten = 0;
	unsigned long copyCount;

	// If first call to library, initialise
	CHECK_FL_INIT();

	if (!file)
		return -1;

	FL_LOCK(&Fs);

	// No write permissions
	if (!(file.flags & FILE_WRITE))
	{
		FL_UNLOCK(&Fs);
		return -1;
	}

	// Append writes to end of file
	if (file.flags & FILE_APPEND)
		file.bytenum = file.filelength;
	// Else write to current position

	// Calculate start sector
	sector = file.bytenum / FAT_SECTOR_SIZE;

	// Offset to start copying data from first sector
	offset = file.bytenum % FAT_SECTOR_SIZE;

	while (bytesWritten < length)
	{
		// We have upto one sector to copy
		copyCount = FAT_SECTOR_SIZE - offset;

		// Only require some of this sector?
		if (copyCount > (length - bytesWritten))
			copyCount = (length - bytesWritten);

		// Do we need to read a new sector?
		if (file.file_data.address != sector)
		{
			// Flush un-written data to file
			if (file.file_data.dirty)
				fl_fflush(file);

			// If we plan to overwrite the whole sector, we don't need to read it first!
			if (copyCount != FAT_SECTOR_SIZE)
			{
				// Read the appropriate sector
				// NOTE: This does not have succeed; if last sector of file
				// reached, no valid data will be read in, but write will 
				// allocate some more space for new data.
				fatfs_sector_reader(&Fs, file.startcluster, sector, file.file_data.sector);
			}

			file.file_data.address = sector;
			file.file_data.dirty = false;
		}

		// Copy from application buffer into sector buffer
		memcpy((byte*)(file.file_data.sector + offset), (byte*)(buffer + bytesWritten), copyCount);

		// Mark buffer as dirty
		file.file_data.dirty = true;
	
		// Increase total read count 
		bytesWritten += copyCount;

		// Increment file pointer
		file.bytenum += copyCount;

		// Move onto next sector and reset copy offset
		sector++;
		offset = 0;
	}

	// Write increased extent of the file?
	if (file.bytenum > file.filelength)
	{
		// Increase file size to new point
		file.filelength = file.bytenum;

		// We are changing the file length and this 
		// will need to be writen back at some point
		file.filelength_changed = 1;
	}

	FL_UNLOCK(&Fs);

	return (size*count);
}
#endif

        //-----------------------------------------------------------------------------
        // fl_fputs: Write a character string to the stream
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fl_fputs(const char * str, void *f)
{
	int len = (int)strlen(str);
	int res = fl_fwrite(str, 1, len, f);

	if (res == len)
		return len;
	else
		return res;
}
#endif

        //-----------------------------------------------------------------------------
        // fl_remove: Remove a file from the filesystem
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fl_remove( const char * filename )
{
	FL_FILE* file;
	int res = -1;

	FL_LOCK(&Fs);

	// Use read_file as this will check if the file is already open!
	file = fl_fopen((char*)filename, "r");
	if (file)
	{
		// Delete allocated space
		if (fatfs_free_cluster_chain(&Fs, file.startcluster))
		{
			// Remove directory entries
			if (fatfs_mark_file_deleted(&Fs, file.parentcluster, (char*)file.shortfilename))
			{
				// Close the file handle (this should not write anything to the file
				// as we have not changed the file since opening it!)
				fl_fclose(file);

				res = 0;
			}
		}
	}

	FL_UNLOCK(&Fs);

	return res;
}
#endif
    }
}