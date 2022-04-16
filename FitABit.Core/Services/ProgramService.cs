using FitABit.Core.Contracts;
using FitABit.Core.Models;
using FitABit.Infrastructure.Data.Repositories;
using FitABit.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Core.Services
{
    public class ProgramService : IProgramService
    {
        private readonly IApplicationDbRepository repo;

        public ProgramService(IApplicationDbRepository repo)
        {
            this.repo = repo;
        }
        public async Task<IEnumerable<ProgramListViewModel>> GetPrograms()
        {
            var programs = await repo.All<Program>()
                .Where(p => p.Difficulty == "Beginner")
                .Select(p => new ProgramListViewModel
                {
                    Name = p.Name,
                    Difficulty = p.Difficulty,
                    Id = p.Id.ToString(),
                })
                .ToListAsync();

            if (programs.Count == 0)
            {
                throw new ArgumentException("No programs");
            }
            return programs;
        }

        public async Task<IEnumerable<ExerciseViewModel>> GetExercises(string id)
        {
            var exercises = await repo.All<Exercise>()
                .Where(e => e.ProgramId.ToString() == id)
                .Select(e => new ExerciseViewModel
                {
                    Name = e.Name,
                    Id = e.Id.ToString(),
                    RestTime = e.RestTime,
                }).ToListAsync();
            if (exercises.Count == 0)
            {
                throw new ArgumentException("No Exercises found");
            }
            return exercises;
        }
    }
}
