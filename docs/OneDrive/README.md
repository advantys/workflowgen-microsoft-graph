# OneDrive

- [Upload a file](#upload-a-file)

## Upload a file

### Permissions required for your MSGraph Apps

To allow WorkflowGen to upload files, you have to enable the following permissions:

- `Group.ReadWrite.All`

- `File.ReadWrite.All`

- `Sites.ReadWrite.All`

### Create the GRAPH_UPLOAD_FILE_ONEDRIVE application

First, you need to create an application in WorkflowGen with a configuration like the following:

- **Name**: `GRAPH_UPLOAD_FILE_ONEDRIVE`

- **Description**: `Upload file to OneDrive group's folder`

- **Type**: `assembly`

- **Assembly full name or path**: `Advantys.Workflow.Applications.MicrosoftGraph`

- **Class full name**: `Advantys.Workflow.Applications.MicrosoftGraph.Graph`

- **Method**: `UploadFileOneDrive`

You can import the following configuration to create the application automatically (make sure your username is included in `ProcessesRuntimeWebServiceAllowedUsers` in the `web.config` file).

1. Go to `http://[YOUR_SITE]/wfgen/ws/processesruntime.asmx?op=CreateWorkflowApplication`.

2. Copy and paste the following configuration to `applicationDefinition`:

    ```xml
    <Application xmlStructureRevisision="1.0"> <Name>GRAPH_UPLOAD_FILE_ONEDRIVE</Name> <Description>Upload file to onedrive group&amp;#39;s folder</Description> <Type>ASSEMBLY</Type> <Method>UploadFileOneDrive</Method> <Active>Y</Active> <Assembly>Advantys.Workflow.Applications.MicrosoftGraph</Assembly> <Class>Advantys.Workflow.Applications.MicrosoftGraph.Graph</Class> <Parameters> <Parameter> <Name>groupId</Name> <Description>groupId</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>folderId</Name> <Description>folderId</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>requestNumber</Name> <Description>requestNumber</Description> <DataType>NUMERIC</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>file</Name> <Description>file</Description> <DataType>FILE</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>RETURN_VALUE</Name> <Description>RETURN_VALUE</Description> <DataType>TEXT</DataType> <Direction>OUT</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> </Parameters> </Application>
    ```

### Parameters
| Name | Description | Type |
| --- | --- |---|
|`file`|File to upload | Required|
|`folderId`|Destination folder ID|Required|
|`groupId`|Destination OneDrive group ID| Required |
|`requestNumber`|Request number (`CURRENT_REQUEST`)| Required |

### Example

You can use the [`ARCHIVING_FILE_ONEDRIVE`](https://github.com/advantys/workflowgen-microsoft-graph/blob/master/processes/OneDrive/ARCHIVING_PROCESS_FILEv1.xml) process as an example. You'll need to use the `BU_ONEDRIVE` global list and create various groups and folders in your OneDrive.

In our example, we have three Business Units represented by groups in OneDrive; in each Business Unit, we have three domains represented by folders.

```
|-- Aerospace
    |-- Aviation
    |-- Defense
    |-- Space
|-- Telecom
    |-- IoT
    |-- Metering
    |-- Telecom
|-- Transport
    |-- Marine
    |-- Racing
    |-- Rail
```

If you want to use the process example, perform the following steps.

1. Go to your Office portal `https://www.office.com` and connect with an Office 365 Administrator account.

2. Go to OneDrive.

3. Under `Recycle bin` you'll see your domain name. Click on `+` to create a group and repeat this as many times as you need.

4. Create folders in your OneDrive groups.

5. In the `BU_ONEDRIVE` global list, update the `IdBuGroup` and `IdDomainFolder` columns.

6. Go to `https://developer.microsoft.com/en-us/graph/graph-explore`.

7. To get `IdBuGroup`, execute the following query as a `GET` request.
    ```
    https://graph.microsoft.com/v1.0/groups
    ```
8. Copy and paste the ID returned in the `IdBuGroup` column

9. To get `IdDomainFolder`, execute the following query as `GET` request:
    ```
    https://graph.microsoft.com/v1.0/groups/[ID_GROUP}/drive/root/children
    ```

10. Copy and paste the ID returned in the `IdDomainFolder` column






