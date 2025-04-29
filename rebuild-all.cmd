@REM Copyright (c) 2023-2025, winscripter
@REM This script is only compatible with Windows. Use
@REM the file rebuild-all.py for cross-platform compatibility.

@echo off
echo Rebuilding...
where dotnet.exe
if %ERRORLEVEL% == 1 goto :NoDotnet

dotnet.exe build ContentDotNet.sln --no-incremental >NUL
if %ERRORLEVEL% == 0 echo Success.
if %ERRORLEVEL% == 1 echo Failed.

goto :EOF

:NoDotnet
echo .NET is not installed.
goto :EOF
