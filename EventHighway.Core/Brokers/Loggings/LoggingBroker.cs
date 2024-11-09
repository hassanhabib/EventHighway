// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace EventHighway.Core.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this.logger = logger;

        public async ValueTask LogInformationAsync(string message) =>
            this.logger.LogInformation(message);

        public async ValueTask LogTraceAsync(string message) =>
            this.logger.LogTrace(message);

        public async ValueTask LogDebugAsync(string message) =>
            this.logger.LogDebug(message);

        public async ValueTask LogWarningAsync(string message) =>
            this.logger.LogWarning(message);

        public async ValueTask LogErrorAsync(Exception exception) =>
            this.logger.LogError(exception, exception.Message);

        public async ValueTask LogCriticalAsync(Exception exception) =>
            this.logger.LogCritical(exception, exception.Message);
    }
}