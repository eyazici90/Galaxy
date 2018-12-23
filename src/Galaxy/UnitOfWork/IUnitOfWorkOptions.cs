using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Galaxy.UnitOfWork
{
    public interface IUnitOfWorkOptions
    {
        
        TimeSpan? Timeout { get; set; }

        IsolationLevel? IsolationLevel { get; set; }
        
        bool? IsLazyLoadEnabled { get; set; }

        void FillDefaultsForNonProvidedOptions(IUnitOfWorkOptions defaultOptions);
    }
}
