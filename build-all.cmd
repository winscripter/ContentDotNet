@REM Copyright (c) 2023-2025, winscripter
@REM This script is only compatible with Windows. Use
@REM the file build-all.py for cross-platform compatibility.

@echo off
echo Building...
where dotnet.exe
if %ERRORLEVEL% == 1 goto :NoDotnet

dotnet.exe build ContentDotNet.sln >NUL
if %ERRORLEVEL% == 0 echo Success.
if %ERRORLEVEL% == 1 echo Failed.

goto :EOF

:NoDotnet
echo .NET is not installed.
goto :EOF
