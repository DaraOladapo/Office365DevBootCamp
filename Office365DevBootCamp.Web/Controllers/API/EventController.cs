using AdaptiveCards;
using AdaptiveCards.Rendering.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Office365DevBootCamp.Web.Data;
using Office365DevBootCamp.Web.Models;
using Office365DevBootCamp.Web.Models.Binding;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Office365DevBootCamp.Web.Controllers.API
{
    [Route("api/event/")]
    public class EventController : Controller
    {
        private AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationBindingModel registrationBindingModel)
        {
            HtmlTag htmlTag = new HtmlTag("", true);
            if (string.IsNullOrWhiteSpace(registrationBindingModel.EmailAddress))
            {
                return BadRequest(new GenericViewModel
                {
                    Message = "Email Address needs to supplied."
                });
            }
            if (string.IsNullOrWhiteSpace(registrationBindingModel.PhoneNumber))
            {
                return BadRequest(new GenericViewModel
                {
                    Message = "Phone Number needs to supplied."
                });
            }
            var attendee = await _context.Attendees.FirstOrDefaultAsync(x => x.EmailAddress == registrationBindingModel.EmailAddress);
            if (attendee != null)
            {
                var response = StatusCode((int)HttpStatusCode.Conflict, new GenericViewModel
                {
                    Message = "Sorry, a user with that email address has been registered."
                });
                return response;
            }
            attendee = new Models.Data.Attendee
            {
                FirstName = registrationBindingModel.FirstName,
                LastName = registrationBindingModel.LastName,
                EmailAddress = registrationBindingModel.EmailAddress,
                RegistrationDate = DateTime.Now
            };
            // Create a card renderer
            AdaptiveCardRenderer renderer = new AdaptiveCardRenderer();
            // For fun, check the schema version this renderer supports
            AdaptiveSchemaVersion schemaVersion = renderer.SupportedSchemaVersion; // 1.0

            // Build a simple card
            // In the real world this would probably be provided as JSON
            var Actions = new List<AdaptiveAction>();
            Actions.Add(
                item: 
            );
            AdaptiveCard card = new AdaptiveCard()
            {
                Body = new List<AdaptiveElement>(){
new AdaptiveContainer(){},
new AdaptiveContainer(){}
                },
                Actions = new List<AdaptiveAction>()
                {

                }
            };

            try
            {
                // Render the card
                RenderedAdaptiveCard renderedCard = renderer.RenderCard(card);

                // Get the output HTML 
                htmlTag = renderedCard.Html;

                // (Optional) Check for any renderer warnings
                // This includes things like an unknown element type found in the card
                // Or the card exceeded the maxmimum number of supported actions, etc
                IList<AdaptiveWarning> warnings = renderedCard.Warnings;
            }
            catch (AdaptiveException ex)
            {
                // Failed rendering
            }

            var _Attendee = _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();
            await Services.EmailService.SendRegistrationEmail(attendee, htmlTag);

            return Created($"http://events.windforcecorp.com/{_Attendee.Entity.AttendeeID.ToString()}", _Attendee);
        }
    }
}
