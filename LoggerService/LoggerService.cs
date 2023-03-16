using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Core;

namespace LoggerService
{
    public sealed class LoggerService : ILoggerService
    {
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
            Log.Debug(messageTemplate, propertyValues);
        }

        public void Information(string messageTemplate, params object?[]? propertyValues)
        {
            Log.Information(messageTemplate, propertyValues);
        }

        public void Warning(string messageTemplate, params object?[]? propertyValues)
        {
            Log.Warning(messageTemplate, propertyValues);
        }

        public void Error(string messageTemplate, params object?[]? propertyValues)
        {
            Log.Error(messageTemplate, propertyValues);
        }

        public void Fatal(string messageTemplate, params object?[]? propertyValues)
        {
            Log.Fatal(messageTemplate, propertyValues);
        }

        public void Debug(string message, Exception exception)
        {
            Log.Debug(message, exception);
        }

        public void Information(string message, Exception exception)
        {
            Log.Information(message, exception);
        }

        public void Warning(string message, Exception exception)
        {
            Log.Warning(message, exception);
        }

        public void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public void Fatal(string message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public void Error(Exception exception)
        {
            Log.Error(SerializeException(exception, ExceptionName));
        }

        public void Fatal(Exception exception)
        {
            Log.Fatal(SerializeException(exception, ExceptionName));
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