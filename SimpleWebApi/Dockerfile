#FROM mcr.microsoft.com/dotnet/framework/runtime:4.8
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY /bin/Release/net6.0/publish .

#RUN powershell -Command Add-WindowsFeature "telnet-client"


EXPOSE 7072
EXPOSE 5072

ENTRYPOINT ["dotnet", "SimpleWebApi.dll"]
#ENTRYPOINT ["tail", "-f", "/dev/null"]