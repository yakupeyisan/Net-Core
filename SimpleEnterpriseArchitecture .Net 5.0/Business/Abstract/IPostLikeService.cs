using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IPostLikeService
    {
        public IDataResult<List<PostLike>> GetAll(Expression<Func<PostLike, bool>> filter = null);
        public IDataResult<PostLike> Get(Expression<Func<PostLike, bool>> filter);
        public IResult Add(PostLike postLike);
        public IResult Delete(PostLike postLike);

    }
}
