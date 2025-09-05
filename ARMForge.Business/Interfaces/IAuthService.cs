using ARMForge.Kernel.Entities;
using ARMForge.Types.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterRequestDto request);
        Task<string?> LoginAsync(LoginRequestDto request);

    }
}
