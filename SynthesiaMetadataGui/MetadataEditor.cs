using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Synthesia
{
    public partial class MetadataEditor : Form
    {
        private FileInfo File { get; set; }
        private MetadataFile Metadata { get; set; }

        private bool m_dirty = false;
        private bool Dirty
        {
            get { return m_dirty; }

            set
            {
                m_dirty = value;
                UpdateTitle();
            }
        }

        SongEntry SelectedSong { get { return SongList.SelectedItem as SongEntry; } }

        public bool OkayToProceed()
        {
            if (!Dirty) return true;

            DialogResult r = MessageBox.Show("Would you like to save your changes first?", "Save Changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (r == DialogResult.Cancel) return false;
            if (r == DialogResult.Yes) if (!SaveChanges()) return false;

            return true;
        }

        public bool SaveChanges()
        {
            if (File == null)
            {
                if (SaveMetadataDialog.ShowDialog(this) != DialogResult.OK) return false;
                File = new FileInfo(SaveMetadataDialog.FileName);
            }

            using (FileStream output = File.Create()) Metadata.Save(output);

            Dirty = false;
            return true;
        }

        public void CreateNew()
        {
            if (!OkayToProceed()) return;

            File = null;
            Metadata = new MetadataFile();

            WipeSelection();
            Dirty = false;
        }

        public MetadataEditor()
        {
            InitializeComponent();
            UnbindSong();
            CreateNew();
        }

        public void UpdateTitle()
        {
            Text = "Synthesia Metadata Editor - " + (File == null ? "Untitled.synthesia" : File.Name) + (Dirty ? "*" : "");
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RemoveSong_Click(object sender, EventArgs e)
        {
            if (SelectedSong == null) return;
            if (MessageBox.Show("Are you sure you want to remove all metadata associated with the selected song(s)?  (This may remove metadata not visible to this editor!)", "Remove Metadata?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            Metadata.RemoveSong(SelectedSong.UniqueId);
            WipeSelection();

            Dirty = true;
        }

        private void AddSong_Click(object sender, EventArgs e)
        {
            if (OpenSongDialog.ShowDialog() != DialogResult.OK) return;

            List<string> existingIds = (from s in Metadata.Songs select s.UniqueId).ToList();

            foreach (string s in OpenSongDialog.FileNames)
            {
                FileInfo songFile = new FileInfo(s);

                string md5 = songFile.Md5sum();
                if (existingIds.Contains(md5)) continue;

                Metadata.AddSong(new SongEntry()
                {
                    UniqueId = md5,
                    Title = songFile.Name.Substring(0, songFile.Name.Length - songFile.Extension.Length)
                });
            }

            UpdateSongList();
            Dirty = true;
        }

        private void NewMenu_Click(object sender, EventArgs e)
        {
            CreateNew();
        }

        private void OpenMenu_Click(object sender, EventArgs e)
        {
            if (!OkayToProceed()) return;
            if (OpenMetadataDialog.ShowDialog() != DialogResult.OK) return;

            File = new FileInfo(OpenMetadataDialog.FileName);
            using (FileStream input = File.OpenRead())
                Metadata = new MetadataFile(input);

            WipeSelection();
            Dirty = false;
        }

        private void SaveMenu_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void SaveAsMenu_Click(object sender, EventArgs e)
        {
            FileInfo previous = File;

            File = null;
            if (!SaveChanges()) File = previous;
        }

        private void AboutMenu_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private void UpdateSongList()
        {
            SongList.BeginUpdate();

            string previousId = null;
            if (SelectedSong != null) previousId = SelectedSong.UniqueId;

            SongList.Items.Clear();

            if (Metadata != null)
            {
                foreach (SongEntry s in Metadata.Songs)
                {
                    SongList.Items.Add(s);
                    if (previousId != null && s.UniqueId == previousId) SongList.SelectedItem = s;
                }
            }

            SongList.EndUpdate();
        }

        private void MetadataEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!OkayToProceed()) e.Cancel = true;
        }

        private void SongList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedSong == null) UnbindSong();
            else BindSong();
        }

        private void UnbindSong()
        {
            UniqueIdBox.Text = "(No song selected)";
            TitleBox.Clear();
            SubtitleBox.Clear();
            
            ComposerBox.Clear();
            ArrangerBox.Clear();
            CopyrightBox.Clear();
            LicenseBox.Clear();

            DifficultyBox.Value = 0;
            RatingBox.Value = 0;

            TagBox.Clear();
            TagList.Items.Clear();

            PropertiesGroup.Enabled = false;
        }

        private bool IgnoreUpdates { get; set; }

        private void BindSong()
        {
            SongEntry e = SelectedSong;
            if (e == null) throw new InvalidOperationException("Cannot bind a null song.");

            IgnoreUpdates = true;

            PropertiesGroup.Enabled = true;

            UniqueIdBox.Text = e.UniqueId;
            TitleBox.Text = e.Title;
            SubtitleBox.Text = e.Subtitle;

            ComposerBox.Text = e.Composer;
            ArrangerBox.Text = e.Arranger;
            CopyrightBox.Text = e.Copyright;
            LicenseBox.Text = e.License;

            DifficultyBox.Value = e.Difficulty ?? 0;
            RatingBox.Value = e.Rating ?? 0;

            TagList.Items.Clear();
            foreach (var i in e.Tags) TagList.Items.Add(i);

            IgnoreUpdates = false;
        }

        private void TagBox_TextChanged(object sender, EventArgs e)
        {
            if (TagBox.Text.Contains(';')) TagBox.Text = TagBox.Text.Replace(";", "");
            AddTag.Enabled = TagBox.Text.Length > 0 && !TagList.Items.Contains(TagBox.Text);
        }

        private void TagList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveTag.Enabled = TagList.SelectedIndex != -1;
        }

        private void RebindAfterChange()
        {
            Dirty = true;
            Metadata.AddSong(SelectedSong);
            BindSong();
        }

        private void AddTag_Click(object sender, EventArgs e)
        {
            SelectedSong.AddTag(TagBox.Text);
            TagBox.Clear();

            RebindAfterChange();
        }

        private void RemoveTag_Click(object sender, EventArgs e)
        {
            if (TagList.SelectedItem == null) return;

            string tag = TagList.SelectedItem as string;

            SelectedSong.RemoveTag(tag);
            RebindAfterChange();

            TagBox.Text = tag;
        }

        private void RatingBox_ValueChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;

            SelectedSong.Rating = (RatingBox.Value == 0) ? (int?)null : Convert.ToInt32(RatingBox.Value);
            RebindAfterChange();
        }

        private void DifficultyBox_ValueChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;

            SelectedSong.Difficulty = (DifficultyBox.Value == 0) ? (int?)null : Convert.ToInt32(DifficultyBox.Value);
            RebindAfterChange();
        }

        private void TitleBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;
            SelectedSong.Title = TitleBox.Text;
            RebindAfterChange();

            UpdateSelectedSongTitle();
        }

        private void UpdateSelectedSongTitle()
        {
            int i = SongList.SelectedIndex;
            SongEntry e = SelectedSong;

            SongList.SelectedIndexChanged -= SongList_SelectedIndexChanged;

            SongList.BeginUpdate();
            SongList.ClearSelected();
            SongList.Items[i] = e;
            SongList.SelectedIndex = i;
            SongList.EndUpdate();

            SongList.SelectedIndexChanged += SongList_SelectedIndexChanged;
        }  

        private void SubtitleBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;
            SelectedSong.Subtitle = SubtitleBox.Text;
            RebindAfterChange();
        }

        private void ComposerBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;
            SelectedSong.Composer = ComposerBox.Text;
            RebindAfterChange();
        }

        private void ArrangerBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;
            SelectedSong.Arranger = ArrangerBox.Text;
            RebindAfterChange();
        }

        private void CopyrightBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;
            SelectedSong.Copyright = CopyrightBox.Text;
            RebindAfterChange();
        }

        private void LicenseBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;
            SelectedSong.License = LicenseBox.Text;
            RebindAfterChange();
        }

        private void MetadataEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.OemPeriod && SongList.SelectedIndex != -1 && SongList.SelectedIndex < SongList.Items.Count - 1)
            {
                SongList.SelectedIndex = SongList.SelectedIndex + 1;
                e.Handled = true;
            }

            if (e.Control && e.KeyCode == Keys.Oemcomma && SongList.SelectedIndex != -1 && SongList.SelectedIndex > 0)
            {
                SongList.SelectedIndex = SongList.SelectedIndex - 1;
                e.Handled = true;
            }
        }

        public void ForceOpenFile(string filename)
        {
            File = new FileInfo(filename);
            using (FileStream input = File.OpenRead()) Metadata = new MetadataFile(input);

            WipeSelection();
            Dirty = false;
        }

        private void MetadataEditor_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] filenames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (filenames.Length > 1) return;
            if (!System.IO.File.Exists(filenames[0])) return;

            FileInfo file = new FileInfo(filenames[0]);

            if (file.Extension.ToLower() != ".synthesia") return;
            if (!OkayToProceed()) return;

            File = file;
            using (FileStream input = File.OpenRead()) Metadata = new MetadataFile(input);

            WipeSelection();
            Dirty = false;
        }

        private void MetadataEditor_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] filenames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (filenames.Length > 1) return;

            FileInfo file = new FileInfo(filenames[0]);
            if (file.Extension.ToLower() == ".synthesia") e.Effect = DragDropEffects.All;
        }

        private void WipeSelection()
        {
            UpdateSongList();
            SongList.SelectedIndex = -1;

            IgnoreUpdates = true;
            UnbindSong();
            IgnoreUpdates = false;
        }


    }

}
