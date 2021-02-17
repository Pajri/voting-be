using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Models
{
    public class Category
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Voting> Votings { get; set; }
    }
}
