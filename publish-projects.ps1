[CmdletBinding()]
Param (
    [Parameter(Mandatory=$true)][string] $OutPath = "publish",
    [Parameter(Mandatory=$true)][string] $Version = "0.0.1"
)

$projects_to_publish = Get-ChildItem *.csproj -Recurse | ForEach-Object {
    $p = [xml](Get-Content $_);
    if($p.Project.sdk -match 'Worker$|Web$'){ 
        New-Object psobject -Property @{ 
            Project = $_|Resolve-Path -Relative
            Name=$_.BaseName 
            } 
    }
} 


$projects_to_publish | ForEach-Object { 
    $path = Join-Path $OutPath $_.Name
    dotnet publish $_.Project  -o $path  -r win10-x64 -c Release -v n /p:Version="$Version"
}