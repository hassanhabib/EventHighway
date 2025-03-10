﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses;
using EventHighway.Core.Services.Foundations.EventAddresses;

namespace EventHighway.Core.Clients.EventAddresses
{
    public class EventAddressesClient : IEventAddressesClient
    {
        private readonly IEventAddressService eventAddressService;

        public EventAddressesClient(IEventAddressService eventAddressService) =>
            this.eventAddressService = eventAddressService;

        public async ValueTask<EventAddress> RegisterEventAddressAsync(EventAddress eventAddress) =>
            await this.eventAddressService.AddEventAddressAsync(eventAddress);

        public async ValueTask<IQueryable<EventAddress>> RetrieveAllEventAddressesAsync() =>
            await this.eventAddressService.RetrieveAllEventAddressesAsync();

        public async ValueTask<EventAddress> RetrieveEventAddressByIdAsync(Guid eventAddressId) =>
            await this.eventAddressService.RetrieveEventAddressByIdAsync(eventAddressId);
    }
}
