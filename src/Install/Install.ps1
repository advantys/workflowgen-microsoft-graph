if (Test-Path "$PSScriptRoot\config.json"){
  $settings = (Get-Content "$PSScriptRoot\config.json" -Raw) | ConvertFrom-Json
}
else{
  Write-Host "The configuration file config.json was not found in the present working directory."
}

#Checking parameter validity
Write-Host $settings.WebAppPath

if($settings.WebAppPath -eq $null -Or $settings.WebAppPath -eq ""){
    $message = "Please specify a 'WebAppPath' parameter."
    Write-Host $message
    exit 1
}
if($settings.ServicesPath -eq $null -Or $settings.ServicesPath -eq ""){
    $message = "Please specify a 'ServicesPath' parameter in config.json."
    Write-Host $message
    exit 1
}

if(Test-Path $settings.WebAppPath)
{
    $message = "WebAppPath valid"
    Write-Host $message

    if(Test-Path $settings.ServicesPath)
    {
        $message = "ServicesPath valid"
        Write-Host $message
        LaunchMsGraphDownload

        $webPath = $settings.WebAppPath
        $servicePath = $settings.ServicesPath
        #Copy WorkflowGenMsGraph dll
        Copy-Item "$PSScriptRoot\Advantys.Workflow.Applications.MicrosoftGraph.dll" -Destination "$webPath\bin"
        Copy-Item "$PSScriptRoot\Advantys.Workflow.Applications.MicrosoftGraph.dll" -Destination "$webPath\ws\bin"
        Copy-Item "$PSScriptRoot\Advantys.Workflow.Applications.MicrosoftGraph.dll" -Destination "$servicePath\bin"
    }
    else
    {
        $message = "ServicesPath not valid, folder not found. Please correct it in config.json"
        Write-Host $message
        exit 1
    }
}
else
{
    $message = "WebAppPath not valid, folder not found. Please correct it in config.json"
    Write-Host $message
    exit 1
}

function LaunchMsGraphDownload
{
    $url = "https://dist.nuget.org/win-x86-commandline/v4.8.1/nuget.exe"
    $output = "$PSScriptRoot\nuget.exe"
    $workingFolder = "$PSScriptRoot\workingFolder"
    $webPath = $settings.WebAppPath
    $servicePath = $settings.ServicesPath


    Invoke-WebRequest -Uri $url -OutFile $output


    #Get Microsoft-Graph Package
    $command = "$PSScriptRoot\nuget.exe install Microsoft.Graph -Version 1.10.0 -OutputDirectory $workingFolder"

    iex $command

    Copy-Item "$workingFolder\Microsoft.Graph.1.10.0\lib\net45\Microsoft.Graph.dll" -Destination "$webPath\bin"
    Copy-Item "$workingFolder\Microsoft.Graph.1.10.0\lib\net45\Microsoft.Graph.dll" -Destination "$webPath\ws\bin"
    Copy-Item "$workingFolder\Microsoft.Graph.1.10.0\lib\net45\Microsoft.Graph.dll" -Destination "$servicePath\bin"

    Copy-Item "$workingFolder\Microsoft.Graph.Core.1.10.0\lib\net45\Microsoft.Graph.Core.dll" -Destination "$webPath\bin"
    Copy-Item "$workingFolder\Microsoft.Graph.Core.1.10.0\lib\net45\Microsoft.Graph.Core.dll" -Destination "$webPath\ws\bin"
    Copy-Item "$workingFolder\Microsoft.Graph.Core.1.10.0\lib\net45\Microsoft.Graph.Core.dll" -Destination "$servicePath\bin"


    #Get Microsoft-Graph-identity Package
    $command = "$PSScriptRoot\nuget.exe install Microsoft.Identity.Client -Version 2.0.0-preview -OutputDirectory $workingFolder"

    iex $command
    Copy-Item "$workingFolder\Microsoft.Identity.Client.2.0.0-preview\lib\net45\Microsoft.Identity.Client.dll" -Destination "$webPath\bin"
    Copy-Item "$workingFolder\Microsoft.Identity.Client.2.0.0-preview\lib\net45\Microsoft.Identity.Client.dll" -Destination "$webPath\ws\bin"
    Copy-Item "$workingFolder\Microsoft.Identity.Client.2.0.0-preview\lib\net45\Microsoft.Identity.Client.dll" -Destination "$servicePath\bin"

    Remove-Item $output
    Remove-Item $workingFolder -Force -Recurse
}