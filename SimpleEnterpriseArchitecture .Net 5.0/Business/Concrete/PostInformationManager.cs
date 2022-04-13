using Business.Abstract;
using Core.Abstract;
using Core.Entities.Concrete;
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
        public IPostLikeService _postLikeService;
        public IPostSaveService _postSaveService;
        public IPostCommentService _postCommentService;
        private ICloudinaryService _cloudinaryService;
        private IPhotoService _photoService;

        public PostInformationManager(
            IPostInformationRepository postInformationRepository,
            IPostLikeService postLikeService,
            IPostSaveService postSaveService,
            IPostCommentService postCommentService,
            ICloudinaryService cloudinaryService, 
            IPhotoService photoService)
        {
            _postInformationRepository = postInformationRepository;
            _postLikeService = postLikeService;
            _postSaveService = postSaveService;
            _postCommentService = postCommentService;
            _cloudinaryService = cloudinaryService;
            _photoService = photoService;
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
            _postLikeService.Add(postLike);
            return new SuccessResult("Post liked");
        }
        public IResult UnLikePost(PostLike postLike)
        {
            var likeResult = _postLikeService.Get(l => l.PostId == postLike.PostId && l.UserId == postLike.UserId);
            _postLikeService.Delete(likeResult.Data);
            return new SuccessResult("Post unliked");
        }
        public IResult SavePost(PostSave postSave)
        {
            _postSaveService.Add(postSave);
            return new SuccessResult("Post liked");
        }
        public IResult UnSavePost(PostSave postSave)
        {
            var saveResult = _postSaveService.Get(l => l.PostId == postSave.PostId && l.UserId == postSave.UserId);
            _postSaveService.Delete(saveResult.Data);
            return new SuccessResult("Post unsaved");
        }

        public IResult Add(PostAddDto postAdd)
        {
            var photos = new List<Photo>();
            postAdd.Files.ForEach(f =>
            {
                photos.Add(new Photo() { Url = _cloudinaryService.UploadImage(f) });
            });
            var post = new PostInformation()
            {
                UserId = postAdd.UserId,
                Description = postAdd.Description,
                Location = postAdd.Location,
                SharedDate = DateTime.Now,
                Photos= photos
            };
            _postInformationRepository.Add(post);
            return new SuccessResult("Gönderi Eklendi");
        }

        public IResult AddComment(PostComment postComment)
        {
            return this._postCommentService.Add(postComment);
        }
    }
}
