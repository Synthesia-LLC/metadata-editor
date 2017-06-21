using System.Collections.Generic;

namespace Synthesia
{
   /// <summary>
   /// Mostly transient data structure for reading.  To effect
   /// change on the group tree, use the methods in MetadataFile.
   /// </summary>
   public class GroupEntry
   {
      public string Name { get; set; }
      public List<GroupEntry> Groups { get; } = new List<GroupEntry>();
      public List<SongEntry> Songs { get; } = new List<SongEntry>();
   }
}
