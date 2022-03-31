using Core.Entities;
using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ViewPostInformation:IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime SharedDate { get; set; }
        public List<Photo> Photos { get; set; }
        public User User { get; set; }
        public bool IsLiked { get; set; }
        public bool IsSaved { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }

        public ViewPostInformation()
        {
        }
        public ViewPostInformation(PostInformation post)
        {
            this.Id = post.Id;
            this.UserId = post.UserId;
            this.Description = post.Description;
            this.Location = post.Location;
            this.SharedDate = post.SharedDate;
            this.Photos = post.Photos;
            this.User = post.User;
        }
    }
}
