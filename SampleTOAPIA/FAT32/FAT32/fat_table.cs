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
    using stdlib;

    public partial class fatfs : fatlib
    {


        //-----------------------------------------------------------------------------
        //							FAT Sector Buffer
        //-----------------------------------------------------------------------------
        public static int FAT32_GET_32BIT_WORD(sector_buffer pbuf, int location)
        {
            return (int)GET_32BIT_WORD(pbuf.sector, location);
        }

        public static void FAT32_SET_32BIT_WORD(sector_buffer pbuf, int location, int value)
        {
            SET_32BIT_WORD(pbuf.sector, location, (uint)value);
            pbuf.dirty = true;
        }

        //-----------------------------------------------------------------------------
        // fatfs_fat_read_sector: Read a FAT sector
        //-----------------------------------------------------------------------------
        static sector_buffer fatfs_fat_read_sector(fatfs fs, uint sector)
        {
            sector_buffer last = null;
            sector_buffer pcur = fs.fat_buffer_head;

            // Itterate through sector buffer list
            while (pcur != null)
            {
                // Sector already in sector list
                if (pcur.address == sector)
                    break;

                // End of list?
                if (pcur.next == null)
                {
                    // Remove buffer from list
                    if (last != null)
                        last.next = null;
                    // We the first and last buffer in the chain?
                    else
                        fs.fat_buffer_head = null;
                }

                last = pcur;
                pcur = pcur.next;
            }

            // We found the sector already in FAT buffer chain
            if (pcur != null)
                return pcur;

            // Else, we removed the last item from the list
            pcur = last;

            // Add to start of sector buffer list (now newest sector)
            pcur.next = fs.fat_buffer_head;
            fs.fat_buffer_head = pcur;

            // Writeback sector if changed
            if (pcur.dirty)
            {
                if (!fs.disk_io.WriteSector(pcur.address, pcur.sector))
                    return null;

                // Now no longer 'dirty'
                pcur.dirty = false;
            }

            // Address is now new sector
            pcur.address = sector;

            // Read next sector
            if (!fs.disk_io.ReadSector(pcur.address, pcur.sector))
            {
                // Read failed, invalidate buffer address
                pcur.address = FAT32_INVALID_CLUSTER;
                return null;
            }

            return pcur;
        }

        //-----------------------------------------------------------------------------
        // fatfs_fat_purge: Purge 'dirty' FAT sectors to disk
        //-----------------------------------------------------------------------------
        bool fatfs_fat_purge(fatfs fs)
        {
            sector_buffer pcur = fs.fat_buffer_head;

            // Itterate through sector buffer list
            while (pcur != null)
            {
                // Writeback sector if changed
                if (pcur.dirty)
                {
                    if (!fs.disk_io.WriteSector(pcur.address, pcur.sector))
                        return false;

                    pcur.dirty = false;
                }
                pcur = pcur.next;
            }

            return true;
        }

        //-----------------------------------------------------------------------------
        //						General FAT Table Operations
        //-----------------------------------------------------------------------------

        //-----------------------------------------------------------------------------
        // fatfs_find_next_cluster: Return cluster number of next cluster in chain by 
        // reading FAT table and traversing it. Return 0xffffffff for end of chain.
        //-----------------------------------------------------------------------------
        uint fatfs_find_next_cluster(fatfs fs, uint current_cluster)
        {
            uint fat_sector_offset;
            uint position;
            uint nextcluster;

            sector_buffer pbuf;

            // Why is '..' labelled with cluster 0 when it should be 2 ??
            if (current_cluster == 0)
                current_cluster = 2;

            // Find which sector of FAT table to read
            fat_sector_offset = current_cluster / 128;

            // Read FAT sector into buffer
            pbuf = fatfs_fat_read_sector(fs, fs.fat_begin_lba + fat_sector_offset);
            if (!pbuf)
                return (FAT32_LAST_CLUSTER);

            // Find 32 bit entry of current sector relating to cluster number 
            position = (current_cluster - (fat_sector_offset * 128)) * 4;

            // Read Next Clusters value from Sector Buffer
            nextcluster = FAT32_GET_32BIT_WORD(pbuf, (ushort)position);

            // Mask out MS 4 bits (its 28bit addressing)
            nextcluster = nextcluster & 0x0FFFFFFF;

            // If 0x0FFFFFFF then end of chain found
            if (nextcluster == 0x0FFFFFFF)
                return (FAT32_LAST_CLUSTER);
            else
                // Else return next cluster
                return (nextcluster);
        }
        //-----------------------------------------------------------------------------
        // fatfs_set_fs_info_next_free_cluster: Write the next free cluster to the FSINFO table
        //-----------------------------------------------------------------------------
        void fatfs_set_fs_info_next_free_cluster(uint newValue)
        {
            // Load sector to change it
            sector_buffer pbuf = fs.fatfs_fat_read_sector(this.lba_begin + this.fs_info_sector);
            if (pbuf == null)
                return;

            // Change 
            FAT32_SET_32BIT_WORD(pbuf, 492, newValue);

            fs.next_free_cluster = newValue;
        }

        //-----------------------------------------------------------------------------
        // fatfs_find_blank_cluster: Find a free cluster entry by reading the FAT
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_find_blank_cluster(fatfs fs, uint start_cluster, uint *free_cluster)
{
	uint fat_sector_offset, position;
	uint nextcluster;
	uint current_cluster = start_cluster;
	struct sector_buffer *pbuf;

	do
	{
		// Find which sector of FAT table to read
		fat_sector_offset = current_cluster / 128;

		if ( fat_sector_offset < fs.fat_sectors)
		{
			// Read FAT sector into buffer
			pbuf = fatfs_fat_read_sector(fs, fs.fat_begin_lba+fat_sector_offset);
			if (!pbuf)
				return 0;

			// Find 32 bit entry of current sector relating to cluster number 
			position = (current_cluster - (fat_sector_offset * 128)) * 4; 

			// Read Next Clusters value from Sector Buffer
			nextcluster = FAT32_GET_32BIT_WORD(pbuf, (ushort)position);	 

			// Mask out MS 4 bits (its 28bit addressing)
			nextcluster = nextcluster & 0x0FFFFFFF;		

			if (nextcluster !=0 )
				current_cluster++;
		}
		else
			// Otherwise, run out of FAT sectors to check...
			return 0;
	}
	while (nextcluster != 0x0);

	// Found blank entry
	*free_cluster = current_cluster;
	return 1;
} 
#endif

        //-----------------------------------------------------------------------------
        // fatfs_fat_set_cluster: Set a cluster link in the chain. NOTE: Immediate
        // write (slow).
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_fat_set_cluster(fatfs fs, uint cluster, uint next_cluster)
{
	struct sector_buffer *pbuf;
	uint fat_sector_offset, position;

	// Find which sector of FAT table to read
	fat_sector_offset = cluster / 128;

	// Read FAT sector into buffer
	pbuf = fatfs_fat_read_sector(fs, fs.fat_begin_lba+fat_sector_offset);
	if (!pbuf)
		return 0;

	// Find 32 bit entry of current sector relating to cluster number 
	position = (cluster - (fat_sector_offset * 128)) * 4; 

	// Write Next Clusters value to Sector Buffer
	FAT32_SET_32BIT_WORD(pbuf, (ushort)position, next_cluster);	 

	return 1;					 
} 
#endif

        //-----------------------------------------------------------------------------
        // fatfs_free_cluster_chain: Follow a chain marking each element as free
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_free_cluster_chain(fatfs fs, uint start_cluster)
{
	uint last_cluster;
	uint next_cluster = start_cluster;
	
	// Loop until end of chain
	while ( (next_cluster != FAT32_LAST_CLUSTER) && (next_cluster != 0x00000000) )
	{
		last_cluster = next_cluster;

		// Find next link
		next_cluster = fatfs_find_next_cluster(fs, next_cluster);

		// Clear last link
		fatfs_fat_set_cluster(fs, last_cluster, 0x00000000);
	}

	return 1;
} 
#endif

        //-----------------------------------------------------------------------------
        // fatfs_fat_add_cluster_to_chain: Follow a chain marking and then add a new entry
        // to the current tail.
        //-----------------------------------------------------------------------------
#if FATFS_INC_WRITE_SUPPORT
int fatfs_fat_add_cluster_to_chain(fatfs fs, uint start_cluster, uint newEntry)
{
	uint last_cluster = FAT32_LAST_CLUSTER;
	uint next_cluster = start_cluster;

	if (start_cluster == FAT32_LAST_CLUSTER)
		return 0;
	
	// Loop until end of chain
	while ( next_cluster != FAT32_LAST_CLUSTER )
	{
		last_cluster = next_cluster;

		// Find next link
		next_cluster = fatfs_find_next_cluster(fs, next_cluster);
		if (!next_cluster)
			return 0;
	}

	// Add link in for new cluster
	fatfs_fat_set_cluster(fs, last_cluster, newEntry);

	// Mark new cluster as end of chain
	fatfs_fat_set_cluster(fs, newEntry, FAT32_LAST_CLUSTER);

	return 1;
} 
#endif
    }
}