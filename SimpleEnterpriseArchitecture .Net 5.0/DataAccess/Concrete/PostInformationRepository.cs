using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace DataAccess.Concrete
{
    public class PostInformationRepository : EfBaseRepository<PostInformation, InstagramDbContext>, IPostInformationRepository
    {
        public List<ViewPostInformation> GetAllViewPostInformations(int userId,Expression<Func<ViewPostInformation, bool>> filter = null)
        {
            using (var context = new InstagramDbContext())
            {
                var result = from post in context.PostInformations
                             select new ViewPostInformation(post)
                             {
                                 User = context.Users.Where(u => u.Id == post.UserId).SingleOrDefault(),
                                 CommentCount = context.PostComments.Where(c => c.PostId == post.Id).ToList().Count,
                                 Photos = (from photo in context.Photos
                                           join postPhoto in context.PostPhotos
                                           on photo.Id equals postPhoto.PhotoId
                                           where postPhoto.PostId == post.Id
                                           select new Photo { Id=photo.Id,Url=photo.Url }).ToList(),
                                 LikeCount = context.PostLikes.Where(l=>l.PostId==post.Id).ToList().Count,
                                 IsLiked = context.PostLikes.Where(l=>l.PostId==post.Id&&l.UserId==userId).ToList().Count>0,
                                 IsSaved = context.PostSaves.Where(s=>s.PostId==post.Id&&s.UserId == userId).ToList().Count>0,
                             };
                return (filter==null)?result.ToList():result.Where(filter).ToList();
            }
        }

        public ViewPostInformation GetViewPostInformation(int userId, Expression<Func<ViewPostInformation, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
