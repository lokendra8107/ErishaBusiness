using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class LoginDto
    {
        public LoginDto()
        {
            HttpClient = new HttpClient();
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public HttpClient HttpClient { get; set; }
    }

    public class ResetPasswordDTO
    {
        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,15})", ErrorMessage = "Minimum 8 characters, at least one upper letter, one number and one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string token { get; set; }

        public string Code { get; set; }
        public string CurrentPassword { get; set; }
    }

    public class ForgetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnMessage { get; set; }
    }

    public class LoginUserDto
    {
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password), MinLength(5, ErrorMessage = "Password cannot be less than 5 characters."), MaxLength(15, ErrorMessage = "Password cannot be longer than 15 characters.")]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
