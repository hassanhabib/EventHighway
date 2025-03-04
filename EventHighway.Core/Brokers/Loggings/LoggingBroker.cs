// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EventHighway.Core.Brokers.Loggings
{
    internal class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this.logger = logger;

        public async ValueTask LogErrorAsync(Exception exception)
        {
            this.logger.LogError(
                message: exception.Message,
                args: exception);
        }
    }
}
