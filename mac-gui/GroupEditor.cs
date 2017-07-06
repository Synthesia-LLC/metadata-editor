using System;
using Foundation;
using AppKit;

namespace Synthesia
{
   public partial class GroupEditor : NSWindow
   {
      [Export("initWithCoder:")]
      public GroupEditor(NSCoder coder) : base(coder) { }
      public GroupEditor(IntPtr handle) : base(handle) { }

      public override void AwakeFromNib() { base.AwakeFromNib(); }
   }
}
