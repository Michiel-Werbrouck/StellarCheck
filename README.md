# StellarCheck
A fast and easy-to-use Stellaris mod compatibility checker.

# How it works:
This tool will scan through "C:\Program Files (x86)\Steam\steamapps\workshop\content\281990" and check every mod in it.

It will:
scan the amount of mods,
check whether the mod is still active (check if you are still subscribed),
check the contents of the mod zip file,
check if any vanilla files have been overwritten.

# Extra info: 
This program is made in C#, using the DotNetZip library for checking the zip contents.
X Incompatibilities found (This means that there are X mods that are in conflict, the program shows the path in red above) 
X Duplicate files found (This is the amount of duplicate files, this can also show that your current mod collection is... Not the stablest one.) 
X Errors (Ignore this)
