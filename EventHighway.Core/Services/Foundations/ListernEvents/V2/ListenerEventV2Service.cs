// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Brokers.Loggings;
using EventHighway.Core.Brokers.Storages;
using EventHighway.Core.Brokers.Times;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;

namespace EventHighway.Core.Services.Foundations.ListernEvents.V2
{
    internal partial class ListenerEventV2Service : IListenerEventV2Service
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public ListenerEventV2Service(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<ListenerEventV1> AddListenerEventV2Async(ListenerEventV1 listenerEventV2) =>
        TryCatch(async () =>
        {
            await ValidateListenerEventV2OnAddAsync(listenerEventV2);

            return await storageBroker.InsertListenerEventV2Async(listenerEventV2);
        });

        public ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV2sAsync() =>
        TryCatch(async () => await this.storageBroker.SelectAllListenerEventV2sAsync());

        public ValueTask<ListenerEventV1> ModifyListenerEventV2Async(ListenerEventV1 listenerEventV2) =>
        TryCatch(async () =>
        {
            await ValidateListenerEventV2OnModifyAsync(listenerEventV2);

            ListenerEventV1 maybeListenerEventV2 =
                await this.storageBroker.SelectListenerEventV2ByIdAsync(
                    listenerEventV2.Id);

            ValidateListenerEventV2AgainstStorage(listenerEventV2, maybeListenerEventV2);

            return await storageBroker.UpdateListenerEventV2Async(listenerEventV2);
        });

        public ValueTask<ListenerEventV1> RemoveListenerEventV2ByIdAsync(Guid listenerEventV2Id) =>
        TryCatch(async () =>
        {
            ValidateListenerEventV2Id(listenerEventV2Id);

            ListenerEventV1 maybeListenerEventV2 =
                await this.storageBroker.SelectListenerEventV2ByIdAsync(listenerEventV2Id);

            ValidateListenerEventV2Exists(maybeListenerEventV2, listenerEventV2Id);

            return await this.storageBroker.DeleteListenerEventV2Async(maybeListenerEventV2);
        });
    }
}
