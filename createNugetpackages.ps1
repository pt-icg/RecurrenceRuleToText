#REQUIRES -Version 2.0

<#
.SYNOPSIS
    Create a nuget package for defined projects.
.DESCRIPTION
    Create a nuget package for a list of defined projects and push each to our nuget server.
.NOTES
    Requirements   : installed nuget.exe and msbuild.exe
    Copyright 2022 - RECOM GmbH
#>


Set-StrictMode -Version 2.0
$ErrorActionPreference = 'Stop'

# list of projects for which a nuget package should be created
$projectDirs = @('IcgSoftware.RecurrenceRuleToText')

function buildSolution {
    Write-Host 'Building solution..'
    $msBuildExe = ''
    if (Test-Path env:JENKINS_HOME) {
        $msBuildExe = 'C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\bin\msbuild.exe'
    } else {
        $msBuildExe = 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe'
    }
    
    $argumentList = '.\\IcgSoftware.RecurrenceRuleToText.sln /m:4 /nodeReuse:false /p:Configuration=Release /p:Platform="Any CPU" /p:SkipNativeBuilds=true /t:Build /nr:false'
    Start-Process $msBuildExe -ArgumentList $argumentList -NoNewWindow -Wait
}

function createNugetPackages {
    $nugetCLI = 'nuget.exe';
  
    $copyRight = 'We do not want anyone to see our source code, but if for any reason our code is stolen or otherwise obtained, we want to have a license that does not allow disclosure of any kind.'
    
    foreach ($projectDir in $projectDirs){
        Write-Host "Start creating nuget package for project '$projectDir'"
        try
        {
            Push-Location $projectDir
            & $nugetCLI spec -Force  -Verbosity detailed
            $nuspecs = Get-ChildItem -Path .\ -Filter '*.nuspec'
            $nuspec = $nuspecs[0].Name
            if(-not (Test-Path $nuspec)) {throw "Could nod find .nuspec file '$nuspec'"}
            (Get-Content $nuspec) -replace '<releaseNotes>.*',"<releaseNotes>Branch: $branch</releaseNotes>" `
                                  -replace '<tags>.*',"<tags>RECOM_API $branch $projectDir</tags>" `
                                  -replace '<projectUrl>.*',"" `
                                  -replace '<iconUrl>.*',"" `
                                  -replace '<iconUrl>.*',"" `
                                  -replace '<license.*',"" ` `
                                | Set-Content $nuspec
    
            & $nugetCLI pack -Verbosity detailed -exclude '*.md' -Properties Configuration=Release
            $nupkgs = Get-ChildItem -Path .\ -Filter '*.nupkg'
            $nupkg = $nupkgs[0].Name
            #if(-not (Test-Path $nupkg)) {throw "Could nod find .nupkg file '$nupkg'"}
            #& $nugetCLI push ".\$nupkg" -ApiKey J7iGYLGANgdqgPWq8efD   # -Source comes from nuget.config -> defaultPushSource
        }
        finally
        {
            Pop-Location
        }
     }
}

buildSolution;
createNugetPackages