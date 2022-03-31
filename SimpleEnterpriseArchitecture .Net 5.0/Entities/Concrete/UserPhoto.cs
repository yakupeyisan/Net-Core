using Core.Entities;

namespace Entities.Concrete
{
    public class UserPhoto : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhotoId { get; set; }
    }
}
