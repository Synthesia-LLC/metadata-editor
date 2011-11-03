using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace Synthesia
{
    public partial class MetadataEditor : Form
    {
        readonly HashSet<string> SongExtensions = new HashSet<string>() { ".mid", ".midi", ".kar" };
        readonly HashSet<string> MetaExtensions = new HashSet<string>() { ".synthesia", ".xml" };

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
            AddSongs(OpenSongDialog.FileNames);
        }

        private void AddSongs(string[] filenames)
        {
            List<string> existingIds = (from s in Metadata.Songs select s.UniqueId).ToList();

            foreach (string s in filenames)
            {
                FileInfo songFile = new FileInfo(s);
                if (!songFile.Exists) continue;

                if (!SongExtensions.Contains(songFile.Extension.ToLower())) continue;

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
            IgnoreUpdates = true;

            UniqueIdBox.Text = "(No song selected)";
            TitleBox.Clear();
            SubtitleBox.Clear();
            
            ComposerBox.Clear();
            ArrangerBox.Clear();
            CopyrightBox.Clear();
            LicenseBox.Clear();

            DifficultyBox.Value = 0;
            RatingBox.Value = 0;

            FingerHintBox.Clear();
            HandsBox.Clear();

            TagBox.Clear();
            TagList.Items.Clear();

            PropertiesGroup.Enabled = false;

            IgnoreUpdates = false;
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

            FingerHintBox.Text = e.FingerHints;
            HandsBox.Text = e.HandParts;

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

        private void SubtitleBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;
            SelectedSong.Subtitle = SubtitleBox.Text;
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

        private void FingerHintBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;
            SelectedSong.FingerHints = FingerHintBox.Text;
            RebindAfterChange();
        }

        private void HandsBox_TextChanged(object sender, EventArgs e)
        {
            if (IgnoreUpdates) return;
            SelectedSong.HandParts = HandsBox.Text;
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
            if (filenames.Length > 1) AddSongs(filenames);
            else
            {
                FileInfo file = new FileInfo(filenames[0]);

                if (SongExtensions.Contains(file.Extension.ToLower()))
                {
                    AddSongs(filenames);
                    return;
                }

                if (!MetaExtensions.Contains(file.Extension.ToLower())) return;
                if (!OkayToProceed()) return;

                File = file;
                using (FileStream input = File.OpenRead()) Metadata = new MetadataFile(input);

                WipeSelection();
                Dirty = false;
            }
        }

        private void MetadataEditor_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] filenames = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (filenames.Length > 1)
            {
                foreach (string f in filenames)
                    if (!SongExtensions.Contains(new FileInfo(f).Extension.ToLower())) return;
            }
            else
            {
                string extension = new FileInfo(filenames[0]).Extension.ToLower();
                if (!SongExtensions.Contains(extension) && !MetaExtensions.Contains(extension)) return;
            }

            e.Effect = DragDropEffects.All;
        }

        private void WipeSelection()
        {
            UpdateSongList();
            SongList.SelectedIndex = -1;

            IgnoreUpdates = true;
            UnbindSong();
            IgnoreUpdates = false;
        }

        private string SynthesiaDataPath
        {
            get
            {
                // The data directory is different on the Mac version
                int platform = (int)Environment.OSVersion.Platform;
                bool unix = platform == 4 || platform == 6 || platform == 128 || Environment.OSVersion.Platform == PlatformID.MacOSX;

                string path = "";
                if (!unix)
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                }
                else
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    path = Path.Combine(path, "Library");
                    path = Path.Combine(path, "Application Support");
                }
                path = Path.Combine(path, "Synthesia");

                return path;
            }
        }

        struct ImportResults
        {
            public int Imported;
            public int Changed;
            public int Identical;

            public bool ProblemEncountered;

            public string ToDisplayString(string importType)
            {
                if (ProblemEncountered) return string.Format("Unable to import {0}.", importType);
                return string.Format("Imported {0} for {1} song{2}.  ({3} changed, {4} identical.)", importType, Imported, (Imported == 1 ? "" : "s"), Changed, Identical);
            }
        }

        private void ImportMenu_Click(object sender, EventArgs e)
        {
            if (SongList.Items.Count == 0)
            {
                MessageBox.Show(this, "You must have at least one song entry in this metadata file to perform a data import.  Add a song and try again.", "No song entries", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (ImportSelection importDialog = new ImportSelection())
            {
                if (importDialog.ShowDialog(this) != DialogResult.OK) return;

                Dictionary<string, ImportResults> results = new Dictionary<string,ImportResults>();
                if (importDialog.ImportFingerHints) results["finger hints"] = ImportFingerHints();
                if (importDialog.ImportHandParts) results["hand parts"] = ImportHandParts();

                SongList_SelectedIndexChanged(this, new EventArgs());

                MessageBox.Show(this, string.Join(Environment.NewLine, from r in results select r.Value.ToDisplayString(r.Key)), "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if ((from r in results where r.Value.Changed > 0 select true).Any()) Dirty = true;
            }
        }


        ImportResults ImportHandParts()
        {
            ImportResults results = new ImportResults();
            results.ProblemEncountered = true;

            string SongInfoPath = Path.Combine(SynthesiaDataPath, "songInfo.xml");

            FileInfo songInfoFile = new FileInfo(SongInfoPath);
            if (!songInfoFile.Exists)
            {
                MessageBox.Show(this, "Couldn't find song info file in the Synthesia data directory.  Aborting hand part import.", "Missing songInfo.xml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return results;
            }

            try
            {
                XDocument doc = XDocument.Load(SongInfoPath);

                XElement topLevel = doc.Element("LocalSongInfoList");
                if (topLevel == null) throw new InvalidDataException("Couldn't find top-level LocalSongInfoList element.");

                if (topLevel.AttributeOrDefault("version", "1") != "1")
                {
                    MessageBox.Show(this, "Data in songInfo.xml is in a newer format.  Unable to hand parts import.  (Maybe check for a newer version of the metadata editor.)", "songInfo.xml too new!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return results;
                }

                var elements = topLevel.Elements("SongInfo");
                var parts = (from i in elements select new 
                {
                    hash = i.AttributeOrDefault("hash"),
                    left = i.AttributeOrDefault("leftHand"),
                    right = i.AttributeOrDefault("rightHand"),
                    both = i.AttributeOrDefault("bothHands")
                }).Where(i => !string.IsNullOrWhiteSpace(i.left) || !string.IsNullOrWhiteSpace(i.right) || !string.IsNullOrWhiteSpace(i.both)).ToDictionary(i => i.hash);

                foreach (SongEntry s in SongList.Items)
                {
                    if (!parts.ContainsKey(s.UniqueId)) continue;
                    results.Imported++;

                    string oldParts = s.HandParts;

                    var match = parts[s.UniqueId];
                    string newParts = string.Join(";", match.left, match.right, match.both);

                    if (oldParts == newParts) results.Identical++;
                    else
                    {
                        s.HandParts = newParts;
                        Metadata.AddSong(s);

                        results.Changed++;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unable to read songInfo.xml.  Aborting hand part import.\n\n{0}", ex));
                return results;
            }

            results.ProblemEncountered = false;
            return results;
        }

        ImportResults ImportFingerHints()
        {
            ImportResults results = new ImportResults();
            results.ProblemEncountered = true;

            string FingerHintPath = Path.Combine(SynthesiaDataPath, "fingers.xml");

            FileInfo fingerHintFile = new FileInfo(FingerHintPath);
            if (!fingerHintFile.Exists)
            {
                MessageBox.Show(this, "Couldn't find finger hint file in the Synthesia data directory.  Aborting import.", "Missing fingers.xml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return results;
            }

            // Bulk pull the fingers out of the file
            Dictionary<string, string> allFingers = new Dictionary<string, string>();
            try
            {
                XDocument doc = XDocument.Load(FingerHintPath);

                XElement topLevel = doc.Element("LocalFingerInfoList");
                if (topLevel == null) throw new InvalidDataException("Couldn't find top-level LocalFingerInfoList element.");

                if (topLevel.AttributeOrDefault("version", "1") != "1")
                {
                    MessageBox.Show(this, "Data in fingers.xml is in a newer format.  Unable to import.  (Maybe check for a newer version of the metadata editor.)", "Fingers.xml too new!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return results;
                }

                var elements = topLevel.Elements("FingerInfo");
                foreach (var f in elements) allFingers[f.AttributeOrDefault("hash")] = f.AttributeOrDefault("fingers");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unable to read fingers.xml.  Aborting import.\n\n{0}", ex));
                return results;
            }

            foreach (SongEntry s in SongList.Items)
            {
                if (!allFingers.ContainsKey(s.UniqueId)) continue;
                results.Imported++;

                string oldHints = s.FingerHints;
                string newHints = allFingers[s.UniqueId];

                if (oldHints == newHints) results.Identical++;
                else
                {
                    s.FingerHints = newHints;
                    Metadata.AddSong(s);

                    results.Changed++;
                }
            }

            results.ProblemEncountered = false;
            return results;
        }

    }

}
