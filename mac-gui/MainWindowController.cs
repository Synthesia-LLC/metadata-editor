using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using AppKit;

namespace Synthesia
{
   public partial class MainWindowController : NSWindowController, IGuiForm
   {
      public GuiController c { get; set; }

      public IEnumerable<SongEntry> SelectedSongs => new SongEntry[] { /* TODO */ };
      public string WindowTitle { set { Window.Title = value; } }
      public void DeselectAllSongs() { /* TODO */ }

      public bool AskYesNo(string message, string title) { var alert = new NSAlert { MessageText = title, InformativeText = message }; alert.AddButton("No"); alert.AddButton("Yes"); return alert.RunModal() == (int)NSAlertButtonReturn.Second; }
      public void ShowInfo(string message, string title) { new NSAlert { MessageText = title, InformativeText = message, AlertStyle = NSAlertStyle.Informational }.RunModal(); }
      public void ShowError(string message, string title) { new NSAlert { MessageText = title, InformativeText = message, AlertStyle = NSAlertStyle.Critical }.RunModal(); }
      public void ShowExclamation(string message, string title) { new NSAlert { MessageText = title, InformativeText = message, AlertStyle = NSAlertStyle.Warning }.RunModal(); }

      public string SaveMetadataFilename()
      {
         var panel = new NSSavePanel() { Title = "Save Metadata File", AllowedFileTypes = (from e in c.MetaExtensions select e.Substring(1)).ToArray(), AllowsOtherFileTypes = true };
         return (panel.RunModal() == 1) ? panel.Url?.Path ?? null : null;
      }
      public string OpenMetadataFilename()
      {
         var panel = new NSOpenPanel() { Title = "Open Metadata File", AllowedFileTypes = (from e in c.MetaExtensions select e.Substring(1)).ToArray(), AllowsOtherFileTypes = true, AllowsMultipleSelection = false, CanChooseFiles = true, CanChooseDirectories = false };
         return (panel.RunModal() == 1) ? panel.Url?.Path ?? null : null;
      }
      public string RetargetSongFilename()
      {
			var panel = new NSOpenPanel() { Title = "Retarget Metadata", AllowedFileTypes = (from e in c.SongExtensions select e.Substring(1)).ToArray(), AllowsOtherFileTypes = true, AllowsMultipleSelection = false, CanChooseFiles = true, CanChooseDirectories = false };
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
         alert.AddButton("Yes");
         alert.AddButton("No");
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
         // NOTE: This c.set is actually superfluous.  The GuiController sets it for us.
         c = new GuiController(this, "");

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
      }

      public override void AwakeFromNib() { base.AwakeFromNib(); }
      public new MainWindow Window { get { return (MainWindow)base.Window; } }

      // New, Open, Save, Save As, and Import handled in AppDelegate (About and Exit handled automatically)

      partial void removeSongClicked(NSObject sender) { c.RemoveSelectedSongs(); }
      partial void groupingClicked(NSObject sender) { c.Grouping(); }
      partial void retargetClicked(NSObject sender) { c.RetargetUniqueId(); }
      partial void addSongClicked(NSObject sender)
      {
			var panel = new NSOpenPanel() { Title = "Add Songs", AllowedFileTypes = (from e in c.SongExtensions select e.Substring(1)).ToArray(), AllowsOtherFileTypes = true, AllowsMultipleSelection = true, CanChooseFiles = true, CanChooseDirectories = false };
         if (panel.RunModal() != 1) return;
         c.AddSongs((from s in panel.Urls select s?.Path ?? null).ToArray());
      }

      // TODO: SongList_SelectedIndexChanged

      public void RefreshSongList()
      {
         // TODO
      }

      public void ClearSongControls()
      {
         // TODO
      }

      // TODO: BindBox
      // TODO: BindNumericBox
      // TODO: BookmarkListItem

      public void BindSongControls()
      {
         // TODO
      }

      partial void tagAddClicked(NSObject sender)
      {
         // TODO
      }

      partial void tagRemoveClicked(NSObject sender)
      {
         // TODO
      }

      // TODO: tagTextChanged
      // TODO: tagSelectedIndexChanged

      partial void bookmarkAddClicked(NSObject sender)
      {
         // TODO
      }

      partial void bookmarkRemoveClicked(NSObject sender)
      {
         // TODO
      }

      // TODO: bookmarkDescriptionChanged
      // TODO: bookmarkSelectedIndexChanged

      public void UpdateSelectedSongTitle()
      {
         // TODO
      }

      // TODO: DragDrop
      // TODO: DragEnter

      public ImportOptions AskImportOptions()
      {
         // TODO
         return ImportOptions.Cancel;
      }

      public bool LaunchGroupEditor(MetadataFile m)
      {
         // TODO
         return false;
      }

      partial void backgroundBrowseClicked(NSObject sender)
      {
         var relative = c.BrowseBackground();
         if (relative != null) BackgroundBox.StringValue = relative;
      }
   }
}
