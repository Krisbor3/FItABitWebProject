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
            this.repo= repo;
        }
        public async Task<IEnumerable<ProgramListViewModel>> GetPrograms()
        {
            return await repo.All<Program>()
                .Select(p => new ProgramListViewModel
                {
                    Name = p.Name,
                    Difficulty = p.Difficulty,
                    Id = p.Id.ToString(),
                    Exercises = p.Exercises.Select(e => new ExerciseViewModel
                    {
                        Name = e.Name,
                        Id = e.Id.ToString(),
                        RestTime = e.RestTime,
                        Details = e.Details.Select(d => new DetailViewModel
                        {
                            Id = d.Id.ToString(),
                            Kilograms = d.Kilograms,
                            Reps = d.Reps,
                            Sets = d.Sets
                        }).ToList(),
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
