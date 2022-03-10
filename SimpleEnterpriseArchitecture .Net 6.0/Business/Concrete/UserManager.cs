using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IDataResult<List<User>> GetAll(Expression<Func<User>>? filter=null)
        {
            return new SuccessDataResult<List<User>>(this._userRepository.GetAll(filter),"Kullanıcılar listelendi");
        }
        public IDataResult<User?> Get(Expression<Func<User>> filter)
        {
            return new SuccessDataResult<User?>(this._userRepository.Get(filter), "Kullanıcı getirildi");
        }
        [LogAspect(typeof(FileLogger),Priority =1)]
        [ValidationAspect(typeof(UserValidator),Priority =2)]
        public IResult Add(User user)
        {
           this._userRepository.Add(user);
            return new SuccessResult("Kişi eklendi");
        }

        public IResult Delete(User user)
        {
            this._userRepository.Delete(user);
            return new SuccessResult("Kişi silindi");
        }


        public IResult Update(User user)
        {
            this._userRepository.Update(user);
            return new SuccessResult("Kişi güncellendi");
        }
    }
}
