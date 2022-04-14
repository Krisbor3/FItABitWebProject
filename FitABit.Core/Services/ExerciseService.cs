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

        public async Task<bool> AddDetails(DetailViewModel model)
        {
            bool result = false;
            var id = new Guid();
            var detail = new Detail()
            {
                Id = id,
                Kilograms = model.Kilograms,
                Reps = model.Reps,
                Sets = model.Sets,
                ExerciseId=Guid.Parse(model.ExerciseId),
                UserId=Guid.Parse(model.UserId),
            };

            if (detail.Reps != 0 && detail.Kilograms>=0 && detail.Sets!=0)
            {
                await repo.AddAsync(detail);
                await repo.SaveChangesAsync();
                result = true;
            }
            return result;
        }



        public async Task<IEnumerable<ExerciseViewModel>> GetExercisesForBackDay()
        {
            var program = await repo.All<Program>()
                .Where(p=>p.Name=="BackDay")
                .Select(program=>new ProgramListViewModel
                {
                    Id=program.Id.ToString()
                })
                .FirstAsync();

            return await repo.All<Exercise>()
                .Where(e => e.ProgramId.ToString() == program.Id)
                .Select(e => new ExerciseViewModel
                {
                    Id = e.Id.ToString(),
                    Name = e.Name,
                    RestTime = e.RestTime
                })
                .ToListAsync();
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

        public async Task<IEnumerable<ExerciseViewModel>> GetExercisesForLegDay()
        {
            var program = await repo.All<Program>()
               .Where(p => p.Name == "LegDay")
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

        public async Task<IEnumerable<DetailViewModel>> SeeResults(string exerciseId,string userId)
        {
            return await repo.All<Detail>()
                 .Where(d => d.ExerciseId.ToString() == exerciseId)
                 .Where(d=>d.UserId==Guid.Parse(userId))
                 .Select(d => new DetailViewModel
                 {
                     Reps = d.Reps,
                     Sets = d.Sets,
                     Kilograms = d.Kilograms,
                 })
                 .ToListAsync();
        }
    }
}
