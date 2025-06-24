#!/bin/bash

echo "🔧 Building ASP.NET Razor Pages project..."

# Navigate to your project folder (optional if already there)
cd /run/me/t/1DDA6D775041A90F/project/PlantSync

# Restore packages (optional)
dotnet restore

# Build the project
dotnet build
echo "✅ Build complete."
# Optional: Run the project
dotnet run
echo "🚀 Project is running."

#!/bin/bash
dotnet build && dotnet run &

sleep 2
# Open the web application in the default browser
xdg-open http://localhost:5012