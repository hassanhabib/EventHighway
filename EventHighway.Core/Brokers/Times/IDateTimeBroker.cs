// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace EventHighway.Core.Brokers.Times
{
    internal interface IDateTimeBroker
    {
        ValueTask<DateTimeOffset> GetDateTimeOffsetAsync();
    }
}
