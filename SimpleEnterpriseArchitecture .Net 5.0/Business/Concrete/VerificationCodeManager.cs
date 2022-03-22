using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class VerificationCodeManager : IVerificationCodeService
    {
        private IVerificationCodeRepository _verificationCodeRepository;

        public VerificationCodeManager(IVerificationCodeRepository verificationCodeRepository)
        {
            _verificationCodeRepository = verificationCodeRepository;
        }

        public IDataResult<List<VerificationCode>> GetAll(Expression<Func<VerificationCode, bool>> filter = null)
        {
            var verificationCodes = _verificationCodeRepository.GetAll(filter);
            return new SuccessDataResult<List<VerificationCode>>(verificationCodes,"Doğrulama kodları getirildi.");
        }

        public IDataResult<VerificationCode> Get(Expression<Func<VerificationCode, bool>> filter)
        {
            var verificationCode = _verificationCodeRepository.Get(filter);
            return new SuccessDataResult<VerificationCode>(verificationCode, "Doğrulama kodu getirildi");
        }

        public IResult Add(VerificationCode verificationCode)
        {
            _verificationCodeRepository.Add(verificationCode);
            return new SuccessResult("Doğrulama kodu eklendi.");
        }
        public IResult Update(VerificationCode verificationCode)
        {
            _verificationCodeRepository.Update(verificationCode);
            return new SuccessResult("Doğrulama kodu güncellendi.");
        }

        public IResult Delete(VerificationCode verificationCode)
        {
            _verificationCodeRepository.Delete(verificationCode);
            return new SuccessResult("Doğrulama kodu silindi.");
        }
    }
}
