// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.EventAddresses;
using EventHighway.Core.Models.EventAddresses.Exceptions;
using System.Threading.Tasks;
using Xeptions;

namespace EventHighway.Core.Services.Foundations.EventAddresses
{
    internal partial class EventAddressService
    {
        private delegate ValueTask<EventAddress> ReturningEventAddressFunction();
        private async ValueTask<EventAddress> TryCatch(ReturningEventAddressFunction returningConfigurationFunction)
        {
            try
            {
                return await returningConfigurationFunction();
            }
            catch (NullEventAddressException nullEventAddressException)
            {
                throw CreateAndLogValidationException(nullEventAddressException);
            }
        }
        private EventAddressValidationException CreateAndLogValidationException(Xeption innerException)
        {
            var configurationValidationException =
                new EventAddressValidationException(
                    message: "EventAddress validation error occurred, fix errors and try again",
                    innerException: innerException);
            this.loggingBroker.LogErrorAsync(configurationValidationException);
            return configurationValidationException;
        }
    }
}