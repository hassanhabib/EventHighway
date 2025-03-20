// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Services.Foundations.EventAddresses.V2;

namespace EventHighway.Core.Clients.EventAddresses.V2
{
    internal class EventAddressesV2Client : IEventAddressesV2Client
    {
        private readonly IEventAddressV2Service eventAddressV2Service;

        public EventAddressesV2Client(IEventAddressV2Service eventAddressV2Service) =>
            this.eventAddressV2Service = eventAddressV2Service;

        public async ValueTask<EventAddressV2> RegisterEventAddressV2Async(EventAddressV2 eventAddressV2)
        {
            return await this.eventAddressV2Service.AddEventAddressV2Async(eventAddressV2);
        }
    }
}
