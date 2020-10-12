using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aibidia.Common.Extensions
{
    public static class StreamExtensions
    {
        public static void AddText(this Stream stream, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            stream.Write(info, 0, info.Length);
        }
    }
}
