// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;

namespace EventHighway.Core.Brokers.Storages
{
    internal partial interface IStorageBroker
    {
        ValueTask<EventAddressV1> InsertEventAddressV2Async(EventAddressV1 eventAddressV2);
        ValueTask<EventAddressV1> SelectEventAddressV2ByIdAsync(Guid eventAddressV2Id);
        ValueTask<EventAddressV1> DeleteEventAddressV2Async(EventAddressV1 eventAddressV2);
    }
}
