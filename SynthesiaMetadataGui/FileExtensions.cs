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

        // Source: http://stackoverflow.com/a/340454/1744288
        public static string MakeRelativePath(this FileInfo from, FileInfo to)
        {
            if (from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");

            Uri fromUri = new Uri(from.FullName);
            Uri toUri = new Uri(to.FullName);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
