using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserForLoginDto : IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UserForRegisterDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class UserForVerificationDto : IDto
    {
        public int EmailId { get; set; }
        public string Code { get; set; }
    }
}
