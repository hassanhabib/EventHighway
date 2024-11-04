// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventAddresses;

namespace EventHighway.Core.Services.Foundations.EventAddresses
{
    internal partial interface IEventAddressService
    {
        ValueTask<EventAddress> AddEventAddressAsync(EventAddress eventAddress);
        ValueTask<EventAddress> RetrieveEventAddressByIdAsync(Guid eventAddressId);
    }
}
