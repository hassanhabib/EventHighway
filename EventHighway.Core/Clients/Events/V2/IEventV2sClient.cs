// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V2;

namespace EventHighway.Core.Clients.Events.V2
{
    public interface IEventV2sClient
    {
        ValueTask<EventV2> SubmitEventV2Async(EventV2 eventV2);
        ValueTask FireScheduledPendingEventV2sAsync();
    }
}
