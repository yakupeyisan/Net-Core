using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IPhotoService
    {
        public IDataResult<List<Photo>> GetAll(Expression<Func<Photo, bool>> filter = null);
        public IDataResult<Photo> Get(Expression<Func<Photo, bool>> filter);
        public IResult Add(Photo photo);
        public IResult Update(Photo photo);
        public IResult Delete(Photo photo);

    }
}
