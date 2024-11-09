// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.EventAddresses;
using EventHighway.Core.Models.EventAddresses.Exceptions;

namespace EventHighway.Core.Services.Foundations.EventAddresses
{
    internal partial class EventAddressService
    {
        private void ValidateEventAddressOnAdd(EventAddress eventAddress)
        {
            ValidateEventAddressIsNotNull(eventAddress);
        }

        private void ValidateEventAddressIsNotNull(EventAddress eventAddress) =>
            _ = eventAddress ?? throw new NullEventAddressException(message: "EventAddress is null");
    }}
