using Microsoft.Graph;
using System;
using System.Configuration;
using System.IO;

namespace Advantys.Workflow.Applications.MicrosoftGraph
{
    public partial class Graph
    { 
        static GraphServiceClient GraphClient => GetAuthenticatedClientForApp();
        static string Tenant => ConfigurationManager.AppSettings["MicrosoftGraphTenant"];
        static string AuthorityUri => "https://login.microsoftonline.com/" + Tenant + "/oauth2/v2.0/token";
        static string RedirectUriForAppAuth => "https://login.microsoftonline.com";
        static string ClientId => ConfigurationManager.AppSettings["MicrosoftGraphClientId"];
        static string ClientSecret => ConfigurationManager.AppSettings["MicrosoftGraphClientSecret"];
        static string ServiceAccountId => ConfigurationManager.AppSettings["MicrosoftGraphServiceAccountId"];
        static string LogFilePath => ConfigurationManager.AppSettings["MicrosoftGraphServiceLogPath"] + DateTime.Now.ToString("dd-MM-yyyy") + "-log.txt";
        const string Success = "ok";
        
        private static void Log(string message)
        {
            using (StreamWriter streamWriter = System.IO.File.AppendText(LogFilePath))
            {
                streamWriter.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + message);
            }
        }

    }
}
