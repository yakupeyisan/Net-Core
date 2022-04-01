using Core.Entities;

namespace Core.Entities.Concrete
{
    public class Photo : IEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }

    }
}
