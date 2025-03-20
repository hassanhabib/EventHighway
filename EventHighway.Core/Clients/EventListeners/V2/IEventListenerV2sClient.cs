// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;

namespace EventHighway.Core.Clients.EventListeners.V2
{
    public interface IEventListenerV2sClient
    {
        ValueTask<EventListenerV2> RegisterEventListenerV2Async(EventListenerV2 eventListenerV2);
    }
}
