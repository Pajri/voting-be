using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Dtos
{
    public class CreateVotingRequestDto
    {
        [Required]
        [Display(Name = "name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [Display(Name = "dueDate")]
        public DateTime DueDate { get; set; }

        [Required]
        [Display(Name = "categoryId")]
        public Guid CategoryId { get; set; }
    }
}
