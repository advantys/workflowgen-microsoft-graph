using Microsoft.Graph;
using System;
using System.IO;
using WorkflowGen.My.Data;

namespace Advantys.Workflow.Applications.MicrosoftGraph
{
    public partial class Graph
    {
        public static string UploadFileOneDrive(string groupId, string folderId, double requestNumber, WorkflowFile file)
        {
            // Check parameters
            if (string.IsNullOrEmpty(groupId))
                return "The group id is required";
            if (string.IsNullOrEmpty(folderId))
                return "The folder id is required";
            if (string.IsNullOrEmpty(requestNumber.ToString()))
                return "The request number is required";
            if (file == null)
                return "The file is required";

            try
            {
                // It's not recommended to use MemoryStream for large file
                GraphClient.Groups[groupId].Drive.Items[folderId].ItemWithPath(string.Format("{0}_{1}", requestNumber, file.Name)).
                    Content.Request().PutAsync<DriveItem>(new MemoryStream(file.Content)).Wait();

                return Success;
            }
            catch (Exception e)
            {
                Log("[UploadFileGroup] Error - " + e.Message + " - InnerException : " + e.InnerException);
                return "[UploadFileGroup] Error - " + e.Message;
            }

        }

    }
}
