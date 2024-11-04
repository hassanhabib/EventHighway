// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Events;
using EventHighway.Core.Services.Foundations.EventAddresses;
using EventHighway.Core.Services.Foundations.Events;

namespace EventHighway.Core.Services.Orchestrations.Events
{
    internal partial class EventOrchestrationService : IEventOrchestrationService
    {
        private readonly IEventAddressService eventAddressService;
        private readonly IEventService eventService;

        public EventOrchestrationService(
            IEventAddressService eventAddressService,
            IEventService eventService)
        {
            this.eventAddressService = eventAddressService;
            this.eventService = eventService;
        }

        public async ValueTask<Event> SubmitEventAsync(Event @event)
        {
            _ = await this.eventAddressService.RetrieveEventAddressByIdAsync(
                @event.EventAddressId);

            return await this.eventService.AddEventAsync(@event);
        }
    }
}
