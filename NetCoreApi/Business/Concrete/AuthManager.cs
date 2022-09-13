using Business.Abstract;
using Core.Utilities.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;

        public AuthManager(IUserDal userDal, ITokenHelper tokenHelper, IUserService userService)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return accessToken;
        }

        public async Task<User> Login(LoginDto loginDto)
        {
            var user = await _userDal.GetAsnc(x => x.Mail == loginDto.Mail);
            if (user == null)
            {
                throw new ApplicationException("Kullanıcı bulunamadı");
            }

            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);

            if (!result)
            {
                throw new ApplicationException("Şifrenizi kontrol edin");
            }

            return user;


        }

        public async Task<User> Register(RegisterDto registerDto)
        {
            var userToCheck = await _userDal.GetAsnc(x => x.Mail == registerDto.Mail);
            if (userToCheck != null)
            {
                throw new ApplicationException("Mail adresi daha önce alınmış");
            }

            if (registerDto.Password != registerDto.PasswordAgain)
            {
                throw new ApplicationException("Şifreler uyuşmuyor. Lütfen kontrol ediniz.");
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Mail = registerDto.Mail,
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _userDal.Add(user);
            return user;

        }

        
    }
}
