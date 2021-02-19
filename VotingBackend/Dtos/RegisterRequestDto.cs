using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Constants;

namespace VotingBackend.Dtos
{
    public class RegisterRequestDto
    {
        [Required]
        [Display(Name = "firstName")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(Constant.REGEX_EMAIL, ErrorMessage = Constant.REGEX_EMAIL_ERROR)]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "password")]
        [RegularExpression(Constant.REGEX_PASSWORD, ErrorMessage = Constant.REGEX_PASSWORD_ERROR)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [Display(Name = "passwordConfirmation")]
        public string PasswordConfirmation { get; set; }

        [Required]
        [Display(Name = "gender")]
        public int Gender { get; set; }

        [Required]
        [Display(Name = "age")]
        public int Age { get; set; }

        public string Role { get; set; }
    }
}
