// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Processings.EventListeners.V2.Exceptions;
using EventHighway.Core.Models.Services.Processings.ListenerEvents.V2.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Orchestrations.EventListeners.V2
{
    internal partial class EventListenerV2OrchestrationService
    {
        private delegate ValueTask<IQueryable<EventListenerV2>> ReturningEventListenerV2sFunction();
        private delegate ValueTask<ListenerEventV2> ReturningListenerEventV2Function();
        private delegate ValueTask<IQueryable<ListenerEventV2>> ReturningListenerEventV2sFunction();

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
            catch (EventListenerV2ProcessingValidationException
                eventListenerV2ProcessingValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventListenerV2ProcessingValidationException);
            }
            catch (EventListenerV2ProcessingDependencyException
                eventListenerV2ProcessingDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV2ProcessingDependencyException);
            }
            catch (EventListenerV2ProcessingServiceException
                eventListenerV2ProcessingServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV2ProcessingServiceException);
            }
            catch (Exception exception)
            {
                var failedEventListenerV2OrchestrationServiceException =
                    new FailedEventListenerV2OrchestrationServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV2OrchestrationServiceException);
            }
        }

        private async ValueTask<ListenerEventV2> TryCatch(
            ReturningListenerEventV2Function returningListenerEventV2Function)
        {
            try
            {
                return await returningListenerEventV2Function();
            }
            catch (NullListenerEventV2OrchestrationException
                nullListenerEventV2OrchestrationException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    nullListenerEventV2OrchestrationException);
            }
            catch (ListenerEventV2ProcessingValidationException
                listenerEventV2ProcessingValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    listenerEventV2ProcessingValidationException);
            }
            catch (ListenerEventV2ProcessingDependencyValidationException
                listenerEventV2ProcessingDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    listenerEventV2ProcessingDependencyValidationException);
            }
            catch (ListenerEventV2ProcessingDependencyException
                listenerEventV2ProcessingDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    listenerEventV2ProcessingDependencyException);
            }
            catch (ListenerEventV2ProcessingServiceException
                listenerEventV2ProcessingServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    listenerEventV2ProcessingServiceException);
            }
            catch (Exception exception)
            {
                var failedEventListenerV2OrchestrationServiceException =
                    new FailedEventListenerV2OrchestrationServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV2OrchestrationServiceException);
            }
        }

        private async ValueTask<IQueryable<ListenerEventV2>> TryCatch(
            ReturningListenerEventV2sFunction returningListenerEventV2sFunction)
        {
            try
            {
                return await returningListenerEventV2sFunction();
            }
            catch (ListenerEventV2ProcessingDependencyException
                listenerEventV2ProcessingDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    listenerEventV2ProcessingDependencyException);
            }
            catch (ListenerEventV2ProcessingServiceException
                listenerEventV2ProcessingServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    listenerEventV2ProcessingServiceException);
            }
            catch (Exception exception)
            {
                var failedEventListenerV2OrchestrationServiceException =
                    new FailedEventListenerV2OrchestrationServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV2OrchestrationServiceException);
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

        private async ValueTask<EventListenerV2OrchestrationDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(
                Xeption exception)
        {
            var eventListenerV2OrchestrationDependencyValidationException =
                new EventListenerV2OrchestrationDependencyValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventListenerV2OrchestrationDependencyValidationException);

            return eventListenerV2OrchestrationDependencyValidationException;
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
