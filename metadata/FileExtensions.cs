using System;
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
         if (from == null) throw new ArgumentNullException(nameof(from));
         if (to == null) throw new ArgumentNullException(nameof(to));

         Uri fromUri, toUri;

         // NOTE: new Uri(something.FullName) gets escaped strangely under Xamarin/Mono, so we pass them in using Uri-format to skip the escaping step
         if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
         {
            fromUri = new Uri("file://" + from.FullName, UriKind.Absolute);
            toUri = new Uri("file://" + to.FullName, UriKind.Absolute);
         }
         else
         {
            fromUri = new Uri(from.FullName);
            toUri = new Uri(to.FullName);
         }

         Uri relativeUri = fromUri.MakeRelativeUri(toUri);
         string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

         return relativePath.Replace('/', Path.DirectorySeparatorChar);
      }
   }
}
