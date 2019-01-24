using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Csharp_LognReg.Models
{
    public class User
    {
 
        [Key]
        public int Id { get; set; }

        [Display(Name="First Name")]
        [Required(ErrorMessage ="Please enter your first name")]
        [MinLength(3, ErrorMessage ="First Name should be longer than 3 letters")]
        public string Firstname { get; set; }

        [Display(Name="Last Name")]
        [Required(ErrorMessage ="Please enter your last name")]
        [MinLength(3, ErrorMessage ="Last Name should be longer than 3 letters")]
        public string Lastname { get; set; }

        [Display(Name="Email")]
        [Required(ErrorMessage ="Please enter your email address")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        public string Email { get; set; }

        [Display(Name="Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Please enter a password")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password { get; set; }

        public DateTime Createdat { get; set; }

        public DateTime Updatedat { get; set; }

        [NotMapped]
        [Display(Name="Confirm Password")]
        [Compare("Confirm", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }

        public User()
        {
            Createdat = DateTime.Now;
            Updatedat = DateTime.Now;
        }

    }
}