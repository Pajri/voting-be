using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Models
{
    public class Voting
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int VotersCount { get; set; }
        public DateTime DueDate { get; set; }
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
        public virtual ICollection<User> Voters { get; set; }

    }
}
