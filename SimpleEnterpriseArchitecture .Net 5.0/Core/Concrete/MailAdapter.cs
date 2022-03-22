using Core.Abstract;
using Core.Utilities.Results;
using Entegrations.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{  
    public class MailAdapter : IMailService
    {
        public IResult Send(List<string> recipientEmails, string subject, string content)
        {
            SmtpMailEntegration smtpMailEntegration = new SmtpMailEntegration();
            smtpMailEntegration.Send(recipientEmails,subject,content);
            return new SuccessResult("Mail gönderildi.");
        } 
    }
}
