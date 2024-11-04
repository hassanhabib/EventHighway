// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners;

namespace EventHighway.Core.Services.Processings.EventListeners
{
    internal interface IEventListenerProcessingService
    {
        ValueTask<IQueryable<EventListener>> RetrieveEventListenersByAddressIdAsync(Guid addressId);
    }
}
