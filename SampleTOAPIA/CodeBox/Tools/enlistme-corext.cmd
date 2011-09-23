@rem Enlist a developper in the source tree for the [CoReXT] Build Environment (ddoub)
@rem

:: This is a CodeBox-specific version of the traditional enlistme.cmd.
:: Differences (if environment variable CODEBOX is defined):
:: - The "branch" passed in is expected to be a project name.  It's not 
::   expected that it will be among the results of 'sd branches', or that
::   it will have a clientviews entry.  It's only preferable that it's among
::   the results of 'sd dirs //depot/<project>'
:: - %COREXT_VERSION% is expected to be set.
:: - Variables to put in clientview specifications are %SDCLIENT%, %Project%, and
::   %COREXT_VERSION%.
:: - The default client view is found under //depot/codebox/tools/clientviews/corext_default.
:: - The client view when COREXT_SHARED is specified is found under 
::   //depot/codebox/Tools/clientviews/corext_shared.
:: - If you want to override a clientview for this project, check in corext_default
::   or corext_shared under  
::   //depot/<project>/codebox/override/tools/clientviews/
:: - CodeBox icon is shown on shortcut rather than CoreXT icon.

@rem
@rem    Turn echo off unless Verbose is defined
@rem
@echo off
if DEFINED Verbose echo on

@echo [CoreXT][Enlistment] Running %~d0%~p0%~n0 ...

@rem __________________________________________________________________________
@rem
@rem    Turn on setlocal to keep environment changes local to this script
@rem    Turn on delayed environment variable expansion
@rem
@rem __________________________________________________________________________

setlocal ENABLEDELAYEDEXPANSION

@rem __________________________________________________________________________
@rem
@rem    Prepare temporary directory.
@rem
@rem __________________________________________________________________________

if "%tmp%"=="" set tmp=%SystemDrive%\temp
if not exist "%tmp%" mkdir "%tmp%"
if not exist "%tmp%" (
    @echo   Error: Unable to create a temporary folder.
    @echo          %tmp%
    goto :EndOfScript
)


@rem __________________________________________________________________________
@rem
@rem    These variables get used during usage statement so they must 
@rem    be defined here
@rem
@rem __________________________________________________________________________

set ScriptName=%0
set ScriptCmdName=%~n0
set ToolsPath=%~d0%~p0
set Branch=
set ExcludeList=
set SrcDom=REDMOND

set EnlistmeDirectory=%~d0%~p0
set ClientviewsDirectory=%~d0%~p0clientviews

:: Codebox-specific
if defined CODEBOX (
  set ClientviewsOverrideDirectory=%~d0%~p0override\clientviews
  if not defined COREXT_VERSION (
    echo    Error: COREXT_VERSION not set.  If CODEBOX is set, COREXT_VERSION
    echo           is expected.
    goto :EndOfScript
  )
)


set ClientOptions=nomodtime noclobber

for /F "eol= delims=~" %%d in ('CD') do set CurrentDirectory=%%d

@rem
@rem collect arguments
@rem

if exist "%EnlistmeDirectory%enlistme-xt.cmd" (
    call "%EnlistmeDirectory%enlistme-xt.cmd" %*
)

if exist "%EnlistmeDirectory%\sd.ini" (
  for /f %%i in ('findstr /I /c:"SDPORT" %EnlistmeDirectory%\sd.ini') do set %%i
)

if "%SDPORT%"=="" (
    echo   Error: SDPORT not set. Contact the depot admin.
    echo          The sd.ini or enlistme-xt.cmd are probably missing
    echo          or do not set the right value. 
    goto :EndOfScript
)

for /F "delims=: tokens=1,2" %%i in ( "%SDPORT%" ) do set EnlistServer=%%i&set EnlistPort=%%j

@rem __________________________________________________________________________
@rem
@rem    Exclusions from enlistment
@rem
@rem __________________________________________________________________________

if NOT "%COREXT_SHARED%"=="" (
    set ExcludeList=
    for %%a in (bbt bin dll hcw tracewpp wix tools sdk perl sdepot yukon locapply) do (
        set ExcludeList=!ExcludeList! public/ext/%%a/...
    )
) else if NOT "%COREXT_SHARED_SDK%"=="" (
    set ExcludeList=public/ext/sdk/...
)

