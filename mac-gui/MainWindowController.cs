using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using AppKit;
using System.Reflection;

namespace Synthesia
{
   public partial class MainWindowController : NSWindowController, IGuiForm
   {
      public GuiController c { get; set; }

      readonly SimpleTableSource<SongEntry> Songs = new SimpleTableSource<SongEntry>();
      readonly SimpleTableSource<Bookmark> Bookmarks = new SimpleTableSource<Bookmark>();
      readonly SimpleTableSource<string> Tags = new SimpleTableSource<string>();

      public IEnumerable<SongEntry> SelectedSongs => from s in SongList.SelectedRows select Songs.Data[(int)s];
      public string WindowTitle { set { Window.Title = value; } }
      public void DeselectAllSongs() { SongList.DeselectAll(this); }

      public bool AskYesNo(string message, string title) { var alert = new NSAlert { MessageText = title, InformativeText = message }; alert.AddButton("No"); alert.AddButton("Yes"); return alert.RunModal() == (int)NSAlertButtonReturn.Second; }
      public void ShowInfo(string message, string title) { new NSAlert { MessageText = title, InformativeText = message, AlertStyle = NSAlertStyle.Informational }.RunModal(); }
      public void ShowError(string message, string title) { new NSAlert { MessageText = title, InformativeText = message, AlertStyle = NSAlertStyle.Critical }.RunModal(); }
      public void ShowExclamation(string message, string title) { new NSAlert { MessageText = title, InformativeText = message, AlertStyle = NSAlertStyle.Warning }.RunModal(); }

      public string SaveMetadataFilename()
      {
         var panel = new NSSavePanel() { Title = "Save Metadata File", AllowedFileTypes = (from e in GuiController.MetaExtensions select e.Substring(1)).ToArray(), AllowsOtherFileTypes = true };
         return (panel.RunModal() == 1) ? panel.Url?.Path ?? null : null;
      }
      public string OpenMetadataFilename()
      {
         var panel = new NSOpenPanel() { Title = "Open Metadata File", AllowedFileTypes = (from e in GuiController.MetaExtensions select e.Substring(1)).ToArray(), AllowsOtherFileTypes = true, AllowsMultipleSelection = false, CanChooseFiles = true, CanChooseDirectories = false };
         return (panel.RunModal() == 1) ? panel.Url?.Path ?? null : null;
      }
      public string RetargetSongFilename()
      {
			var panel = new NSOpenPanel() { Title = "Retarget Metadata", AllowedFileTypes = (from e in GuiController.SongExtensions select e.Substring(1)).ToArray(), AllowsOtherFileTypes = true, AllowsMultipleSelection = false, CanChooseFiles = true, CanChooseDirectories = false };
			return (panel.RunModal() == 1) ? panel.Url?.Path ?? null : null;
		}
      public string PickImageFilename()
      {
         var panel = new NSOpenPanel() { Title = "Background Image for Song", AllowedFileTypes = new string[] { "jpg", "jpeg", "gif", "bmp", "png", "tif", "tiff", "tga" }, AllowsOtherFileTypes = true, AllowsMultipleSelection = false, CanChooseFiles = true, CanChooseDirectories = false };
			return (panel.RunModal() == 1) ? panel.Url?.Path ?? null : null;
		}

      public bool OkayToProceed()
      {
         if (!c.Dirty) return true;

         var alert = new NSAlert { InformativeText = "Would you like to save your changes first?", MessageText = "Save Changes?" };
         alert.AddButton("Save");
         alert.AddButton("Discard");
         alert.AddButton("Cancel");

         var result = alert.RunModal();
         switch ((NSAlertButtonReturn)(int)result)
         {
            case NSAlertButtonReturn.First: return c.SaveChanges();
            case NSAlertButtonReturn.Third: return false;
         }

         // WindowShouldClose is followed immediately by ApplicationShouldTerminate,
         // so we force Dirty to false here to avoid the double "are you sure" prompt
         c.Dirty = false;
         return true;
      }

