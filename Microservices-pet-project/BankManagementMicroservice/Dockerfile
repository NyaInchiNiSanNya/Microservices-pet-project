FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["AccountManagementMicroservice.API/AccountManagementMicroservice.csproj", "AccountManagementMicroservice.API/"]
COPY ["AccountManagementMicroservice.BusinessLogic/AccountManagementMicroservice.BusinessLogic.csproj", "AccountManagementMicroservice.BusinessLogic/"]
COPY ["AccountManagementMicroservice.CQRS/AccountManagementMicroservice.CQRS.csproj", "AccountManagementMicroservice.CQRS/"]
COPY ["AccountManagementMicroservice.Data/AccountManagementMicroservice.Data.csproj", "AccountManagementMicroservice.Data/"]
COPY ["AccountManagementMicroservice.DTOs/AccountManagementMicroservice.DTOs.csproj", "AccountManagementMicroservice.DTOs/"]
COPY ["AccountManagementMicroservice.IServices/AccountManagementMicroservice.IServices.csproj", "AccountManagementMicroservice.IServices/"]
RUN dotnet restore "AccountManagementMicroservice.API/AccountManagementMicroservice.csproj"

COPY . .

WORKDIR "/src/AccountManagementMicroservice.API/"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime

COPY --from=publish /app/publish .
RUN ls -l /app

ENTRYPOINT ["dotnet", "AccountManagementMicroservice.dll"]