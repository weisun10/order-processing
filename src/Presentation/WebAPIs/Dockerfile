# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY . ./
WORKDIR /app/Presentation/WebAPIs

RUN dotnet restore
RUN dotnet publish -c Release -o /out

# Use the official .NET runtime image for the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /out .

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "WebAPIs.dll"]