@rem __________________________________________________________________________
@rem
@rem    Check whether within an existing branch
@rem
@rem __________________________________________________________________________

if NOT "%COREXTBRANCH%"=="" (
    @echo   Error: Enlisting from an existing build environment.
    @echo          You must open a new clean cmd.exe window to enlist.
    goto :EndOfScript
)

:: CodeBox-specific
if not "%CODEBOXPROJECT%"=="" (
  @echo   Error: Enlisting from an existing CodeBox project environment.
  @echo          You must open a new clean cmd.exe window to enlist.
  goto :EndOfScript
)

@rem __________________________________________________________________________
@rem
@rem    Allow remote domains to enlist
@rem
@rem __________________________________________________________________________

if NOT "%USERDOMAIN%"=="%SrcDom%" (
    @echo Warning: Enlisting to a server in %SrcDom% from %USERDOMAIN%.
    @echo          Make sure USERNAME, USERDOMAIN and SDPASSWD are properly set 
    @echo          if you are not logged in as an authorized domain account.
)

@rem __________________________________________________________________________
@rem
@rem    Make sure SD binaries are in the path
@rem
@rem __________________________________________________________________________

if NOT EXIST %ToolsPath%\SD.exe (
    @echo Error: Cannot find %ToolsPath%\SD.exe.
    goto :EndOfScript
) else (
    @echo Using %ToolsPath%SD.exe
)

@rem __________________________________________________________________________
@rem
@rem    Parameter Checking
@rem
@rem __________________________________________________________________________

if "%1"=="" goto :Usage

set ProjectList=tools/autocheckin tools/build tools/path1st tools/raid public/ext/sdepot public/inc build
set FileList=public/ext/tools/.../cut.exe public/ext/tools/.../alias.exe tools/coretools/icon.ico

set EnlistAll=1
set EnlistShowClient=0

:ArgLoop
if "%1"=="" goto :ArgLoopDone

    @rem    Go to Usage routine if first parameter contain a question mark or starts with a dash
    for %%a in (./ .- .) do if ".%1." == "%%a?." goto :Usage

    @rem    Create a shortcut only with -shortcut
    for %%a in (./ .- .) do if ".%1." == "%%ashortcut." ( call :SetInetroot & call :ShortCut & goto :EndOfScript )

    @rem    Run custom scripts with -custom
    for %%a in (./ .- .) do if ".%1." == "%%acustom." ( call :Custom & goto :EndOfScript )

    @rem    Set variable if -all is passed in as a parameter
    for %%a in (./ .- .) do if ".%1." == "%%aall." ( set EnlistAll=1& shift& goto :ArgLoop )

    @rem    Set variable if -min is passed in as a parameter
    for %%a in (./ .- .) do if ".%1." == "%%amin." ( set EnlistAll=0& shift& goto :ArgLoop )

    @rem    Set variable if -nopause is passed in as a parameter
    for %%a in (./ .- .) do if ".%1." == "%%anopause." ( set NOPAUSE=1& shift& goto :ArgLoop )

    @rem    Set variable if -showclient is passed in as a parameter
    for %%a in (./ .- .) do if ".%1." == "%%ashowclient." ( set EnlistShowClient=1& shift& goto :ArgLoop )

    @rem    Set variable if -startmenu is passed in as a parameter
    for %%a in (./ .- .) do if ".%1." == "%%astartmenu." ( set ShortCutInStartMenu=1& shift& goto :ArgLoop )

    @rem    First argument sets branch
    if "%Branch%"=="" set Branch=%1& shift& goto :ArgLoop

    @rem    Other arguments get added to ProjectList variable
    set ProjectList=%ProjectList% %1& set EnlistAll=0& shift& goto :ArgLoop

:ArgLoopDone

@rem __________________________________________________________________________
@rem
@rem    Check the branch
@rem
@rem __________________________________________________________________________

    if "%SDUSER%"=="" set SDUSER=%USERDOMAIN%\%USERNAME%

    @rem    Check validity of first parameter
    if "%Branch%"=="" goto :Usage
    set Project=%Branch%

    @echo %Branch% | findstr /I "^[\-/].*" 1>nul 2>nul
    if not errorlevel 1 (
:: Codebox-specific
        if defined CODEBOX (
          @echo Error: no project specified.
        ) else (
          @echo Error: no branch specified. 
        )
        goto :Usage
    )

