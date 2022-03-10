using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect:MethodInterception
    {
        public Type _validatorType;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Wrong validation type.");
            }
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validatorInstance = Activator.CreateInstance(_validatorType);
            if (validatorInstance == null)
            {
                throw new System.Exception("Validator not be null.");
            }
            var validator = (IValidator)validatorInstance;
            var entityType = _validatorType?.BaseType?.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(type => type.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator,entity);
            }
        }
    }
}
