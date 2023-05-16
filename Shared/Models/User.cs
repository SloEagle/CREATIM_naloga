using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREATIM_naloga.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Not a valid E-mail address.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a phone number.")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public bool Bussiness { get; set; } = false;
        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Tax number must be 8 characters long.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only input digits 0-9")]
        public string TaxNumber { get; set; } = string.Empty;
        public Group Group { get; set; } = new Group();
    }
}
