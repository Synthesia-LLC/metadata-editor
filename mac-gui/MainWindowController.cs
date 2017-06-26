using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using AppKit;

namespace Synthesia
{
   public partial class MainWindowController : NSWindowController
   {
      public GuiController c { get; set; }

      // public IEnumerable<SongEntry> SelectedSongs => new List<SongEntry>();
      public string WindowTitle { set { Window.Title = value; } }
      // DeselectAllSongs

      public bool AskYesNo(string message, string title) { var alert = new NSAlert { MessageText = title, InformativeText = message }; alert.AddButton("No"); alert.AddButton("Yes"); return alert.RunModal() == (int)NSAlertButtonReturn.Second; }
      public void ShowInfo(string message, string title) { new NSAlert { MessageText = title, InformativeText = message, AlertStyle = NSAlertStyle.Informational }.RunModal(); }
      public void ShowError(string message, string title) { new NSAlert { MessageText = title, InformativeText = message, AlertStyle = NSAlertStyle.Critical }.RunModal(); }
      public void ShowExclamation(string message, string title) { new NSAlert { MessageText = title, InformativeText = message, AlertStyle = NSAlertStyle.Warning }.RunModal(); }

		public MainWindowController(IntPtr handle) : base(handle)
      {
      }

      [Export("initWithCoder:")]
      public MainWindowController(NSCoder coder) : base(coder)
      {
      }

      public MainWindowController() : base("MainWindow")
      {
         // NOTE: This c.set is actually superfluous.  The GuiController sets it for us.
         //c = new GuiController(this, "");
      }

      public override void AwakeFromNib()
      {
         base.AwakeFromNib();
      }

      public new MainWindow Window
      {
         get { return (MainWindow)base.Window; }
      }

      partial void groupingClicked(NSObject sender)
      {
         //c.Grouping();
      }
   }
}
