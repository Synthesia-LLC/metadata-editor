using System;
using System.Linq;
using System.Collections.Generic;
using Foundation;
using AppKit;
using CoreGraphics;

namespace Synthesia
{
   public partial class GroupEditorController : NSWindowController
   {
      public class TreeNode : NSObject
      {
         public TreeNode Parent;
         public List<TreeNode> Children = new List<TreeNode>();
         public bool IsGroup;
         public string Name;
         public string SongId;
      }

      public class OutlineSource<Type> : NSOutlineViewDataSource where Type : TreeNode
      {
         public List<Type> Data = new List<Type>();

         public override nint GetChildrenCount(NSOutlineView outlineView, NSObject item)
         {
            if (item == null) return Data.Count;
            return (item as Type).Children.Count;
         }

         public override NSObject GetChild(NSOutlineView outlineView, nint childIndex, NSObject item)
         {
            if (item == null) return Data[(int)childIndex];
            return (item as Type).Children[(int)childIndex];
         }

         public override bool ItemExpandable(NSOutlineView outlineView, NSObject item)
         {
            if (item == null) return true;
            return (item as Type).IsGroup;
         }
      }

      public class GroupOutlineDelegate : NSOutlineViewDelegate
      {
         readonly OutlineSource<TreeNode> Source;
         public Action SelectionChanged;

         public GroupOutlineDelegate(OutlineSource<TreeNode> s, Action selectionChanged = null) { Source = s; SelectionChanged = selectionChanged; }

         public override NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
         {
            NSTableCellView view = (NSTableCellView)outlineView.MakeView("cell", this);
            if (view == null)
            {
               view = new NSTableCellView()
               {
                  Identifier = "cell",
                  ImageView = new NSImageView(new CGRect(2, 0, 16, 16)),
                  TextField = new NSTextField(new CGRect(20, 0, 900, 16))
                  {
                     AutoresizingMask = NSViewResizingMask.WidthSizable,
                     BackgroundColor = NSColor.Clear,
                     Bordered = false,
                     Selectable = false,
                     Editable = false
                  }
               };
               view.AddSubview(view.ImageView);
               view.AddSubview(view.TextField);
            }

            var node = item as TreeNode;
            view.ImageView.Image = NSImage.ImageNamed(node.IsGroup ? "iconGroup" : "iconSong");
            view.TextField.StringValue = node.Name;
            view.TextField.TextColor = node.IsGroup ? NSColor.Blue : NSColor.ControlText;

            return view;
         }

         public override void SelectionDidChange(NSNotification notification) { SelectionChanged?.Invoke(); }
      }

      MetadataFile Metadata { get; set; }
      public bool MadeChanges { get; private set; }

      readonly SimpleTableSource<SongEntry> Songs = new SimpleTableSource<SongEntry>();
      readonly OutlineSource<TreeNode> Groups = new OutlineSource<TreeNode>();

      void PopulateGroup(TreeNode parent, GroupEntry group, List<SongEntry> remainingSongs)
      {
         TreeNode n = new TreeNode() { Name = group.Name, IsGroup = true, Parent = parent };
         (parent == null ? Groups.Data : parent.Children).Add(n);

         foreach (var g in group.Groups) PopulateGroup(n, g, remainingSongs);
         foreach (var s in group.Songs)
         {
            TreeNode node = new TreeNode() { SongId = s.UniqueId, Name = (from r in remainingSongs where r.UniqueId == s.UniqueId select r.ToString()).FirstOrDefault() ?? s.ToString(), IsGroup = false, Parent = n };
            n.Children.Add(node);

            remainingSongs.RemoveAll(song => song.UniqueId == s.UniqueId);
         }
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

         GroupNameBox.Changed += (sender, e) =>
         {
            if (string.IsNullOrWhiteSpace(GroupNameBox.StringValue)) return;
            SelectedNode.Name = Metadata.RenameGroup(PathToSelectedGroup(), GroupNameBox.StringValue.Trim());
            ReloadListsWhilePreservingSelections();
            MadeChanges = true;
         };

         GroupList.DataSource = Groups;
         GroupList.Delegate = new GroupOutlineDelegate(Groups, UpdateControlAvailability);

         SongList.DataSource = Songs;
         SongList.Delegate = new SimpleTableDelegate<SongEntry>(Songs, UpdateControlAvailability);

         var remainingSongs = Metadata.Songs.ToList();
         foreach (GroupEntry g in Metadata.Groups) PopulateGroup(null, g, remainingSongs);
         foreach (SongEntry e in remainingSongs) Songs.Data.Add(e);

         UpdateControlAvailability();
         GroupList.ExpandItem(null, true);
      }

