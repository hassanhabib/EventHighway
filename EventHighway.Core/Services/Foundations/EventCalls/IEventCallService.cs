// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall;

namespace EventHighway.Core.Services.Foundations.EventCalls
{
    internal interface IEventCallService
    {
        ValueTask<EventCall> RunAsync(EventCall eventCall);
    }
}
