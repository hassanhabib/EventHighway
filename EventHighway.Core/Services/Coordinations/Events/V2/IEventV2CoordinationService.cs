// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace EventHighway.Core.Services.Coordinations.Events.V2
{
    internal interface IEventV2CoordinationService
    {
        ValueTask FireScheduledPendingEventV2sAsync();
    }
}
