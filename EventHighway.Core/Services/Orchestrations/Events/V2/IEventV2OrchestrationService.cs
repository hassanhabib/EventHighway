// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventCall.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2;

namespace EventHighway.Core.Services.Orchestrations.Events.V2
{
    internal interface IEventV2OrchestrationService
    {
        ValueTask<EventV2> SubmitEventV2Async(EventV2 eventV2);
        ValueTask<IQueryable<EventV2>> RetrieveScheduledPendingEventV2sAsync();
        ValueTask<EventCallV2> RunEventCallV2Async(EventCallV2 eventCallV2);
    }
}
