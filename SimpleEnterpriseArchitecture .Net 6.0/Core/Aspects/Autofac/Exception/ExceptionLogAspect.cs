using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private BaseLoggerService _baseLoggerService;

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(BaseLoggerService))
            {
                throw new System.Exception("Wrong Logger Type");
            }
            var instance = Activator.CreateInstance(loggerService);
            if (instance == null)
            {
                throw new System.Exception("Logger type not be null");
            }
            _baseLoggerService = (BaseLoggerService)instance;
        }
        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.Message;
            _baseLoggerService.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                string? param = invocation.GetConcreteMethod().GetParameters()[i].Name;
                if (param != null)
                    logParameters.Add(new LogParameter()
                    {
                        Name =param,
                        Value = invocation.Arguments[i],
                        Type = invocation.Arguments[i].GetType().Name
                    });
            }
            return new LogDetailWithException()
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
        }
    }
}
