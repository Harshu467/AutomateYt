# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy backend
COPY ./backend ./backend

# 🔥 Move into correct project folder
WORKDIR /src/backend/API

# Restore + publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# ---------- RUNTIME ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install ffmpeg
RUN apt-get update && apt-get install -y ffmpeg

# Copy built output
COPY --from=build /app/out .

# Run app
ENTRYPOINT ["dotnet", "API.dll"]
