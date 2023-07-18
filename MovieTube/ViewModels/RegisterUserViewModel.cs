using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieTube.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [DisplayName("User Name")]
        [Remote("CheckUserAvilablity","User",ErrorMessage ="User Name Already Exists, Try Another User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote("CheckUserAvilablityByEmail", "User", ErrorMessage = "Email Already Exists, Try Another Email")]
        public string Email { get; set;}

        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password",ErrorMessage ="The Confirm Password Field Doesn't Match The Password Field")]
        public string ConfirmPassword { get; set; }
        

    }
}
