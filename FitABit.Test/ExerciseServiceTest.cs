using FitABit.Core.Contracts;
using FitABit.Core.Models;
using FitABit.Core.Services;
using FitABit.Infrastructure.Data.Repositories;
using FitABit.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Test
{
    public class ExerciseServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();

            var serviceCollection = new ServiceCollection();

            //add all dependancies in the ctor that we test
            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<IExerciseService, ExerciseService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public void AddDetailWorks()
        {
            var model = new DetailViewModel()
            {
                Id = "e9942ffc-435d-4a8a-a58e-1a7fee34ed8a",
                Kilograms = 5,
                Reps = 5,
                Sets = 5,
                ExerciseId = "376b7646-be73-47f4-87c8-ff273ff8ed9f",
                UserId = "c5d5265c-adac-452a-9e1f-b3052c501542"
            };

            var service = serviceProvider.GetService<IExerciseService>();

            Assert.DoesNotThrowAsync(async () => await service.AddDetails(model));

        }

        [Test]
        public void GetExercisesForBackDayWorks()
        {

            var service = serviceProvider.GetService<IExerciseService>();

            Assert.DoesNotThrowAsync(async () => await service.GetExercisesForBackDay());

        }

        [Test]
        public void GetExercisesForChestDayThrows()
        {

            var service = serviceProvider.GetService<IExerciseService>();

            Assert.CatchAsync(async () => await service.GetExercisesForChestDay());

        }

        [Test]
        public void GetExercisesForLegDayWorks()
        {

            var service = serviceProvider.GetService<IExerciseService>();

            Assert.DoesNotThrowAsync(async () => await service.GetExercisesForLegDay());

        }

        [Test]
        public void SeeResultsWorks()
        {
            string exerciseId = "376b7646-be73-47f4-87c8-ff273ff8ed9f";
            string userId = "c5d5265c-adac-452a-9e1f-b3052c501542";

            var service = serviceProvider.GetService<IExerciseService>();

            Assert.DoesNotThrowAsync(async () => await service.SeeResults(exerciseId,userId));

        }

        [Test]
        public void SeeResultsThrows()
        {
            string exerciseId = "invalid";
            string userId = "invalid";

            var service = serviceProvider.GetService<IExerciseService>();

            Assert.CatchAsync(async () => await service.SeeResults(exerciseId, userId));

        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            //backDay
            var program = new Program()
            {
                Id = Guid.Parse("8783b6f9-0c79-476f-9cab-10d196e4639a"),
                Name = "BackDay",
                Difficulty = "Beginner",

            };

            var exercise = new Exercise()
            {
                Id = Guid.Parse("376b7646-be73-47f4-87c8-ff273ff8ed9f"),
                Name = "name",
                ProgramId = program.Id,
            };

            var user = new ApplicationUser()
            {
                Id = "c5d5265c-adac-452a-9e1f-b3052c501542",

            };
            var detail = new Detail()
            {
                Id = Guid.Parse("e9942ffc-435d-4a8a-a58e-1a7fee34ed8a"),
                Kilograms = 5,
                Reps = 5,
                Sets = 5,
                ExerciseId = exercise.Id,
                UserId = Guid.Parse(user.Id),
            };

            //legDay
            var programL = new Program()
            {
                Id = Guid.Parse("be001c90-3317-4e08-bf35-b2a9e3f1d78d"),
                Name = "LegDay",
                Difficulty = "Beginner",
            };

            var exerciseL = new Exercise()
            {
                Id = Guid.Parse("c97bafb9-ee20-477c-863a-814dd238aacd"),
                Name = "legRaises",
                ProgramId = programL.Id,
            };

            var userL = new ApplicationUser()
            {
                Id = "d9ccc5fa-a9ee-4df7-9756-f92aff520102",

            };
            var detailL = new Detail()
            {
                Id = Guid.Parse("d6a017f7-9f3f-4a6f-b361-9dbf5c65db30"),
                Kilograms = 5,
                Reps = 5,
                Sets = 5,
                ExerciseId = exerciseL.Id,
                UserId = Guid.Parse(userL.Id),
            };



            await repo.AddAsync(program);
            await repo.AddAsync(user);
            await repo.AddAsync(exercise);
            await repo.AddAsync(detail);

            await repo.AddAsync(programL);
            await repo.AddAsync(userL);
            await repo.AddAsync(exerciseL);
            await repo.AddAsync(detailL);

            await repo.SaveChangesAsync();
        }
    }
}
