using FitABit.Core.Constants;
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
    public class InstructorService : IInstructorService
    {
        private readonly IApplicationDbRepository repo;

        public InstructorService(IApplicationDbRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Instructor> GetInstructorById(string id)
        {
            return await repo.GetByIdAsync<Instructor>(id);
        }

        public async Task<IEnumerable<InstructorViewModel>> GetInstructors()
        {
            return await repo.All<Instructor>()
                .Select(u => new InstructorViewModel
                {
                    Id = u.Id.ToString(),
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Description=u.Description,
                    PictureUrl=u.PictureUrl,
                    Role=u.Role,
                })
                .ToListAsync();
        }

        public async Task<InstructorViewModel> GetUsersForEdit(string id)
        {
            var instructor = await repo.GetByIdAsync<Instructor>(id);

            return new InstructorViewModel()
            {
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Id = instructor.Id.ToString(),
                Description = instructor.Description,
                PictureUrl = instructor.PictureUrl,
                Role = instructor.Role
            };
        }

        public async Task<bool> UpdateUser(InstructorViewModel model)
        {
            bool result = false;
            var instructor = await repo.GetByIdAsync<Instructor>(model.Id);

            if (instructor != null)
            {
                instructor.FirstName = model.FirstName;
                instructor.LastName = model.LastName;
                instructor.Id = new Guid(model.Id);
                await repo.SaveChangesAsync();
                result = true;
            }
            return result;
        }
    }
}
