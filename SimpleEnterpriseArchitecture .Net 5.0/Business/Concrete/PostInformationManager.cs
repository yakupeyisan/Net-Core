using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
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

        public PostInformationManager(IPostInformationRepository postInformationRepository)
        {
            _postInformationRepository = postInformationRepository;
        }

        public IDataResult<List<ViewPostInformation>> GetAllViewPostInformations(int userId, Expression<Func<ViewPostInformation, bool>> filter = null)
        {
            return new SuccessDataResult<List<ViewPostInformation>>(_postInformationRepository.GetAllViewPostInformations(userId,filter),"Postlar listelendi");
        }

        public IDataResult<ViewPostInformation> GetViewPostInformation(int userId, Expression<Func<ViewPostInformation, bool>> filter)
        {
            return new SuccessDataResult<ViewPostInformation>( _postInformationRepository.GetViewPostInformation(userId, filter), "Postlar getirildi");
        }
    }
}
