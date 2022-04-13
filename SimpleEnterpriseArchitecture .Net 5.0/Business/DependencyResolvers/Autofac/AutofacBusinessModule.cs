using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<MailTransactionManager>().As<IMailTransactionService>();
            builder.RegisterType<MailTransactionRepository>().As<IMailTransactionRepository>();
            builder.RegisterType<VerificationCodeManager>().As<IVerificationCodeService>();
            builder.RegisterType<VerificationCodeRepository>().As<IVerificationCodeRepository>();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<PostInformationManager>().As<IPostInformationService>();
            builder.RegisterType<PostInformationRepository>().As<IPostInformationRepository>();
            builder.RegisterType<PostLikeRepository>().As<IPostLikeRepository>();
            builder.RegisterType<PostCommentManager>().As<IPostCommentService>();
            builder.RegisterType<PostCommentRepository>().As<IPostCommentRepository>();
            builder.RegisterType<PostSaveRepository>().As<IPostSaveRepository>();
            builder.RegisterType<PostLikeManager>().As<IPostLikeService>();
            builder.RegisterType<PostSaveManager>().As<IPostSaveService>();
            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>();
            builder.RegisterType<PhotoManager>().As<IPhotoService>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions() 
                { 
                    Selector = new AspectInterceptorSelector() 
                }).SingleInstance(); 
        }
    }
}
