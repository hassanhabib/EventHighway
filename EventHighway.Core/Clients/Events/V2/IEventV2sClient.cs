// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace EventHighway.Core.Clients.Events.V2
{
    public interface IEventV2sClient
    {
        ValueTask FireScheduledPendingEventV2sAsync();
    }
}
