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

      public string SaveMetadataFilename() { return null; /* TODO */ }
      public string OpenMetadataFilename() { return null; /* TODO */ }
      public string RetargetSongFilename() { return null; /* TODO */ }
      public string PickImageFilename() { return null; /* TODO */ }

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
      }

      public override void AwakeFromNib() { base.AwakeFromNib(); }
      public new MainWindow Window { get { return (MainWindow)base.Window; } }

      // New, Open, Save, Save As, and Import handled in AppDelegate (About and Exit handled automatically)

      partial void removeSongClicked(NSObject sender) { c.RemoveSelectedSongs(); }
      partial void groupingClicked(NSObject sender) { c.Grouping(); }
      partial void retargetClicked(NSObject sender) { c.RetargetUniqueId(); }
      partial void addSongClicked(NSObject sender)
      {
         // TODO
         c.AddSongs(new string[] { });
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

      // TODO: boxes changed!

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
         // TODO
      }
   }
}
