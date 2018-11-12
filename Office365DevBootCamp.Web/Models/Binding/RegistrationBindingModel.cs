using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Office365DevBootCamp.Web.Models.Binding
{
    public class RegistrationBindingModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