      [Export("initWithCoder:")]
      public MainWindowController(NSCoder coder) : base(coder) { }
      public MainWindowController(IntPtr handle) : base(handle) { }
      public MainWindowController() : base("MainWindow")
      {
         Window.WindowShouldClose += (sender) => { return OkayToProceed(); };

         TitleBox.Changed += (sender, e) => { c.TitleChanged(TitleBox.StringValue); };
         SubtitleBox.Changed += (sender, e) => { c.SubtitleChanged(SubtitleBox.StringValue); };
         BackgroundBox.Changed += (sender, e) => { c.BackgroundChanged(BackgroundBox.StringValue); };
         RatingBox.Changed += (sender, e) => { c.RatingChanged(RatingBox.IntValue); };
         DifficultyBox.Changed += (sender, e) => { c.DifficultyChanged(DifficultyBox.IntValue); };
         ComposerBox.Changed += (sender, e) => { c.ComposerChanged(ComposerBox.StringValue); };
         ArrangerBox.Changed += (sender, e) => { c.ArrangerChanged(ArrangerBox.StringValue); };
         CopyrightBox.Changed += (sender, e) => { c.CopyrightChanged(CopyrightBox.StringValue); };
         MadeFamousByBox.Changed += (sender, e) => { c.MadeFamousByChanged(MadeFamousByBox.StringValue); };
         FingerHintBox.TextDidChange += (sender, e) => { c.FingerHintChanged(FingerHintBox.String); };
         LicenseBox.Changed += (sender, e) => { c.LicenseChanged(LicenseBox.StringValue); };
         HandsBox.Changed += (sender, e) => { c.HandsChanged(HandsBox.StringValue); };
         PartsBox.TextDidChange += (sender, e) => { c.PartsChanged(PartsBox.String); };

         TagBox.Changed += (sender, e) => {
            if (TagBox.StringValue.Contains(';')) TagBox.StringValue = TagBox.StringValue.Replace(";", "");
            AddTagButton.Enabled = TagBox.StringValue.Length > 0 && !Tags.Data.Contains(TagBox.StringValue);
         };

			BookmarkLabelBox.Changed += (sender, e) =>
			{
            if (BookmarkLabelBox.StringValue.Contains(';')) BookmarkLabelBox.StringValue = BookmarkLabelBox.StringValue.Replace(";", "");
			};
		}

      public override void AwakeFromNib()
      {
         base.AwakeFromNib();
         (Window.ContentView as DragDropView).controller = this;

			SongList.DataSource = Songs;
         SongList.Delegate = new SimpleTableDelegate<SongEntry>(Songs, () => { c.SelectionChanged(); });

         BookmarkList.DataSource = Bookmarks;
         BookmarkList.Delegate = new SimpleTableDelegate<Bookmark>(Bookmarks, () => { RemoveBookmarkButton.Enabled = BookmarkList.SelectedRowCount > 0; });

			TagList.DataSource = Tags;
         TagList.Delegate = new SimpleTableDelegate<string>(Tags, () => { RemoveTagButton.Enabled = TagList.SelectedRowCount > 0; });

			// NOTE: This c.set is actually superfluous.  The GuiController sets it for us.
			c = new GuiController(this, "");
		}

      public new MainWindow Window { get { return (MainWindow)base.Window; } }

      // New, Open, Save, Save As, and Import handled in AppDelegate (About and Exit handled automatically)

      partial void removeSongClicked(NSObject sender) { c.RemoveSelectedSongs(); }
      partial void groupingClicked(NSObject sender) { c.Grouping(); }
      partial void retargetClicked(NSObject sender) { c.RetargetUniqueId(); }
      partial void addSongClicked(NSObject sender)
      {
			var panel = new NSOpenPanel() { Title = "Add Songs", AllowedFileTypes = (from e in GuiController.SongExtensions select e.Substring(1)).ToArray(), AllowsOtherFileTypes = true, AllowsMultipleSelection = true, CanChooseFiles = true, CanChooseDirectories = false };
         if (panel.RunModal() != 1) return;
         c.AddSongs((from s in panel.Urls select s?.Path ?? null).ToArray());
      }

