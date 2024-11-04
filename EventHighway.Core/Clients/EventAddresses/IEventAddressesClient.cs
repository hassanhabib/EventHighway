// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventAddresses;

namespace EventHighway.Core.Clients.EventAddresses
{
    public interface IEventAddressesClient
    {
        ValueTask<EventAddress> RegisterEventAddressAsync(EventAddress eventAddress);
        ValueTask<EventAddress> RetrieveEventAddressByIdAsync(Guid eventAddressId);
    }
}
