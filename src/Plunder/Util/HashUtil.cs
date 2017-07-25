using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Plunder.Util
{
    public static class HashUtil
    {
        public static string Md5(string input)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            var bytes = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            var sBuilder = new StringBuilder();
            var result = bytes.Select(b => $"{b:x2}").ToArray();
            var output = string.Join(string.Empty, result);
            return output;
        }
    }
}
