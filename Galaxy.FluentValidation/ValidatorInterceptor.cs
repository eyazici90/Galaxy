using Autofac;
using Castle.DynamicProxy;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Galaxy.Bootstrapping;
using Galaxy.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.FluentValidation
{
    public class ValidatorInterceptor : IInterceptor
    {
        private readonly IResolver _resolver;
        public ValidatorInterceptor(IResolver resolver) {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }


        public void Intercept(IInvocation invocation)
        {
            AssertRequest(invocation);
            invocation.Proceed();
            var methodCall = invocation.MethodInvocationTarget;
            var isAsync = methodCall.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;
            if (isAsync && typeof(Task).IsAssignableFrom(methodCall.ReturnType))
            {
                invocation.ReturnValue = InterceptAsync(invocation, (dynamic)invocation.ReturnValue);
            }

        }

        private static ValidationResult ValidateTyped<T>(IValidator<T> validator, T request, string[] ruleset, IValidatorSelector selector = null)
        {
            return validator.Validate(request, selector: selector);
        }

        private void AssertRequest(IInvocation invocation)
        {
            if (!invocation.Arguments.Any())
                return;

            var disableValidationAttribute = invocation.Method.GetCustomAttribute(typeof(DisableValidationAttribute));

            if (disableValidationAttribute != null)
                return;


            var requestObject = invocation.Arguments[0];
            var requestType = requestObject.GetType();
            if (requestType.IsValueType || requestType == typeof(string))
                return;

            var requestValidatorType = typeof(IValidator<>).MakeGenericType(requestType);
            if (GalaxyCoreModule.Container == default)
                throw new GalaxyException($"You should set builded container of {nameof(GalaxyCoreModule)} : {nameof(GalaxyCoreModule.Container)}");

            var validator = _resolver.ResolveOptional(requestValidatorType);

            if (validator == null)
                return;

            var validationResult = GetType().GetMethod(nameof(ValidateTyped), BindingFlags.Static | BindingFlags.NonPublic)
                 .MakeGenericMethod(requestObject.GetType())
                 .Invoke(null, new[] { validator, requestObject, null, null }) as ValidationResult;

            if (validationResult != null && validationResult.IsValid)
                return;

            if (validationResult != null && validationResult.Errors.Any())
                throw new GalaxyException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
        }

        private async Task InterceptAsync(IInvocation invocation, Task task)
        {
            await task.ConfigureAwait(false);
        }

        private async Task<T> InterceptAsync<T>(IInvocation invocation, Task<T> task)
        {
            T result = await task.ConfigureAwait(false);
            return result;
        }
    }
}
