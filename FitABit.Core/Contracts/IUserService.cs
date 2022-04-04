using FitABit.Core.Models;
using FitABit.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FitABit.Core.Constants
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();
        Task<UserEditViewModel> GetUsersForEdit(string id);
        Task<bool> UpdateUser(UserEditViewModel model);
        Task<ApplicationUser> GetUserById(string id);
    }
}
