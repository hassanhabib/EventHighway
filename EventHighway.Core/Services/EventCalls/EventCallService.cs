// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Apis;
using EventHighway.Core.Models.EventCall;

namespace EventHighway.Core.Services.EventCalls
{
    internal class EventCallService : IEventCallService
    {
        private readonly IApiBroker apiBroker;

        public EventCallService(IApiBroker apiBroker) =>
            this.apiBroker = apiBroker;

        public ValueTask<EventCall> RunAsync(EventCall eventCall) => 
            throw new NotImplementedException();
    }
}
