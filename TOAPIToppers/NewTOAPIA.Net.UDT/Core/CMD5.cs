namespace NewTOAPIA.Net.Udt
{
    using System.Security.Cryptography;

    public class CMD5
    {
        public static void compute(byte[] input, out byte[] result)
        {
            MD5 md5Hasher = MD5.Create();

            result = md5Hasher.ComputeHash(input);
        }
    }
}
