using FitABit.Core.Models;
using FitABit.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitABit.Core.Contracts
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorViewModel>> GetInstructors();
        Task<InstructorViewModel> GetUsersForEdit(string id);
        Task<bool> UpdateUser(InstructorViewModel model);

        Task<Instructor> GetInstructorById(string id);
    }
}
