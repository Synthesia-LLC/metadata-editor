using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Synthesia
{
   [Flags]
   public enum ImportOptions
   {
      Cancel = 0,
      FingerHints = 0x1, HandParts = 0x2, Parts = 0x4,
      StandardPath = 0x8
   }

   public interface IGuiForm
   {
      GuiController c { set; }

      string WindowTitle { set; }
      bool OkayToProceed();

      bool AskYesNo(string message, string title);
      void ShowInfo(string message, string title);
      void ShowError(string message, string title);
      void ShowExclamation(string message, string title);

      string SaveMetadataFilename();
      string OpenMetadataFilename();
      string RetargetSongFilename();
      string PickImageFilename();

      ImportOptions AskImportOptions();

      // Returns true if changes were made
      bool LaunchGroupEditor(MetadataFile m);

      IEnumerable<SongEntry> SelectedSongs { get; }
      void UpdateSelectedSongTitle();
      void RefreshSongList();
      void DeselectAllSongs();

      void ClearSongControls();
      void BindSongControls();
   }

   public class GuiController
   {
      readonly IGuiForm f;
      public GuiController(IGuiForm f, string initialFile)
      {
         // This circular reference is a little aggressive here, but we need the form's
         // controller to be set immediately for our CreateNew call.  This is all
         // evidence that things are coupled quite a bit tighter than they should be.
         this.f = f;
         f.c = this;

         UnbindSong();
         CreateNew();
         if (!System.IO.File.Exists(initialFile)) return;

         File = new FileInfo(initialFile);
         if (LinkExtensions.Contains(File.Extension.ToLower())) File = new FileInfo(WindowsShell.Shortcut.Resolve(initialFile));

         using (FileStream input = File.OpenRead()) Metadata = new MetadataFile(input);
         WipeSelection();
         Dirty = false;
      }

      readonly HashSet<string> SongExtensions = new HashSet<string>() { ".mid", ".midi", ".kar" };
      readonly HashSet<string> MetaExtensions = new HashSet<string>() { ".synthesia", ".xml" };
      readonly HashSet<string> LinkExtensions = new HashSet<string>() { ".lnk" };

      public FileInfo File { get; set; }
      public MetadataFile Metadata { get; set; }
      private bool IgnoreUpdates { get; set; }

      private bool m_dirty;
      public bool Dirty
      {
         get { return m_dirty; }
         set
         {
            m_dirty = value;
            f.WindowTitle = "Synthesia Metadata Editor - " + (File?.Name ?? "Untitled.synthesia") + (value ? "*" : "");
         }
      }

      public void SaveAs()
      {
         FileInfo previous = File;

         File = null;
         if (!SaveChanges()) File = previous;
      }

      public bool SaveChanges()
      {
         if (File == null)
         {
            var filename = f.SaveMetadataFilename();
            if (filename == null) return false;
            File = new FileInfo(filename);
         }

         using (FileStream output = File.Create()) Metadata.Save(output);

         Dirty = false;
         return true;
      }

      public void CreateNew()
      {
         if (!f.OkayToProceed()) return;

         File = null;
         Metadata = new MetadataFile();

         WipeSelection();
         Dirty = false;
      }

      public void Open()
      {
         if (!f.OkayToProceed()) return;

         var filename = f.OpenMetadataFilename();
         if (filename == null) return;

         File = new FileInfo(filename);
         using (FileStream input = File.OpenRead()) Metadata = new MetadataFile(input);

         WipeSelection();
         Dirty = false;
      }

      public void Import()
      {
         if (!Metadata.Songs.Any())
         {
            f.ShowExclamation("You must have at least one song entry in this metadata file to perform a data import.  Add a song and try again.", "No song entries");
            return;
         }

         ImportOptions options = f.AskImportOptions();
         if (options == 0) return;

         bool standardPath = options.HasFlag(ImportOptions.StandardPath);

         var results = new Dictionary<string, ImportResults>();
         if (options.HasFlag(ImportOptions.FingerHints)) results["finger hints"] = ImportFingerHints(standardPath);
         if (options.HasFlag(ImportOptions.HandParts)) results["hand parts"] = ImportHandParts(standardPath);
         if (options.HasFlag(ImportOptions.Parts)) results["parts"] = ImportParts(standardPath);

         SelectionChanged();

         Dirty |= (from r in results where r.Value.Changed > 0 select true).Any();
         f.ShowInfo(string.Join(Environment.NewLine, from r in results select r.Value.ToDisplayString(r.Key)), "Import Complete");
      }

      public void Grouping()
      {
         if (!Metadata.Songs.Any())
         {
            f.ShowExclamation("You must have at least one song entry in this metadata file to manage groups.  Add a song and try again.", "No song entries");
            return;
         }

         Dirty |= f.LaunchGroupEditor(Metadata);
      }

      public void RetargetUniqueId()
      {
         var selectedList = f.SelectedSongs.ToList();
         if (selectedList.Count != 1) return;

         var filename = f.RetargetSongFilename();
         if (filename == null) return;

         FileInfo songFile = new FileInfo(filename);
         if (!songFile.Exists) return;

         string md5 = songFile.Md5sum();
         if ((from s in Metadata.Songs where s.UniqueId == md5 select s).Any())
         {
            f.ShowExclamation("This metadata file already contains a song with the new unique ID.  Cannot update this unique ID.", "Duplicate Unique ID");
            return;
         }

         var song = selectedList.First() as SongEntry;
         if (!Metadata.UpdateSongUniqueId(song.UniqueId, md5))
         {
            f.ShowExclamation("There was a problem retargeting this entry with a new unique ID.", "Couldn't Update Unique ID");
            return;
         }

         song.UniqueId = md5;

         RebindAfterChange();
         Dirty = true;
      }

      public string BrowseBackground()
      {
         if (File == null)
         {
            f.ShowInfo("Before adding background images, you should save this metadata file someplace near the image files.  That will let the editor use the correct 'relative' paths so they'll work in Synthesia.", "Save the metadata file before adding images");
            return null;
         }

         var filename = f.PickImageFilename();
         if (filename == null) return null;

         FileInfo imageFile = new FileInfo(filename);
         if (!imageFile.Exists) return null;

         string relative = File.MakeRelativePath(imageFile);
         if (relative.Contains(':')) f.ShowExclamation("It looks like the image is on a separate drive from the metadata file.  This image will probably only display on your own computer.", "Absolute paths are trouble");

         return relative;
      }

      public void RemoveSelectedSongs()
      {
         var selected = f.SelectedSongs.ToList();

         if (!selected.Any()) return;
         if (!f.AskYesNo("Are you sure you want to remove all metadata associated with the selected song(s)?  This will remove the song(s) from any groups containing it.  This may also remove metadata not visible to this editor!", "Remove Metadata?")) return;

         foreach (SongEntry s in selected) Metadata.RemoveSong(s.UniqueId);
         WipeSelection();

         Dirty = true;
      }

      public void AddSongs(string[] filenames)
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

         f.RefreshSongList();
         Dirty = true;
      }

      public void SelectionChanged()
      {
         if (!f.SelectedSongs.Any()) UnbindSong();
         else BindSong();
      }

      private void WipeSelection()
      {
         f.RefreshSongList();
         f.DeselectAllSongs();
         UnbindSong();
      }

      private void RebindAfterChange()
      {
         Dirty = true;
         foreach (SongEntry e in f.SelectedSongs) Metadata.AddSong(e);
         BindSong();
      }

      void UnbindSong()
      {
         IgnoreUpdates = true;
         f.ClearSongControls();
         IgnoreUpdates = false;
      }

      void BindSong()
      {
         IgnoreUpdates = true;
         f.BindSongControls();
         IgnoreUpdates = false;
      }

      public void AddTag(string tag)
      {
         foreach (SongEntry entry in f.SelectedSongs) entry.AddTag(tag);
         RebindAfterChange();
      }

      public void RemoveTag(string tag)
      {
         foreach (SongEntry entry in f.SelectedSongs) entry.RemoveTag(tag);
         RebindAfterChange();
      }

      public void RatingChanged(int? rating)
      {
         if (IgnoreUpdates) return;

         foreach (SongEntry entry in f.SelectedSongs) entry.Rating = rating;
         RebindAfterChange();
      }

      public void DifficultyChanged(int? difficulty)
      {
         if (IgnoreUpdates) return;

         foreach (SongEntry entry in f.SelectedSongs) entry.Difficulty = difficulty;
         RebindAfterChange();
      }

      public void TitleChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.Title = t;
         RebindAfterChange();

         f.UpdateSelectedSongTitle();
      }

      public void SubtitleChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.Subtitle = t;
         RebindAfterChange();

         f.UpdateSelectedSongTitle();
      }

      public void BackgroundChanged(string b)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.BackgroundImage = b;
         RebindAfterChange();

         f.UpdateSelectedSongTitle();
      }

      public void ComposerChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.Composer = t;
         RebindAfterChange();
      }

      public void ArrangerChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.Arranger = t;
         RebindAfterChange();
      }

      public void MadeFamousByChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.MadeFamousBy = t;
         RebindAfterChange();
      }

      public void CopyrightChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.Copyright = t;
         RebindAfterChange();
      }

      public void LicenseChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.License = t;
         RebindAfterChange();
      }

      public void FingerHintChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.FingerHints = t;
         RebindAfterChange();
      }

      public void HandsChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.HandParts = t;
         RebindAfterChange();
      }

      public void PartsChanged(string t)
      {
         if (IgnoreUpdates) return;
         foreach (SongEntry entry in f.SelectedSongs) entry.Parts = t;
         RebindAfterChange();
      }

      public void AddBookmark(int measure, string b)
      {
         foreach (SongEntry entry in f.SelectedSongs) entry.AddBookmark(measure, b);
         RebindAfterChange();
      }

      public void RemoveBookmark(int measure)
      {
         foreach (SongEntry entry in f.SelectedSongs) entry.RemoveBookmark(measure);
         RebindAfterChange();
      }

      public bool AllowDragDrop(string[] filenames)
      {
         if (filenames.Length > 1)
         {
            foreach (string file in filenames)
               if (!SongExtensions.Contains(new FileInfo(file).Extension.ToLower())) return false;
         }
         else
         {
            string extension = new FileInfo(filenames[0]).Extension.ToLower();
            if (!SongExtensions.Contains(extension) && !MetaExtensions.Contains(extension) && !LinkExtensions.Contains(extension)) return false;
         }

         return true;
      }

      public void DragDropFiles(string[] filenames)
      {
         if (filenames.Length > 1) AddSongs(filenames);
         else
         {
            FileInfo file = new FileInfo(filenames[0]);
            if (LinkExtensions.Contains(file.Extension.ToLower())) file = new FileInfo(WindowsShell.Shortcut.Resolve(filenames[0]));

            if (SongExtensions.Contains(file.Extension.ToLower()))
            {
               AddSongs(filenames);
               return;
            }

            if (!MetaExtensions.Contains(file.Extension.ToLower())) return;
            if (!f.OkayToProceed()) return;

            File = file;
            using (FileStream input = File.OpenRead()) Metadata = new MetadataFile(input);

            WipeSelection();
            Dirty = false;
         }
      }

      static private string SynthesiaDataPath(bool standard)
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
         path = Path.Combine(path, standard ? "Synthesia" : "SynthesiaDev");

         return path;
      }

      struct ImportResults
      {
         public int Imported;
         public int Changed;
         public int Identical;

         public bool ProblemEncountered;

         public string ToDisplayString(string importType)
         {
            if (ProblemEncountered) return $"Unable to import {importType}.";
            return $"Imported {importType} for {Imported} song{(Imported == 1 ? "" : "s")}.  ({Changed} changed, {Identical} identical.)";
         }
      }

      ImportResults ImportHandParts(bool standardPath)
      {
         ImportResults results = new ImportResults() { ProblemEncountered = true };
         string SongInfoPath = Path.Combine(SynthesiaDataPath(standardPath), "songInfo.xml");

         FileInfo songInfoFile = new FileInfo(SongInfoPath);
         if (!songInfoFile.Exists)
         {
            f.ShowError("Couldn't find song info file in the Synthesia data directory.  Aborting hand part import.", "Missing songInfo.xml");
            return results;
         }

         try
         {
            XDocument doc = XDocument.Load(SongInfoPath);

            XElement topLevel = doc.Element("LocalSongInfoList");
            if (topLevel == null) throw new InvalidDataException("Couldn't find top-level LocalSongInfoList element.");

            if (topLevel.AttributeOrDefault("version", "1") != "1")
            {
               f.ShowExclamation("Data in songInfo.xml is in a newer format.  Unable to import hand parts.  (Check for a newer version of the metadata editor.)", "songInfo.xml too new!");
               return results;
            }

            var elements = topLevel.Elements("SongInfo");
            var parts = (from i in elements
                         select new
                         {
                            hash = i.AttributeOrDefault("hash"),
                            left = i.AttributeOrDefault("leftHand"),
                            right = i.AttributeOrDefault("rightHand"),
                            both = i.AttributeOrDefault("bothHands")
                         }).Where(i => !string.IsNullOrWhiteSpace(i.left) || !string.IsNullOrWhiteSpace(i.right) || !string.IsNullOrWhiteSpace(i.both)).ToDictionary(i => i.hash);

            foreach (var s in Metadata.Songs.ToList())
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
            f.ShowError($"Unable to read songInfo.xml.  Aborting hand part import.\n\n{ex}", "Import error!");
            return results;
         }

         results.ProblemEncountered = false;
         return results;
      }

      ImportResults ImportParts(bool standardPath)
      {
         ImportResults results = new ImportResults() { ProblemEncountered = true };
         string SongInfoPath = Path.Combine(SynthesiaDataPath(standardPath), "songInfo.xml");

         FileInfo songInfoFile = new FileInfo(SongInfoPath);
         if (!songInfoFile.Exists)
         {
            f.ShowError("Couldn't find song info file in the Synthesia data directory.  Aborting part import.", "Missing songInfo.xml");
            return results;
         }

         try
         {
            XDocument doc = XDocument.Load(SongInfoPath);

            XElement topLevel = doc.Element("LocalSongInfoList");
            if (topLevel == null) throw new InvalidDataException("Couldn't find top-level LocalSongInfoList element.");

            if (topLevel.AttributeOrDefault("version", "1") != "1")
            {
               f.ShowExclamation("Data in songInfo.xml is in a newer format.  Unable to import parts.  (Check for a newer version of the metadata editor.)", "songInfo.xml too new!");
               return results;
            }

            var elements = topLevel.Elements("SongInfo");
            var parts = (from i in elements
                         select new
                         {
                            hash = i.AttributeOrDefault("hash"),
                            parts = i.AttributeOrDefault("parts"),
                         }).Where(i => !string.IsNullOrWhiteSpace(i.parts)).ToDictionary(i => i.hash);

            foreach (var s in Metadata.Songs.ToList())
            {
               if (!parts.ContainsKey(s.UniqueId)) continue;
               results.Imported++;

               string oldParts = s.Parts;

               var match = parts[s.UniqueId];

               if (oldParts == match.parts) results.Identical++;
               else
               {
                  s.Parts = match.parts;
                  Metadata.AddSong(s);

                  results.Changed++;
               }
            }

         }
         catch (Exception ex)
         {
            f.ShowError($"Unable to read songInfo.xml.  Aborting part import.\n\n{ex}", "Import error!");
            return results;
         }

         results.ProblemEncountered = false;
         return results;
      }

      ImportResults ImportFingerHints(bool standardPath)
      {
         ImportResults results = new ImportResults() { ProblemEncountered = true };
         string FingerHintPath = Path.Combine(SynthesiaDataPath(standardPath), "fingers.xml");

         FileInfo fingerHintFile = new FileInfo(FingerHintPath);
         if (!fingerHintFile.Exists)
         {
            f.ShowError("Couldn't find finger hint file in the Synthesia data directory.  Aborting import.", "Missing fingers.xml");
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
               f.ShowExclamation("Data in fingers.xml is in a newer format.  Unable to import.  (Check for a newer version of the metadata editor.)", "Fingers.xml too new!");
               return results;
            }

            var elements = topLevel.Elements("FingerInfo");
            foreach (var fi in elements) allFingers[fi.AttributeOrDefault("hash")] = fi.AttributeOrDefault("fingers");
         }
         catch (Exception ex)
         {
            f.ShowError($"Unable to read fingers.xml.  Aborting import.\n\n{ex}", "Import error!");
            return results;
         }

         foreach (SongEntry s in Metadata.Songs.ToList())
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
