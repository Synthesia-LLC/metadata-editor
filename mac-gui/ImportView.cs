using System;
using Foundation;

namespace Synthesia
{
   public partial class ImportView : AppKit.NSView
   {
      [Export("initWithCoder:")]
      public ImportView(NSCoder coder) : base(coder) { }
      public ImportView(IntPtr handle) : base(handle) { }
   }
}
