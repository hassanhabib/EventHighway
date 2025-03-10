// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses;

namespace EventHighway.Core.Services.Foundations.EventAddresses
{
    public partial interface IEventAddressService
    {
        ValueTask<EventAddress> AddEventAddressAsync(EventAddress eventAddress);
        ValueTask<IQueryable<EventAddress>> RetrieveAllEventAddressesAsync();
        ValueTask<EventAddress> RetrieveEventAddressByIdAsync(Guid eventAddressId);
    }
}
