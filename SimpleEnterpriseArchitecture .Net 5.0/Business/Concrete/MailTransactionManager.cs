using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MailTransactionManager : IMailTransactionService
    {
        private IMailTransactionRepository _mailTransactionRepository;

        public MailTransactionManager(IMailTransactionRepository mailTransactionRepository)
        {
            _mailTransactionRepository = mailTransactionRepository;
        }


        public IDataResult<List<MailTransaction>> GetAll(Expression<Func<MailTransaction, bool>> filter = null)
        {
            var mailTransactions = _mailTransactionRepository.GetAll(filter);
            return new SuccessDataResult<List<MailTransaction>>(mailTransactions, "Mail işlemleri getirildi.");
        }

        public IDataResult<MailTransaction> Get(Expression<Func<MailTransaction, bool>> filter)
        {
            var mailTransaction = _mailTransactionRepository.Get(filter);
            return new SuccessDataResult<MailTransaction>(mailTransaction, "Mail işlemi getirildi.");
        }

        public IResult Add(MailTransaction mailTransaction)
        {
            _mailTransactionRepository.Add(mailTransaction);
            return new SuccessResult("Mail işlemi eklendi.");
        }

        public IResult Update(MailTransaction mailTransaction)
        {
            _mailTransactionRepository.Update(mailTransaction);
            return new SuccessResult("Mail işlemi güncellendi.");
        }
        public IResult Delete(MailTransaction mailTransaction)
        {
            _mailTransactionRepository.Delete(mailTransaction);
            return new SuccessResult("Mail işlemi silindi.");
        }
    }
}
