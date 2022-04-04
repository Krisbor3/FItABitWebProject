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
    public class ExerciseService : IExerciseService
    {
        private readonly IApplicationDbRepository repo;

        public ExerciseService(IApplicationDbRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<ExerciseViewModel>> GetExercisesForChestDay()
        {
            var program = await repo.All<Program>()
                .Where(p => p.Name == "ChestDay")
                .Select(program => new ProgramListViewModel
                {
                    Id = program.Id.ToString()
                }).FirstAsync();

            return await repo.All<Exercise>()
                .Where(e => e.ProgramId.ToString() == program.Id)
                .Select(e => new ExerciseViewModel
                {
                    Id = e.Id.ToString(),
                    Name = e.Name,
                    RestTime = e.RestTime,
                })
                .ToListAsync();
        }
    }
}
