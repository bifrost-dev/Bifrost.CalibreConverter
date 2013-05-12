DEVENV_PATH="c:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe"
SLN_PATH="Bifrost.CalibreConverter.sln"

CURRENT_DIR = CreateObject("Scripting.FileSystemObject").GetAbsolutePathName(".") + "\"

command = chr(34) & DEVENV_PATH & chr(34) & " " & chr(34) & CURRENT_DIR & SLN_PATH & chr(34)

Set shell = CreateObject("WScript.Shell")
set environment=shell.Environment("System")
environment("COMPLUS_ZapDisable") = "1"

shell.Run command

environment.Remove "COMPLUS_ZapDisable"

Set shell = Nothing

' This script disables optimizations when debugging Reference Source
' source: http://blogs.msdn.com/b/kirillosenkov/archive/2009/01/27/how-to-disable-optimizations-during-debugging.aspx
