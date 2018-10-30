using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Log
{
    public interface ILog
    {
        string Name { get; }

        bool IsEnabled { get; }

        void Debug<T>(string messageTemplate, T propertyValue);
       
        void Debug(string messageTemplate);
       
        void Debug(Exception exception, string messageTemplate);
       
        void Debug<T>(Exception exception, string messageTemplate, T propertyValue);
       
        void Debug(Exception exception, string messageTemplate, params object[] propertyValues);
       
        void Debug(string messageTemplate, params object[] propertyValues);
      
        void Error(Exception exception, string messageTemplate, params object[] propertyValues);
      
        void Error(string messageTemplate);
   
        void Error<T>(string messageTemplate, T propertyValue);

        void Error(Exception exception, string messageTemplate);
     
        void Error(string messageTemplate, params object[] propertyValues);
     
        void Error<T>(Exception exception, string messageTemplate, T propertyValue);

        void Fatal(string messageTemplate);

        void Fatal(string messageTemplate, params object[] propertyValues);
       
        void Fatal(Exception exception, string messageTemplate);
       
        void Fatal<T>(Exception exception, string messageTemplate, T propertyValue);

        void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);
      
        void Fatal<T>(string messageTemplate, T propertyValue);

        void Information(Exception exception, string messageTemplate, params object[] propertyValues);
 
        void Information<T>(Exception exception, string messageTemplate, T propertyValue);
      
        void Information(Exception exception, string messageTemplate);
       
        void Information(string messageTemplate, params object[] propertyValues);

        void Information<T>(string messageTemplate, T propertyValue);

        void Information(string messageTemplate);

        void Verbose(string messageTemplate);
  
        void Verbose<T>(string messageTemplate, T propertyValue);
       
        void Verbose(Exception exception, string messageTemplate, params object[] propertyValues);
 
        void Verbose<T>(Exception exception, string messageTemplate, T propertyValue);
      
        void Verbose(Exception exception, string messageTemplate);
      
        void Verbose(string messageTemplate, params object[] propertyValues);

        void Warning<T>(Exception exception, string messageTemplate, T propertyValue);
      
        void Warning(string messageTemplate);

        void Warning(Exception exception, string messageTemplate);
       
        void Warning(Exception exception, string messageTemplate, params object[] propertyValues);
       
        void Warning(string messageTemplate, params object[] propertyValues);
       
        void Warning<T>(string messageTemplate, T propertyValue);
     
    }
}
