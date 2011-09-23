namespace FAT32
{
public class lfn_cache : fatlib
{
	// Long File Name Structure (max 260 LFN length)
	public byte[,] String = new byte[MAX_LONGFILENAME_ENTRIES,MAX_SFN_ENTRY_LENGTH];
	public byte no_of_strings;

//-----------------------------------------------------------------------------
// fatfs_lfn_cache_init: Clear long file name cache
//-----------------------------------------------------------------------------
void fatfs_lfn_cache_init(bool wipeTable)
{
	int i;
	this.no_of_strings = 0;

	// Zero out buffer also
	if (wipeTable)
		for (i=0;i<MAX_LONGFILENAME_ENTRIES;i++)
			memset(this.String[i], 0x00, MAX_SFN_ENTRY_LENGTH);
}

//-----------------------------------------------------------------------------
// fatfs_lfn_cache_entry - Function extracts long file name text from sector 
// at a specific offset
//-----------------------------------------------------------------------------
void fatfs_lfn_cache_entry(byte[] entryBuffer)
{
	byte LFNIndex, i;
	LFNIndex = (byte)(entryBuffer[0] & 0x0F);

	// Limit file name to cache size!
	if (LFNIndex > MAX_LONGFILENAME_ENTRIES)
		return ;

	// This is an error condition
	if (LFNIndex == 0)
		return ;

	if (this.no_of_strings == 0) 
		this.no_of_strings = LFNIndex;

	this.String[LFNIndex-1,0] = entryBuffer[1];
	this.String[LFNIndex-1,1] = entryBuffer[3];
	this.String[LFNIndex-1,2] = entryBuffer[5];
	this.String[LFNIndex-1,3] = entryBuffer[7];
	this.String[LFNIndex-1,4] = entryBuffer[9];
	this.String[LFNIndex-1,5] = entryBuffer[0x0E];
	this.String[LFNIndex-1,6] = entryBuffer[0x10];
	this.String[LFNIndex-1,7] = entryBuffer[0x12];
	this.String[LFNIndex-1,8] = entryBuffer[0x14];
	this.String[LFNIndex-1,9] = entryBuffer[0x16];
	this.String[LFNIndex-1,10] = entryBuffer[0x18];		 		  		  	 		 
	this.String[LFNIndex-1,11] = entryBuffer[0x1C];
	this.String[LFNIndex-1,12] = entryBuffer[0x1E];

	for (i=0; i<MAX_SFN_ENTRY_LENGTH; i++)
		if (this.String[LFNIndex-1,i]==0xFF) 
			this.String[LFNIndex-1,i] = 0x20; // Replace with spaces
} 

//-----------------------------------------------------------------------------
// fatfs_lfn_cache_get: Get a reference to the long filename
//-----------------------------------------------------------------------------
byte[] fatfs_lfn_cache_get()
{
	// Null terminate long filename
	if (this.no_of_strings > 0)
		this.String[this.no_of_strings-1,MAX_SFN_ENTRY_LENGTH-1] = 0;
	else
		this.String[0,0] = 0;

	return this.String[0,0];
}
}
}