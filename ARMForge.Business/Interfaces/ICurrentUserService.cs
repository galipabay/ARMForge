using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface ICurrentUserService
    {
        bool IsAdmin { get; }
        string UserId { get; }
        bool HasRole(string role);
    }
}
