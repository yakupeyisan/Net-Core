using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private BaseLoggerService _baseLoggerService;

        public LogAspect(Type loggerService)
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

        protected override void OnBefore(IInvocation invocation)
        {
            _baseLoggerService.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                string? param = invocation.GetConcreteMethod()?.GetParameters()[i].Name;
                if (param != null)
                    logParameters.Add(new LogParameter()
                    {
                        Name = param,
                        Value = invocation.Arguments[i],
                        Type = invocation.Arguments[i].GetType().Name
                    });
            }
            return new LogDetail()
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
        }
    }
}
