using Core.Entities;

namespace Entities.Concrete
{
    public class PostSave : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
