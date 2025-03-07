// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.EventCall.V2;
using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Models.Orchestrations.Events.V2.Exceptions;
using EventHighway.Core.Models.Processings.Events.V2.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Orchestrations.Events.V2
{
    internal partial class EventV2OrchestrationService
    {
        private delegate ValueTask<IQueryable<EventV2>> ReturningEventV2sFunction();
        private delegate ValueTask<EventCallV2> ReturningEventCallV2Function();

        private async ValueTask<IQueryable<EventV2>> TryCatch(ReturningEventV2sFunction returningEventV2sFunction)
        {
            try
            {
                return await returningEventV2sFunction();
            }
            catch (EventV2ProcessingDependencyException eventV2ProcessingDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(eventV2ProcessingDependencyException);
            }
            catch (EventV2ProcessingServiceException eventV2ProcessingServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(eventV2ProcessingServiceException);
            }
            catch (Exception exception)
            {
                var failedEventV2OrchestrationServiceException =
                    new FailedEventV2OrchestrationServiceException(
                        message: "Failed event service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedEventV2OrchestrationServiceException);
            }
        }

        private async ValueTask<EventCallV2> TryCatch(ReturningEventCallV2Function returningEventCallV2Function)
        {
            try
            {
                return await returningEventCallV2Function();
            }
            catch (NullEventCallV2OrchestrationException nullEventCallV2OrchestrationException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullEventCallV2OrchestrationException);
            }
        }

        private async ValueTask<EventV2OrchestrationValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var eventV2OrchestrationValidationException =
                new EventV2OrchestrationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2OrchestrationValidationException);

            return eventV2OrchestrationValidationException;
        }

        private async ValueTask<EventV2OrchestrationDependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventV2OrchestrationDependencyException =
                new EventV2OrchestrationDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventV2OrchestrationDependencyException);

            return eventV2OrchestrationDependencyException;
        }

        private async ValueTask<EventV2OrchestrationServiceException> CreateAndLogServiceExceptionAsync(
            Xeption exception)
        {
            var eventV2OrchestrationServiceException =
                new EventV2OrchestrationServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2OrchestrationServiceException);

            return eventV2OrchestrationServiceException;
        }
    }
}
