﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public String Role { get; set; }
        public String RefreshToken { get; set; }

        public virtual ICollection<Voting> Votings { get; set;  }
    }
}
