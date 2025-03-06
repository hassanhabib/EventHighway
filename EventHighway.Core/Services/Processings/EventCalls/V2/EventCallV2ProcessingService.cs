// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Services.Foundations.EventCalls.V2;

namespace EventHighway.Core.Services.Processings.EventCalls.V2
{
    internal partial class EventCallV2ProcessingService : IEventCallV2ProcessingService
    {
        private readonly IEventCallV2Service eventCallV2Service;
        private readonly ILoggingBroker loggingBroker;

        public EventCallV2ProcessingService(
            IEventCallV2Service eventCallV2Service,
            ILoggingBroker loggingBroker)
        {
            this.eventCallV2Service = eventCallV2Service;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventCallV2> RunEventCallV2Async(EventCallV2 eventCallV2) =>
        TryCatch(async () =>
        {
            ValidateEventCallV2IsNotNull(eventCallV2);

            return await this.eventCallV2Service.RunEventCallV2Async(eventCallV2);
        });
    }
}
