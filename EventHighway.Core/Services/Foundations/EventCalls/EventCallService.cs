// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Brokers.Apis;
using EventHighway.Core.Models.EventCall;

namespace EventHighway.Core.Services.Foundations.EventCalls
{
    internal class EventCallService : IEventCallService
    {
        private readonly IApiBroker apiBroker;

        public EventCallService(IApiBroker apiBroker) =>
            this.apiBroker = apiBroker;

        public async ValueTask<EventCall> RunAsync(EventCall eventCall)
        {
            string response =
                await apiBroker.PostAsync(
                    content: eventCall.Content,
                    url: eventCall.Endpoint,
                    secret: eventCall.Secret);

            eventCall.Response = response;

            return eventCall;
        }
    }
}
