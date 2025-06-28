#!/bin/bash

# Railway Deployment Script for IntentManagementAPI
# This script helps prepare and deploy your .NET API to Railway

echo "🚂 Railway Deployment Script for IntentManagementAPI"
echo "=================================================="

# Check if Railway CLI is installed
if ! command -v railway &> /dev/null; then
    echo "❌ Railway CLI is not installed."
    echo "📦 Installing Railway CLI..."
    npm install -g @railway/cli
fi

# Check if user is logged in to Railway
if ! railway whoami &> /dev/null; then
    echo "🔐 Please log in to Railway..."
    railway login
fi

# Check if we're in a git repository
if ! git rev-parse --git-dir > /dev/null 2>&1; then
    echo "❌ This is not a git repository."
    echo "📝 Please initialize git and push to GitHub first:"
    echo "   git init"
    echo "   git add ."
    echo "   git commit -m 'Initial commit'"
    echo "   git remote add origin <your-github-repo-url>"
    echo "   git push -u origin main"
    exit 1
fi

# Check if we have the necessary files
echo "🔍 Checking for required files..."

if [ ! -f "Dockerfile" ]; then
    echo "❌ Dockerfile not found!"
    exit 1
fi

if [ ! -f "railway.toml" ]; then
    echo "❌ railway.toml not found!"
    exit 1
fi

if [ ! -f "IntentManagementAPI.csproj" ]; then
    echo "❌ IntentManagementAPI.csproj not found!"
    exit 1
fi

echo "✅ All required files found!"

# Check if we have uncommitted changes
if ! git diff-index --quiet HEAD --; then
    echo "⚠️  You have uncommitted changes."
    echo "📝 Please commit your changes before deploying:"
    echo "   git add ."
    echo "   git commit -m 'Prepare for Railway deployment'"
    echo "   git push"
    exit 1
fi

echo "✅ All changes are committed!"

# Initialize Railway project if not already done
if [ ! -f ".railway/project.json" ]; then
    echo "🚀 Initializing Railway project..."
    railway init
fi

# Deploy to Railway
echo "🚀 Deploying to Railway..."
railway up

echo "✅ Deployment completed!"
echo "🌐 Your API should be available at the URL provided by Railway"
echo "📊 Check the Railway dashboard for logs and monitoring" 