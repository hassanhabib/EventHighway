// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;

namespace EventHighway.Core.Clients.ListenerEvents.V2
{
    public interface IListenerEventV2sClient
    {
        ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV2sAsync();
        ValueTask<ListenerEventV1> RemoveListenerEventV2ByIdAsync(Guid listenerEventV2Id);
    }
}
