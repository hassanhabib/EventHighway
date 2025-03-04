// ---------------------------------------------------------------
// Copyright (c) Aspen Publishing. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EventHighway.Core.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
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
