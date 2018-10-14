using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.UnitOfWork
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableUnitOfWorkAttribute : Attribute // Marker Attribute
    {
    }
}
