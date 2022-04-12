using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Core.Models
{
    public class DetailViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Sets")]
        public int Sets { get; set; }

        [Required]
        [Display(Name = "Reps")]
        public int Reps { get; set; }

        [Required]
        [Display(Name = "Kilograms")]
        public int Kilograms { get; set; }

        public string ExerciseId { get; set; }
    }
}
