# Outlook

- [Send mail](#send-mail)
- [Schedule a meeting](#schedule-a-meeting)

## Send mail

### Permissions required for your MSGraph Apps

To allow workflowGen to create and send mail, you need to enable the following permissions:

- `Mail.ReadWrite`

- `Mail.Send`

### Create the GRAPH_SEND_MAIL application

First, you need to create an application in WorkflowGen with a configuration like the following:

- **Name**: `GRAPH_SEND_MAIL`

- **Description**: `Send an email with Microsoft Graph`

- **Type**: `assembly`

- **Assembly full name or path**: `Advantys.Workflow.Applications.MicrosoftGraph`

- **Class full name**: `Advantys.Workflow.Applications.MicrosoftGraph.Graph`

- **Method**: `SendMail`

You can import the following configuration to create the application automatically (make sure your username is included in `ProcessesRuntimeWebServiceAllowedUsers` in the `web.config`).

1. Go to `http://[YOUR_SITE]/wfgen/ws/processesruntime.asmx?op=CreateWorkflowApplication`

2. Copy and paste the following configuration to `applicationDefinition`:

```xml
 <Application xmlStructureRevisision="1.0"> <Name>GRAPH_SEND_MAIL</Name> <Description>Send an email with microsoft graph</Description> <Type>ASSEMBLY</Type> <Method>SendMail</Method> <Active>Y</Active> <Assembly>Advantys.Workflow.Applications.MicrosoftGraph</Assembly> <Class>Advantys.Workflow.Applications.MicrosoftGraph.Graph</Class> <Parameters> <Parameter> <Name>subject</Name> <Description>subject</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>content</Name> <Description>content</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>toRecipients</Name> <Description>toRecipients</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>ccRecipients</Name> <Description>ccRecipients</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>bccRecipients</Name> <Description>bccRecipients</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>attachment</Name> <Description>attachment</Description> <DataType>FILE</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>RETURN_VALUE</Name> <Description>RETURN_VALUE</Description> <DataType>TEXT</DataType> <Direction>OUT</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> </Parameters> </Application>
```

### Parameters
| Name | Description | Type |
| --- | --- |---|
|`subject`|Subject of the email|Required|
|`content`|Content of the email (HTML or text)|Required|
|`toRecipients`|List of principal recipients separated by semicolons (`;`)| Required |
|`ccRecipients`|List of CC recipients separated by semicolons (`;`)| Optional |
|`bccRecipients`|List of BCC recipients separated by semicolons (`;`)| Optional |
|`attachment`|Attachment to send with email|Optional|

### Example

You can use the [`SEND_MAIL_OFFICE`](https://github.com/advantys/workflowgen-microsoft-graph/blob/master/processes/Outlook/SEND_MAIL_OFFICEv1.xml) process as an example.

## Schedule a meeting

### Permissions required for your MSGraph Apps

To allow WorkflowGen to schedule meetings, you need to enable the `Calendar.ReadWrite` permission.


### Create the GRAPH_SCHEDULE_MEETING application

Create an application in WorkflowGen with configuration as follows:

- **Name**: `GRAPH_SCHEDULE_MEETING`

- **Description**: `Schedule a meeting with Microsoft Graph`

- **Type**: `assembly`

- **Assembly full name or path**: `Advantys.Workflow.Applications.MicrosoftGraph`

- **Class full name**: `Advantys.Workflow.Applications.MicrosoftGraph.Graph`

- **Method**: `ScheduleMeeting`

You can import the following configuration to create the application automatically (make sure your username is included in `ProcessesRuntimeWebServiceAllowedUsers` in the `web.config`):

1. Go to `http://[YOUR_SITE]/wfgen/ws/processesruntime.asmx?op=CreateWorkflowApplication`

2. Copy and paste the following configuration to `applicationDefinition`:

```xml
 <Application xmlStructureRevisision="1.0"> <Name>GRAPH_SCHEDULE_MEETING</Name> <Description>Schedule a meeting with microsoft graph</Description> <Type>ASSEMBLY</Type> <Method>ScheduleMeeting</Method> <Active>Y</Active> <Assembly>Advantys.Workflow.Applications.MicrosoftGraph</Assembly> <Class>Advantys.Workflow.Applications.MicrosoftGraph.Graph</Class> <Parameters> <Parameter> <Name>attendees</Name> <Description>attendees</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>subject</Name> <Description>subject</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>content</Name> <Description>content</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>location</Name> <Description>location</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>startDate</Name> <Description>startDate</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>startTime</Name> <Description>startTime</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>endDate</Name> <Description>endDate</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>endTime</Name> <Description>endTime</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>dateFormat</Name> <Description>dateFormat</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>timeFormat</Name> <Description>timeFormat</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>culture</Name> <Description>culture</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>wfgTimeZone</Name> <Description>wfgTimeZone</Description> <DataType>TEXT</DataType> <Direction>IN</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> <Parameter> <Name>RETURN_VALUE</Name> <Description>RETURN_VALUE</Description> <DataType>TEXT</DataType> <Direction>OUT</Direction> <Required>Y</Required> <Default>N</Default> </Parameter> </Parameters> </Application>
```

### Parameters
| Name | Description | Type |
| --- | --- |---|
| `attendees` | Attendees of the meeting | Required |
| `subject` | Subject of the meeting | Required |
| `content` | Content of the meeting | Optional |
| `location` | Location of the meeting | Optional |
| `startDate` | Start date of the meeting (e.g. `01-09-2018`) | Required |
| `endDate` | End date of the meeting (e.g. `01-09-2018`) | Required |
| `startTime` | Start time of the meeting (e.g. `10:00`) | Required |
| `endTime` | End time of the meeting (e.g. `11:00`) | Required |
| `dateFormat` | Date format, which needs to correspond to `startDate` and `endDate` (e.g. `dd-MM-yyyy`) | Required |
| `timeFormat` | Time format, which needs to correspond to `startTime` and `endTime` (e.g. `HH:mm`) | Required |
| `culture` | Culture used by the current user; you can get this value using the `System.Language` macro | Required |
| `wfgTimeZone` | Current user's time zone; you need to add code in your form | Required |

#### Get current user time zone

1. Add read-only field to the form (e.g. the current time zone with `TIMEZONE` ID).

2. Add the following code in code-behind:

```csharp
protected void Page_Load(object sender, EventArgs e)
{
    base.Page_Load(sender, e);
    GetCurrentTimeZone();
}

private void GetCurrentTimeZone()
{
    WorkflowGen.My.Globalization.TimeZoneInformation tz = new WorkflowGen.My.Globalization.TimeZoneInformation(this.CurrentTimeZoneInfo);
    REQUEST_TIME_ZONE.Text = tz.DisplayName;
}
```

### Example

You can use the [`SCHEDULE_MEETING_OFFICE`](https://github.com/advantys/workflowgen-microsoft-graph/blob/master/processes/Outlook/SCHEDULE_MEETING_OFFICEv1.xml) process as an example.
