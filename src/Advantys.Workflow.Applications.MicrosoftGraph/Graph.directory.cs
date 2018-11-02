using Microsoft.Graph;
using System;

namespace Advantys.Workflow.Applications.MicrosoftGraph
{
    public partial class Graph
    {

        public static string CreateOfficeUser(string lastName, string firstName, string password, string email,
                                        string mobilePhone, string country, string city, string department,
                                        string postalCode, string jobTitle, string officeLocation)
        {
            // Check parameters
            if (string.IsNullOrEmpty(lastName))
                return "The last name is required";
            if (string.IsNullOrEmpty(firstName))
                return "The first name is required";
            if (string.IsNullOrEmpty(password))
                return "The password is required";
            if (string.IsNullOrEmpty(email))
                return "The email is required";

            try
            {
                GraphClient.Users.Request().AddAsync(new User()
                {
                    //Require
                    AccountEnabled = true,
                    DisplayName = firstName + " " + lastName,
                    MailNickname = firstName + lastName,
                    UserPrincipalName = email,
                    PasswordProfile = new PasswordProfile()
                    {
                        ForceChangePasswordNextSignIn = true,
                        Password = password
                    },
                    //Optional
                    MobilePhone = mobilePhone == "" ? null : mobilePhone,
                    Country = country == "" ? null : country,
                    City = city == "" ? null : city,
                    PostalCode = postalCode == "" ? null : postalCode,
                    Department = department == "" ? null : department,
                    JobTitle = jobTitle == "" ? null : jobTitle,
                    OfficeLocation = officeLocation == "" ? null : officeLocation,
                }).Wait();

                return Success;
            }
            catch (Exception e)
            {
                Log("Adding user not possible - Exception : " + e.Message + " InnerException : " + e.InnerException);
                return "Adding user not possible - " + e.Message;
            }

        }
    }
}
