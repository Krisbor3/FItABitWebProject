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
    public class ProgramServiceTest
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
                .AddSingleton<IProgramService, ProgramService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public void GetProgramsWorks()
        {

            var service = serviceProvider.GetService<IProgramService>();

            Assert.DoesNotThrowAsync(async () => await service.GetPrograms());

        }

        [Test]
        public void GetProgramsThrows()
        {
            dbContext.Dispose();

            var service = serviceProvider.GetService<IProgramService>();

            Assert.CatchAsync(async () => await service.GetPrograms(), "No programs");

        }

        [Test]
        public void GetExercisesThrows()
        {

            string id = "invalid";

            var service = serviceProvider.GetService<IProgramService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.GetExercises(id), "No Exercises found");

        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var pId = Guid.Parse("1b98e18c-020c-4fbd-80a9-bcf21a06530d");
            var exercise = new Exercise
            {
                Id = Guid.Parse("215040b1-58ae-4cd2-9fb6-a6fca3f227d2"),
                Name = "Push ups",
                RestTime = 15,
                ProgramId = pId
            };
            

            var program = new Program()
            {
                Name = "ValidName",
                Difficulty = "Beginner",
                Id = pId
            };

            await repo.AddAsync(exercise);
            await repo.AddAsync(program);
            await repo.SaveChangesAsync();
        }
    }
}