      public void RefreshSongList()
      {
         var selectedIds = (from s in SelectedSongs select s.UniqueId).ToList();
         if (c.Metadata != null) Songs.Data = c.Metadata.Songs.ToList();

			SongList.ReloadData();
         SongList.DeselectAll(this);
         for (int i = 0; i < Songs.Data.Count; ++i) if (selectedIds.Contains(Songs.Data[i].UniqueId)) SongList.SelectRow(i, true);
		}

      public void ClearSongControls() { PropertiesGroup.Hidden = true; }

		void BindBox(NSTextField box, PropertyInfo prop)
		{
			int values = (from e in SelectedSongs select prop.GetValue(e, null) as string).Distinct().Count();

         box.TextColor = values == 1 ? NSColor.ControlText : NSColor.DisabledControlText;
         box.StringValue = values == 1 ? prop.GetValue(SelectedSongs.First()) as string ?? "" : "(Various)";
		}

		void BindBox(NSTextView box, PropertyInfo prop)
		{
         var cursor = box.SelectedRanges;

			int values = (from e in SelectedSongs select prop.GetValue(e, null) as string).Distinct().Count();

			box.TextColor = values == 1 ? NSColor.ControlText : NSColor.DisabledControlText;
			box.Value = values == 1 ? prop.GetValue(SelectedSongs.First()) as string ?? "" : "(Various)";

         box.SelectedRanges = cursor;
		}

		void BindNumericBox(NSTextField box, PropertyInfo prop)
		{
			int values = (from e in SelectedSongs select prop.GetValue(e, null) as int?).Distinct().Count();

			box.TextColor = values == 1 ? NSColor.ControlText : NSColor.DisabledControlText;
			box.IntValue = values == 1 ? (prop.GetValue(SelectedSongs.First()) as int?) ?? 0 : 0;
		}

      public void BindSongControls()
      {
         PropertiesGroup.Hidden = false;

         BindBox(UniqueIdBox, typeof(SongEntry).GetProperty("UniqueId"));
			BindBox(TitleBox, typeof(SongEntry).GetProperty("Title"));
			BindBox(SubtitleBox, typeof(SongEntry).GetProperty("Subtitle"));

			BindBox(BackgroundBox, typeof(SongEntry).GetProperty("BackgroundImage"));

			BindBox(ComposerBox, typeof(SongEntry).GetProperty("Composer"));
			BindBox(ArrangerBox, typeof(SongEntry).GetProperty("Arranger"));
			BindBox(CopyrightBox, typeof(SongEntry).GetProperty("Copyright"));
			BindBox(LicenseBox, typeof(SongEntry).GetProperty("License"));
			BindBox(MadeFamousByBox, typeof(SongEntry).GetProperty("MadeFamousBy"));

			BindNumericBox(DifficultyBox, typeof(SongEntry).GetProperty("Difficulty"));
			BindNumericBox(RatingBox, typeof(SongEntry).GetProperty("Rating"));

			BindBox(FingerHintBox, typeof(SongEntry).GetProperty("FingerHints"));
			BindBox(HandsBox, typeof(SongEntry).GetProperty("HandParts"));
			BindBox(PartsBox, typeof(SongEntry).GetProperty("Parts"));

         int selectedCount = (int)SongList.SelectedRowCount;
			SortedDictionary<string, int> tagFrequency = new SortedDictionary<string, int>();
			Dictionary<KeyValuePair<int, string>, int> bookmarkFrequency = new Dictionary<KeyValuePair<int, string>, int>();

			RetargetButton.Enabled = selectedCount == 1;

			foreach (SongEntry e in SelectedSongs)
			{
				foreach (string tag in e.Tags) tagFrequency[tag] = tagFrequency.ContainsKey(tag) ? tagFrequency[tag] + 1 : 1;
				foreach (var b in e.Bookmarks) bookmarkFrequency[b] = bookmarkFrequency.ContainsKey(b) ? bookmarkFrequency[b] + 1 : 1;
			}

			Tags.Data.Clear();
			foreach (var tag in tagFrequency) if (tag.Value == selectedCount) Tags.Data.Add(tag.Key);
         TagList.ReloadData();

			Bookmarks.Data.Clear();
			foreach (var b in bookmarkFrequency) if (b.Value == selectedCount) Bookmarks.Data.Add(new Bookmark(b.Key.Key, b.Key.Value));
         BookmarkList.ReloadData();
		}

