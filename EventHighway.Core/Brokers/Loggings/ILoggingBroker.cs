// ---------------------------------------------------------------
// Copyright (c) Aspen Publishing. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace EventHighway.Core.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        ValueTask LogErrorAsync(Exception exception);
    }
}
