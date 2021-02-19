using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Dtos
{
    public class CreateCategoryRequestDto
    {
        [Required]
        [Display(Name = "name")]
        public string Name { get; set; }
    }
}