:: CodeBox-specific -- In this section we look at CodeBox projects differently
:: from branches
if not defined CODEBOX (
    @echo Checking branch or client %Branch%
    if exist "%tmp%\branches" ( del /f/q "%tmp%\branches" )
    if exist "%tmp%\branches" (
        @echo Error: could not delete %tmp%\branches.
        goto :EndOfScript
    )

    call %ToolsPath%SD -p %SDPORT% branches > "%tmp%\branchesall"
    for /f "tokens=2 delims= " %%a in (%tmp%\branchesall) do echo %%a >> "%tmp%\branches"
    for %%a in ( %ClientviewsDirectory%\*.* ) do echo %%~na >> "%tmp%\branches"

    if not exist "%tmp%\branches" (
        @echo Error: no branches available in this depot.
        goto :EndOfScript
    )

    findstr /i %Branch% "%tmp%\branches" >nul
    if errorlevel 1 (
        echo Warning: Branch %Branch% does not appear to exist, is empty or a view is unavailable.
    )
) else (
    set Project=%Branch%
    
    :: Make sure that it wasn't a "-" or "/" argument
    echo %Project% | findstr /i "^[\-/].* 1>nul 2>nul
    if not errorlevel 1 (
        @echo Error: No project specified.
        goto :Usage
    )

    @echo Checking project %Project%
    call %ToolsPath%SD -p %SDPORT% dirs //depot/%Project% 2>nul | findstr /i /c:"//depot/%Project%" 1>nul 2>nul
    if errorlevel 1 (
      @echo Warning: Project %Project% does not appear to exist or is empty.
    )
    
    if defined COREXT_SHARED (
        set CodeBoxClientView=%ClientviewsDirectory%\corext_shared
        if exist %ClientviewsOverrideDirectory%\corext_shared (
            set CodeBoxClientView=%ClientviewsOverrideDirectory%\corext_shared
        )
    ) else (
       set CodeBoxClientView=%ClientviewsDirectory%\corext_default
       if exist %ClientviewsOverrideDirectory%\corext_default (
         set CodeBoxClientView=%ClientviewsOverrideDirectory%\corext_default
       )
    )
)

