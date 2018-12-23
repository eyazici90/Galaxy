using System;
using System.Collections.Generic;
using System.Data;
using System.Text; 

namespace Galaxy.UnitOfWork
{
    public class UnitOfWorkOptions : IUnitOfWorkOptions
    { 
        public TimeSpan? Timeout { get; set; }
        
        public IsolationLevel? IsolationLevel { get; set; }
       
        public bool? IsLazyLoadEnabled { get; set; }

        public void FillDefaultsForNonProvidedOptions(IUnitOfWorkOptions defaultOptions)
        { 
 
            if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
            {
                Timeout = defaultOptions.Timeout.Value;
            }

            if (!IsolationLevel.HasValue && defaultOptions.IsolationLevel.HasValue)
            {
                IsolationLevel = defaultOptions.IsolationLevel.Value;
            }

            if (!IsLazyLoadEnabled.HasValue)
            {
                IsLazyLoadEnabled = defaultOptions.IsLazyLoadEnabled;
            }
        }
    }
}
