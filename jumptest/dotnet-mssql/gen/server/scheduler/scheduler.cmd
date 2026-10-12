@echo off
start "scheduler" cmd /k "cd /d "%~dp0\scheduler" && dotnet scheduler.dll"
