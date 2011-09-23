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
    using System.IO;
    using System.Text;

    public class FileSystemPath
    {

        //-----------------------------------------------------------------------------
        // fatfs_total_path_levels: Take a filename and path and count the sub levels
        // of folders. E.g. C:\folder\file.zip = 1 level
        // Acceptable input formats are:
        //		c:\folder\file.zip
        //		/dev/etc/samba.conf
        // Returns: -1 = Error, 0 or more = Ok
        //-----------------------------------------------------------------------------
        public static int fatfs_total_path_levels(string path)
        {
            if (path == null)
                return -1;

            int levels = 0;
            int length = path.Length;
            int offset = 0;
            char expectedchar;


            // Acceptable formats:
            //  c:\folder\file.zip
            //  /dev/etc/samba.conf
            if (path[offset] == '/')
            {
                expectedchar = '/';
                offset++;
            }
            else if (path[offset+1] == ':' || path[offset+2] == '\\')
            {
                expectedchar = '\\';
                offset += 3;
            }
            else
                return -1;

            // Count levels in path string
            while (path[offset]>0)
            {
                // Fast forward through actual subdir text to next slash
                for (; path[offset]>0; )
                {
                    // If slash detected escape from for loop
                    if (path[offset] == expectedchar) 
                    { 
                        offset++; 
                        break; 
                    }
                    offset++;
                }

                // Increase number of subdirs founds
                levels++;
            }

            // Subtract the file itself
            return levels - 1;
        }
        
        //-----------------------------------------------------------------------------
        // fatfs_get_substring: Get a substring from 'path' which contains the folder
        // (or file) at the specified level.
        // E.g. C:\folder\file.zip : Level 0 = C:\folder, Level 1 = file.zip
        // Returns: -1 = Error, 0 = Ok
        //-----------------------------------------------------------------------------
        public static int fatfs_get_substring(string path, int levelreq, StringBuilder output, int max_len)
        {
            int i;
            int pathlen = 0;
            int levels = 0;
            int copypnt = 0;
            char expectedchar;

            if (!path || max_len <= 0)
                return -1;

            // Acceptable formats:
            //  c:\folder\file.zip
            //  /dev/etc/samba.conf
            if (*path == '/')
            {
                expectedchar = '/';
                path++;
            }
            else if (path[1] == ':' || path[2] == '\\')
            {
                expectedchar = '\\';
                path += 3;
            }
            else
                return -1;

            // Get string length of path
            pathlen = (int)strlen(path);

            // Loop through the number of times as characters in 'path'
            for (i = 0; i < pathlen; i++)
            {
                // If a '\' is found then increase level
                if (*path == expectedchar) levels++;

                // If correct level and the character is not a '\' or '/' then copy text to 'output'
                if ((levels == levelreq) && (*path != expectedchar) && (copypnt < (max_len - 1)))
                    output[copypnt++] = *path;

                // Increment through path string
                *path++;
            }

            // Null Terminate
            output[copypnt] = '\0';

            // If a string was copied return 0 else return 1
            if (output[0] != '\0')
                return 0;	// OK
            else
                return -1;	// Error
        }

        //-----------------------------------------------------------------------------
        // fatfs_split_path: Full path contains the passed in string. 
        // Returned is the path string and file Name string
        // E.g. C:\folder\file.zip . path = C:\folder  filename = file.zip
        // E.g. C:\file.zip . path = [blank]  filename = file.zip
        //-----------------------------------------------------------------------------
        int fatfs_split_path(string full_path, out string path, int max_path, out string filename, int max_filename)
        {
            int strindex;

            path = Path.GetDirectoryName(full_path);
            filename = Path.GetFileName(full_path);

            // Count the levels to the filepath
            int levels = fatfs_total_path_levels(full_path);
            if (levels == -1)
                return -1;

            //// Get filename part of string
            //if (fatfs_get_substring(full_path, levels, filename, max_filename) != 0)
            //    return -1;

            // If root file
            if (levels == 0)
                path[0] = '\0';
            else
            {
                strindex = (int)strlen(full_path) - (int)strlen(filename);
                if (strindex > max_path)
                    strindex = max_path;

                memcpy(path, full_path, strindex);
                path[strindex - 1] = '\0';
            }

            return 0;
        }

        //-----------------------------------------------------------------------------
        // FileString_StrCmpNoCase: Compare two strings case with case sensitivity
        //-----------------------------------------------------------------------------
        static int FileString_StrCmpNoCase(string s1, string s2, int n)
        {
            int diff;
            char a, b;

            while (n--)
            {
                a = *s1;
                b = *s2;

                // Make lower case if uppercase
                if ((a >= 'A') && (a <= 'Z'))
                    a += 32;
                if ((b >= 'A') && (b <= 'Z'))
                    b += 32;

                diff = a - b;

                // If different
                if (diff)
                    return diff;

                // If run out of strings
                if ((*s1 == 0) || (*s2 == 0))
                    break;

                s1++;
                s2++;
            }
            return 0;
        }
        //-----------------------------------------------------------------------------
        // FileString_GetExtension: Get index to extension within filename
        // Returns -1 if not found or index otherwise
        //-----------------------------------------------------------------------------
        static int FileString_GetExtension(string str)
        {
            int dotPos = -1;
            char* strSrc = str;

            // Find last '.' in string (if at all)
            while (*strSrc)
            {
                if (*strSrc == '.')
                    dotPos = (int)(strSrc - str);

                *strSrc++;
            }

            return dotPos;
        }
        //-----------------------------------------------------------------------------
        // FileString_TrimLength: Get length of string excluding trailing spaces
        // Returns -1 if not found or index otherwise
        //-----------------------------------------------------------------------------
        static int FileString_TrimLength(string str, int strLen)
        {
            int length = strLen;
            char* strSrc = str + strLen - 1;

            // Find last non white space
            while (strLen != 0)
            {
                if (*strSrc == ' ')
                    length = (int)(strSrc - str);
                else
                    break;

                strSrc--;
                strLen--;
            }

            return length;
        }

        //-----------------------------------------------------------------------------
        // fatfs_compare_names: Compare two filenames (without copying or changing origonals)
        // Returns 1 if match, 0 if not
        //-----------------------------------------------------------------------------
        int fatfs_compare_names(string strA, string strB)
        {
            char* ext1 = null;
            char* ext2 = null;
            int ext1Pos, ext2Pos;
            int file1Len, file2Len;

            // Get both files extension
            ext1Pos = FileString_GetExtension(strA);
            ext2Pos = FileString_GetExtension(strB);

            // NOTE: Extension position can be different for matching 
            // filename if trailing space are present before it!
            // Check that if one has an extension, so does the other
            if ((ext1Pos == -1) && (ext2Pos != -1))
                return 0;
            if ((ext2Pos == -1) && (ext1Pos != -1))
                return 0;

            // If they both have extensions, compare them
            if (ext1Pos != -1)
            {
                // Set pointer to start of extension
                ext1 = strA + ext1Pos + 1;
                ext2 = strB + ext2Pos + 1;

                // If they dont match
                if (FileString_StrCmpNoCase(ext1, ext2, (int)strlen(ext1)) != 0)
                    return 0;

                // Filelength is upto extensions
                file1Len = ext1Pos;
                file2Len = ext2Pos;
            }
            // No extensions
            else
            {
                // Filelength is actual filelength
                file1Len = (int)strlen(strA);
                file2Len = (int)strlen(strB);
            }

            // Find length without trailing spaces (before ext)
            file1Len = FileString_TrimLength(strA, file1Len);
            file2Len = FileString_TrimLength(strB, file2Len);

            // Check the file lengths match
            if (file1Len != file2Len)
                return 0;

            // Compare main part of filenames
            if (FileString_StrCmpNoCase(strA, strB, file1Len) != 0)
                return 0;
            else
                return 1;
        }
    }
}