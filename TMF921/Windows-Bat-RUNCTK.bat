@echo off

copy CHANGE_ME.json DO_NOT_CHANGE\cypress\fixtures\config.json
cd DO_NOT_CHANGE
call npm install
call npm start
call npm run report

REM After the report is generated, modify the file
set reportPath=cypress\reports\cypress\reports\index.html
set modifiedReportPath=..\REPORT.HTML

if exist %reportPath% (
    echo Modifying the report file...
    powershell -Command "(Get-Content %reportPath%).replace('.state={expanded:!1}', '.state={expanded:1}') | Set-Content %reportPath%"
    
    echo Copying modified report to %modifiedReportPath%...
    copy %reportPath% %modifiedReportPath%
) else (
    echo Report file does not exist. Exiting.
)

cd ..
start REPORT.HTML
