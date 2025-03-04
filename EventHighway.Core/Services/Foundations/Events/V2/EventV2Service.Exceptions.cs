// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Models.Events.V2.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Foundations.Events.V2
{
    internal partial class EventV2Service
    {
        private delegate ValueTask<EventV2> ReturningEventV2Function();

        private async ValueTask<EventV2> TryCatch(ReturningEventV2Function returningEventV2Function)
        {
            try
            {
                return await returningEventV2Function();
            }
            catch (NullEventV2Exception nullEventV2Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(nullEventV2Exception);
            }
        }

        private async ValueTask<EventV2ValidationException> CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var eventV2ValidationException =
                new EventV2ValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2ValidationException);

            return eventV2ValidationException;
        }
    }
}