@rem __________________________________________________________________________
@rem
@rem    Main script body
@rem
@rem __________________________________________________________________________

    Call :SetInetroot

    @rem __________________________________________________________________________
    @rem
    @rem    Find and available client
    @rem
    @rem __________________________________________________________________________
    
    call :SetUniqueClient

    @rem
    @rem    Print out what this script will do
    @rem
 if not defined CODEBOX (
    echo.
    echo Note: If you are unsure about anything email "corext".
    echo       This script will create a workspace for %SDPORT% %Branch%.
    echo       Files will be created in the %INETROOT% directory.
    if "%EnlistAll%"=="1" (
        echo       You will be enlisted in the complete tree.
    ) else (
        echo       You will be enlisted in a series of default paths
        echo       plus whatever projects you named on the
        echo       command line, currently:
        echo           %ProjectList%
        echo           %FileList%
    )
    if NOT "%COREXT_SHARED%"=="" (
        echo       Shared CoreXT will be used: %COREXT_SHARED%
    ) else if NOT "%COREXT_SHARED_SDK%"=="" (
        echo       Shared SDK will be used: %COREXT_SHARED_SDK%
    )
    echo.
    echo       For more info, and a detailed list of projects available,
    echo           run "%ScriptName% /?".
    echo.
 ) else (
    echo  You will be enlisted in project %Project% with the following client view:
    echo.
    for /f "tokens=1,2*" %%l in ( %CodeBoxClientView% ) do (
        cmd /c echo  %%l %%m 
    )
    echo.
 )
   

    echo %INETROOT% | findstr \\ > nul
    if errorlevel 1 (
        echo.
        echo Error: The enlistment root path "%INETROOT%" must have at least one backslash in it.
        goto :EndOfScript
    )

    set enlistmeSpaces=0
    for %%i in ( %INETROOT% ) do set /a enlistmeSpaces=!enlistmeSpaces!+1
    if not "%enlistmeSpaces%"=="1" (
        echo.
        echo Error: The enlistment root path "%INETROOT%" must not contain spaces.
        goto :EndOfScript
    )

    echo If this is not what you want to do, hit Ctrl-C now. Otherwise,

    if /I "%NOPAUSE%" NEQ "1" pause

    if not exist %INETROOT% mkdir %INETROOT%
    if not exist %INETROOT% (
        echo Error: Could not make project root dir %INETROOT%.
        goto :EndOfScript
    )
    cd /d %INETROOT%

    @rem __________________________________________________________________________
    @rem
    @rem    Ready to enlist.
    @rem
    @rem __________________________________________________________________________
  
    @rem __________________________________________________________________________
    @rem
    @rem    Create an sd.ini to signal the location of the enlistment root, client and server.
    @rem
    @rem __________________________________________________________________________

    echo # sd.ini created by CoreXT enlistment > sd.ini
    echo SDPORT=%SDPORT% >> sd.ini
    echo SDCLIENT=%SDCLIENT% >> sd.ini
    if not "%SDPROXY%" == "" echo SDPROXY=%SDPROXY% >> sd.ini

    @rem
    @rem    Generate client view
    @rem

    del /f/q "%tmp%\clientview.tmp"

    call :SetUniqueClient

    echo Client: %SDCLIENT%                                               >> "%tmp%\clientview.tmp"
    echo.                                                                 >> "%tmp%\clientview.tmp"
    echo Owner: %SDUSER%                                                  >> "%tmp%\clientview.tmp"
    echo.                                                                 >> "%tmp%\clientview.tmp"
    echo Description: Created by %SDUSER%.                                >> "%tmp%\clientview.tmp"
    echo.                                                                 >> "%tmp%\clientview.tmp"
    echo Root: %INETROOT%                                                 >> "%tmp%\clientview.tmp"
    echo.                                                                 >> "%tmp%\clientview.tmp"
    echo Options: %ClientOptions%                                         >> "%tmp%\clientview.tmp"
    echo.                                                                 >> "%tmp%\clientview.tmp"
    echo View:                                                            >> "%tmp%\clientview.tmp"

  if not defined CODEBOX (
    if exist "%ClientviewsDirectory%\%Branch%" (
        for /f "tokens=1,2*" %%l in ( %ClientviewsDirectory%\%Branch% ) do (
            cmd /c echo  %%l %%m >> "%tmp%\clientview.tmp"
        )
    ) else (
        if "%EnlistAll%"=="1" (
            echo     //depot/%Branch%/...  //%SDCLIENT%/... >> "%tmp%\clientview.tmp"
        ) else (
            for %%a in ( %ProjectList% ) do (
                echo     //depot/%Branch%/%%a/...  //%SDCLIENT%/%%a/... >> "%tmp%\clientview.tmp"
            )
            for %%a in ( %FileList% ) do (
                echo     //depot/%Branch%/%%a  //%SDCLIENT%/%%a >> "%tmp%\clientview.tmp"
            )
        )
    )

    if not exist "%ClientviewsDirectory%\%Branch%" (
        for %%a in (%ExcludeList%) do (
            echo     -//depot/%Branch%/%%a //%SDCLIENT%/%%a >> "%tmp%\clientview.tmp"
        )
    )
  ) else (
      for /f "tokens=1,2*" %%l in ( %CodeBoxClientView% ) do (
        cmd /c echo  %%l %%m >> "%tmp%\clientview.tmp"
      )
  )

    @rem
    @rem    If -showclient is specified, print out client files
    @rem
    if "%EnlistShowClient%"=="1" (
      type "%tmp%\clientview.tmp"
    )

    @rem
    @rem    If running in verbose mode print out client files
    @rem
    if DEFINED Verbose type "%tmp%\clientview.tmp" & pause

    @rem
    @rem    Register client view
    @rem
    %ToolsPath%SD -p %SDPORT% client -i < "%tmp%\clientview.tmp"

    @rem
    @rem    Get files
    @rem
    cd /d %INETROOT%

    %ToolsPath%SD -p %SDPORT% sync -f ...

    call :ShortCut
    call :Custom

    echo.
    echo.
    echo You are now enlisted in the %Branch% sources on the %SDPORT% SD server.
    echo.
    if "%ShortCutInStartMenu%"=="" echo A shortcut for %Branch% has been placed on your desktop.
    if NOT "%ShortCutInStartMenu%"=="" echo A shortcut for %Branch% has been placed in your Start.CoReXT menu.
    echo Use this link to open a build window and use your enlistment.
    echo.

    goto :End


