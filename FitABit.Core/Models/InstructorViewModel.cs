using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Core.Models
{
    public class InstructorViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name ="Role")]
        public string Role { get; set; }

        
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "PictureUrl")]
        public string PictureUrl { get; set; }
    }
}
