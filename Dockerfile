# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia o arquivo .csproj para o diretório correto no container
COPY GerEsportes-BackEnd/GerEsportes-BackEnd.csproj ./GerEsportes-BackEnd/

# Restaura as dependências
WORKDIR /app/GerEsportes-BackEnd
RUN dotnet restore

# Copia o código-fonte e publica a aplicação
COPY . ./
RUN dotnet publish -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copia os arquivos da etapa de build
COPY --from=build /out ./

# Define a variável de ambiente para o banco de dados
ENV ASPNETCORE_ENVIRONMENT=Production

# Expõe a porta da API
EXPOSE 5000

# Comando de inicialização
ENTRYPOINT ["dotnet", "GerEsportes-BackEnd.dll"]
