
:: ----------------------------------------
:: Turn echo off unless verbose is defined.
:: ----------------------------------------
@echo off
if defined Verbose echo on

@echo Preparing the CodeBox environment
@echo.
@echo Using Source Depot for source control.
@echo CodeBox project is %CODEBOXPROJECT%
@echo.

@echo Adding "%inetroot%\CodeBox\tools" to path.
set path=%path%;"%inetroot%\CodeBox\tools"

if exist "%inetroot%\CodeBox\override\Tools\runme.cmd" (
  @echo Calling "%inetroot%\CodeBox\override\Tools\runme.cmd"
  "%inetroot%\CodeBox\override\Tools\runme.cmd"
  goto :End
)

if "%VS80COMNTOOLS%"=="" (
  @echo Warning: Could not find variable VS80COMNTOOLS.  Visual Studio 2005 may not be installed.  Not running vsvars32.bat.
) else (
  if not exist "%VS80COMNTOOLS%vsvars32.bat" (
    @echo Warning: Could not find "%VS80COMNTOOLS%vsvars32.bat"
  ) else (
    call "%VS80COMNTOOLS%vsvars32.bat"
  )
)

:End
@echo.

