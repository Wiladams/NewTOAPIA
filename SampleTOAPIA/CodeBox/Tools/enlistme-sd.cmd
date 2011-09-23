:: Enlist a developer in the source tree for the given project.

:: ----------------------------------------
:: Turn echo off unless verbose is defined.
:: ----------------------------------------
@echo off
if defined Verbose echo on

@echo Running %~d0%~p0%~n0 ...

:: ------------------------------------------------------------------
:: Turn on setlocal to keep environment changes local to this script.
:: Turn on delayed environment variable expansion.
:: ------------------------------------------------------------------

setlocal enabledelayedexpansion

:: ----------------------------
:: Prepare temporary directory
:: ----------------------------

if "%tmp%"=="" set tmp="%USERPROFILE%\Local Settings\Temp"
if not exist "%tmp%" mkdir "%tmp%"
if not exist "%tmp%" (
  @echo Error: Unable to create a temporary folder.
  @echo        %tmp%
  goto :EndOfScript
)

:: -------------------------------------------------------------
:: These variables get used during usage statement so they must
:: be defined here.
:: -------------------------------------------------------------

set ScriptName=%0
set ScriptCmdName=%~n0
set ToolsPath=%~d0%~p0
set Branch=
set ExcludeList=

:: This is the domain the server is on that you're trying to get access to.
:: It's a sanity check to make sure the user is logged in as a domain user.
set SrcDom=REDMOND

:: This is the directory this script is in.
set EnlistmeDirectory=%~d0%~p0

:: Here's where user might have put overrides.
set OverrideDirectory=%EnlistmeDirectory%override

set ClientviewsDirectory=%~d0%~p0clientviews

:: Here's where the user might have checked something in.
set ClientviewsOverrideDirectory=%~d0%~p0override\clientviews

set ClientOptions=nomodtime noclobber


:: set SDPORT to whatever is in the sd.ini found in the current directory
if exist "%EnlistmeDirectory%\sd.ini" (
  for /f %%i in ('findstr /i /c:"SDPORT" "%EnlistmeDirectory%\sd.ini"') do set %%i
)

if "%SDPORT%"=="" (
  @echo Error: SDPORT not set.  Contact 'codebox'.
  @echo        sd.ini is not found or does not set the right value.
  goto :EndOfScript
)

:: ----------------------------------------------------------------------
:: Make sure we're not enlisting from a codebox or corext command window.
:: ----------------------------------------------------------------------
if not "%CODEBOXPROJECT%"=="" (
  @echo Error: Enlisting from an existing CodeBox project environment.
  @echo        You must open a new clean cmd.exe window to enlist.
  goto: EndOfScript
)

if not "%COREXTBRANCH%"=="" (
  @echo Error: Enlisting from an existing CoreXT environment.
  @echo        You must open a new clean cmd.exe window to enlist.
  goto :EndOfScript
)

:: ----------------------------
:: Make sure SD.exe is present.
:: ----------------------------
if not exist "%ToolsPath%SD.exe" (
  @echo Error: Cannot find "%ToolsPath%\SD.exe".
  goto :EndOfScript
) else (
  @echo Using "%ToolsPath%SD.exe"
)

:: ------------------------------------------------------------------------
:: Check if there is an enlistme.cmd in override directory.  If so, run it.
:: ------------------------------------------------------------------------
if exist "%OverrideDirectory%\enlistme.cmd" (
  call "%OverrideDirectory%\enlistme.cmd" %*
  goto :EndOfScript
)

:: ------------------------------------------------------------------------
:: If COREXT_VERSION is set, we should run enlistme-corext.cmd instead.
:: ------------------------------------------------------------------------
if not "%COREXT_VERSION%"=="" (
  set CODEBOX=1
  call "%EnlistmeDirectory%\enlistme-corext.cmd" %*
  goto :EndOfScript
)

:: ----------------
:: Check parameters
:: ----------------
if "%1"=="" goto :Usage

:ArgLoop

