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

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}"
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProgramListViewModel>> GetPrograms()
        {
            return await repo.All<Program>()
                .Select(p => new ProgramListViewModel
                {
                    Name= p.Name,
                    Difficulty= p.Difficulty,
                    Id=p.Id.ToString(),
                    Exercises = p.Exercises.Select(e=>new ExerciseViewModel
                    {
                        Name= e.Name,
                        Id=e.Id.ToString(),
                        RestTime=e.RestTime,
                        Details=e.Details.Select(d=>new DetailViewModel
                        {
                            Id=d.Id.ToString(),
                            Kilograms=d.Kilograms,
                            Reps=d.Reps,
                            Sets=d.Sets
                        }).ToList(),
                    }).ToList()
                })
                .ToListAsync();
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