@rem __________________________________________________________________________
@rem
@rem    Routine Description:
@rem        Usage : Prints out help message for user
@rem
@rem    Arguments:
@rem        None
@rem __________________________________________________________________________
@rem
:Usage
    @rem
    @rem    Default to Main if %Branch% isn't set
    @rem

    echo ==============================================================================
    echo  CoReXT Enlistme :: Usage
    echo.
    echo   %ScriptCmdName% [Branch] [ProjectList] [-all] [-min]
    echo            [-shortcut] [-startmenu] [-custom]
    echo.
    echo  Use this script to enlist in sources from the %SDPORT% server. You must
    echo  first create the root directory where you want the files placed, for example,
    echo  d:\src\core. Then "cd" into that directory.
    echo.
    echo  The following command: %ScriptName% [branch] -all
    echo  will enlist you in the CoReXT depot's [branch] branch and
    echo  all included directories.
    echo.
    echo  Here is the complete list of available branches on %SDPORT%:
    echo  - (please wait, this might take a minute) -----------------------------------
    @rem
    @rem    Run SD branches command to get list of available branches
    @rem

    call %ToolsPath%SD -p %SDPORT% branches > "%tmp%\branches"
    for /f "tokens=2 delims= " %%a in (%tmp%\branches) do echo   %%a 
    for %%a in ( %ClientviewsDirectory%\*.* ) do echo   %%~na (view)

    echo  -----------------------------------------------------------------------------
    echo.
    echo  All subsequent arguments are added to the list of directories under the
    echo  branch to enlist in. The -all option will enlist you in all the directories
    echo  so that you don't have to type every directory on the command line.
    echo  Note: directory names must be separated with forward-slash, not backslash.
    echo.
    if "%branch%"=="" goto :Usage_NoBranch

    echo  Currently, the directories available in %Branch% are:
    @rem
    @rem    Run %ToolsPath%SD dirs command to get list of available projects.
    @rem
    set AnyProjectsFound=
    echo  - (please wait, this might take a minute) -----------------------------------
    for /f "tokens=3 delims=/" %%a in ('%ToolsPath%SD -p %SDPORT% dirs //depot/%Branch%/*') do set AnyProjectsFound=%%a& echo  %%a
    echo  -----------------------------------------------------------------------------

    @rem
    @rem    Echo <none> if there are none found.
    @rem
    if "%AnyProjectsFound%" == "" echo ^<none^>
:Usage_NoBranch
    @rem
    @rem    Return to calling environment
    @rem
    echo  for questions and problems, please email corext.
    echo ==============================================================================
    goto :EndOfScript

:End

@rem
@rem    Return to calling environment
@rem
endlocal

@rem
@rem    Keep some variables so that user can use SD right away.
@rem
set SDEDITOR=notepad.exe
set SDUSER=%USERDOMAIN%\%USERNAME%
goto :EOF

:Custom
@rem
@rem    Execute custom scripts per enlistment.
@rem
if exist "%INETROOT%\tools\coretools\core-enlistme.cmd" (
    @echo Running [CoReXT] custom scripts ...
    call "%INETROOT%\tools\coretools\core-enlistme.cmd"
)
if exist "%INETROOT%\build\enlistme.cmd" (
    @echo Running [%Branch%] custom scripts ...
    call "%INETROOT%\build\enlistme.cmd"
)
goto :EOF

