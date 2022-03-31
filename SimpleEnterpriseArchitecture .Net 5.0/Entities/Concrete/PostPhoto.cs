using Core.Entities;

namespace Entities.Concrete
{
    public class PostPhoto : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int PhotoId { get; set; }
    }
}
