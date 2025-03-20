// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;

namespace EventHighway.Core.Clients.ListenerEvents.V2
{
    public interface IListenerEventV2sClient
    {
        ValueTask<IQueryable<ListenerEventV2>> RetrieveAllListenerEventV2sAsync();
    }
}
