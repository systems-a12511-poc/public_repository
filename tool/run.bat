SET PYTHONUTF8=1
cd %~dp0
cd ..
sam local start-api --port 3000 --debug  --env-vars "./src/backend/local-env.json" --warm-containers LAZY
pause
