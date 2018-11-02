using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace Advantys.Workflow.Applications.MicrosoftGraph
{
    public partial class Graph
    {
        private static ConfidentialClientApplication IdentityAppOnlyApp = new ConfidentialClientApplication(ClientId, AuthorityUri, 
                                                                                                            RedirectUriForAppAuth, new ClientCredential(ClientSecret), 
                                                                                                            new TokenCache(), new TokenCache());
        private static string TokenForApp = null;

        private static async Task<string> GetTokenForAppAsync()
        {
            AuthenticationResult authResult;

            authResult = await IdentityAppOnlyApp.AcquireTokenForClientAsync(new string[] { "https://graph.microsoft.com/.default" });
            TokenForApp = authResult.AccessToken;

            return TokenForApp;
        }

        private static GraphServiceClient GetAuthenticatedClientForApp()
        {

            // Create Microsoft Graph client.
            try
            {
                return new GraphServiceClient(
                    "https://graph.microsoft.com/v1.0",
                    new DelegateAuthenticationProvider(
                        async (requestMessage) =>
                        {
                            var token = await GetTokenForAppAsync();
                            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

                        }));
            }

            catch (Exception e)
            {
                throw new Exception("Could not create a graph client: " + e.Message);
            }
        }


    }
}
