# Azure

- [Add user](#add-user)

## Add user

### Permissions required for your MSGraph Apps

To allow workflowGen to create user, you need to enable the following permissions:

- `User.ReadWrite.All`

- `Directory.ReadWrite.All`

### Create the GRAPH_CREATE_USER_OFFICE application

First, you need to create an application in WorkflowGen with a configuration like the following:

- **Name**: `GRAPH_CREATE_USER_OFFICE`

- **Description**: `Create office 365 user with MsGraph`

- **Type**: `assembly`

- **Assembly full name or path**: `Advantys.Workflow.Applications.MicrosoftGraph`

- **Class full name**: `Advantys.Workflow.Applications.MicrosoftGraph.Graph`

- **Method**: `CreateOfficeUser`

You can import the following configuration to create the application automatically (make sure your username is included in `ProcessesRuntimeWebServiceAllowedUsers` in the `web.config`).

1. Go to `http://[YOUR_SITE]/wfgen/ws/processesruntime.asmx?op=CreateWorkflowApplication`

2. Copy and paste the following configuration to `applicationDefinition`:

```xml
 <Application xmlStructureRevisision="1.0"> <Name>GRAPH_CREATE_USER_OFFICE</Name> <Description>Create office 365 user with MsGraph</Description> <Type>ASSEMBLY</Type> <Method>CreateOfficeUser</Method> <Active>Y</Active> <Assembly>Advantys.Workflow.Applications.MicrosoftGraph</Assembly> <Class>Advantys.Workflow.Applications.MicrosoftGraph.Graph</Class> <Parameters> <Parameter> <Name>lastName</Name> <Description>lastName</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>firstName</Name> <Description>firstName</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>password</Name> <Description>password</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>email</Name> <Description>email</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>mobilePhone</Name> <Description>mobilePhone</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>country</Name> <Description>country</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>city</Name> <Description>city</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>department</Name> <Description>department</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>postalCode</Name> <Description>postalCode</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>jobTitle</Name> <Description>jobTitle</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>officeLocation</Name> <Description>officeLocation</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>RETURN_VALUE</Name> <Description>RETURN_VALUE</Description> <DataType>TEXT</DataType> <Direction>OUT</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> </Parameters></Application>
```

### Parameters
| Name | Description | Type |
| --- | --- |---|
| `lastname` | User's lastname | Required |
| `firstname` | User's firstname | Required |
| `password` | User's password, at his first connection he needs to change it (ex P@ssw0rd) | Required |
| `email` | User's email (e.g firstname.lastname@YOUR_DOMAIN.com). Your domain needs to be associate with your office 365 subscription | Required |
| `mobilePhone` | User's mobile phone | Optional |	
| `country` | User's country | Optional |	
| `city` | User's city | Optional |	
| `postalCode` | User's postal code | Optional |	
| `department` | User's department | Optional |	
| `jobTitle` | User's job title | Optional |	
| `officeLocation` | User office's location | Optional |

### Example

You can use the [`CREATE_OFFICE365_USER`](https://github.com/advantys/workflowgen-microsoft-graph/blob/develop/processes/OneDrive/ARCHIVING_PROCESS_FILEv1.xml) process as an example.