using System;
using System.Collections.Generic;
using System.Data;
using System.Text; 

namespace Galaxy.UnitOfWork
{ 
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class EnableUnitOfWorkAttribute : Attribute  
    {
        public IUnitOfWorkOptions  UnitOfWorkOptions { get; private set; }

        public EnableUnitOfWorkAttribute()
        {
            UnitOfWorkOptions = new UnitOfWorkOptions();
        }

        public EnableUnitOfWorkAttribute(IsolationLevel isolationLevel): this()
        {
            UnitOfWorkOptions.IsolationLevel = isolationLevel;
        }

        public EnableUnitOfWorkAttribute(int timeout): this()
        {
            UnitOfWorkOptions.Timeout = TimeSpan.FromSeconds(timeout);
        } 
  
        public EnableUnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout): this()
        {
            UnitOfWorkOptions.IsolationLevel = isolationLevel;
            UnitOfWorkOptions.Timeout = TimeSpan.FromSeconds(timeout);
        }
  
    }
}
