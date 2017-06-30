using System;
using Foundation;
using AppKit;

namespace Synthesia
{
   public partial class ImportViewController : NSViewController
   {
      [Export("initWithCoder:")]
      public ImportViewController(NSCoder coder) : base(coder) { }
      public ImportViewController(IntPtr handle) : base(handle) { }
      public ImportViewController() : base("ImportView", NSBundle.MainBundle) { }

      public new ImportView View => (ImportView)base.View;

      public bool FingerHints => fingerHints.State == NSCellStateValue.On;
      public bool HandParts => handParts.State == NSCellStateValue.On;
      public bool Parts => parts.State == NSCellStateValue.On;

      public bool StandardSource => importSource.IndexOfSelectedItem == 0;
   }
}
