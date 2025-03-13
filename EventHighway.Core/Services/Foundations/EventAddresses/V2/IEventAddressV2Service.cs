// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;

namespace EventHighway.Core.Services.Foundations.EventAddresses.V2
{
    internal interface IEventAddressV2Service
    {
        ValueTask<EventAddressV2> AddEventAddressV2Async(EventAddressV2 eventAddressV2);
        ValueTask<EventAddressV2> RetrieveEventAddressV2ByIdAsync(Guid eventAddressV2Id);
        ValueTask<EventAddressV2> RemoveEventAddressV2ByIdAsync(Guid eventAddressV2Id);
    }
}
