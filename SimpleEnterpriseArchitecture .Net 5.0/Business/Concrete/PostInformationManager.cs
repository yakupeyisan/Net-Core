using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PostInformationManager : IPostInformationService
    {
        public IPostInformationRepository _postInformationRepository;
        public IPostLikeRepository _postLikeRepository;
        public IPostSaveRepository _postSaveRepository;
        public IPostCommentRepository _postCommentRepository;

        public PostInformationManager(
            IPostInformationRepository postInformationRepository, 
            IPostLikeRepository postLikeRepository,
            IPostSaveRepository postSaveRepository,
            IPostCommentRepository postCommentRepository)
        {
            _postInformationRepository = postInformationRepository;
            _postLikeRepository = postLikeRepository;
            _postSaveRepository = postSaveRepository;
            _postCommentRepository = postCommentRepository;
        }

        public IDataResult<List<ViewPostInformation>> GetAllViewPostInformations(int userId, Expression<Func<ViewPostInformation, bool>> filter = null)
        {
            return new SuccessDataResult<List<ViewPostInformation>>(_postInformationRepository.GetAllViewPostInformations(userId, filter), "Postlar listelendi");
        }

        public IDataResult<ViewPostInformation> GetViewPostInformation(int userId, Expression<Func<ViewPostInformation, bool>> filter)
        {
            return new SuccessDataResult<ViewPostInformation>(_postInformationRepository.GetViewPostInformation(userId, filter), "Postlar getirildi");
        }
        public IResult LikePost(PostLike postLike)
        {
            _postLikeRepository.Add(postLike);
            return new SuccessResult("Post liked");
        }
        public IResult UnLikePost(PostLike postLike)
        {
            var like = _postLikeRepository.Get(l => l.PostId == postLike.PostId && l.UserId == postLike.UserId);
            _postLikeRepository.Delete(like);
            return new SuccessResult("Post unliked");
        }
        public IResult SavePost(PostSave postSave)
        {
            _postSaveRepository.Add(postSave);
            return new SuccessResult("Post liked");
        }
        public IResult UnSavePost(PostSave postSave)
        {
            var save = _postSaveRepository.Get(l => l.PostId == postSave.PostId && l.UserId == postSave.UserId);
            _postSaveRepository.Delete(save);
            return new SuccessResult("Post unsaved");
        }
    }
}
