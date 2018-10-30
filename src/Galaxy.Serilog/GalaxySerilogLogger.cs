using Galaxy.Log;
using System;


namespace Galaxy.Serilog
{
    public sealed class GalaxySerilogLogger : ILog
    {
        private readonly global::Serilog.ILogger _logger;
        private readonly ILogConfigurations _logConfigurations;
        public GalaxySerilogLogger(global::Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public GalaxySerilogLogger(global::Serilog.ILogger logger, ILogConfigurations logConfigurations)
        {
            _logger = logger;
            _logConfigurations = logConfigurations; 
        }

        public string Name => _logConfigurations.Name;

        public bool IsEnabled => _logConfigurations.IsEnabled;

        public void Debug<T>(string messageTemplate, T propertyValue)
        {
            _logger.Debug(messageTemplate, propertyValue);
        }

        public void Debug(string messageTemplate)
        {
            _logger.Debug(messageTemplate);
        }

        public void Debug(Exception exception, string messageTemplate)
        {
            _logger.Debug(exception, messageTemplate);
        }

        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Debug(exception, messageTemplate, propertyValue);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Debug(exception, messageTemplate, propertyValues);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            _logger.Debug(messageTemplate, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Error(exception, messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate)
        {
            _logger.Error(messageTemplate);
        }

        public void Error<T>(string messageTemplate, T propertyValue)
        {
            _logger.Error(messageTemplate, propertyValue);
        }

        public void Error(Exception exception, string messageTemplate)
        {
            _logger.Error(exception, messageTemplate);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            _logger.Error(messageTemplate, propertyValues);
        }

        public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Error(exception, messageTemplate, propertyValue);
        }

        public void Fatal(string messageTemplate)
        {
            _logger.Fatal(messageTemplate);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            _logger.Fatal(messageTemplate, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate)
        {
            _logger.Fatal(exception, messageTemplate);
        }

        public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Fatal(exception, messageTemplate, propertyValue);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Fatal(exception, messageTemplate, propertyValues);
        }

        public void Fatal<T>(string messageTemplate, T propertyValue)
        {
            _logger.Fatal(messageTemplate, propertyValue);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Information(exception, messageTemplate, propertyValues);
        }

        public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Information(exception, messageTemplate, propertyValue);
        }

        public void Information(Exception exception, string messageTemplate)
        {
            _logger.Information(exception, messageTemplate);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            _logger.Information( messageTemplate, propertyValues);
        }

        public void Information<T>(string messageTemplate, T propertyValue)
        {
            _logger.Information(messageTemplate, propertyValue);
        }

        public void Information(string messageTemplate)
        {
            _logger.Information(messageTemplate);
        }

        public void Verbose(string messageTemplate)
        {
            _logger.Verbose(messageTemplate);
        }

        public void Verbose<T>(string messageTemplate, T propertyValue)
        {
            _logger.Verbose(messageTemplate, propertyValue);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Verbose(exception, messageTemplate, propertyValues);
        }

        public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Verbose(exception, messageTemplate, propertyValue);
        }

        public void Verbose(Exception exception, string messageTemplate)
        {
            _logger.Verbose(exception, messageTemplate);
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            _logger.Verbose(messageTemplate, propertyValues);
        }

        public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Warning(exception , messageTemplate, propertyValue);
        }

        public void Warning(string messageTemplate)
        {
            _logger.Warning(messageTemplate);
        }

        public void Warning(Exception exception, string messageTemplate)
        {
            _logger.Warning(exception, messageTemplate);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Warning(exception, messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            _logger.Warning(messageTemplate, propertyValues);
        }

        public void Warning<T>(string messageTemplate, T propertyValue)
        {
            _logger.Warning(messageTemplate, propertyValue);
        }
    }
}
