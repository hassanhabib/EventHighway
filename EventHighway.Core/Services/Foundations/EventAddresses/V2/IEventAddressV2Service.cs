// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;

namespace EventHighway.Core.Services.Foundations.EventAddresses.V2
{
    internal interface IEventAddressV2Service
    {
        ValueTask<EventAddressV1> AddEventAddressV2Async(EventAddressV1 eventAddressV2);
        ValueTask<EventAddressV1> RetrieveEventAddressV2ByIdAsync(Guid eventAddressV2Id);
        ValueTask<EventAddressV1> RemoveEventAddressV2ByIdAsync(Guid eventAddressV2Id);
    }
}
