using System.IO;
using AppKit;
using Foundation;

namespace Synthesia
{
   [Register("AppDelegate")]
   public class AppDelegate : NSApplicationDelegate
   {
      MainWindowController controller;
      string fileToOpen;

      public override void DidFinishLaunching(NSNotification notification)
      {
         controller = new MainWindowController();
         controller.Window.MakeKeyAndOrderFront(this);

			if (fileToOpen != null) controller.c.Open(fileToOpen);
		}

      public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender) { return true; }

      public override NSApplicationTerminateReply ApplicationShouldTerminate(NSApplication sender)
      {
         return controller.OkayToProceed() ? NSApplicationTerminateReply.Now : NSApplicationTerminateReply.Cancel;
      }

      public override bool OpenFile(NSApplication sender, string filename)
      {
         var f = new FileInfo(filename);
         if (!f.Exists) return false;
         if (!GuiController.MetaExtensions.Contains(f.Extension.ToLower())) return false;

         // If we're just starting up, keep the filename for later (after
         // we're initialized), otherwise kick off a file open right now.
         if (controller == null) fileToOpen = filename;
         else controller.c.Open(filename);

         return true;
      }

      [Export("newDocument:")] void New(NSObject sender) { controller.c.CreateNew(); }
      [Export("openDocument:")] void Open(NSObject sender) { controller.c.Open(); }
      [Export("saveDocument:")] void Save(NSObject sender) { controller.c.SaveChanges(); }
      [Export("saveDocumentAs:")] void SaveAs(NSObject sender) { controller.c.SaveAs(); }
      [Export("fetch:")] void Import(NSObject sender) { controller.c.Import(); }
   }
}
