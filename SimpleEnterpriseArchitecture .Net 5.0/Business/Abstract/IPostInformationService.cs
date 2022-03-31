using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPostInformationService
    {
        IDataResult<List<ViewPostInformation>> GetAllViewPostInformations(int userId,Expression<Func<ViewPostInformation, bool>> filter = null);
        IDataResult<ViewPostInformation> GetViewPostInformation(int userId, Expression<Func<ViewPostInformation, bool>> filter);
    }
}
