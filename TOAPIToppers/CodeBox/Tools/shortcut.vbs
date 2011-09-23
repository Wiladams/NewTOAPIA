' shortcut.vbs
'
' Windows Scripting Host VBScript program
' creates a shortcut on the desktop for enslitments
'
' Usage:
'   Required arguments:
'     /n: name of shortcut, must end with .lnk
'     /i: path & name of icon, must end with .ico
'     /p: name of program to run, e.g. "cmd.exe"
'     /d: working directory path
'   Optional arguments:
'     /a: arguments passed to the program
'
'   NOTE: "_CUR_DIR_" keyword in the arguments will be replaced by the %CD% command variable
'         because it cannot be passed as a literal from a .cmd file
'    
'   NOTE: "_INETROOT_" keyword in the icon path and arguments will be replaced
'         with the working directory path. This reduces the size of the 
'         arguments passed to the script to keep it within the limits of
'         the length of arguments that can be passed from a .cmd file

Set oShell = CreateObject("WScript.Shell")
Set args = WScript.Arguments.Named

' get the enlistment folder path
workingDirectory = args("d")

' _CUR_DIR_ keyword is replaced by the current directory variable %CD%
shortcutArgs = Replace(args("a"), "_CUR_DIR_", "%CD%")

' _INETROOT_ keyword is replaced with working/enlistment directory
iconPathName = Replace(args("i"), "_INETROOT_", workingDirectory)

Set oShortcut = oShell.CreateShortcut(oShell.SpecialFolders("Desktop") & "\" & args("n"))
oShortcut.TargetPath = args("p")
oShortcut.Arguments = shortcutArgs
oShortcut.WorkingDirectory = workingDirectory
oShortcut.IconLocation = iconPathName
oShortcut.WindowStyle = 4
oShortcut.Save
