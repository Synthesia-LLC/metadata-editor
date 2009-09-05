using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Synthesia
{
    public static class FileExtensions
    {
        public static string Md5sum(this FileInfo file)
        {
            if (file == null) throw new InvalidOperationException("Null FileInfo passed to Md5sum()");

            using (FileStream input = file.OpenRead())
                return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(input)).Replace("-", "").ToLower();
        }
    }
}
