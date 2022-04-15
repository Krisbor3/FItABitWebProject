using FitABit.Core.Constants;
using FitABit.Core.Models;
using FitABit.Core.Services;
using FitABit.Infrastructure.Data.Repositories;
using FitABit.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace FitABit.Test
{
    public class UserServiceTest
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
                .AddSingleton(sp=>dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository,ApplicationDbRepository>()
                .AddSingleton<IUserService,UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public void UnknownUserEmail()
        {
            var email = "notValid";

            var service = serviceProvider.GetService<IUserService>();

            Assert.CatchAsync<ArgumentException>(async () => await service.GetUserByEmail(email), "Unknown user email");
        }

        [Test]
        public void FoundUserEmail()
        {
            var email = "myTestEmail";

            var service = serviceProvider.GetService<IUserService>();

            Assert.DoesNotThrowAsync(async () => await service.GetUserByEmail(email));

        }

        [Test]
        public void GetUserByIdThrows()
        {
            var id = "invalidId";

            var service = serviceProvider.GetService<IUserService>();

            Assert.CatchAsync(async () => await service.GetUserById(id), "User with this id does not exist");

        }

        [Test]
        public void GetUserByIdWorks()
        {
            var id = "validId";

            var service = serviceProvider.GetService<IUserService>();

            Assert.DoesNotThrowAsync(async () => await service.GetUserById(id));

        }

        [Test]
        public void GetUserWorks()
        {
            
            var service = serviceProvider.GetService<IUserService>();

            Assert.DoesNotThrowAsync(async () => await service.GetUsers());

        }

        [Test]
        public void GetUserForEditWorks()
        {
            string id = "validId";

            var service = serviceProvider.GetService<IUserService>();

            Assert.DoesNotThrowAsync(async () => await service.GetUsersForEdit(id));

        }

        [Test]
        public void UpdateUser()
        {
            var model = new UserEditViewModel()
            {
                FirstName = "Pesho",
                LastName = "Petrov",
                Id = "validId"
            };

            var service = serviceProvider.GetService<IUserService>();

            Assert.DoesNotThrowAsync(async () => await service.UpdateUser(model));

        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var user = new ApplicationUser()
            {
                Email = "myTestEmail",
                FirstName = "Pesho",
                Id = "validId"
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
        }
    }
}