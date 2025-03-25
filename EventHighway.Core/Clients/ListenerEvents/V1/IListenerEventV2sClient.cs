// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;

namespace EventHighway.Core.Clients.ListenerEvents.V1
{
    public interface IListenerEventV1sClient
    {
        ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV1sAsync();
        ValueTask<ListenerEventV1> RemoveListenerEventV1ByIdAsync(Guid listenerEventV1Id);
    }
}
