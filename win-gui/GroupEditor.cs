using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Synthesia
{
    public partial class GroupEditor : Form
    {
        MetadataFile Metadata { get; set; }

        public bool MadeChanges { get; private set; }

        bool RenameBoxEnabled = true;

        private void PopulateGroup(TreeNode parent, GroupEntry group, List<SongEntry> remainingSongs)
        {
            TreeNodeCollection nodes = parent == null ? GroupList.Nodes : parent.Nodes;

            TreeNode groupNode = nodes.Add(group.Name);
            groupNode.ForeColor = Color.RoyalBlue;

            foreach (var g in group.Groups) PopulateGroup(groupNode, g, remainingSongs);
            foreach (var s in group.Songs)
            {
                TreeNode node = groupNode.Nodes.Add(s.UniqueId, (from r in remainingSongs where r.UniqueId == s.UniqueId select r.ToString()).FirstOrDefault() ?? "Unknown Song", 1, 1);

                SongEntry matching = remainingSongs.Find(song => song.UniqueId == s.UniqueId) ?? s;
                node.Tag = matching;

                remainingSongs.RemoveAll(song => song.UniqueId == s.UniqueId);
            }
        }

        public GroupEditor(MetadataFile metadata)
        {
            MadeChanges = false;
            Metadata = metadata;
            InitializeComponent();

            List<SongEntry> remainingSongs = Metadata.Songs.ToList();
            foreach (GroupEntry g in Metadata.Groups) PopulateGroup(null, g, remainingSongs);
            foreach (SongEntry e in remainingSongs) SongList.Items.Add(e);
            GroupList.ExpandAll();

            UpdateControlAvailability();
        }

        private bool SelectionIsGroup
        {
            get
            {
                if (GroupList.SelectedNode == null) return false;
                return GroupList.SelectedNode.ForeColor == Color.RoyalBlue;
            }
        }

        private void UpdateControlAvailability()
        {
            bool hasSelection = GroupList.SelectedNode != null;
            bool selectionIsGroup = SelectionIsGroup;
            GroupNameBox.Enabled = selectionIsGroup;

            bool before = RenameBoxEnabled;
            RenameBoxEnabled = false;
            GroupNameBox.Text = GroupNameBox.Enabled ? GroupList.SelectedNode.Text : "";
            RenameBoxEnabled = before;

            AddButton.Enabled = hasSelection && SongList.SelectedIndices.Count > 0;
            RemoveButton.Enabled = hasSelection && !selectionIsGroup;

            MoveUpButton.Enabled = hasSelection && GroupList.SelectedNode.PrevNode != null && GroupList.SelectedNode.ForeColor == GroupList.SelectedNode.PrevNode.ForeColor;
            MoveDownButton.Enabled = hasSelection && GroupList.SelectedNode.NextNode != null && GroupList.SelectedNode.ForeColor == GroupList.SelectedNode.NextNode.ForeColor;
        }

        private void GroupList_AfterSelect(object sender, TreeViewEventArgs e) { UpdateControlAvailability(); }
        private void SongList_SelectedIndexChanged(object sender, EventArgs e) { UpdateControlAvailability(); }
        private void MoveUpButton_Click(object sender, EventArgs e) { SwapSelected(true); }
        private void MoveDownButton_Click(object sender, EventArgs e) { SwapSelected(false); }

        private void GroupContextMenu_Opening(object sender, CancelEventArgs e)
        {
            bool hasSelection = GroupList.SelectedNode != null;
            bool selectionIsGroup = SelectionIsGroup;
            bool selectionIsSong =  hasSelection && !selectionIsGroup;

            int songsInSelectedGroup = 0;
            string groupName = "";

            if (hasSelection)
            {
                TreeNode group = GroupList.SelectedNode;
                if (!selectionIsGroup) group = group.Parent;

                groupName = group.Text;
                foreach (TreeNode t in group.Nodes) if (t.ForeColor != Color.RoyalBlue) ++songsInSelectedGroup;
            }

            CreateSubGroupMenu.Enabled = selectionIsGroup;
            RemoveGroupMenu.Enabled = selectionIsGroup;
            RemoveSongsFromGroupMenu.Enabled = songsInSelectedGroup > 0;


            CreateSubGroupMenu.Text = CreateSubGroupMenu.Enabled ? string.Format("Create group under {0}", groupName) : "Create sub-group";
            RemoveGroupMenu.Text = RemoveGroupMenu.Enabled ? string.Format("Remove {0}", groupName) : "Remove group";
            RemoveSongsFromGroupMenu.Text = RemoveSongsFromGroupMenu.Enabled ? string.Format("Remove all {0} songs from {1}", songsInSelectedGroup, groupName) : "Remove all songs from group";
        }

        /// <summary>
        /// If a song is selected, returns the path up to the group
        /// </summary>
        private List<string> PathToSelectedGroup()
        {
            if (GroupList.SelectedNode == null) return null;

            TreeNode selectedGroup = GroupList.SelectedNode;
            if (selectedGroup.ForeColor != Color.RoyalBlue) selectedGroup = selectedGroup.Parent;

            return selectedGroup.FullPath.Split(new string[] { GroupList.PathSeparator }, StringSplitOptions.None).ToList();
        }

        private TreeNode AddTreeViewGroup(TreeNode parent, string name)
        {
            TreeNodeCollection nodes = parent == null ? GroupList.Nodes : parent.Nodes;

            // Add it after the last group (but before any songs)
            int index = 0;
            foreach (TreeNode t in nodes)
            {
                if (t.ForeColor != Color.RoyalBlue) break;
                ++index;
            }

            TreeNode node = nodes.Insert(index, name);
            node.ForeColor = Color.RoyalBlue;
            GroupList.SelectedNode = node;
            GroupNameBox.Focus();

            MadeChanges = true;
            return node;
        }

        private TreeNode AddTreeViewSong(TreeNode parent, SongEntry song)
        {
            if (parent == null) throw new InvalidOperationException("Songs can only be added to existing groups.");

            TreeNode node = parent.Nodes.Add(song.UniqueId, song.ToString(), 1, 1);
            node.Tag = song;
            GroupList.SelectedNode = node;

            MadeChanges = true;
            return node;
        }

        private void UnwindSongs(TreeNode group, bool recursive)
        {
            List<TreeNode> toRemove = new List<TreeNode>();
            foreach (TreeNode t in group.Nodes)
            {
                if (t.ForeColor != Color.RoyalBlue) toRemove.Add(t);
                else if (recursive) UnwindSongs(t, true);
            }

            foreach (TreeNode t in toRemove)
            {
                t.Remove();
                SongList.Items.Add(t.Tag);
            }

            if (toRemove.Count > 0) MadeChanges = true;
        }

        private void CreateTopLevelGroupMenu_Click(object sender, EventArgs e)
        {
            string groupName = Metadata.AddGroup(new List<string> { "Group" });
            TreeNode node = AddTreeViewGroup(null, groupName);
        }

        private void CreateSubGroupMenu_Click(object sender, EventArgs e)
        {
            List<string> path = PathToSelectedGroup();
            path.Add("Group");

            string actualName = Metadata.AddGroup(path);
            AddTreeViewGroup(GroupList.SelectedNode, actualName);
        }

        private void GroupNameBox_TextChanged(object sender, EventArgs e)
        {
            if (!RenameBoxEnabled) return;
            if (string.IsNullOrWhiteSpace(GroupNameBox.Text)) return;

            string newName = Metadata.RenameGroup(PathToSelectedGroup(), GroupNameBox.Text.Trim());
            GroupList.SelectedNode.Text = newName;
            MadeChanges = true;
        }

        private void SwapSelected(bool withPrev)
        {
            TreeNode selected = GroupList.SelectedNode;
            TreeNode other = withPrev ? selected.PrevNode : selected.NextNode;
            int prevIndex = selected.Index + (withPrev ? -1 : 1);

            TreeNode parent = selected.Parent;
            TreeNodeCollection nodes = parent == null ? GroupList.Nodes : parent.Nodes;

            List<string> path = PathToSelectedGroup();
            if (SelectionIsGroup)
            {
                path.RemoveAt(path.Count - 1);
                Metadata.SwapGroups(path, selected.Text, other.Text);
            }
            else Metadata.SwapSongsInGroup(path, (selected.Tag as SongEntry).UniqueId, (other.Tag as SongEntry).UniqueId);

            nodes.Remove(selected);
            nodes.Insert(prevIndex, selected);
            GroupList.SelectedNode = selected;

            MadeChanges = true;
        }

        private void RemoveGroupMenu_Click(object sender, EventArgs e)
        {
            if (GroupList.SelectedNode.Nodes.Count > 0 && MessageBox.Show("This group isn't empty.  Are you sure you want to remove it?", "Remove Group Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            Metadata.RemoveGroup(PathToSelectedGroup());

            UnwindSongs(GroupList.SelectedNode, true);
            GroupList.SelectedNode.Remove();

            MadeChanges = true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            List<SongEntry> selected = (from SongEntry s in SongList.SelectedItems select s).ToList();

            List<string> path = PathToSelectedGroup();
            TreeNode group = GroupList.SelectedNode;
            if (group.ForeColor != Color.RoyalBlue) group = group.Parent;

            GroupList.BeginUpdate();
            SongList.BeginUpdate();

            foreach (var s in selected)
            {
                Metadata.AddSongToGroup(path, s.UniqueId);
                AddTreeViewSong(group, s);

                SongList.Items.Remove(s);
            }

            GroupList.EndUpdate();
            SongList.EndUpdate();

            MadeChanges = true;
        }

        private void RemoveSongsFromGroupMenu_Click(object sender, EventArgs e)
        {
            Metadata.RemoveAllSongsFromGroup(PathToSelectedGroup());

            TreeNode group = GroupList.SelectedNode;
            if (group.ForeColor != Color.RoyalBlue) group = group.Parent;

            UnwindSongs(group, false);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            SongEntry song = GroupList.SelectedNode.Tag as SongEntry;
            Metadata.RemoveSongFromGroup(PathToSelectedGroup(), song.UniqueId);
            SongList.Items.Add(song);

            GroupList.SelectedNode.Remove();

            MadeChanges = true;
        }

        private void TestWriteButton_Click(object sender, EventArgs e)
        {
            Console.Out.WriteLine();
            Metadata.Save(Console.OpenStandardOutput());
            Console.Out.WriteLine();
        }

    }
}
