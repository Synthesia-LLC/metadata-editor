using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Synthesia
{
   public class RawMetadataFile
   {
      FileInfo SourcePath { get; set; }
      XDocument Raw { get; set; }

      public RawMetadataFile(FileInfo f)
      {
         SourcePath = f;

         using (FileStream input = f.OpenRead())
         using (var reader = new StreamReader(input))
            Raw = XDocument.Load(reader, LoadOptions.None);

         XElement top = Raw.Root;
         if (top == null || top.Name != "SynthesiaMetadata") throw new InvalidOperationException("Stream does not contain a valid Synthesia metadata file.");
         if (top.AttributeOrDefault("Version") != "1") throw new InvalidOperationException("Unknown Synthesia metadata version.  A newer version of this editor may be available.");
         if (top.Element("Songs") == null) throw new InvalidOperationException("Metadata file is missing Songs section.");
      }

      /// <summary>
      /// Adds all the songs from the passed in metadata file to this one.  Returns true if changes were made to this file.
      /// </summary>
      public bool AddRange(RawMetadataFile other, Action<string> log)
      {
         var theirSongs = other.Raw.XPathSelectElements("/SynthesiaMetadata/Songs/Song");
         if (!theirSongs.Any())
         {
            log("SKIPPING: No songs found.");
            return false;
         }

         var ourSongs = Raw.Root.Element("Songs");

         bool madeChanges = false;
         foreach (XElement s in theirSongs)
         {
            string id = s.AttributeOrDefault("UniqueId");
            if (string.IsNullOrWhiteSpace(id))
            {
               log("SKIPPING song without a UniqueId.");
               continue;
            }

            if (Raw.XPathSelectElement(string.Format("/SynthesiaMetadata/Songs/Song[@UniqueId='{0}']", id)) != null)
            {
               log(string.Format("SKIPPING duplicate song \"{0}\"!", s.AttributeOrDefault("Title", "(No Title)")));
               continue;
            }

            ourSongs.Add(s);
            madeChanges = true;
         }

         var groups = (from s in other.StatisticsList where s.Key == "Groups" select s.Value).SingleOrDefault();
         if (groups > 0) log(string.Format("SKIPPING {0} groups.  Groups must be migrated manually!", groups));

         return madeChanges;
      }

      public string Statistics
      {
         get { return string.Join(", ", from s in StatisticsList select string.Format("{0} {1}", s.Value, s.Key)); }
      }

      public IEnumerable<KeyValuePair<string, int>> StatisticsList
      {
         get
         {
            yield return new KeyValuePair<string, int>("Songs", Raw.XPathSelectElements("/SynthesiaMetadata/Songs/Song").Count());
            yield return new KeyValuePair<string, int>("Groups", Raw.XPathSelectElements("/SynthesiaMetadata/Groups//Group").Count());
         }
      }

      /// <summary>
      /// Saves over the previous location where this metadata was originally loaded
      /// </summary>
      public void Save()
      {
         using (FileStream output = SourcePath.Create()) Raw.Save(output);
      }

   }
}
