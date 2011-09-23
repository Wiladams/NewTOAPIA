:: ----------------------------------------
:: Turn echo off unless verbose is defined.
:: ----------------------------------------
@echo off
if defined Verbose echo on

@echo Preparing the CodeBox environment
@echo.
@echo Using TFS for source control.
@echo CodeBox project is %CODEBOXPROJECT%
@echo.

@echo Adding %inetroot%\CodeBox\tools to path.
set path=%path%;%inetroot%\CodeBox\tools

if exist "%inetroot%\CodeBox\override\Tools\runme.cmd" (
  @echo Calling "%inetroot%\CodeBox\override\Tools\runme.cmd"
  "%inetroot%\CodeBox\override\Tools\runme.cmd"
  goto :End
)

if exist "%inetroot%\CodeBox\CheckinApproval\UpdateCodeBoxCheckinPolicies.exe" (
  @echo.
  @echo on
  "%inetroot%\CodeBox\CheckinApproval\UpdateCodeBoxCheckinPolicies.exe"
  @echo off
  @echo.
)

set _VSTOOLS=%VS90COMNTOOLS%
if "%_VSTOOLS%"=="" set _VSTOOLS=%VS80COMNTOOLS%

if "%_VSTOOLS%"=="" (
  @echo Warning: Could not find variable VS80COMNTOOLS or VS90COMNTOOLS. Visual Studio 2005 or 2008 may not be installed.  Not running vsvars32.bat.
) else (
  if not exist "%_VSTOOLS%vsvars32.bat" (
    @echo Warning: Could not find "%_VSTOOLS%vsvars32.bat"
  ) else (
    call "%_VSTOOLS%vsvars32.bat"
  )
)

:End
@echo.

