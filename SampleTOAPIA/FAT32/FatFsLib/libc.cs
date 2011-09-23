namespace stdlib
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class libc
    {
        public const double HUGE_VAL = System.Double.MaxValue;
        public const uint SHRT_MAX = System.UInt16.MaxValue;

        public const int _IONBF = 0;
        public const int _IOFBF = 1;
        public const int _IOLBF = 2;

        public const int SEEK_SET = 0;
        public const int SEEK_CUR = 1;
        public const int SEEK_END = 2;

        /* =================================================================== */
        /*
        ** Local configuration. You can use this space to add your redefinitions
        ** without modifying the main part of the file.
        */

        // standard c library routines

        public static bool isalpha(char c) { return Char.IsLetter(c); }
        public static bool iscntrl(char c) { return Char.IsControl(c); }
        public static bool isdigit(char c) { return Char.IsDigit(c); }
        public static bool islower(char c) { return Char.IsLower(c); }
        public static bool ispunct(char c) { return Char.IsPunctuation(c); }
        public static bool isspace(char c) { return (c == ' ') || (c >= (char)0x09 && c <= (char)0x0D); }
        public static bool isupper(char c) { return Char.IsUpper(c); }
        public static bool isalnum(char c) { return Char.IsLetterOrDigit(c); }
        public static bool isxdigit(char c) { return "0123456789ABCDEFabcdef".IndexOf(c) >= 0; }

        public static bool isalpha(int c) { return Char.IsLetter((char)c); }
        public static bool iscntrl(int c) { return Char.IsControl((char)c); }
        public static bool isdigit(int c) { return Char.IsDigit((char)c); }
        public static bool islower(int c) { return Char.IsLower((char)c); }
        public static bool ispunct(int c) { return ((char)c != ' ') && !isalnum((char)c); } // *not* the same as Char.IsPunctuation
        public static bool isspace(int c) { return ((char)c == ' ') || ((char)c >= (char)0x09 && (char)c <= (char)0x0D); }
        public static bool isupper(int c) { return Char.IsUpper((char)c); }
        public static bool isalnum(int c) { return Char.IsLetterOrDigit((char)c); }
        public static bool isprint(byte c)
        {
            return (c >= (byte)' ') && (c <= (byte)127);
        }

        public static char tolower(char c) { return Char.ToLower(c); }
        public static char toupper(char c) { return Char.ToUpper(c); }
        public static char tolower(int c) { return Char.ToLower((char)c); }
        public static char toupper(int c) { return Char.ToUpper((char)c); }

        public static ulong strtoul(CharPtr s, out CharPtr end, int base_)
        {
            try
            {
                end = new CharPtr(s.chars, s.index);

                // skip over any leading whitespace
                while (end[0] == ' ')
                    end = end.next();

                // ignore any leading 0x
                if ((end[0] == '0') && (end[1] == 'x'))
                    end = end.next().next();
                else if ((end[0] == '0') && (end[1] == 'X'))
                    end = end.next().next();

                // do we have a leading + or - sign?
                bool negate = false;
                if (end[0] == '+')
                    end = end.next();
                else if (end[0] == '-')
                {
                    negate = true;
                    end = end.next();
                }

                // loop through all chars
                bool invalid = false;
                bool had_digits = false;
                ulong result = 0;
                while (true)
                {
                    // get this char
                    char ch = end[0];

                    // which digit is this?
                    int this_digit = 0;
                    if (isdigit(ch))
                        this_digit = ch - '0';
                    else if (isalpha(ch))
                        this_digit = tolower(ch) - 'a' + 10;
                    else
                        break;

                    // is this digit valid?
                    if (this_digit >= base_)
                        invalid = true;
                    else
                    {
                        had_digits = true;
                        result = result * (ulong)base_ + (ulong)this_digit;
                    }

                    end = end.next();
                }

                // were any of the digits invalid?
                if (invalid || (!had_digits))
                {
                    end = s;
                    return System.UInt64.MaxValue;
                }

                // if the value was a negative then negate it here
                if (negate)
                    result = (ulong)-(long)result;

                // ok, we're done
                return (ulong)result;
            }
            catch
            {
                end = s;
                return 0;
            }
        }

        public static void putchar(char ch)
        {
            Console.Write(ch);
        }

        public static void putchar(int ch)
        {
            Console.Write((char)ch);
        }

        public static int parse_scanf(string str, CharPtr fmt, params object[] argp)
        {
            int parm_index = 0;
            int index = 0;
            while (fmt[index] != 0)
            {
                if (fmt[index++] == '%')
                    switch (fmt[index++])
                    {
                        case 's':
                            {
                                argp[parm_index++] = str;
                                break;
                            }
                        case 'c':
                            {
                                argp[parm_index++] = Convert.ToChar(str);
                                break;
                            }
                        case 'd':
                            {
                                argp[parm_index++] = Convert.ToInt32(str);
                                break;
                            }
                        case 'l':
                            {
                                argp[parm_index++] = Convert.ToDouble(str);
                                break;
                            }
                        case 'f':
                            {
                                argp[parm_index++] = Convert.ToDouble(str);
                                break;
                            }
                        //case 'p':
                        //    {
                        //        result += "(pointer)";
                        //        break;
                        //    }
                    }
            }
            return parm_index;
        }

        public static void printf(CharPtr str, params object[] argv)
        {
            Tools.printf(str.ToString(), argv);
        }

        public static void sprintf(CharPtr buffer, CharPtr str, params object[] argv)
        {
            string temp = Tools.sprintf(str.ToString(), argv);
            strcpy(buffer, temp);
        }

        public static int fprintf(Stream stream, CharPtr str, params object[] argv)
        {
            string result = Tools.sprintf(str.ToString(), argv);
            char[] chars = result.ToCharArray();
            byte[] bytes = new byte[chars.Length];
            for (int i = 0; i < chars.Length; i++)
                bytes[i] = (byte)chars[i];
            stream.Write(bytes, 0, bytes.Length);
            return 1;
        }

        public const int EXIT_SUCCESS = 0;
        public const int EXIT_FAILURE = 1;

        public static int errno()
        {
            return -1;	// todo: fix this - mjf
        }

        public static CharPtr strerror(int error)
        {
            return String.Format("error #{0}", error); // todo: check how this works - mjf
        }

        public static CharPtr getenv(CharPtr envname)
        {
            // todo: fix this - mjf
            //if (envname == "LUA_PATH)
            //return "MyPath";
            return null;
        }


        public static int memcmp(CharPtr ptr1, CharPtr ptr2, uint size) { return memcmp(ptr1, ptr2, (int)size); }
        public static int memcmp(CharPtr ptr1, CharPtr ptr2, int size)
        {
            for (int i = 0; i < size; i++)
                if (ptr1[i] != ptr2[i])
                {
                    if (ptr1[i] < ptr2[i])
                        return -1;
                    else
                        return 1;
                }
            return 0;
        }

        public static CharPtr memchr(CharPtr ptr, char c, uint count)
        {
            for (uint i = 0; i < count; i++)
                if (ptr[i] == c)
                    return new CharPtr(ptr.chars, (int)(ptr.index + i));
            return null;
        }

        public static byte[] memset(byte[] ptr, int c, uint count)
        {
            for (uint i = 0; i < count; i++)
                ptr[i] = (byte)c;

            return ptr;
        }

        public static CharPtr strpbrk(CharPtr str, CharPtr charset)
        {
            for (int i = 0; str[i] != '\0'; i++)
                for (int j = 0; charset[j] != '\0'; j++)
                    if (str[i] == charset[j])
                        return new CharPtr(str.chars, str.index + i);
            return null;
        }

        // find c in str
        public static CharPtr strchr(CharPtr str, char c)
        {
            for (int index = str.index; str.chars[index] != 0; index++)
                if (str.chars[index] == c)
                    return new CharPtr(str.chars, index);
            return null;
        }

        public static CharPtr strcpy(CharPtr dst, CharPtr src)
        {
            int i;
            for (i = 0; src[i] != '\0'; i++)
                dst[i] = src[i];
            dst[i] = '\0';
            return dst;
        }

        public static CharPtr strcat(CharPtr dst, CharPtr src)
        {
            int dst_index = 0;
            while (dst[dst_index] != '\0')
                dst_index++;
            int src_index = 0;
            while (src[src_index] != '\0')
                dst[dst_index++] = src[src_index++];
            dst[dst_index++] = '\0';
            return dst;
        }

        public static CharPtr strncat(CharPtr dst, CharPtr src, int count)
        {
            int dst_index = 0;
            while (dst[dst_index] != '\0')
                dst_index++;
            int src_index = 0;
            while ((src[src_index] != '\0') && (count-- > 0))
                dst[dst_index++] = src[src_index++];
            return dst;
        }

        public static uint strcspn(CharPtr str, CharPtr charset)
        {
            int index = str.ToString().IndexOfAny(charset.ToString().ToCharArray());
            if (index < 0)
                index = str.ToString().Length;
            return (uint)index;
        }

        public static CharPtr strncpy(CharPtr dst, CharPtr src, int length)
        {
            int index = 0;
            while ((src[index] != '\0') && (index < length))
            {
                dst[index] = src[index];
                index++;
            }
            while (index < length)
                dst[index++] = '\0';
            return dst;
        }

        public static int strlen(CharPtr str)
        {
            int index = 0;
            while (str[index] != '\0')
                index++;
            return index;
        }

        public static double fmod(double a, double b)
        {
            float quotient = (int)Math.Floor(a / b);
            return a - quotient * b;
        }

        public static double modf(double a, out double b)
        {
            b = Math.Floor(a);
            return a - Math.Floor(a);
        }

        public static long lmod(double a, double b)
        {
            return (long)a % (long)b;
        }

        public static int getc(Stream f)
        {
            return f.ReadByte();
        }

        public static void ungetc(int c, Stream f)
        {
            if (f.Position > 0)
                f.Seek(-1, SeekOrigin.Current);
        }

        public static Stream stdout = Console.OpenStandardOutput();
        public static Stream stdin = Console.OpenStandardInput();
        public static Stream stderr = Console.OpenStandardError();
        public static int EOF = -1;

        public static void fputs(CharPtr str, Stream stream)
        {
            Console.Write(str.ToString());
        }

        public static int feof(Stream s)
        {
            return (s.Position >= s.Length) ? 1 : 0;
        }

        public static int fread(CharPtr ptr, int size, int num, Stream stream)
        {
            int num_bytes = num * size;
            byte[] bytes = new byte[num_bytes];
            try
            {
                int result = stream.Read(bytes, 0, num_bytes);
                for (int i = 0; i < result; i++)
                    ptr[i] = (char)bytes[i];
                return result / size;
            }
            catch
            {
                return 0;
            }
        }

        public static int fwrite(CharPtr ptr, int size, int num, Stream stream)
        {
            int num_bytes = num * size;
            byte[] bytes = new byte[num_bytes];
            for (int i = 0; i < num_bytes; i++)
                bytes[i] = (byte)ptr[i];
            try
            {
                stream.Write(bytes, 0, num_bytes);
            }
            catch
            {
                return 0;
            }
            return num;
        }

        public static int strcmp(CharPtr s1, CharPtr s2)
        {
            if (s1 == s2)
                return 0;
            if (s1 == null)
                return -1;
            if (s2 == null)
                return 1;

            for (int i = 0; ; i++)
            {
                if (s1[i] != s2[i])
                {
                    if (s1[i] < s2[i])
                        return -1;
                    else
                        return 1;
                }
                if (s1[i] == '\0')
                    return 0;
            }
        }

        public static CharPtr fgets(CharPtr str, Stream stream)
        {
            int index = 0;
            try
            {
                while (true)
                {
                    str[index] = (char)stream.ReadByte();
                    if (str[index] == '\n')
                        break;
                    if (index >= str.chars.Length)
                        break;
                    index++;
                }
            }
            catch
            {
            }
            return str;
        }

        public static double frexp(double x, out int expptr)
        {
            expptr = (int)Math.Log(x, 2) + 1;
            double s = x / Math.Pow(2, expptr);
            return s;
        }

        public static double ldexp(double x, int expptr)
        {
            return x * Math.Pow(2, expptr);
        }

        public static CharPtr strstr(CharPtr str, CharPtr substr)
        {
            int index = str.ToString().IndexOf(substr.ToString());
            if (index < 0)
                return null;
            return new CharPtr(str + index);
        }

        public static CharPtr strrchr(CharPtr str, char ch)
        {
            int index = str.ToString().LastIndexOf(ch);
            if (index < 0)
                return null;
            return str + index;
        }

        public static Stream fopen(CharPtr filename, CharPtr mode)
        {
            string str = filename.ToString();
            FileMode filemode = FileMode.Open;
            FileAccess fileaccess = (FileAccess)0;
            for (int i = 0; mode[i] != '\0'; i++)
                switch (mode[i])
                {
                    case 'r':
                        fileaccess = fileaccess | FileAccess.Read;
                        if (!File.Exists(str))
                            return null;
                        break;

                    case 'w':
                        filemode = FileMode.Create;
                        fileaccess = fileaccess | FileAccess.Write;
                        break;
                }
            try
            {
                return new FileStream(str, filemode, fileaccess);
            }
            catch
            {
                return null;
            }
        }

        public static Stream freopen(CharPtr filename, CharPtr mode, Stream stream)
        {
            try
            {
                stream.Flush();
                stream.Close();
            }
            catch { }

            return fopen(filename, mode);
        }

        public static void fflush(Stream stream)
        {
            stream.Flush();
        }

        public static int ferror(Stream stream)
        {
            return 0;	// todo: fix this - mjf
        }

        public static int fclose(Stream stream)
        {
            stream.Close();
            return 0;
        }

        public static Stream tmpfile()
        {
            return new FileStream(Path.GetTempFileName(), FileMode.Create, FileAccess.ReadWrite);
        }

        public static int fscanf(Stream f, CharPtr format, params object[] argp)
        {
            string str = Console.ReadLine();
            return parse_scanf(str, format, argp);
        }

        public static int fseek(Stream f, long offset, int origin)
        {
            try
            {
                f.Seek(offset, (SeekOrigin)origin);
                return 0;
            }
            catch
            {
                return 1;
            }
        }


        public static int ftell(Stream f)
        {
            return (int)f.Position;
        }

        public static int clearerr(Stream f)
        {
            //Debug.Assert(false, "clearerr not implemented yet - mjf");
            return 0;
        }

        public static int setvbuf(Stream stream, CharPtr buffer, int mode, uint size)
        {
            Debug.Assert(false, "setvbuf not implemented yet - mjf");
            return 0;
        }

        public static void memcpy<T>(T[] dst, T[] src, int length)
        {
            for (int i = 0; i < length; i++)
                dst[i] = src[i];
        }

        public static void memcpy<T>(T[] dst, int offset, T[] src, int length)
        {
            for (int i = 0; i < length; i++)
                dst[offset + i] = src[i];
        }

        public static void memcpy<T>(T[] dst, T[] src, int srcofs, int length)
        {
            for (int i = 0; i < length; i++)
                dst[i] = src[srcofs + i];
        }

        public static void memcpy(CharPtr ptr1, CharPtr ptr2, uint size) { memcpy(ptr1, ptr2, (int)size); }
        public static void memcpy(CharPtr ptr1, CharPtr ptr2, int size)
        {
            for (int i = 0; i < size; i++)
                ptr1[i] = ptr2[i];
        }

        public static object VOID(object f) { return f; }


        // one of the primary objectives of this port is to match the C version of Lua as closely as
        // possible. a key part of this is also matching the behaviour of the garbage collector, as
        // that affects the operation of things such as weak tables. in order for this to occur the
        // size of structures that are allocated must be reported as identical to their C++ equivelents.
        // that this means that variables such as global_State.totalbytes no longer indicate the true
        // amount of memory allocated.
        //public static int GetUnmanagedSize(Type t)
        //{
        //    if (t == typeof(global_State))
        //        return 228;
        //    else if (t == typeof(Lua.LG))
        //        return 376;
        //    else if (t == typeof(CallInfo))
        //        return 24;
        //    else if (t == typeof(lua_TValue))
        //        return 16;
        //    else if (t == typeof(Table))
        //        return 32;
        //    else if (t == typeof(Node))
        //        return 32;
        //    else if (t == typeof(GCObject))
        //        return 120;
        //    else if (t == typeof(IGCObjectRef))
        //        return 4;
        //    else if (t == typeof(ArrayRef))
        //        return 4;
        //    else if (t == typeof(Closure))
        //        return 0;	// handle this one manually in the code
        //    else if (t == typeof(Proto))
        //        return 76;
        //    else if (t == typeof(luaL_Reg))
        //        return 8;
        //    else if (t == typeof(luaL_Buffer))
        //        return 524;
        //    else if (t == typeof(lua_State))
        //        return 120;
        //    else if (t == typeof(lua_Debug))
        //        return 100;
        //    else if (t == typeof(CallS))
        //        return 8;
        //    else if (t == typeof(LoadF))
        //        return 520;
        //    else if (t == typeof(LoadS))
        //        return 8;
        //    else if (t == typeof(lua_longjmp))
        //        return 72;
        //    else if (t == typeof(SParser))
        //        return 20;
        //    else if (t == typeof(Token))
        //        return 16;
        //    else if (t == typeof(LexState))
        //        return 52;
        //    else if (t == typeof(FuncState))
        //        return 572;
        //    else if (t == typeof(GCheader))
        //        return 8;
        //    else if (t == typeof(lua_TValue))
        //        return 16;
        //    else if (t == typeof(TString))
        //        return 16;
        //    else if (t == typeof(LocVar))
        //        return 12;
        //    else if (t == typeof(UpVal))
        //        return 32;
        //    else if (t == typeof(CClosure))
        //        return 40;
        //    else if (t == typeof(LClosure))
        //        return 24;
        //    else if (t == typeof(TKey))
        //        return 16;
        //    else if (t == typeof(ConsControl))
        //        return 40;
        //    else if (t == typeof(LHS_assign))
        //        return 32;
        //    else if (t == typeof(expdesc))
        //        return 24;
        //    else if (t == typeof(upvaldesc))
        //        return 2;
        //    else if (t == typeof(BlockCnt))
        //        return 12;
        //    else if (t == typeof(Zio))
        //        return 20;
        //    else if (t == typeof(Mbuffer))
        //        return 12;
        //    else if (t == typeof(LoadState))
        //        return 16;
        //    else if (t == typeof(MatchState))
        //        return 272;
        //    else if (t == typeof(stringtable))
        //        return 12;
        //    else if (t == typeof(FilePtr))
        //        return 4;
        //    else if (t == typeof(Udata))
        //        return 24;
        //    else if (t == typeof(Char))
        //        return 1;
        //    else if (t == typeof(UInt16))
        //        return 2;
        //    else if (t == typeof(Int16))
        //        return 2;
        //    else if (t == typeof(UInt32))
        //        return 4;
        //    else if (t == typeof(Int32))
        //        return 4;
        //    else if (t == typeof(Single))
        //        return 4;
        //    Debug.Assert(false, "Trying to get unknown sized of unmanaged type " + t.ToString());
        //    return 0;
        //}
    }
}