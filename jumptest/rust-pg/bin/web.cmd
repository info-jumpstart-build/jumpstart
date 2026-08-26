@echo off
start "web" cmd /k "cd /d "%~dp0\web" && npm run dev"
