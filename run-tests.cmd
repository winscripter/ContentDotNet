@REM Copyright (c) 2023-2025, winscripter
@REM This script is only compatible with Windows. Use
@REM the file run-tests.py for cross-platform compatibility.

@echo off
echo Testing...
where dotnet.exe
if %ERRORLEVEL% == 1 goto :NoDotnet

dotnet.exe test ContentDotNet.sln --no-incremental >NUL
if %ERRORLEVEL% == 0 echo Success.
if %ERRORLEVEL% == 1 echo Failed.

goto :EOF

:NoDotnet
echo .NET is not installed.
goto :EOF
