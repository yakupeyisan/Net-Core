using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class PostSaveManager : IPostSaveService
    {
        private IPostSaveRepository _postSaveRepository;

        public PostSaveManager(IPostSaveRepository postSaveRepository)
        {
            _postSaveRepository = postSaveRepository;
        }

        public IDataResult<List<PostSave>> GetAll(Expression<Func<PostSave, bool>> filter = null)
        {
            return new SuccessDataResult<List<PostSave>>(this._postSaveRepository.GetAll(filter), "Kullanıcılar listelendi");
        }
        public IDataResult<PostSave> Get(Expression<Func<PostSave, bool>> filter)
        {
            return new SuccessDataResult<PostSave>(this._postSaveRepository.Get(filter), "Kullanıcı getirildi");
        }
        public IResult Add(PostSave postSave)
        {
            this._postSaveRepository.Add(postSave);
            return new SuccessResult("Yorum eklendi");
        }

        public IResult Delete(PostSave postSave)
        {
            this._postSaveRepository.Delete(postSave);
            return new SuccessResult("Yorum silindi");
        }

    }
}
