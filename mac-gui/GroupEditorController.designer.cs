// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Synthesia
{
	[Register ("GroupEditorController")]
	partial class GroupEditorController
	{
		[Outlet]
		AppKit.NSButton AddButton { get; set; }

		[Outlet]
		AppKit.NSButton DownButton { get; set; }

		[Outlet]
		AppKit.NSOutlineView GroupList { get; set; }

		[Outlet]
		AppKit.NSTextField GroupNameBox { get; set; }

		[Outlet]
		AppKit.NSButton RemoveButton { get; set; }

		[Outlet]
		AppKit.NSTableView SongList { get; set; }

		[Outlet]
		AppKit.NSButton UpButton { get; set; }

		[Action ("addClicked:")]
		partial void addClicked (Foundation.NSObject sender);

		[Action ("closeClicked:")]
		partial void closeClicked (Foundation.NSObject sender);

		[Action ("downClicked:")]
		partial void downClicked (Foundation.NSObject sender);

		[Action ("menuCreateGroupUnderClicked:")]
		partial void menuCreateGroupUnderClicked (Foundation.NSObject sender);

		[Action ("menuCreateTopLevelClicked:")]
		partial void menuCreateTopLevelClicked (Foundation.NSObject sender);

		[Action ("menuRemoveAllSongsClicked:")]
		partial void menuRemoveAllSongsClicked (Foundation.NSObject sender);

		[Action ("menuRemoveGroupClicked:")]
		partial void menuRemoveGroupClicked (Foundation.NSObject sender);

		[Action ("removeClicked:")]
		partial void removeClicked (Foundation.NSObject sender);

		[Action ("upClicked:")]
		partial void upClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}

			if (DownButton != null) {
				DownButton.Dispose ();
				DownButton = null;
			}

			if (GroupList != null) {
				GroupList.Dispose ();
				GroupList = null;
			}

			if (GroupNameBox != null) {
				GroupNameBox.Dispose ();
				GroupNameBox = null;
			}

			if (RemoveButton != null) {
				RemoveButton.Dispose ();
				RemoveButton = null;
			}

			if (SongList != null) {
				SongList.Dispose ();
				SongList = null;
			}

			if (UpButton != null) {
				UpButton.Dispose ();
				UpButton = null;
			}
		}
	}
}
