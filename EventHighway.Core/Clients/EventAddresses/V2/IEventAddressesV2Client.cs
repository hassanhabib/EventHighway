// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;

namespace EventHighway.Core.Clients.EventAddresses.V2
{
    public interface IEventAddressesV2Client
    {
        ValueTask<EventAddressV2> RegisterEventAddressV2Async(EventAddressV2 eventAddressV2);
        ValueTask<EventAddressV2> RemoveEventAddressV2ByIdAsync(Guid eventAddressV2Id);
    }
}