:ShortCut
@rem
@rem    Put a shortcut on the desktop or start menu
@rem    _INETROOT_ icon path keyword is replaced with the startup path by shortcut.vbs
@rem    _CUR_DIR_ arguments keyword is replaced with %CD% by shortcut.vbs
@rem
echo Creating shortcut for %Branch% ...
set ShortcutStartup=%INETROOT%

if not defined CODEBOX (
    if exist "%INETROOT%\build\icon.ico" (
        set BranchIcon=_INETROOT_\build\icon.ico
    ) else (
        set BranchIcon=_INETROOT_\tools\coretools\icon.ico
    )
) else (
    set BranchIcon=_INETROOT_\codebox\tools\icon.ico
)

set "Args=/k set INETROOT=_CUR_DIR_&set COREXTBRANCH=%Branch%"
if NOT "%COREXT_SHARED%"=="" (
    set "Args=%Args%&set COREXT_SHARED=%COREXT_SHARED%"
) else if NOT "%COREXT_SHARED_SDK%"=="" (
    set "Args=%Args%&set COREXT_SHARED_SDK=%COREXT_SHARED_SDK%"
)

if defined CODEBOX (
  set "Args=%Args%&set CODEBOXPROJECT=%Project%
)

set "Args=%Args%&.\tools\path1st\myenv.cmd"
if not defined CODEBOX (
    set ShortcutName=CoReXT %Branch% %EnlistServer% %EnlistPort%
    goto :CreateShortCut
) 

:: This section creates the CodeBox version of the shortcut name
set ShortcutName=CodeBox %Project%
set ShortcutNumber=0

:ShortCut_Next
if exist "%userprofile%\desktop\%shortcutname%*.lnk" (
  for /f "tokens=*" %%i in ( 'dir /b "%userprofile%\desktop\%ShortcutName%*.lnk"' ) do (
    if "%ShortcutNumber%"=="0" (
      if /i "%%i"=="%ShortcutName%.lnk" (
          set ShortcutNumber=1
          goto :ShortCut_Next
      )   
    ) else (
       if /i "%%i"=="%ShortcutName% [%ShortcutNumber%].lnk" (
         set /a ShortcutNumber=%ShortcutNumber%+1
         goto :ShortCut_Next
       )
     )
  )
)

if not "%ShortcutNumber%"=="0" (
  set ShortcutName=%ShortcutName% [%ShortcutNumber%]
)

:CreateShortcut

"%ToolsPath%shortcut.vbs" /p:cmd.exe /n:"%ShortcutName%.lnk" /i:"%BranchIcon%" /a:"%Args%" /d:%ShortcutStartup%

if "%ShortCutInStartMenu%"=="" goto :EOF
@rem
@rem    Shortcut on Start Menu instead of Desktop
@rem
if exist "%USERPROFILE%\Start Menu\CoReXT\%ShortcutName%.lnk" del "%USERPROFILE%\Start Menu\CoReXT\%ShortcutName%.lnk"
if not exist "%USERPROFILE%\Start Menu\CoReXT" md "%USERPROFILE%\Start Menu\CoReXT"
move "%USERPROFILE%\Desktop\%ShortcutName%.lnk" "%USERPROFILE%\Start Menu\CoReXT\%ShortcutName%.lnk"
goto :EOF

:SetInetroot
@rem
@rem    Check to make sure %INETROOT% variable is set and if it's valid
@rem
if "%INETROOT%"=="" (
    set INETROOT=%CurrentDirectory%
)
echo Creating an enlistment in %INETROOT%.
goto :EOF

:SetUniqueClient
%ToolsPath%sd -p %SDPORT% clients > "%tmp%\%USERNAME%-clients.tmp"
set ClientNumber=1
:SetUniqueClient_Next
 for /F "tokens=2" %%i in ( %tmp%\%USERNAME%-clients.tmp ) do (
    if /I "%%i"=="%COMPUTERNAME%-%branch%-%ClientNumber%" (
        set /A ClientNumber=%ClientNumber%+1
        goto :SetUniqueClient_Next
    )
 )
set SDCLIENT=%COMPUTERNAME%-%branch%-%ClientNumber%
echo Creating a client called %SDCLIENT%
del /f/q "%tmp%\%USERNAME%-clients.tmp"
goto :EOF

:EndOfScript
endlocal
goto :EOF
