// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace EventHighway.Core.Brokers.Loggings
{
    internal interface ILoggingBroker
    {
        ValueTask LogErrorAsync(Exception exception);
    }
}
