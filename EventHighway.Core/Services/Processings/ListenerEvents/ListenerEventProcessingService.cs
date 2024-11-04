// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents;
using EventHighway.Core.Services.Foundations.ListernEvents;

namespace EventHighway.Core.Services.Processings.ListenerEvents
{
    internal class ListenerEventProcessingService : IListenerEventProcessingService
    {
        private readonly IListenerEventService listenerEventService;

        public ListenerEventProcessingService(IListenerEventService listenerEventService) =>
            this.listenerEventService = listenerEventService;

        public async ValueTask<ListenerEvent> AddListenerEventAsync(ListenerEvent listenerEvent) =>
            await this.listenerEventService.AddListenerEventAsync(listenerEvent);

        public ValueTask<ListenerEvent> ModifyListenerEventAsync(ListenerEvent listenerEvent)
        {
            throw new NotImplementedException();
        }
    }
}
