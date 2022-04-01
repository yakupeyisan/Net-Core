using Core.DataAccess.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class PhotoRepository : EfBaseRepository<Photo, InstagramDbContext>, IPhotoRepository
    {
    }
}