if "%1"=="" goto :ArgLoopDone

:: Go to Usage routine if first parameter contains a question mark or starts 
:: with a dash
for %%a in (./ .- .) do if ".%1." == "%%a?." goto :Usage

:: First argument sets branch
if "%Project%"=="" set Project=%1& shift& goto :ArgLoop

:ArgLoopDone

:: -------------------------------------
:: Check that Project is a valid project
:: -------------------------------------

if "%SDUSER%"=="" set SDUSER=%USERDOMAIN%\%USERNAME%

:: Make sure that it wasn't a "-" or "/" argument
@echo %Project% | findstr /i "^[\-/].* 1>nul 2>nul
if not errorlevel 1 (
    @echo Error: No project specified.
    goto :Usage
)

@echo Checking project %Project%
call "%ToolsPath%SD" -p %SDPORT% dirs //depot/%Project% 2>nul | findstr /i /c:"//depot/%Project%" 1>nul 2>nul
if errorlevel 1 (
  @echo Warning: Project %Project% does not appear to exist or is empty.
)

:: ------------
:: Set INETROOT
:: ------------
:: Set INETROOT to the current directory which is the local enlistment folder
set INETROOT=%CD%
@echo Creating an enlistment in %INETROOT%.

:: ------------------------------------------------
:: Prepare %INETROOT%.
:: ------------------------------------------------

:: Print out what this script will do

@echo This script will create a workspace for %SDPORT% %Project%.
@echo Files will be created in the %INETROOT% directory.

:: Verify that root directory has at least one backslash.
@echo %INETROOT% | findstr \\ >nul
if errorlevel 1 (
  @echo.
  @echo Error: The enlistment root path "%INETROOT%" must have at least one backslash in it.
  goto :EndOfScript
)

:: Make sure %INETROOT% exists.
if not exist "%INETROOT%" (
  @echo Error: Could not make project root directory
  goto :EndOfScript
)
cd /d %INETROOT%

:: ----------------------------------------------------------------------
:: Create an sd.ini to signal the location of the enlistment root, client
:: and server.
:: ----------------------------------------------------------------------

call :SetUniqueClient

@echo # sd.ini created by CodeBox enlistment script > sd.ini
@echo SDPORT=%SDPORT% >> sd.ini
@echo SDCLIENT=%SDCLIENT% >> sd.ini
if not "%SDPROXY%" == "" echo SDPROXY=%SDPROXY% >> sd.ini

:: Generate client view

set CodeBoxClientView=%ClientViewsDirectory%\default
if exist "%ClientViewsOverrideDirectory%\default" (
  set CodeBoxClientView=%ClientViewsOverrideDirectory%\default
)

del /f /q "%tmp%\clientview.tmp"

echo Client: %SDCLIENT% >> "%tmp%\clientview.tmp"
echo. >> "%tmp%\clientview.tmp"
echo Owner: %SDUSER% >> "%tmp%\clientview.tmp"
echo. >> "%tmp%\clientview.tmp"
echo Description: Created by %SDUSER% >> "%tmp%\clientview.tmp"
echo. >> "%tmp%\clientview.tmp"
echo Root: "%INETROOT%" >> "%tmp%\clientview.tmp"
echo. >> "%tmp%\clientview.tmp"
echo Options: %ClientOptions% >> "%tmp%\clientview.tmp"
echo. >> "%tmp%\clientview.tmp"
echo View: >> "%tmp%\clientview.tmp"

for /f "tokens=1,2*" %%l in ( %CodeBoxClientView% ) do (
    cmd /c echo     %%l %%m >> "%tmp%\clientview.tmp"
)


:: Register client view.
"%ToolsPath%SD" -p %SDPORT% client -i < "%tmp%\clientview.tmp"

:: Work around a bug in SD that affects clients running on Windows Server 2008.
:: If SDEDITOR is not set, SD uses notepad.exe, but assumes it exists in c:\Windows.
:: In Server 2008, notepad.exe exists only in c:\Windows\System32 (in earlier
:: OSs, it actually exists in both places), so SD can't find it. Setting the
:: SDEDITOR environment variable causes SD to look for notepad.exe on the path,
:: and all is well.

