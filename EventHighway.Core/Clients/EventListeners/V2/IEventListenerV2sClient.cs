// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V1;

namespace EventHighway.Core.Clients.EventListeners.V2
{
    public interface IEventListenerV2sClient
    {
        ValueTask<EventListenerV1> RegisterEventListenerV2Async(EventListenerV1 eventListenerV2);
        ValueTask<EventListenerV1> RemoveEventListenerV2ByIdAsync(Guid eventListenerV2Id);
    }
}
