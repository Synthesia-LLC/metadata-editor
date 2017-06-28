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
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		AppKit.NSTextField ArrangerBox { get; set; }

		[Outlet]
		AppKit.NSTextField BackgroundBox { get; set; }

		[Outlet]
		AppKit.NSTextField ComposerBox { get; set; }

		[Outlet]
		AppKit.NSTextField CopyrightBox { get; set; }

		[Outlet]
		AppKit.NSTextField DifficultyBox { get; set; }

		[Outlet]
		AppKit.NSTextView FingerHintBox { get; set; }

		[Outlet]
		AppKit.NSTextField HandsBox { get; set; }

		[Outlet]
		AppKit.NSTextField LicenseBox { get; set; }

		[Outlet]
		AppKit.NSTextField MadeFamousByBox { get; set; }

		[Outlet]
		AppKit.NSTextView PartsBox { get; set; }

		[Outlet]
		AppKit.NSTextField RatingBox { get; set; }

		[Outlet]
		AppKit.NSTextField SubtitleBox { get; set; }

		[Outlet]
		AppKit.NSTextField TitleBox { get; set; }

		[Action ("addSongClicked:")]
		partial void addSongClicked (Foundation.NSObject sender);

		[Action ("backgroundBrowseClicked:")]
		partial void backgroundBrowseClicked (Foundation.NSObject sender);

		[Action ("bookmarkAddClicked:")]
		partial void bookmarkAddClicked (Foundation.NSObject sender);

		[Action ("bookmarkRemoveClicked:")]
		partial void bookmarkRemoveClicked (Foundation.NSObject sender);

		[Action ("groupingClicked:")]
		partial void groupingClicked (Foundation.NSObject sender);

		[Action ("removeSongClicked:")]
		partial void removeSongClicked (Foundation.NSObject sender);

		[Action ("retargetClicked:")]
		partial void retargetClicked (Foundation.NSObject sender);

		[Action ("tagAddClicked:")]
		partial void tagAddClicked (Foundation.NSObject sender);

		[Action ("tagRemoveClicked:")]
		partial void tagRemoveClicked (Foundation.NSObject sender);

		[Action ("titleChanged:")]
		partial void titleChanged (AppKit.NSTextField sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleBox != null) {
				TitleBox.Dispose ();
				TitleBox = null;
			}

			if (SubtitleBox != null) {
				SubtitleBox.Dispose ();
				SubtitleBox = null;
			}

			if (BackgroundBox != null) {
				BackgroundBox.Dispose ();
				BackgroundBox = null;
			}

			if (RatingBox != null) {
				RatingBox.Dispose ();
				RatingBox = null;
			}

			if (DifficultyBox != null) {
				DifficultyBox.Dispose ();
				DifficultyBox = null;
			}

			if (ComposerBox != null) {
				ComposerBox.Dispose ();
				ComposerBox = null;
			}

			if (ArrangerBox != null) {
				ArrangerBox.Dispose ();
				ArrangerBox = null;
			}

			if (CopyrightBox != null) {
				CopyrightBox.Dispose ();
				CopyrightBox = null;
			}

			if (MadeFamousByBox != null) {
				MadeFamousByBox.Dispose ();
				MadeFamousByBox = null;
			}

			if (FingerHintBox != null) {
				FingerHintBox.Dispose ();
				FingerHintBox = null;
			}

			if (LicenseBox != null) {
				LicenseBox.Dispose ();
				LicenseBox = null;
			}

			if (HandsBox != null) {
				HandsBox.Dispose ();
				HandsBox = null;
			}

			if (PartsBox != null) {
				PartsBox.Dispose ();
				PartsBox = null;
			}
		}
	}
}
