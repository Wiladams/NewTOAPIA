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

//-----------------------------------------------------------------------------
// fatfs_sfn_create_entry: Create the short filename directory entry
//-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
void fatfs_sfn_create_entry(char *shortfilename, uint size, uint startCluster, FAT32_ShortEntry *entry, int dir)
{
	int i;

	// Copy short filename
	for (i=0;i<11;i++)
		entry.Name[i] = shortfilename[i];

	// Unless we have a RTC we might as well set these to 1980
	entry.CrtTimeTenth = 0x00;
	entry.CrtTime[1] = entry.CrtTime[0] = 0x00;
	entry.CrtDate[1] = 0x00;
	entry.CrtDate[0] = 0x20;
	entry.LstAccDate[1] = 0x00;
	entry.LstAccDate[0] = 0x20;
	entry.WrtTime[1] = entry.WrtTime[0] = 0x00;
	entry.WrtDate[1] = 0x00;
	entry.WrtDate[0] = 0x20;	

	if (!dir)
		entry.Attr = FILE_TYPE_FILE;
	else
		entry.Attr = FILE_TYPE_DIR;

	entry.NTRes = 0x00;

	entry.FstClusHI = (ushort)((startCluster>>16) & 0xFFFF);
	entry.FstClusLO = (ushort)((startCluster>>0) & 0xFFFF);
	entry.FileSize = size;
}
#endif

//-----------------------------------------------------------------------------
// fatfs_lfn_create_sfn: Create a padded SFN 
//-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_lfn_create_sfn(char *sfn_output, char *filename)
{
	int i;
	int dotPos = -1;
	char ext[3];
	int pos;
	int len = (int)strlen(filename);

	// Invalid to start with .
	if (filename[0]=='.')
		return 0;

	memset(sfn_output, ' ', 11);
	memset(ext, ' ', 3);

	// Find dot seperator
	for (i = 0; i< len; i++)
	{
		if (filename[i]=='.')
			dotPos = i;
	}

	// Extract extensions
	if (dotPos!=-1)
	{
		// Copy first three chars of extension
		for (i = (dotPos+1); i < (dotPos+1+3); i++)
			if (i<len)
				ext[i-(dotPos+1)] = filename[i];

		// Shorten the length to the dot position
		len = dotPos;
	}

	// Add filename part
	pos = 0; 
	for (i=0;i<len;i++)
	{
		if ( (filename[i]!=' ') && (filename[i]!='.') )
			sfn_output[pos++] = (char)toupper(filename[i]);
		
		// Fill upto 8 characters
		if (pos==8)
			break;
	}

	// Add extension part
	for (i=8;i<11;i++)
		sfn_output[i] = (char)toupper(ext[i-8]);

	return 1;
}
#endif

//-----------------------------------------------------------------------------
// fatfs_lfn_generate_tail:
// sfn_input = Input short filename, spaced format & in upper case
// sfn_output = Output short filename with tail
//-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_lfn_generate_tail(char *sfn_output, char *sfn_input, uint tailNum)
{
	int tail_chars;
	char tail_str[8];

	if (tailNum > 99999)
		return 0;

	// Convert to number
	memset(tail_str, 0x00, sizeof(tail_str)); 
	tail_str[0] = '~';
	itoa(tailNum, tail_str+1, 10);
	
	// Copy in base filename
    memcpy(sfn_output, sfn_input, 11);
	   
	// Overwrite with tail
	tail_chars = (int)strlen(tail_str);
	memcpy(sfn_output+(8-tail_chars), tail_str, tail_chars);

	return 1;
}
#endif
}