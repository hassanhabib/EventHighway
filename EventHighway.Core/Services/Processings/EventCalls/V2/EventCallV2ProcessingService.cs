// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.EventCall.V1;
using EventHighway.Core.Services.Foundations.EventCalls.V1;

namespace EventHighway.Core.Services.Processings.EventCalls.V2
{
    internal partial class EventCallV2ProcessingService : IEventCallV2ProcessingService
    {
        private readonly IEventCallV1Service eventCallV2Service;
        private readonly ILoggingBroker loggingBroker;

        public EventCallV2ProcessingService(
            IEventCallV1Service eventCallV2Service,
            ILoggingBroker loggingBroker)
        {
            this.eventCallV2Service = eventCallV2Service;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<EventCallV1> RunEventCallV2Async(EventCallV1 eventCallV2) =>
        TryCatch(async () =>
        {
            ValidateEventCallV2IsNotNull(eventCallV2);

            return await this.eventCallV2Service.RunEventCallV1Async(eventCallV2);
        });
    }
}
