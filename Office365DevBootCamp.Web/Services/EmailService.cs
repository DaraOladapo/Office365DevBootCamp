using AdaptiveCards.Rendering.Html;
using Office365DevBootCamp.Web.Models;
using Office365DevBootCamp.Web.Models.Data;
using PostmarkDotNet;
using System.Threading.Tasks;

namespace Office365DevBootCamp.Web.Services
{
    public static class EmailService
    {
        private static string _EmailAPIKey = Startup.EmailAPIKey;

        public static async Task<EmailResponse> SendRegistrationEmail(Attendee _Attendee, HtmlTag html)
        {
            var message = new PostmarkMessage()
            {
                To = _Attendee.EmailAddress,
                From = "events@windforcecorp.com",
                TrackOpens = true,
                Subject = "Windforce Developer Conference Registration",
                HtmlBody = html.Text,
                Tag = "Windforce Developer Conference Registration",
            };
            var client = new PostmarkClient(_EmailAPIKey);
            var sendResult = await client.SendMessageAsync(message);
            EmailResponse EmailSendResponse = EmailResponse.Failure;
            if (sendResult.Status == PostmarkStatus.Success) { EmailSendResponse = EmailResponse.Success; }
            else { EmailSendResponse = EmailResponse.Failure; }
            return EmailSendResponse;
        }
        public static async Task<EmailResponse> SendConfirmationEmail(Attendee _Attendee, HtmlTag html)
        {
            var message = new PostmarkMessage()
            {
                To = _Attendee.EmailAddress,
                From = "events@windforcecorp.com",
                TrackOpens = true,
                Subject = "Windforce Developer Conference Confirmation",
                HtmlBody = html.Text,
                Tag = "Windforce Developer Conference Confirmation",
            };
            var client = new PostmarkClient(_EmailAPIKey);
            var sendResult = await client.SendMessageAsync(message);
            EmailResponse EmailSendResponse = EmailResponse.Failure;
            if (sendResult.Status == PostmarkStatus.Success) { EmailSendResponse = EmailResponse.Success; }
            else { EmailSendResponse = EmailResponse.Failure; }
            return EmailSendResponse;
        }
        public static async Task<EmailResponse> SendCancellationEmail(Attendee _Attendee, HtmlTag html)
        {
            var message = new PostmarkMessage()
            {
                To = _Attendee.EmailAddress,
                From = "events@windforcecorp.com",
                TrackOpens = true,
                Subject = "Windforce Developer Conference Cancellation",
                HtmlBody = html.Text,
                Tag = "Windforce Developer Conference Cancellation",
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
