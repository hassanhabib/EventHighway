// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
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

        public ValueTask<Event> SubmitEventAsync(Event @event)
        {
            throw new NotImplementedException();
        }
    }
}
