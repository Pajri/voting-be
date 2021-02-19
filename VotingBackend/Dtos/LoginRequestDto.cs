using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VotingBackend.Constants;

namespace VotingBackend.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        [Display(Name = "email")]
        [RegularExpression(Constant.REGEX_EMAIL,ErrorMessage = Constant.REGEX_EMAIL_ERROR)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "password")]
        [RegularExpression(Constant.REGEX_PASSWORD, ErrorMessage = Constant.REGEX_PASSWORD_ERROR)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
