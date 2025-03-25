// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;

namespace EventHighway.Core.Clients.EventAddresses.V1
{
    public interface IEventAddressesV1Client
    {
        ValueTask<EventAddressV1> RegisterEventAddressV1Async(EventAddressV1 eventAddressV1);
        ValueTask<IQueryable<EventAddressV1>> RetrieveAllEventAddressV1sAsync();
        ValueTask<EventAddressV1> RemoveEventAddressV1ByIdAsync(Guid eventAddressV1Id);
    }
}
