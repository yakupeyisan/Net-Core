using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IPostCommentService
    {
        public IDataResult<List<PostComment>> GetAll(Expression<Func<PostComment, bool>> filter = null);
        public IDataResult<PostComment> Get(Expression<Func<PostComment, bool>> filter);
        public IResult Add(PostComment postComment);
        public IResult Update(PostComment postComment);
        public IResult Delete(PostComment postComment);

    }
}
