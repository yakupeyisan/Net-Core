using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class PostInformation : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime SharedDate { get; set; }
        public virtual User User { get; set; }
        public virtual List<Photo> Photos { get; set; }
    }
}
