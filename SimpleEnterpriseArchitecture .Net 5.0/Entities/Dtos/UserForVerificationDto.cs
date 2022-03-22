using Core.Entities;

namespace Entities.Dtos
{
    public class UserForVerificationDto : IDto
    {
        public int EmailId { get; set; }
        public string Code { get; set; }
    }
}
