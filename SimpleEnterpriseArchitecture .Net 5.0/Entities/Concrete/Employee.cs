using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
    public class Account : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsVerification { get; set; }
        public string WebSite { get; set; }
        public string Biography { get; set; }
    }
    public class AccountAvatar : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
    }
    public class PostInformation : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime SharedDate { get; set; }
    }
    public class PostTag : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
    public class PostComment : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
    }
    public class PostLike : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
    public class TextMessage : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReceiverId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
