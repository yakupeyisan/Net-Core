using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IPostInformationRepository : IBaseRepository<PostInformation>
    {
        List<ViewPostInformation> GetAllViewPostInformations(int userId,Expression<Func<ViewPostInformation,bool>> filter=null);
        ViewPostInformation GetViewPostInformation(int userId,Expression<Func<ViewPostInformation,bool>> filter);
    }
}