      TreeNode SelectedNode { get { return GroupList.SelectedRowCount == 0 ? null : GroupList.ItemAtRow(GroupList.SelectedRow) as TreeNode; } }

      void ReloadListsWhilePreservingSelections()
      {
         var songRows = SongList.SelectedRows;
         var groupRow = GroupList.SelectedRow;
         SongList.ReloadData();
         GroupList.ReloadData();

         // The rest of this is kind of arduous...

         var songD = SongList.Delegate as SimpleTableDelegate<SongEntry>;
         var songA = songD.SelectionChanged;
         songD.SelectionChanged = null;
         SongList.SelectRows(songRows, false);
         songD.SelectionChanged = songA;

         var groupD = GroupList.Delegate as GroupOutlineDelegate;
         var groupA = groupD.SelectionChanged;
         groupD.SelectionChanged = null;
         GroupList.SelectRow(groupRow, false);
         groupD.SelectionChanged = groupA;
      }

      void UpdateControlAvailability()
      {
         ReloadListsWhilePreservingSelections();

         bool hasSelection = GroupList.SelectedRowCount > 0;
         TreeNode selection = SelectedNode;
         GroupNameBox.Enabled = selection?.IsGroup ?? false;

         // Xamarin.Mac doesn't fire NSTextField.Changed on StringValue
         // set, so we don't have to worry about the change handler here
         GroupNameBox.StringValue = GroupNameBox.Enabled ? SelectedNode.Name : "";

         AddButton.Enabled = hasSelection && SongList.SelectedRowCount > 0;
         RemoveButton.Enabled = hasSelection && !selection.IsGroup;

         bool hasPrev = false, hasNext = false;
         if (hasSelection)
         {
            var parentList = selection.Parent?.Children ?? Groups.Data;
            var id = parentList.IndexOf(selection);

            hasPrev = id > 0 && parentList[id - 1].IsGroup == selection.IsGroup;
            hasNext = id + 1 < parentList.Count && parentList[id + 1].IsGroup == selection.IsGroup;
         }

         UpButton.Enabled = hasPrev;
         DownButton.Enabled = hasNext;
      }

      partial void upClicked(NSObject sender) { SwapSelected(true); }
      partial void downClicked(NSObject sender) { SwapSelected(false); }
      partial void closeClicked(NSObject sender) { Close(); }

      enum MenuItem { CreateTopLevel, CreateSubGroup, RemoveGroup, RemoveSongs };

      [Action("validateMenuItem:")]
      public bool ValidateMenuItem(NSMenuItem item)
      {
         bool selectionIsGroup = SelectedNode?.IsGroup ?? false;
         int songsInSelectedGroup = 0;
         string groupName = "";

         if (GroupList.SelectedRowCount > 0)
         {
            TreeNode n = SelectedNode;
            if (!n.IsGroup) n = n.Parent;

            groupName = n.Name;
            foreach (var t in n.Children) if (!t.IsGroup) ++songsInSelectedGroup;
         }

         switch ((MenuItem)(int)item.Tag)
         {
            case MenuItem.CreateTopLevel:
               return true;

            case MenuItem.CreateSubGroup:
               item.Title = selectionIsGroup ? $"Create group under {groupName}" : "Create sub-group";
               return selectionIsGroup;

            case MenuItem.RemoveGroup:
               item.Title = selectionIsGroup ? $"Remove {groupName}" : "Remove group";
               return selectionIsGroup;

            case MenuItem.RemoveSongs:
               item.Title = songsInSelectedGroup > 0 ? $"Remove all {songsInSelectedGroup} songs from {groupName}" : "Remove all songs from group";
               return songsInSelectedGroup > 0;
         }

         throw new Exception("Unexpected case");
      }

      List<string> PathToSelectedGroup()
      {
         var node = SelectedNode;
         if (node == null) return null;

         if (!node.IsGroup) node = node.Parent;

         var result = new List<string>();
         while (node != null)
         {
            result.Insert(0, node.Name);
            node = node.Parent;
         }

         return result;
      }

