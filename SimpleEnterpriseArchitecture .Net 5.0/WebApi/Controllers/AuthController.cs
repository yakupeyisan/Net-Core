using Business.Abstract;
using Core.Abstract;
using Core.Utilities.FileOperations;
using Core.Utilities.Results;
using Core.Utilities.Tools;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private IAuthService _authService;
        private IVerificationCodeService _verificationCodeService;
        public AuthController(IAuthService authService,IUserService userService,IVerificationCodeService verificationCodeService)
        {
            _authService = authService;
            _userService = userService;
            _verificationCodeService = verificationCodeService;
        }
        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto register)
        {
            var result = _authService.Register(register);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var code = RandomCodeGenerator.Generate(6);
            var verificationCode = new VerificationCode()
            {
                UserId = result.Data.Id,
                Code = code,
                ExpirationDate = DateTime.Now.AddMinutes(15)
            };
            _verificationCodeService.AddAndSendMail(verificationCode, result.Data, register.Email);
            return Ok(result);
        }
        [HttpPost("activate")]
        public IActionResult Activate(UserForVerificationDto verificationDto)
        {
            var userCheck=_authService.UserVerification(verificationDto);
            if (!userCheck.Success)
            {
                return BadRequest(userCheck.Message);
            }
            var codeResult=_verificationCodeService.Get(c => c.Code == verificationDto.Code && c.ExpirationDate > DateTime.Now);
            if (codeResult.Data == null)
            {
                return BadRequest("Doğrulama kodunuz bulunamadı");
            }
            userCheck.Data.Status = true;
            _userService.Update(userCheck.Data);
            codeResult.Data.ExpirationDate = DateTime.Now;
            _verificationCodeService.Update(codeResult.Data);
            return Ok(new SuccessResult("Aktivasyon işlemi başarılı"));
        }
    }
}
