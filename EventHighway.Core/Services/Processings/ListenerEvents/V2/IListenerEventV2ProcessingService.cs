// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V1;

namespace EventHighway.Core.Services.Processings.ListenerEvents.V2
{
    internal interface IListenerEventV2ProcessingService
    {
        ValueTask<ListenerEventV1> AddListenerEventV2Async(ListenerEventV1 listenerEventV2);
        ValueTask<IQueryable<ListenerEventV1>> RetrieveAllListenerEventV2sAsync();
        ValueTask<ListenerEventV1> ModifyListenerEventV2Async(ListenerEventV1 listenerEventV2);
        ValueTask<ListenerEventV1> RemoveListenerEventV2ByIdAsync(Guid listenerEventV2Id);
    }
}
