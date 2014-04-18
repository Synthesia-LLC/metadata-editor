Synthesia Metadata Editor
=========================

The Synthesia Metadata Editor is an easy to use (Mono-compatible) C# application that can generate and edit fully-compliant Synthesia metadata (.synthesia) files.

The editing capabilities expose all of the available functionality of the Synthesia metadata file format including simple content tags, background images, finger hints, and more as the standard develops.

Requires the .NET 4 Client profile on Windows or the latest version of Mono on any of Mono's supported platforms.

To run in Windows, double-click.

To run in OS X, use "mono MetadataEditor-x.x.x.exe" from a terminal.

Alternatively, package the .exe into an Mac app using the following Mono command-line:
`macpack -appname:MetadataEditor -i ~/synthesia/app_icon.icns -m winforms MetadataEditor-1.0.0.exe`

That still requires the Mono runtime be installed, but no longer requires running things from terminal.
