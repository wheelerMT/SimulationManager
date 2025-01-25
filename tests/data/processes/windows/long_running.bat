@echo off
echo Starting a long-running job...
timeout /t 5 /nobreak >nul
echo Finished long-running job.
exit /b 0
