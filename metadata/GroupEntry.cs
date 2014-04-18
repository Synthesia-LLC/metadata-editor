using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synthesia
{
   /// <summary>
   /// Mostly transient data structure for reading.  To effect
   /// change on the group tree, use the methods in MetadataFile.
   /// </summary>
   public class GroupEntry
   {
      public string Name { get; set; }

      List<GroupEntry> _groups = new List<GroupEntry>();
      public List<GroupEntry> Groups { get { return _groups; } }

      List<SongEntry> _songs = new List<SongEntry>();
      public List<SongEntry> Songs { get { return _songs; } }
   }
}
