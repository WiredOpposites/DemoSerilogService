using Microsoft.Extensions.DependencyInjection;

namespace LoggerService
{
    public interface ILoggerService
    {
        IServiceCollection UseLoggerService(IServiceCollection services);

        void Debug(string messageTemplate, params object?[]? propertyValues);
        void Information(string messageTemplate, params object?[]? propertyValues);
        void Warning(string messageTemplate, params object?[]? propertyValues);
        void Error(string messageTemplate, params object?[]? propertyValues);
        void Fatal(string messageTemplate, params object?[]? propertyValues);
        void Debug(string message, Exception exception);
        void Information(string message, Exception exception);
        void Warning(string message, Exception exception);
        void Error(string message, Exception exception);
        void Fatal(string message, Exception exception);

        void Error(Exception exception);
        void Fatal(Exception exception);
    }
}
