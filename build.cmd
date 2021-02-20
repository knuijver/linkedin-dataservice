@echo off

dotnet build .\linkedin-dataservice.sln

dotnet publish .\linkedin-dataservice.sln -r win10-x64 -c Release -o ./published/