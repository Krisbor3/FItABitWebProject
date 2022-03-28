using System.ComponentModel.DataAnnotations;

namespace FitABit.Infrastructure.Models
{
    public class Instructor
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        [StringLength(60)]
        public string Description { get; set; }

        [Required]
        [StringLength(200)]
        public string PictureUrl { get; set; }
    }
}
