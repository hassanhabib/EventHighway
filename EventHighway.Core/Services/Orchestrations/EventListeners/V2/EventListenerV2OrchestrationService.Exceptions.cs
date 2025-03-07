// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventListeners.V2;
using EventHighway.Core.Models.Orchestrations.EventListeners.V2.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V2
{
    internal partial class EventListenerV2OrchestrationService
    {
        private delegate ValueTask<IQueryable<EventListenerV2>> ReturningEventListenerV2sFunction();

        private async ValueTask<IQueryable<EventListenerV2>> TryCatch(
            ReturningEventListenerV2sFunction returningEventListenerV2sFunction)
        {
            try
            {
                return await returningEventListenerV2sFunction();
            }
            catch (InvalidEventListenerV2OrchestrationException
                invalidEventListenerV2OrchestrationException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventListenerV2OrchestrationException);
            }
        }

        private async ValueTask<EventListenerV2OrchestrationValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2OrchestrationValidationException =
                new EventListenerV2OrchestrationValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV2OrchestrationValidationException);

            return eventListenerV2OrchestrationValidationException;
        }

        private async ValueTask<EventListenerV2OrchestrationDependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2OrchestrationDependencyException =
                new EventListenerV2OrchestrationDependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventListenerV2OrchestrationDependencyException);

            return eventListenerV2OrchestrationDependencyException;
        }

        private async ValueTask<EventListenerV2OrchestrationServiceException> CreateAndLogServiceExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2OrchestrationServiceException =
                new EventListenerV2OrchestrationServiceException(
                    message: "Event listener service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV2OrchestrationServiceException);

            return eventListenerV2OrchestrationServiceException;
        }
    }
}
