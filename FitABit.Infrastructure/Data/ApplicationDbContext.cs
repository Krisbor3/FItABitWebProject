using FitABit.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitABit.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Program> Programs { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Detail> Details { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}