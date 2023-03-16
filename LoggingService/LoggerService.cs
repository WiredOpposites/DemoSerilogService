using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Core;

namespace LoggerService
{
    public sealed class LoggerService : ILoggerService
    {

        private static readonly Logger _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();  

        private const string ExceptionName = "Exception";
        private const string InnerExceptionName = "Inner Exception";
        private const string ExceptionMessageWithoutInnerException = "{0}{1}: {2}Message: {3}{4}StackTrace: {5}.";
        private const string ExceptionMessageWithInnerException = "{0}{1}{2}";

        public IServiceCollection UseLoggerService(IServiceCollection services)
        {
            services.TryAddScoped<ILoggerService, LoggerService>();
            return services;
        }

        public void Debug(string messageTemplate, params object?[]? propertyValues)
        {
            _logger.Debug(messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate, params object?[]? propertyValues)
        {
            _logger.Information(messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object?[]? propertyValues)
        {
            _logger.Warning(messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate, params object?[]? propertyValues)
        {
            _logger.Error(messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate, params object?[]? propertyValues)
        {
            _logger.Fatal(messageTemplate, propertyValues);
        }

        public void Debug(string message, Exception exception)
        {
            _logger.Debug(message, exception);
        }

        public void Information(string message, Exception exception)
        {
            _logger.Information(message, exception);
        }

        public void Warning(string message, Exception exception)
        {
            _logger.Warning(message, exception);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Fatal(string message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }

        public void Error(Exception exception)
        {
            _logger.Error(SerializeException(exception, ExceptionName));
        }

        public void Fatal(Exception exception)
        {
            _logger.Fatal(SerializeException(exception, ExceptionName));
        }

        public static string SerializeException(Exception exception)
        {
            return SerializeException(exception, string.Empty);
        }

        private static string SerializeException(Exception ex, string exceptionMessage)
        {
            var messageAndTrace = string.Format(ExceptionMessageWithoutInnerException, Environment.NewLine,
                exceptionMessage, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace);

            if (ex.InnerException != null)
            {
                messageAndTrace = string.Format(ExceptionMessageWithInnerException, messageAndTrace,
                    Environment.NewLine,
                    SerializeException(ex.InnerException, InnerExceptionName));
            }

            return messageAndTrace + Environment.NewLine;
        }
    }
}