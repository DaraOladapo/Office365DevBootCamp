using Office365DevBootCamp.Web.Models;
using Office365DevBootCamp.Web.Models.Data;
using PostmarkDotNet;
using System.Threading.Tasks;

namespace Office365DevBootCamp.Web.Services
{
    public static class EmailService
    {
        private static string _EmailAPIKey = Startup.EmailAPIKey;

        public static async Task<EmailResponse> SendRegistrationEmail(Attendee _Attendee)
        {
            var message = new PostmarkMessage()
            {
                To = _Attendee.EmailAddress,
                From = "events@windforcecorp.com",
                TrackOpens = true,
                Subject = "A complex email",
                TextBody = "Plain Text Body",
                HtmlBody = "<html><body><img src=\"cid:embed_name.jpg\"/></body></html>",
                Tag = "Windforce Developer Conference Registration",
            };
            var client = new PostmarkClient(_EmailAPIKey);
            var sendResult = await client.SendMessageAsync(message);
            EmailResponse EmailSendResponse = EmailResponse.Failure;
            if (sendResult.Status == PostmarkStatus.Success) { EmailSendResponse = EmailResponse.Success; }
            else { EmailSendResponse = EmailResponse.Failure; }
            return EmailSendResponse;
        }
    }
}
