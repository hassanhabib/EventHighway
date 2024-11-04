// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents;

namespace EventHighway.Core.Services.ListernEvents
{
    internal interface IListenerEventService
    {
        ValueTask<ListenerEvent> AddListenerEventAsync(ListenerEvent listenerEvent);
        ValueTask<ListenerEvent> ModifyListenerEventAsync(ListenerEvent listenerEvent);
    }
}
