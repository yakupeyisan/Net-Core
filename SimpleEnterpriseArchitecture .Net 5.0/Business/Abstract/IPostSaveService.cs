using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IPostSaveService
    {
        public IDataResult<List<PostSave>> GetAll(Expression<Func<PostSave, bool>> filter = null);
        public IDataResult<PostSave> Get(Expression<Func<PostSave, bool>> filter);
        public IResult Add(PostSave postSave);
        public IResult Delete(PostSave postSave);

    }
}
