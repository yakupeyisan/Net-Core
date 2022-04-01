using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class PostLikeManager : IPostLikeService
    {
        private IPostLikeRepository _postLikeRepository;

        public PostLikeManager(IPostLikeRepository postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }

        public IDataResult<List<PostLike>> GetAll(Expression<Func<PostLike, bool>> filter = null)
        {
            return new SuccessDataResult<List<PostLike>>(this._postLikeRepository.GetAll(filter), "Kullanıcılar listelendi");
        }
        public IDataResult<PostLike> Get(Expression<Func<PostLike, bool>> filter)
        {
            return new SuccessDataResult<PostLike>(this._postLikeRepository.Get(filter), "Kullanıcı getirildi");
        }
        public IResult Add(PostLike postLike)
        {
            this._postLikeRepository.Add(postLike);
            return new SuccessResult("Beğeni eklendi");
        }

        public IResult Delete(PostLike postLike)
        {
            this._postLikeRepository.Delete(postLike);
            return new SuccessResult("Beğeni silindi");
        }

    }
    public class PhotoManager : IPhotoService
    {
        private IPhotoRepository _photoRepository;

        public PhotoManager(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public IDataResult<List<Photo>> GetAll(Expression<Func<Photo, bool>> filter = null)
        {
            return new SuccessDataResult<List<Photo>>(this._photoRepository.GetAll(filter), "Kullanıcılar listelendi");
        }
        public IDataResult<Photo> Get(Expression<Func<Photo, bool>> filter)
        {
            return new SuccessDataResult<Photo>(this._photoRepository.Get(filter), "Kullanıcı getirildi");
        }
        public IResult Add(Photo photo)
        {
            this._photoRepository.Add(photo);
            return new SuccessResult("Resim eklendi");
        }
        public IResult Update(Photo photo)
        {
            this._photoRepository.Update(photo);
            return new SuccessResult("Resim güncellendi");
        }
        public IResult Delete(Photo photo)
        {
            this._photoRepository.Delete(photo);
            return new SuccessResult("Resim silindi");
        }

    }
}
