using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Dtos
{
    public class UpdateCategoryRequestDto
    {
        [Required]
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}
