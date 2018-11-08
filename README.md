# WorkflowGen and Microsoft Graph integration samples

## Overview

You can use the [Microsoft Graph API](https://developer.microsoft.com/en-us/graph/docs/concepts/v1-overview) to create integration points between WorkflowGen and Office 365 applications such Outlook, Excel, Teams, OneDrive, SharePoint, and Azure. 

This repository includes the `Advantys.Workflow.Applications.MicrosoftGraph.dll` assembly and the associated Visual Studio project.
This assembly exposes multiple methods that can be used as WorkflowGen applications for each integration feature.

## Contents

`docs`: Workflow applications documentation

`processes`: Process definition file examples

`src`: Visual Studio solution

## Prerequisites

- Office 365 licences (one license for each user that will be accessing Office applications)

- Access to an Azure portal: https://portal.azure.com/

- A service account with an Office 365 licence (such as `workflowgen@YOUR_DOMAIN.com`)

- `Workflowgen.My.dll` 4.2.0 and WorkflowGen Server version 7.10.0 or later


## Installation

### Application registration

1. Create an application on the https://apps.dev.microsoft.com/ site (such as `WorkflowGen`).

2. In **Application Secret** section, generate a new password and save the secret.

3. In **Platforms** section, click **Add platform** and choose **Web**. 

4. Check **Allow implicit Flow** and add `https://login.microsoftonline.com` to the **Redirect URLs**.

5. Each [workflow application](#Workflow-applications-installation) requires specific permissions, so select these in **Application Permissions**, then save the application.

6. To activate permissions, go to the URL `https://login.microsoftonline.com/YOUR_TENANT_ID/adminconsent?client_id=YOUR_CLIENT_ID&redirect_uri=https://login.microsoftonline.com` (replacing `YOUR_TENANT_ID` and `YOUR_CLIENT_ID` with your tenant and client IDs, respectively), and connect using an Administrator account.

### Libraries installation on the WorkflowGen Server

The following components will be installed in the WorkflowGen `\bin` folders (`\wfgen\bin`, `\wfgen\ws\bin`, `..\Program Files\Advantys\WorkflowGen\Service\bin`):

- `Advantys.Workflow.Applications.MicrosoftGraph.dll` 

- `Microsoft.Graph.dll`

- `Microsoft.Graph.Core.dll`

- `Microsoft.Identity.Client.dll`

#### Quick Start

1. Download the last [release pack](https://github.com/advantys/workflowgen-microsoft-graph/releases) to your WorkflowGen server.

2. Edit the `config.json` file, replacing `WebAppPath` and `ServiceAppPath` with your own paths (the default values are already specified).

3. Execute the `Install.ps1` script in PowerShell. 

#### Custom installation

1. Clone the repository.

2. Open the `WorkflowGenMicrosoftGraph.sln` Visual Studio solution.

3. Compile the solution and copy the generated `Advantys.Workflow.Applications.MicrosoftGraph.dll` file to the `src/Install` folder.

4. Edit the `config.json`, replacing `WebAppPath` and `ServiceAppPath` with your own paths (the default values are already specified).

5. Execute the `Install.ps1` script in PowerShell. 
 
### WorkflowGen configuration

Add the following settings to the WorkflowGen `\wfgen\web.config` with your own values:

```xml
<add key="MicrosoftGraphTenant" value="TENANT_ID" />
<add key="MicrosoftGraphClientId" value="CLIENT_ID" />
<add key="MicrosoftGraphClientSecret" value="CLIENT_SECRET" />
<add key="MicrosoftGraphServiceAccountId" value="SERVICE_ACCOUNT_ID" />
<add key="MicrosoftGraphServiceLogPath" value="LOG_PATH" />
```
    
* **`TENANT_ID`**: Your tenant ID, which you can find in Azure AD (Directory ID).

* **`CLIENT_ID`**: The application ID you created earlier.

* **`CLIENT_SECRET`**: The secret password you generated earlier.

* **`SERVICE_ACCOUNT_ID`**: Your service account ID, which you can find in the Azure AD account profile (`object id`).

* **`LOG_PATH`**: The path where the logs will be saved.

## Workflow applications installation

Now that the required components are installed, you can deploy WorkflowGen applications for each integration.

### [Outlook](https://github.com/advantys/workflowgen-microsoft-graph/tree/master/docs/Outlook)

- Send mail 
- Schedule meetings 

### [Azure AD](https://github.com/advantys/workflowgen-microsoft-graph/tree/master/docs/Azure%20AD)

- Add user

### [OneDrive](https://github.com/advantys/workflowgen-microsoft-graph/tree/master/docs/OneDrive)

- Upload a file
