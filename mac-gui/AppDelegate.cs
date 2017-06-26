using AppKit;
using Foundation;

namespace Synthesia
{
   [Register("AppDelegate")]
   public class AppDelegate : NSApplicationDelegate
   {
      MainWindowController controller;

      public override void DidFinishLaunching(NSNotification notification)
      {
         controller = new MainWindowController();
         controller.Window.MakeKeyAndOrderFront(this);
      }

      public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender) { return true; }

      public override NSApplicationTerminateReply ApplicationShouldTerminate(NSApplication sender)
      {
         return controller.OkayToProceed() ? NSApplicationTerminateReply.Now : NSApplicationTerminateReply.Cancel;
      }

      [Export("newDocument:")] void New(NSObject sender) { controller.c.CreateNew(); }
      [Export("openDocument:")] void Open(NSObject sender) { controller.c.Open(); }
      [Export("saveDocument:")] void Save(NSObject sender) { controller.c.SaveChanges(); }
      [Export("saveDocumentAs:")] void SaveAs(NSObject sender) { controller.c.SaveAs(); }
      [Export("fetch:")] void Import(NSObject sender) { controller.c.Import(); }
   }
}
