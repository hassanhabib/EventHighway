// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;

namespace EventHighway.Core.Services.Processings.EventListeners.V1
{
    internal interface IEventListenerV1ProcessingService
    {
        ValueTask<EventListenerV1> AddEventListenerV1Async(EventListenerV1 eventListenerV1);
        ValueTask<IQueryable<EventListenerV1>> RetrieveEventListenerV1sByEventAddressIdAsync(Guid eventAddressId);
        ValueTask<EventListenerV1> RemoveEventListenerV1ByIdAsync(Guid eventListenerV1Id);
    }
}
