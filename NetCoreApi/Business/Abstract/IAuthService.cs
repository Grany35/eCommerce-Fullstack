using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<User> Login(LoginDto loginDto);
        Task<User> Register(RegisterDto registerDto);
        AccessToken CreateAccessToken(User user);
    }
}
