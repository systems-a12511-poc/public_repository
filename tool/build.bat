SET PYTHONUTF8=1
cd %~dp0
cd ..
sam build --use-container --mount-with WRITE
pause
