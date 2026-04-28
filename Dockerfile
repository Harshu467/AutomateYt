# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy backend project
COPY ./backend ./

# Restore dependencies
RUN dotnet restore

# Publish app
RUN dotnet publish -c Release -o /app/out

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install ffmpeg
RUN apt-get update && apt-get install -y ffmpeg

# Copy built app
COPY --from=build /app/out .

# ⚠️ IMPORTANT: check your project name
ENTRYPOINT ["dotnet", "API.dll"]
