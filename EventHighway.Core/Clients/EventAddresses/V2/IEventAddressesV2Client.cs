// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;

namespace EventHighway.Core.Clients.EventAddresses.V2
{
    public interface IEventAddressesV2Client
    {
        ValueTask<EventAddressV1> RegisterEventAddressV2Async(EventAddressV1 eventAddressV2);
        ValueTask<EventAddressV1> RemoveEventAddressV2ByIdAsync(Guid eventAddressV2Id);
    }
}
