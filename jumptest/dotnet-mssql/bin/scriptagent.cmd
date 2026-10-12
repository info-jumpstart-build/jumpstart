@echo off
start "scriptagent" cmd /k "cd /d "%~dp0\scriptagent" && dotnet scriptagent.dll"
