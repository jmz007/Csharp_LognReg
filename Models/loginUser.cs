using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Csharp_LognReg.Models
{
    public class LoginUser
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Email")]
        [Required(ErrorMessage ="Please enter your email address")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        public string Email { get; set; }

        [Display(Name="Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Please enter a password")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password { get; set; }
    }
}