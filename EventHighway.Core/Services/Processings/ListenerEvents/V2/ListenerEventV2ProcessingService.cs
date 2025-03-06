// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.ListenerEvents.V2;
using EventHighway.Core.Services.Foundations.ListernEvents.V2;

namespace EventHighway.Core.Services.Processings.ListenerEvents.V2
{
    internal partial class ListenerEventV2ProcessingService : IListenerEventV2ProcessingService
    {
        private readonly IListenerEventV2Service listenerEventV2Service;
        private readonly ILoggingBroker loggingBroker;

        public ListenerEventV2ProcessingService(
            IListenerEventV2Service listenerEventV2Service,
            ILoggingBroker loggingBroker)
        {
            this.listenerEventV2Service = listenerEventV2Service;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<ListenerEventV2> AddListenerEventV2Async(ListenerEventV2 listenerEventV2) =>
            await this.listenerEventV2Service.AddListenerEventV2Async(listenerEventV2);
    }
}
