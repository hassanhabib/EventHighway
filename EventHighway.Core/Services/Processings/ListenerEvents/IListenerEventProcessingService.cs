// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHighway.Core.Models.ListenerEvents;

namespace EventHighway.Core.Services.Processings.ListenerEvents
{
    internal interface IListenerEventProcessingService
    {
        ValueTask<ListenerEvent> AddListenerEventAsync(ListenerEvent listenerEvent);
    }
}
