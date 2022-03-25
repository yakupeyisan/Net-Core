using Castle.DynamicProxy;
using Core.Abstract;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Mailer
{
    public class SuccessMailAspect:MethodInterception
    {
        BaseMailType _mailType;
        IMailService _mailService;
        public SuccessMailAspect(Type mailType)
        {
            if (mailType.BaseType != typeof(BaseMailType))
            {
                throw new System.Exception("Wrong Mailer Type");
            }
            _mailType = (BaseMailType)Activator.CreateInstance(mailType);
            _mailService = ServiceTool.ServiceProvider.GetService<IMailService>();
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            var mail = "";
            foreach (object arg in invocation.Arguments)
            {
                arg.GetType().GetProperties().ToList().ForEach(p =>
                {
                    if (p.Name == "Email")
                    {
                        mail=(string)p.GetValue(arg, null);
                    }
                });
            }
            _mailService.Send(mail, _mailType.title, _mailType.content);
        }
    }
    public class BaseMailType
    {
        public string content;
        public string title;

        public BaseMailType(string title,string content)
        {
            this.content = content;
            this.title = title;
        }
    }
    public class VerificationMailType : BaseMailType
    {
        public VerificationMailType():base("Yeni Üyelik", @"<!DOCTYPE html>
        <html lang='en'>
          <head>
            <!-- Bootstrap CSS -->
            <link
              href='https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css'
              rel='stylesheet' integrity='sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3'
              crossorigin='anonymous'
            />
            <style>
              .content {
                background-color: rgb(223, 223, 223) !important;
                color: #302b2b;
                padding: 15px;
                border-radius: 10px;
                margin: 35px;
              }
            </style>
          </head>
          <body>
            <div class='container'>
              <div class='content'>
                <h5>Sayın {FullName},</h5>

                <p>
                  Üye olduğunuz için teşekkür ederiz.
                  Üyeliginizi aktif etmek için aşağıdaki butona tıklayınız.
                  <br>
                  Aktivasyon kodunuz: {code}
                </p>
                <a href='https://localhost:4200/activate/{Id}' class='offset-6 col-6 btn btn-primary'> Aktif Et</a>
              </div>
            </div>

            <script
              src='https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js'
              integrity='sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p'
              crossorigin='anonymous'
            ></script>
          </body>
        </html>
        ")
        {
        }
    }
}
