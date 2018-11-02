using Microsoft.Graph;
using System;
using System.Collections.Generic;
using WorkflowGen.My.Data;

namespace Advantys.Workflow.Applications.MicrosoftGraph
{
    public partial class Graph
    {
        public static string SendMail(string subject, string content, string toRecipients,
                                          string ccRecipients, string bccRecipients,
                                          WorkflowFile attachment)
        {
            // Check parameters
            if (string.IsNullOrEmpty(subject))
                return "The subject is required";
            if (string.IsNullOrEmpty(content))
                return "The content is required";
            if (string.IsNullOrEmpty(toRecipients))
                return "Recipients are required";

            try
            {
                // Prepare the recipient list
                string[] splitter = { ";" };
                var splitToRecipientsString = toRecipients.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                var splitCcRecipientsString = ccRecipients.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                var splitBccRecipientsString = bccRecipients.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                List<Recipient> toRecipientList = new List<Recipient>();
                List<Recipient> ccRecipientList = new List<Recipient>();
                List<Recipient> bccRecipientList = new List<Recipient>();

                foreach (string recipient in splitToRecipientsString)
                {
                    toRecipientList.Add(new Recipient { EmailAddress = new EmailAddress { Address = recipient.Trim() } });
                }
                foreach (string recipient in splitCcRecipientsString)
                {
                    ccRecipientList.Add(new Recipient { EmailAddress = new EmailAddress { Address = recipient.Trim() } });
                }
                foreach (string recipient in splitBccRecipientsString)
                {
                    bccRecipientList.Add(new Recipient { EmailAddress = new EmailAddress { Address = recipient.Trim() } });
                }

                MessageAttachmentsCollectionPage attachments = new MessageAttachmentsCollectionPage();

                if (attachment != null)
                {
                    attachments.Add(new FileAttachment
                    {
                        ODataType = "#microsoft.graph.fileAttachment",
                        ContentBytes = attachment.Content,
                        ContentType = attachment.ContentType,
                        Name = attachment.Name
                    });
                }


                var email = new Message
                {
                    Body = new ItemBody
                    {
                        Content = content,
                        ContentType = BodyType.Html,
                    },
                    Subject = subject,
                    ToRecipients = toRecipientList,
                    CcRecipients = ccRecipientList,
                    BccRecipients = bccRecipientList,
                    Attachments = attachments
                };

                try
                {
                    GraphClient.Users[ServiceAccountId].SendMail(email, true).Request().PostAsync().Wait();

                    return Success;
                }
                catch (ServiceException exception)
                {
                    Log("[SendMail] Error - We could not send the message: " + exception.Error == null ? "No error message returned." : exception.Error.Message);
                    return "We could not send the message: " + exception.Error == null ? "No error message returned." : exception.Error.Message;
                }
            }
            catch (Exception e)
            {
                Log("[SendMail] Error - We could not send the message: " + e.Message + " - InnerException : " + e.InnerException);
                return "We could not send the message: " + e.Message;
            }

        }

        public static string ScheduleMeeting(string attendees, string subject, string content,
                                            string location, string startDate, string startTime,
                                            string endDate, string endTime, string dateFormat,
                                            string timeFormat, string culture, string wfgTimeZone)
        {
            // Check parameters
            if (string.IsNullOrEmpty(attendees))
                return "Attendees are required";
            if (string.IsNullOrEmpty(subject))
                return "The subject is required";
            if (string.IsNullOrEmpty(startDate))
                return "The start date is required";
            if (string.IsNullOrEmpty(startTime))
                return "The start time is required";
            if (string.IsNullOrEmpty(endDate))
                return "The end date is required";
            if (string.IsNullOrEmpty(endTime))
                return "The end time is required";
            if (string.IsNullOrEmpty(dateFormat))
                return "The date format is required";
            if (string.IsNullOrEmpty(timeFormat))
                return "The time format is required";
            if (string.IsNullOrEmpty(culture))
                return "The culture is required";
            if (string.IsNullOrEmpty(wfgTimeZone))
                return "The WorkflowGen timezone is required";

            try
            {
                // Delete specific characters form WFG's time zone to conform to TimeZoneCustom.resx
                wfgTimeZone = wfgTimeZone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(":", "")
                        .Replace(" ", "").Replace(",", "").Replace(".", "").Replace("'", "");
                string timeZoneCustom = TimeZoneCustom.ResourceManager.GetString(wfgTimeZone);

                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(culture);

                DateTime start = DateTime.ParseExact(startDate + " " + startTime, dateFormat + " " + timeFormat,
                                           cultureInfo);

                DateTime end = DateTime.ParseExact(endDate + " " + endTime, dateFormat + " " + timeFormat,
                                           cultureInfo);

                // List of attendees
                List<Attendee> attendeesLst = new List<Attendee>();
                foreach (var attendee in attendees.Split(';'))
                {
                    attendeesLst.Add(new Attendee
                    {

                        EmailAddress = new EmailAddress
                        {
                            Address = attendee
                        },
                        Type = AttendeeType.Required
                    });
                }

                // Event body
                ItemBody body = new ItemBody
                {
                    Content = content,
                    ContentType = BodyType.Text
                };

                // Start and end date
                DateTimeTimeZone startEvent = new DateTimeTimeZone
                {
                    DateTime = start.ToString("o"),
                    TimeZone = timeZoneCustom
                };
                DateTimeTimeZone endEvent = new DateTimeTimeZone
                {
                    DateTime = end.ToString("o"),
                    TimeZone = timeZoneCustom
                };

                // Event location
                Location eventLocation = new Location
                {
                    DisplayName = location,
                };

                try
                {

                    GraphClient.Users[ServiceAccountId].Events.Request().AddAsync(new Event
                    {
                        Subject = subject,
                        Location = eventLocation,
                        Attendees = attendeesLst,
                        Body = body,
                        Start = startEvent,
                        End = endEvent
                    }).Wait();
                    return Success;
                }
                catch (ServiceException exception)
                {
                    Log("[ScheduleMeeting] Error - We could not send the meeting: " + exception.Error == null ? "No error message returned." : exception.Error.Message);
                    return "We could not send the meeting: " + exception.Error == null ? "No error message returned." : exception.Error.Message;
                }
            }
            catch (Exception e)
            {
                Log("[ScheduleMeeting] Error - " + e.Message);
                return e.Message;
            }

        }
    }
}
