# pairs-memory-game
Pairs is a memory game with up to 6 x 6 cards placed upside down. Click on a pair to advance

Pairs is a modified version of Codeplex's Pairs but extended and with an Installer written in WIX #.

Pairs is written in WPF with one additional control, IntegerUpDown, from Xceed.Wpf.Toolkit via NuGet.

Pairs runs Windows XP due to it using .NET 4.0 Client Profile.  

However, the installer uses .NET 4.5 i.e. won't run on XP. 
It seems that WixSharp.dll depends on an assembly, System.Design not present in 4.0 Client Profile.
