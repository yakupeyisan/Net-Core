using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class PostCommentManager : IPostCommentService
    {
        private IPostCommentRepository _postCommentRepository;

        public PostCommentManager(IPostCommentRepository postCommentRepository)
        {
            _postCommentRepository = postCommentRepository;
        }

        public IDataResult<List<PostComment>> GetAll(Expression<Func<PostComment, bool>> filter = null)
        {
            return new SuccessDataResult<List<PostComment>>(this._postCommentRepository.GetAll(filter), "Yorumlar listelendi");
        }
        public IDataResult<PostComment> Get(Expression<Func<PostComment, bool>> filter)
        {
            return new SuccessDataResult<PostComment>(this._postCommentRepository.Get(filter), "Yorum getirildi");
        }
        public IResult Add(PostComment postComment)
        {
            this._postCommentRepository.Add(postComment);
            return new SuccessResult("Yorum eklendi");
        }

        public IResult Update(PostComment postComment)
        {
            this._postCommentRepository.Update(postComment);
            return new SuccessResult("Yorum güncellendi");
        }

        public IResult Delete(PostComment postComment)
        {
            this._postCommentRepository.Delete(postComment);
            return new SuccessResult("Yorum silindi");
        }
    }
}
