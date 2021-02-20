using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Dtos
{
    public class VotingDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int VotersCount { get; set; }
        public DateTime DueDate { get; set; }
    }
}
