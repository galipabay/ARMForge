using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> CheckUserById(int id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
