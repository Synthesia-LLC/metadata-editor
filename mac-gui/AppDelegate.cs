using AppKit;
using Foundation;

namespace Synthesia
{
   [Register("AppDelegate")]
   public class AppDelegate : NSApplicationDelegate
   {
      MainWindowController mainWindowController;

      public AppDelegate() { }

      public override void DidFinishLaunching(NSNotification notification)
      {
         mainWindowController = new MainWindowController();
         mainWindowController.Window.MakeKeyAndOrderFront(this);
      }

      public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender) { return true; }

      public override void WillTerminate(NSNotification notification)
      {
         // Insert code here to tear down your application
      }
   }
}
