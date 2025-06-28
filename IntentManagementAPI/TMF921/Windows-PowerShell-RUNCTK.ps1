Clear-Host

# Intro messages
Write-Host "This will run a TMForum API CTK"
Write-Host "In order to be able to run it, you need to have"
Write-Host "NodeJS and NPM installed."
Write-Host "Installing npm dependencies..."
Write-Host

Copy-Item -Path ".\CHANGE_ME.json" -Destination ".\DO_NOT_CHANGE\cypress\fixtures\config.json"
Set-Location -Path ".\DO_NOT_CHANGE"

npm install
npm start
npm run report

$reportPath = ".\cypress\reports\cypress\reports\index.html"
$modifiedReportPath = "..\REPORT.HTML"

if (Test-Path $reportPath) {
    Write-Host "Modifying the report file..."
    (Get-Content $reportPath) -replace '.state={expanded:!1}', '.state={expanded:1}' | Set-Content $reportPath

    Write-Host "Copying modified report to $modifiedReportPath..."
    Copy-Item -Path $reportPath -Destination $modifiedReportPath
} else {
    Write-Host "Report file does not exist. Exiting."
}

Set-Location ..
Start-Process REPORT.HTML
