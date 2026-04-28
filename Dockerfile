# Root Dockerfile for Render deployment
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

# Install ffmpeg
RUN apt-get update && apt-get install -y ffmpeg

# Copy backend project
COPY ./backend ./backend
WORKDIR /app/backend

# Publish
RUN dotnet publish -c Release -o out

WORKDIR /app/backend/out

ENTRYPOINT ["dotnet", "API.dll"]