      partial void tagAddClicked(NSObject sender)
      {
         c.AddTag(TagBox.StringValue);
         TagBox.StringValue = "";
      }

      partial void tagRemoveClicked(NSObject sender)
      {
         int selected = (int)TagList.SelectedRow;
         if (selected < 0) return;

         var tag = Tags.Data[selected];
         c.RemoveTag(tag);
         TagBox.StringValue = tag;
      }

      partial void bookmarkAddClicked(NSObject sender)
      {
         c.AddBookmark(Math.Min(10000, Math.Max(1, BookmarkMeasureBox.IntValue)), BookmarkLabelBox.StringValue);
         BookmarkLabelBox.StringValue = "";
      }

      partial void bookmarkRemoveClicked(NSObject sender)
      {
         int selected = (int)BookmarkList.SelectedRow;
         if (selected < 0) return;

         var bookmark = Bookmarks.Data[selected];
         c.RemoveBookmark(bookmark.Measure);

         BookmarkMeasureBox.IntValue = bookmark.Measure;
         BookmarkLabelBox.StringValue = bookmark.Description ?? "";
      }

      public void UpdateSelectedSongTitle()
      {
			var selected = SongList.SelectedRows;
         if (!selected.Any()) return;

			var d = SongList.Delegate as SimpleTableDelegate<SongEntry>;
         var a = d.SelectionChanged;
         d.SelectionChanged = null;

         SongList.ReloadData();
         SongList.SelectRows(selected, false);

         d.SelectionChanged = a;
      }

      public ImportOptions AskImportOptions()
      {
         var import = new ImportViewController();
         var alert = new NSAlert()
         {
            AlertStyle = NSAlertStyle.Informational,
            InformativeText = "This will scan the auto-saved data files in your Synthesia data directory and import the selected data for any song entries matching those in the current metadata file.\n\nCAUTION: Matching song data will always overwrite the selected fields in this metadata file!  This cannot be reversed.  Save a backup copy first if you are unsure.",
            MessageText = "Import from Synthesia",
            AccessoryView = import.View
         };
         alert.AddButton("Import");
         alert.AddButton("Cancel");
         alert.Layout();

         if (alert.RunSheetModal(Window) == (int)NSAlertButtonReturn.Second) return ImportOptions.Cancel;

         ImportOptions result = 0;
         if (import.StandardSource) result |= ImportOptions.StandardPath;
         if (import.FingerHints) result |= ImportOptions.FingerHints;
         if (import.HandParts) result |= ImportOptions.HandParts;
         if (import.Parts) result |= ImportOptions.Parts;
         return result;
      }

      public bool LaunchGroupEditor(MetadataFile m)
      {
         GroupEditorController editor = new GroupEditorController(c.Metadata);
         NSApplication.SharedApplication.RunModalForWindow(editor.Window);
         return editor.MadeChanges;
      }

      partial void backgroundBrowseClicked(NSObject sender)
      {
         var relative = c.BrowseBackground();
         if (relative == null) return;

         BackgroundBox.StringValue = relative;
         c.BackgroundChanged(relative);
      }
   }
}
