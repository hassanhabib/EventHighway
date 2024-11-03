// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.EventAddress;

namespace EventHighway.Core.Services.EventAddresses
{
    internal partial interface IEventAddressService
    {
        ValueTask<EventAddress> AddEventAddressAsync(EventAddress eventAddress);
    }
}
