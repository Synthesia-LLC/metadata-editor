using System;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace Synthesia
{
   public class SimpleTableSource<Type> : NSTableViewDataSource
   {
      public List<Type> Data = new List<Type>();
      public override nint GetRowCount(NSTableView tableView) { return Data.Count; }
   }

   public class SimpleTableDelegate<Type> : NSTableViewDelegate
   {
      readonly SimpleTableSource<Type> Source;
      public Action SelectionChanged;

      public SimpleTableDelegate(SimpleTableSource<Type> s, Action selectionChanged = null) { Source = s; SelectionChanged = selectionChanged; }

      public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
      {
         NSTextField v = (NSTextField)tableView.MakeView("cell", this);
         if (v == null) v = new NSTextField { Identifier = "cell", BackgroundColor = NSColor.Clear, Bordered = false, Selectable = false, Editable = false };

         v.StringValue = Source.Data[(int)row].ToString();
         return v;
      }

      public override void SelectionDidChange(NSNotification notification) { SelectionChanged?.Invoke(); }
   }
}
