// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;
using EventHighway.Core.Services.Foundations.ListernEvents.V1;

namespace EventHighway.Core.Services.Processings.ListenerEvents.V2
{
    internal partial class ListenerEventV2ProcessingService : IListenerEventV2ProcessingService
    {
        private readonly IListenerEventV1Service listenerEventV2Service;
        private readonly ILoggingBroker loggingBroker;

        public ListenerEventV2ProcessingService(
            IListenerEventV1Service listenerEventV2Service,
            ILoggingBroker loggingBroker)
        {
            this.listenerEventV2Service = listenerEventV2Service;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<ListenerEventV1> AddListenerEventV2Async(ListenerEventV1 listenerEventV2) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV2IsNotNull(listenerEventV2);

            return await this.listenerEventV2Service.AddListenerEventV1Async(listenerEventV2);
        });

        public ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV2sAsync() =>
        TryCatch(async () => await this.listenerEventV2Service.RetrieveAllListenerEventV1sAsync());

        public ValueTask<ListenerEventV1> ModifyListenerEventV2Async(ListenerEventV1 listenerEventV2) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV2IsNotNull(listenerEventV2);

            return await this.listenerEventV2Service.ModifyListenerEventV1Async(listenerEventV2);
        });

        public ValueTask<ListenerEventV1> RemoveListenerEventV2ByIdAsync(
            Guid listenerEventV2Id) => TryCatch(async () =>
        {
            ValidateListenerEventV2Id(listenerEventV2Id);

            return await this.listenerEventV2Service
                .RemoveListenerEventV1ByIdAsync(listenerEventV2Id);
        });
    }
}
