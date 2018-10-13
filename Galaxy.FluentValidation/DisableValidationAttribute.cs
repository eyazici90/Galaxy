using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.FluentValidation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableValidationAttribute : Attribute // Marker Attribute
    {
    }
}
