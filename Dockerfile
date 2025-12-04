# ETAPA 1: Restaurar dependencias y compilar					
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /OmegaAmlApp

EXPOSE 80
EXPOSE 5025

#COPY PROJECT FILES

COPY ["CINLAT.WebApiTest/*.csproj", "CINLAT.WebApiTest/"]

#RESTORE LIBRARIES NEEDED
RUN dotnet restore "CINLAT.WebApiTest/CINLAT.WebApiTest.csproj"

#COPY EVERYTHING ELSE
COPY . . 

RUN echo "- DESPUES DE COPIAR TODO -"
RUN ls -la ./CINLAT.WebApiTest
RUN cat ./CINLAT.WebApiTest/Program.cs   #<---- TEST si existe

#UBICARME EN RUTA RELATIVA Y COMPILAR PROYECTO PARA DEPOSITARLO EN CARPETA out
WORKDIR "/OmegaAmlApp/CINLAT.WebApiTest"
RUN dotnet publish "CINLAT.WebApiTest.csproj" -c Release -o out

# ETAPA 2: Construir imagen
FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR "/OmegaAmlApp/CINLAT.WebApiTest"
#RUN ls -la ./    TEST que archivos hay?

#COPIAR ARCHIVOS Y EJECUTAR EL ENDPOINT
COPY --from=build /OmegaAmlApp/CINLAT.WebApiTest/out .
ENTRYPOINT ["dotnet","CINLAT.WebApiTest.dll"]


