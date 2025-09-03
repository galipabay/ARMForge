using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces;
using ARMForge.Kernel.Interfaces.UnitOfWork;

namespace ARMForge.Infrastructure 
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ARMForgeDbContext context;

        public UnitOfWork(ARMForgeDbContext dbContext)
        {
            context = dbContext;
            Users = new GenericRepository<User>(context);
            Roles = new GenericRepository<Role>(context);
        }

        public IGenericRepository<User> Users { get; private set; }
        public IGenericRepository<Role> Roles { get; private set; }

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
