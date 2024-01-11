FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
#COPY . ./
COPY ./JWT.GeneratorLib ./JWT.GeneratorLib
COPY ./JWT.Generator ./generator

RUN dotnet restore ./JWT.GeneratorLib/JWT.GeneratorLib.csproj
RUN dotnet publish ./JWT.GeneratorLib/JWT.GeneratorLib.csproj -c Release -o out

# Restore as distinct layers
RUN dotnet restore ./generator/JWT.Generator.csproj
# Build and publish a release
RUN dotnet publish ./generator/JWT.Generator.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "JWT.Generator.dll"]