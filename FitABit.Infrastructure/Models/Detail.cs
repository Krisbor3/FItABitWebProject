using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Infrastructure.Models
{
    public class Detail
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Sets { get; set; }

        public int Reps { get; set; }

        public int Kilograms { get; set; }

        public Guid ExerciseId { get; set; }
    }
}
