using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        public int _duration;
        public ICacheManager _cacheManager;

        public CacheAspect(int duration=60)
        {
            _duration = duration;
            var cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            if (cacheManager == null)
            {
                throw new System.Exception("Cache manager not be null.");
            }
            _cacheManager = cacheManager;
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method?.ReflectedType?.FullName}.{invocation.Method?.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key,invocation.ReturnValue,_duration);
        }

    }
}
