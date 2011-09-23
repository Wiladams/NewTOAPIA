namespace FAT32
{
    public class FAT32_ShortEntry : fatlib
    {
        public byte[] Name = new byte[11];
        public byte Attr;
        public byte NTRes;
        public byte CrtTimeTenth;
        public byte[] CrtTime = new byte[2];
        public byte[] CrtDate = new byte[2];
        public byte[] LstAccDate = new byte[2];
        public ushort FstClusHI;
        public byte[] WrtTime = new byte[2];
        public byte[] WrtDate = new byte[2];
        public ushort FstClusLO;
        public uint FileSize;


        //-----------------------------------------------------------------------------
        // fatfs_entry_lfn_text: If LFN text entry found
        //-----------------------------------------------------------------------------
        int fatfs_entry_lfn_text()
        {
            if ((this.Attr & FILE_ATTR_LFN_TEXT) == FILE_ATTR_LFN_TEXT)
                return 1;
            else
                return 0;
        }
        //-----------------------------------------------------------------------------
        // fatfs_entry_lfn_invalid: If SFN found not relating to LFN
        //-----------------------------------------------------------------------------
        int fatfs_entry_lfn_invalid()
        {
            if ((this.Name[0] == FILE_HEADER_BLANK) ||
                 (this.Name[0] == FILE_HEADER_DELETED) ||
                 (this.Attr == FILE_ATTR_VOLUME_ID) ||
                 (this.Attr & FILE_ATTR_SYSHID))
                return 1;
            else
                return 0;
        }
        //-----------------------------------------------------------------------------
        // fatfs_entry_lfn_exists: If LFN exists and correlation SFN found
        //-----------------------------------------------------------------------------
        int fatfs_entry_lfn_exists(lfn_cache lfn)
        {
            if ((this.Attr != FILE_ATTR_LFN_TEXT) &&
                 (this.Name[0] != FILE_HEADER_BLANK) &&
                 (this.Name[0] != FILE_HEADER_DELETED) &&
                 (this.Attr != FILE_ATTR_VOLUME_ID) &&
                 (!(this.Attr & FILE_ATTR_SYSHID)) &&
                 (lfn.no_of_strings))
                return 1;
            else
                return 0;
        }
        //-----------------------------------------------------------------------------
        // fatfs_entry_sfn_only: If SFN only exists
        //-----------------------------------------------------------------------------
        bool fatfs_entry_sfn_only()
        {
            if ((this.Attr != FILE_ATTR_LFN_TEXT) &&
                 (this.Name[0] != FILE_HEADER_BLANK) &&
                 (this.Name[0] != FILE_HEADER_DELETED) &&
                 (this.Attr != FILE_ATTR_VOLUME_ID) &&
                 (!(this.Attr & FILE_ATTR_SYSHID)))
                return true;
            else
                return false;
        }
        //-----------------------------------------------------------------------------
        // fatfs_entry_is_dir: Returns 1 if a directory
        //-----------------------------------------------------------------------------
        bool fatfs_entry_is_dir()
        {
            if (this.Attr & FILE_TYPE_DIR)
                return true;
            else
                return false;
        }
        //-----------------------------------------------------------------------------
        // fatfs_entry_is_file: Returns 1 is a file entry
        //-----------------------------------------------------------------------------
        bool fatfs_entry_is_file()
        {
            if (this.Attr & FILE_TYPE_FILE)
                return true;
            else
                return false;
        }
    }
}