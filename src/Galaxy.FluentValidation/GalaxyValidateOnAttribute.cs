using Autofac;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Galaxy.Bootstrapping;
using Galaxy.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Galaxy.FluentValidation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class GalaxyValidateOnAttribute : Attribute
    {
        public GalaxyValidateOnAttribute(object requestObj, Type validatorTargetTypes)
        {
            if (GalaxyCoreModule.Container == default)
                throw new GalaxyException($"You should set builded container of {nameof(GalaxyCoreModule)} : {nameof(GalaxyCoreModule.Container)}");

            using (var scope = GalaxyCoreModule.Container.BeginLifetimeScope())
            {
                 var resolver = scope.Resolve<IResolver>();
                
                 var requestValidatorType = typeof(IValidator<>).MakeGenericType(validatorTargetTypes);

                 var validator = resolver.ResolveOptional(requestValidatorType);

                 if (validator == null)
                     return;

                 var validationResult = GetType().GetMethod(nameof(ValidateTyped), BindingFlags.Static | BindingFlags.NonPublic)
                      .MakeGenericMethod(validatorTargetTypes)
                      .Invoke(null, new[] { validator, requestObj, null, null }) as ValidationResult;

                 if (validationResult != null && validationResult.IsValid)
                     return;

                 if (validationResult != null && validationResult.Errors.Any())
                     throw new GalaxyException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));

            }
        }
        private static ValidationResult ValidateTyped<T>(IValidator<T> validator, T request, string[] ruleset, IValidatorSelector selector = null)
        {
            return validator.Validate(request, selector: selector);
        }
    }

}
