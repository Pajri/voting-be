using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Constants
{
    public class Constant
    {
        public const int GENDER_MALE = 1;
        public const int GENDER_FEMALE = 2;

        public const string ROLE_ADMIN = "admin";
        public const string ROLE_CLIENT = "client";

        public const string REGEX_EMAIL = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        public const string REGEX_EMAIL_ERROR = @"Invalid email";

        public const string REGEX_PASSWORD = @"^[a-zA-Z0-9]{8,}$";
        public const string REGEX_PASSWORD_ERROR = @"Password must be a combination of upper case, lower case, number, and at least 8 characters long";

    }
}
