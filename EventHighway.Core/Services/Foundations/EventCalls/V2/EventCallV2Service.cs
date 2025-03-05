// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Brokers.Apis;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.EventCall.V2;

namespace EventHighway.Core.Services.Foundations.EventCalls.V2
{
    internal partial class EventCallV2Service : IEventCallV2Service
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public EventCallV2Service(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventCallV2> RunEventCallV2Async(EventCallV2 eventCallV2) =>
        TryCatch(async () =>
        {
            ValidateEventCallV2IsNotNull(eventCallV2);

            string response =
                await apiBroker.PostAsync(
                    content: eventCallV2.Content,
                    url: eventCallV2.Endpoint,
                    secret: eventCallV2.Secret);

            eventCallV2.Response = response;

            return eventCallV2;
        });
    }
}
