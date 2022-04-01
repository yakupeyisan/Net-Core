using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class PostInformationRepository : EfBaseRepository<PostInformation, InstagramDbContext>, IPostInformationRepository
    {
        public List<ViewPostInformation> GetAllViewPostInformations(int userId, Expression<Func<ViewPostInformation, bool>> filter = null)
        {
            using (var context = new InstagramDbContext())
            {
                var result = from post in context.PostInformations
                             select new ViewPostInformation(post)
                             {
                                 User = (from user in context.Users
                                         join account in context.Accounts
                                         on user.Id equals account.UserId
                                         join photo in context.Photos
                                         on user.PhotoId equals photo.Id
                                         into userPhoto
                                         from photo in userPhoto.DefaultIfEmpty()
                                         select new User()
                                         {
                                             Id = user.Id,
                                             UserName = user.UserName,
                                             FullName = user.FullName,
                                             Status = user.Status,
                                             PhotoId = user.PhotoId,
                                             Account = account,
                                             Photo = photo
                                         }).SingleOrDefault(),
                                 CommentCount = context.PostComments.Where(c => c.PostId == post.Id).ToList().Count,
                                 Photos = (from photo in context.Photos
                                           join postPhoto in context.PostPhotos
                                           on photo.Id equals postPhoto.PhotoId
                                           where postPhoto.PostId == post.Id
                                           select new Photo { Id = photo.Id, Url = photo.Url }).ToList(),
                                 LikeCount = context.PostLikes.Where(l => l.PostId == post.Id).ToList().Count,
                                 IsLiked = context.PostLikes.Where(l => l.PostId == post.Id && l.UserId == userId).ToList().Count > 0,
                                 IsSaved = context.PostSaves.Where(s => s.PostId == post.Id && s.UserId == userId).ToList().Count > 0,
                             };
                return (filter == null) ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public ViewPostInformation GetViewPostInformation(int userId, Expression<Func<ViewPostInformation, bool>> filter)
        {
            using (var context = new InstagramDbContext())
            {
                var result = from post in context.PostInformations
                             select new ViewPostInformation(post)
                             {
                                 User = (from user in context.Users
                                         join account in context.Accounts
                                         on user.Id equals account.UserId
                                         join photo in context.Photos
                                         on user.PhotoId equals photo.Id
                                         into userPhoto
                                         from photo in userPhoto.DefaultIfEmpty()
                                         where user.Id == post.UserId
                                         select new User()
                                         {
                                             Id = user.Id,
                                             UserName = user.UserName,
                                             FullName = user.FullName,
                                             Status = user.Status,
                                             PhotoId = user.PhotoId,
                                             Account = account,
                                             Photo = photo
                                         }).SingleOrDefault(),
                                 CommentCount = context.PostComments.Where(c => c.PostId == post.Id).ToList().Count,
                                 Photos = (from photo in context.Photos
                                           join postPhoto in context.PostPhotos
                                           on photo.Id equals postPhoto.PhotoId
                                           where postPhoto.PostId == post.Id
                                           select new Photo { Id = photo.Id, Url = photo.Url }).ToList(),
                                 LikeCount = context.PostLikes.Where(l => l.PostId == post.Id).ToList().Count,
                                 IsLiked = context.PostLikes.Where(l => l.PostId == post.Id && l.UserId == userId).ToList().Count > 0,
                                 IsSaved = context.PostSaves.Where(s => s.PostId == post.Id && s.UserId == userId).ToList().Count > 0,
                             };
                return result.Where(filter).SingleOrDefault();
            }
        }

        public override bool Add(PostInformation entity)
        {
            using (var context= new InstagramDbContext())
            {
                var addedPost=context.Entry(entity);
                addedPost.State = EntityState.Added;
                context.SaveChanges();
                if (entity.Photos != null)
                {
                    entity.Photos.ForEach(p =>
                    {
                        var addedPhoto=context.Entry(p);
                        addedPhoto.State = EntityState.Added;
                        context.SaveChanges();
                        var postPhoto = new PostPhoto()
                        {
                            PhotoId = p.Id,
                            PostId = entity.Id
                        };
                        var addedRelation = context.Entry(postPhoto);
                        addedRelation.State = EntityState.Added;
                        context.SaveChanges();
                    });
                }
                return true;
            }
        }
    }
}
