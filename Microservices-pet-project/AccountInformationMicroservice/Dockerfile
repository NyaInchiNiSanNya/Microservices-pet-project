FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["AccountInformationMicroservice.API/AccountInformationMicroservice.API.csproj", "AccountInformationMicroservice.API/"]
COPY ["AccountInformationMicroservice.CQRS/AccountInformationMicroservice.CQRS.csproj", "AccountInformationMicroservice.CQRS/"]
COPY ["AccountInformationMicroservice.Data/AccountInformationMicroservice.Data.csproj", "AccountInformationMicroservice.Data/"]
COPY ["AccountInformationMicroservice.DTOs/AccountInformationMicroservice.DTOs.csproj", "AccountInformationMicroservice.DTOs/"]
COPY ["AccountInformationMicroservice.IServices/AccountInformationMicroservice.IServices.csproj", "AccountInformationMicroservice.IServices/"]
COPY ["AccountInformationMicroservice.Services/AccountInformationMicroservice.Services.csproj", "AccountInformationMicroservice.Services/"]
RUN dotnet restore "AccountInformationMicroservice.API/AccountInformationMicroservice.API.csproj"

COPY . .

WORKDIR "/src/AccountInformationMicroservice.API/"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime

COPY --from=publish /app/publish .
RUN ls -l /app

ENTRYPOINT ["dotnet", "AccountInformationMicroservice.API.dll"]