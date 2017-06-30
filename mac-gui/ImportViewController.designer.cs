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
	[Register ("ImportViewController")]
	partial class ImportViewController
	{
		[Outlet]
		AppKit.NSButton fingerHints { get; set; }

		[Outlet]
		AppKit.NSButton handParts { get; set; }

		[Outlet]
		AppKit.NSPopUpButton importSource { get; set; }

		[Outlet]
		AppKit.NSButton parts { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (importSource != null) {
				importSource.Dispose ();
				importSource = null;
			}

			if (fingerHints != null) {
				fingerHints.Dispose ();
				fingerHints = null;
			}

			if (handParts != null) {
				handParts.Dispose ();
				handParts = null;
			}

			if (parts != null) {
				parts.Dispose ();
				parts = null;
			}
		}
	}
}
