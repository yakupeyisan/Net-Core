using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Abstract;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
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
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public string MailContent = "";
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user,claims);
            return new SuccessDataResult<AccessToken>(accessToken,"Token oluşturuldu.");
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var checkUser=_userService.Get(u=>u.UserName==userForLoginDto.UserName);

            if(!HashingHelper.VerifyPasswordHash(
                userForLoginDto.Password,
                checkUser.Data.PasswordHash,
                checkUser.Data.PasswordSalt
                ))
            {
                return new ErrorDataResult<User>("Hatalı şifre girdiniz!");
            }
            return new SuccessDataResult<User>(checkUser.Data,"Kullacını girişi başarılı.");
        }
        [ExceptionLogAspect(typeof(DatabaseLogger))]
        [ValidationAspect(typeof(RegisterValidator))]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var check = this.UserExists(userForRegisterDto.UserName);
            if (check.Success)
            {
                return new ErrorDataResult<User>("Bu kullanıcı adı sistemde var!");
            }
            byte[] passwordHash,passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password,out passwordHash,out passwordSalt);
            var user = new User()
            {
                FullName = userForRegisterDto.FullName,
                UserName = userForRegisterDto.UserName,
                Status = false,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user,"Kayıt işlemi başarılı.");

        }

        public IResult ResetPassword(string userName)
        {
            throw new NotImplementedException();
        }
        public IResult UserExists(string userName)
        {
            if (_userService.Get(u => u.UserName == userName).Data == null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public IDataResult<User> UserVerification(UserForVerificationDto userForVerificationDto)
        {
            var disableUserExists=this._userService.Get(u => u.Id == userForVerificationDto.UserId && u.Status == false);
            if (disableUserExists.Data == null) {
                return new ErrorDataResult<User>("Pasif kullanıcı bulunamadı");
            }
            return disableUserExists;
        }
    }
}
