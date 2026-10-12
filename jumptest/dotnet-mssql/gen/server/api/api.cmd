@echo off
start "api" cmd /k "cd /d "%~dp0\api" && dotnet api.dll"