"%ToolsPath%SD" set SDEDITOR=notepad.exe

:: Get files
cd /d %INETROOT%

"%ToolsPath%SD" -p %SDPORT% sync -f ...

call :ShortCut

echo.
echo.
echo You are now enlisted in the %Project% sources on the %SDPORT% SD server. 
echo A shortcut for "CodeBox %Project%" has been placed on your desktop.

goto :End

:Usage

@echo CodeBox Enlistme :: Usage
@echo.
@echo %ScriptCmdName% [ProjectName]
@echo.
@echo Use this script to enlist in sources from the %SDPORT% server.
@echo You must first create the root directory where you want the files
@echo placed--for example, D:\CodeBox\MyProject.  Then "cd" into that directory.
@echo.
@echo The following command will enlist you into the "MyProject" project (if
@echo there were one):
@echo.
@echo   enlistme-sd.cmd MyProject
@echo.

goto :EndOfScript

:End

endlocal

set SDEDITOR=notepad.exe
set SDUSER=%USERDOMAIN%\%USERNAME%
goto :EOF

:: ------------------------------------------------------------------------------
:: Create a shortcut on the user's desktop.
::
:: _INETROOT_ icon path keyword is replaced with the startup path by shortcut.vbs
:: _CUR_DIR_ arguments keyword is replaced with %CD% by shortcut.vbs
:: ------------------------------------------------------------------------------
:ShortCut
set ShortcutStartup=%INETROOT%
set ProjectIcon=_INETROOT_\codebox\tools\icon.ico
set "Args=/k set INETROOT=_CUR_DIR_&set CODEBOXPROJECT=%Project%&.\codebox\tools\runme-sd.cmd"

set ShortcutName=CodeBox %Project%
set ShortcutNumber=0

:ShortCut_Next
if exist "%userprofile%\desktop\%shortcutname%*.lnk" (
  for /f "tokens=*" %%i in ( 'dir /b "%userprofile%\desktop\%shortcutname%*.lnk"' ) do (
    if "%ShortcutNumber%"=="0" (
      if /i "%%i"=="%shortcutname%.lnk" (
          set ShortcutNumber=1
          goto :ShortCut_Next
      )   
    ) else (
       if /i "%%i"=="%shortcutname% [%ShortcutNumber%].lnk" (
         set /a ShortcutNumber=%ShortcutNumber%+1
         goto :ShortCut_Next
       )
     )
  )
)

if not "%ShortcutNumber%"=="0" (
  set ShortcutName=%ShortcutName% [%ShortcutNumber%]
)

"%ToolsPath%shortcut.vbs" /p:cmd.exe /n:"%ShortcutName%.lnk" /i:"%ProjectIcon%" /a:"%Args%" /d:"%ShortcutStartup%"

goto :EOF

:: ---------------------------------------------------------------------------
:: Comes up with a unique client name comprising computername-project-<number>
:: and sets SDCLIENT to this value.
:: ---------------------------------------------------------------------------
:SetUniqueClient

%ToolsPath%sd -p %SDPORT% clients > "%tmp%\%USERNAME%-clients.tmp"
set ClientNumber=1

:SetUniqueClient_Next
for /f "tokens=2" %%i in ( %tmp%\%USERNAME%-clients.tmp ) do (
  if /i "%%i"=="%COMPUTERNAME%-%Project%-%ClientNumber%" (
      set /a ClientNumber=%ClientNumber%+1
      goto :SetUniqueClient_Next
  )
)
set SDCLIENT=%COMPUTERNAME%-%Project%-%ClientNumber%
@echo Creating a client called %SDCLIENT%
del /f /q "%tmpe%\%USERNAME%-clients.tmp"
goto :EOF

:EndOfScript
endlocal
goto :EOF