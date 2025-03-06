// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.Processings.EventListeners.V2.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Processings.EventListeners.V2
{
    internal partial class EventListenerV2ProcessingService
    {
        private delegate ValueTask<IQueryable<EventListenerV2>> ReturningEventListenerV2sFunction();

        private async ValueTask<IQueryable<EventListenerV2>> TryCatch(
            ReturningEventListenerV2sFunction returningEventListenerV2sFunction)
        {
            try
            {
                return await returningEventListenerV2sFunction();
            }
            catch (InvalidEventListenerV2ProcessingException
                invalidEventListenerV2ProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventListenerV2ProcessingException);
            }
        }

        private async ValueTask<EventListenerV2ProcessingValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2ProcessingValidationException =
                new EventListenerV2ProcessingValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV2ProcessingValidationException);

            return eventListenerV2ProcessingValidationException;
        }

        private async ValueTask<EventListenerV2ProcessingDependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2ProcessingDependencyException =
                new EventListenerV2ProcessingDependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventListenerV2ProcessingDependencyException);

            return eventListenerV2ProcessingDependencyException;
        }

        private async ValueTask<EventListenerV2ProcessingServiceException> CreateAndLogServiceExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2ProcessingServiceException =
                new EventListenerV2ProcessingServiceException(
                    message: "Event listener service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV2ProcessingServiceException);

            return eventListenerV2ProcessingServiceException;
        }
    }
}
