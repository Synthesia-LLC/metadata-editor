using System;
using System.Linq;
using System.Collections.Generic;
using AppKit;
using Foundation;

public class DragDropView : NSView
{
   public Synthesia.MainWindowController controller { get; set; }

   public DragDropView(IntPtr handle) : base(handle) { }

   public override void AwakeFromNib()
   {
      base.AwakeFromNib();
      RegisterForDraggedTypes(new string[] { NSPasteboard.NSFilenamesType });
   }

   IEnumerable<string> DraggedFilenames(NSPasteboard pasteboard)
   {
      if (Array.IndexOf(pasteboard.Types, NSPasteboard.NSFilenamesType) < 0) yield break;
      foreach (var i in pasteboard.PasteboardItems) yield return new NSUrl(i.GetStringForType("public.file-url")).Path;
   }

   public override NSDragOperation DraggingEntered(NSDraggingInfo sender)
   {
      return controller.c.AllowDragDrop(DraggedFilenames(sender.DraggingPasteboard).ToArray()) ? NSDragOperation.Copy : NSDragOperation.None;
   }

   public override bool PerformDragOperation(NSDraggingInfo sender)
   {
      return controller.c.DragDropFiles(DraggedFilenames(sender.DraggingPasteboard).ToArray());
   }
}