      void AddTreeViewGroup(TreeNode parent, string name)
      {
         var parentNodes = parent?.Children ?? Groups.Data;

         // Add it after the last group (but before any songs)
         int index = 0;
         foreach (var n in parentNodes)
         {
            if (!n.IsGroup) break;
            ++index;
         }

         var node = new TreeNode { Name = name, IsGroup = true, Parent = parent };
         parentNodes.Insert(index, node);
         UpdateControlAvailability();

         GroupList.SelectRow(GroupList.RowForItem(node), false);
         Window.MakeFirstResponder(GroupNameBox);
         MadeChanges = true;
      }

      void UnwindSongs(TreeNode group, bool recursive)
      {
         var toRemove = new List<TreeNode>();
         foreach (TreeNode t in group.Children)
         {
            if (!t.IsGroup) toRemove.Add(t);
            else if (recursive) UnwindSongs(t, true);
         }

         foreach (TreeNode t in toRemove)
         {
            group.Children.Remove(t);
            Songs.Data.Add((from s in Metadata.Songs where s.UniqueId == t.SongId select s).FirstOrDefault());
         }

         MadeChanges |= toRemove.Count > 0;
      }

      partial void menuCreateTopLevelClicked(NSObject sender)
      {
         AddTreeViewGroup(null, Metadata.AddGroup(new List<string> { "Group" }));
      }

      partial void menuCreateGroupUnderClicked(NSObject sender)
      {
         var path = PathToSelectedGroup();
         path.Add("Group");

         AddTreeViewGroup(SelectedNode, Metadata.AddGroup(path));
      }

      void SwapSelected(bool withPrev)
      {
			// We assume this is only called in valid circumstances (that an element will exist in the
         // given direction and that it will be the same group/song type as the current selection).

			var selection = SelectedNode;

			var parentList = selection.Parent?.Children ?? Groups.Data;
			var id = parentList.IndexOf(selection);

         var otherId = id + (withPrev ? -1 : 1);
         var other = parentList[otherId];

         var path = PathToSelectedGroup();
         if (selection.IsGroup)
         {
            path.RemoveAt(path.Count - 1);
            Metadata.SwapGroups(path, selection.Name, other.Name);
         }
         else Metadata.SwapSongsInGroup(path, selection.SongId, other.SongId);

         parentList.Remove(selection);
         parentList.Insert(otherId, selection);

         UpdateControlAvailability();
         GroupList.SelectRow(GroupList.RowForItem(selection), false);

			MadeChanges = true;
		}

      bool AskYesNo(string message, string title) { var alert = new NSAlert { MessageText = title, InformativeText = message }; alert.AddButton("No"); alert.AddButton("Yes"); return alert.RunModal() == (int)NSAlertButtonReturn.Second; }

      partial void menuRemoveGroupClicked(NSObject sender)
      {
         if (SelectedNode.Children.Count > 0 && !AskYesNo("This group isn't empty.  Are you sure you want to remove it?", "Remove Group")) return;

         Metadata.RemoveGroup(PathToSelectedGroup());
         UnwindSongs(SelectedNode, true);

         var parentList = SelectedNode.Parent?.Children ?? Groups.Data;
         parentList.Remove(SelectedNode);

         UpdateControlAvailability();
         MadeChanges = true;
      }

      partial void menuRemoveAllSongsClicked(NSObject sender)
      {
         Metadata.RemoveAllSongsFromGroup(PathToSelectedGroup());

         var g = SelectedNode;
         if (!g.IsGroup) g = g.Parent;

         UnwindSongs(g, false);
         UpdateControlAvailability();
         MadeChanges = true;
      }

      partial void addClicked(NSObject sender)
      {
         var selected = (from s in SongList.SelectedRows select Songs.Data[(int)s]).ToList();
         var path = PathToSelectedGroup();

         var g = SelectedNode;
         if (!g.IsGroup) g = g.Parent;

         foreach (var s in selected)
         {
            Metadata.AddSongToGroup(path, s.UniqueId);
            g.Children.Add(new TreeNode { Name = s.ToString(), SongId = s.UniqueId, IsGroup = false, Parent = g });

            Songs.Data.Remove(s);
         }

         UpdateControlAvailability();
         MadeChanges = true;
      }

      partial void removeClicked(NSObject sender)
      {
         var song = (from s in Metadata.Songs where s.UniqueId == SelectedNode.SongId select s).FirstOrDefault();
         if (song == null) return;

         Metadata.RemoveSongFromGroup(PathToSelectedGroup(), song.UniqueId);
         Songs.Data.Add(song);
         SelectedNode.Parent.Children.Remove(SelectedNode);

         UpdateControlAvailability();
         MadeChanges = true;
      }
   }
}
