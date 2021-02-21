using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.ViewModel
{
    public class VotingViewModel
    {
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; }
        public int VotersCount { get; set; }


        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

    }
}
