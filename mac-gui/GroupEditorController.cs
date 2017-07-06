using System;
using System.Linq;
using System.Collections.Generic;
using Foundation;
using AppKit;

namespace Synthesia
{
   public partial class GroupEditorController : NSWindowController
   {
      MetadataFile Metadata { get; set; }
      public bool MadeChanges { get; private set; }

      bool RenameBoxEnabled = true;

      void PopulateGroup(NSTreeNode parent, GroupEntry group, List<SongEntry> remainingSongs)
      {
         // TODO
      }

      [Export("initWithCoder:")]
      public GroupEditorController(NSCoder coder) : base(coder) { }
      public GroupEditorController(IntPtr handle) : base(handle) { }
      public GroupEditorController(MetadataFile file) : base("GroupEditor") { Metadata = file; }

      public new GroupEditor Window { get { return (GroupEditor)base.Window; } }

      public override void AwakeFromNib()
      {
         base.AwakeFromNib();
         Window.WillClose += (sender, e) => { NSApplication.SharedApplication.StopModal(); };

         GroupNameBox.Changed += (sender, e) => { /* TODO */ };

         var remainingSongs = Metadata.Songs.ToList();
         foreach (GroupEntry g in Metadata.Groups) PopulateGroup(null, g, remainingSongs);
         foreach (SongEntry e in remainingSongs) { /* TODO */ }
         /* TODO: GroupList.ExpandAll */

         UpdateControlAvailability();
      }

      bool SelectionIsGroup
      {
         get
         {
            // TODO
            return false;
         }
      }

      void UpdateControlAvailability()
      {
         // TODO
      }

      // TODO: GroupList::AfterSelect
      // TODO: SongList::SelectedIndexChanged
      partial void upClicked(NSObject sender) { SwapSelected(true); }
      partial void downClicked(NSObject sender) { SwapSelected(false); }
      partial void closeClicked(NSObject sender) { Close(); }

      // TODO: Context menu opening

      List<string> PathToSelectedGroup()
      {
         // TODO
         return null;
      }

      void AddTreeViewGroup(NSTreeNode parent, string name)
      {
         // TODO
      }

      void AddTreeViewSong(NSTreeNode parent, SongEntry song)
      {
         // TODO
      }

      void UnwindSongs(NSTreeNode group, bool recursive)
      {
         // TODO
      }

      // TODO: create top level
      // TODO: create sub group

      void SwapSelected(bool withPrev)
      {
         // TODO
      }

      // TODO: RemoveGroup

      partial void addClicked(NSObject sender)
      {
         // TODO
      }

      // TODO: RemoveSongsFromGroup

      partial void removeClicked(NSObject sender)
      {
         // TODO
      }

   }
}
