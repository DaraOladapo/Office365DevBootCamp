using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Office365DevBootCamp.Web.Models.Data
{
    public class Attendee
    {
        [Key]
        public Guid AttendeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public bool IsCanlled { get; set; }
        public DateTime CancellationDate { get; set; }
        public int EventRating { get; set; }
    }
}
