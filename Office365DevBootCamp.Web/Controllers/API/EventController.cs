using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Office365DevBootCamp.Web.Data;
using Office365DevBootCamp.Web.Models;
using Office365DevBootCamp.Web.Models.Binding;
using System;
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
            var _Attendee = _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();
            await Services.EmailService.SendRegistrationEmail(attendee);

            return Created($"http://events.windforcecorp.com/{_Attendee.Entity.AttendeeID.ToString()}", _Attendee);
        }
    }
}
