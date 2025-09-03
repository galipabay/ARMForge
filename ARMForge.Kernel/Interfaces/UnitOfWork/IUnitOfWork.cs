using ARMForge.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Kernel.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Role> Roles { get; }
        Task<int> CommitAsync();
    }
}
