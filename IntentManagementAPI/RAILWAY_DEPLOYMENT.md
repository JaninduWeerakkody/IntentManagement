# Railway Deployment Guide for IntentManagementAPI

## Prerequisites

1. **Railway Account**: Sign up at [railway.app](https://railway.app)
2. **GitHub Account**: Your code should be in a GitHub repository
3. **Railway CLI** (optional): Install via `npm install -g @railway/cli`

## Deployment Steps

### Method 1: Deploy via Railway Dashboard (Recommended)

1. **Connect Your Repository**:
   - Go to [railway.app](https://railway.app)
   - Click "New Project"
   - Select "Deploy from GitHub repo"
   - Choose your repository

2. **Configure the Service**:
   - Railway will automatically detect it's a .NET project
   - The `nixpacks.toml` file will configure the build process
   - Railway will use Nixpacks to build your application

3. **Set Environment Variables** (Optional):
   - Go to your project's "Variables" tab
   - Add any custom environment variables if needed:
     ```
     ASPNETCORE_ENVIRONMENT=Production
     JWT__KEY=your-secure-jwt-key
     ```

4. **Deploy**:
   - Railway will automatically build and deploy your application
   - Monitor the build logs for any issues

### Method 2: Deploy via Railway CLI

1. **Install Railway CLI**:
   ```bash
   npm install -g @railway/cli
   ```

2. **Login to Railway**:
   ```bash
   railway login
   ```

3. **Initialize Railway Project**:
   ```bash
   railway init
   ```

4. **Deploy**:
   ```bash
   railway up
   ```

## Configuration Details

### Build Configuration
- **Builder**: Nixpacks (configured in `nixpacks.toml`)
- **.NET Version**: 8.0 SDK
- **Build Process**: 
  - `dotnet restore IntentManagementAPI.csproj`
  - `dotnet build IntentManagementAPI.csproj -c Release -o /app/build`

### Port Configuration
- The application is configured to run on port `8921`
- Railway will automatically assign a public URL
- The configuration files set the correct port binding

### Database
- Currently using SQLite (`IntentManagementAPI.db`)
- For production, consider using Railway's PostgreSQL service
- The database file will be created automatically on first run

### Environment Variables
The following environment variables are automatically set:
- `ASPNETCORE_ENVIRONMENT=Production`
- `ASPNETCORE_URLS=http://0.0.0.0:8921`

## Post-Deployment

1. **Access Your API**:
   - Railway will provide a public URL (e.g., `https://your-app.railway.app`)
   - Swagger UI will be available at `/swagger`

2. **Health Check**:
   - Railway will monitor your app using the `/swagger` endpoint
   - The health check timeout is set to 300 seconds

3. **Logs**:
   - View logs in the Railway dashboard
   - Use `railway logs` if using CLI

## Troubleshooting

### Common Issues

1. **Build Failures**:
   - ✅ **FIXED**: The `nixpacks.toml` file now properly specifies the project file
   - Check that all dependencies are properly specified in `.csproj`
   - Ensure the project file is in the root directory

2. **Port Issues**:
   - Verify the port in configuration files matches your application
   - Check that Kestrel is configured to listen on `0.0.0.0:8921`

3. **Database Issues**:
   - SQLite file will be created automatically
   - Ensure the application has write permissions

### Recent Fixes Applied

1. **Build Issue Resolution**:
   - Created `nixpacks.toml` with proper .NET 8.0 SDK configuration
   - Specified exact project file path in build commands
   - Updated start command to use the correct build output path

2. **Multiple Configuration Formats**:
   - `railway.toml` - TOML format
   - `railway.json` - JSON format  
   - `railway.yaml` - YAML format
   - `railway` - Simple format
   - `.nixpacks` - Alternative Nixpacks format

### Updating Your Application

1. **Automatic Deployments**:
   - Push changes to your GitHub repository
   - Railway will automatically redeploy

2. **Manual Deployments**:
   ```bash
   railway up
   ```

## Security Considerations

1. **JWT Key**: Consider using Railway's environment variables for the JWT secret
2. **HTTPS**: Railway automatically provides HTTPS
3. **CORS**: Configure CORS settings if needed for your frontend

## Monitoring

- Use Railway's built-in monitoring dashboard
- Set up alerts for downtime
- Monitor resource usage (CPU, memory, disk)

## Cost Optimization

- Railway offers a free tier with limitations
- Monitor your usage in the Railway dashboard
- Consider upgrading if you exceed free tier limits

## Files Created for Railway Deployment

- `nixpacks.toml` - Main build configuration
- `railway.toml` - Railway deployment settings
- `railway.json` - Alternative JSON configuration
- `railway.yaml` - Alternative YAML configuration
- `railway` - Simple configuration
- `.nixpacks` - Alternative Nixpacks configuration
- `.railwayignore` - Files to exclude from build
- `appsettings.Production.json` - Production environment settings 