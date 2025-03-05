// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall;
using EventHighway.Core.Models.EventCall.V2;

namespace EventHighway.Core.Services.Foundations.EventCalls.V2
{
    internal interface IEventCallV2Service
    {
        ValueTask<EventCallV2> RunEventCallV2Async(EventCallV2 eventCallV2);
    }
}
