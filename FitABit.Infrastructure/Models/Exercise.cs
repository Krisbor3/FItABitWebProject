using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Infrastructure.Models
{
    public class Exercise
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? RestTime { get; set; }

        [ForeignKey(nameof(Detail))]
        public Guid DetailId { get; set; }
        public Detail Detail { get; set; }
        
    }
}
