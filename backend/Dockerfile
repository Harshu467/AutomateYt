FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

# Install ffmpeg for video processing
RUN apt-get update && apt-get install -y ffmpeg

# Copy project files
COPY . .

# Publish the application
RUN dotnet publish -c Release -o out

WORKDIR /app/out

# IMPORTANT: Change API.dll if your project name is different
ENTRYPOINT ["dotnet", "API.dll"]
