// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall;
using EventHighway.Core.Services.Foundations.EventCalls;

namespace EventHighway.Core.Services.Processings.EventCalls
{
    internal class EventCallProcessingService : IEventCallProcessingService
    {
        private readonly IEventCallService eventCallService;

        public EventCallProcessingService(IEventCallService eventCallService) =>
            this.eventCallService = eventCallService;

        public async ValueTask<EventCall> RunAsync(EventCall eventCall) =>
            await this.eventCallService.RunAsync(eventCall);
    }
}
