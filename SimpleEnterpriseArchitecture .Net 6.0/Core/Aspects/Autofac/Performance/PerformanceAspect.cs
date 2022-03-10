﻿using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect:MethodInterception
    {
        public int _interval;

        public Stopwatch _stopwatch;
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            var stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
            if (stopwatch == null)
            {
                throw new System.Exception("Stopwatch not be null");
            }
            _stopwatch = stopwatch;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance :  { invocation.Method?.DeclaringType?.FullName }.{invocation.Method?.Name}==>{_stopwatch.Elapsed.TotalSeconds}");
                _stopwatch.Reset();
            }
        }
    }
}
