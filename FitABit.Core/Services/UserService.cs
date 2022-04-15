using FitABit.Core.Constants;
using FitABit.Core.Models;
using FitABit.Infrastructure.Data.Repositories;
using FitABit.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;
        //private readonly RoleManager roleManager;
        public UserService(IApplicationDbRepository repo)
        {
            this.repo = repo;
        }

        public async Task<UserListViewModel> GetUserByEmail(string email)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Email == email)
                .Select(u => new UserListViewModel
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = u.FirstName
                })
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException("Unknown user email");
            }
            return user;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);
            if (user == null)
            {
                throw new ArgumentException("User with this id does not exist");
            }
            return user;
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            var users = await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}"
                })
                .ToListAsync();
            return users;
        }

        public async Task<UserEditViewModel> GetUsersForEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new UserEditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            };
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Id = model.Id;
                await repo.SaveChangesAsync();
                result = true;
            }
            return result;
        }
    }
}
