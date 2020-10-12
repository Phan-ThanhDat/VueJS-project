using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Aibidia.Common.Extensions
{
    public static class SHA256Extensions
    {
        public static string HashString(this SHA256 hasher, string content)
        {
            byte[] hashbytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(content));
            var hashBuilder = new StringBuilder();
            foreach (byte b in hashbytes)
            {
                hashBuilder.Append(b.ToString("x2"));
            }
            return hashBuilder.ToString();
        }
    }
}
