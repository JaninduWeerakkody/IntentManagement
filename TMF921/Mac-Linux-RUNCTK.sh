::q#!/bin/bash

clear

# Intro
echo "This will run a TMForum API CTK"
echo "In order to be able to run it, you need to have"
echo "NodeJS, NPM, and Newman installed."
echo

cp ./CHANGE_ME.json ./DO_NOT_CHANGE/cypress/fixtures/config.json
cd ./DO_NOT_CHANGE || { echo "Failed to change directory"; exit 1; }
npm install
npm start
npm run report
reportPath="./cypress/reports/cypress/reports/index.html"
modifiedReportPath="../REPORT.HTML"

# Check if the report exists
if [ -f "$reportPath" ]; then
    echo "Modifying the report file..."

    # Modify the report content using sed
    sed -i 's/.state={expanded:!1}/.state={expanded:1}/g' "$reportPath"

    echo "Copying modified report to $modifiedReportPath..."
    cp "$reportPath" "$modifiedReportPath"
else
    echo "Report file does not exist. Exiting."
fi
cd ..
start REPORT.HTML