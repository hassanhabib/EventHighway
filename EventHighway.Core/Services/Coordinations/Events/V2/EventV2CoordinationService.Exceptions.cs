// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Orchestrations.EventListeners.V1.Exceptions;
using EventHighway.Core.Models.Services.Orchestrations.Events.V1.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Coordinations.Events.V2
{
    internal partial class EventV2CoordinationService
    {
        private delegate ValueTask<EventV1> ReturningEventV2Function();
        private delegate ValueTask ReturningNothingFunction();

        private async ValueTask<EventV1> TryCatch(ReturningEventV2Function returningEventV2Function)
        {
            try
            {
                return await returningEventV2Function();
            }
            catch (NullEventV2CoordinationException
                nullEventV2CoordinationException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    nullEventV2CoordinationException);
            }
            catch (InvalidEventV2CoordinationException
                invalidEventV2CoordinationException)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventV2CoordinationException);
            }
            catch (EventV1OrchestrationValidationException
                eventV2OrchestrationValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventV2OrchestrationValidationException);
            }
            catch (EventV1OrchestrationDependencyValidationException
                eventV2OrchestrationDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventV2OrchestrationDependencyValidationException);
            }
            catch (EventListenerV1OrchestrationValidationException
                eventListenerV2OrchestrationValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventListenerV2OrchestrationValidationException);
            }
            catch (EventListenerV1OrchestrationDependencyValidationException
                eventListenerV2OrchestrationDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventListenerV2OrchestrationDependencyValidationException);
            }
            catch (EventV1OrchestrationDependencyException
                eventV2OrchestrationDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventV2OrchestrationDependencyException);
            }
            catch (EventV1OrchestrationServiceException
                eventV2OrchestrationServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventV2OrchestrationServiceException);
            }
            catch (EventListenerV1OrchestrationDependencyException
                eventListenerV2OrchestrationDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV2OrchestrationDependencyException);
            }
            catch (EventListenerV1OrchestrationServiceException
                eventListenerV2OrchestrationServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV2OrchestrationServiceException);
            }
            catch (Exception exception)
            {
                var failedEventV2CoordinationServiceException =
                    new FailedEventV2CoordinationServiceException(
                        message: "Failed event service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventV2CoordinationServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                await returningNothingFunction();
            }
            catch (EventV1OrchestrationDependencyException
                eventV2OrchestrationDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventV2OrchestrationDependencyException);
            }
            catch (EventV1OrchestrationServiceException
                eventV2OrchestrationServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventV2OrchestrationServiceException);
            }
            catch (EventListenerV1OrchestrationDependencyException
                eventListenerV2OrchestrationDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV2OrchestrationDependencyException);
            }
            catch (EventListenerV1OrchestrationServiceException
                eventListenerV2OrchestrationServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(
                    eventListenerV2OrchestrationServiceException);
            }
            catch (EventV1OrchestrationValidationException
                eventV2OrchestrationValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventV2OrchestrationValidationException);
            }
            catch (EventV1OrchestrationDependencyValidationException
                eventV2OrchestrationDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventV2OrchestrationDependencyValidationException);
            }
            catch (EventListenerV1OrchestrationValidationException
                eventListenerV2OrchestrationValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventListenerV2OrchestrationValidationException);
            }
            catch (EventListenerV1OrchestrationDependencyValidationException
                eventListenerV2OrchestrationDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(
                    eventListenerV2OrchestrationDependencyValidationException);
            }
            catch (Exception exception)
            {
                var failedEventV2CoordinationServiceException =
                    new FailedEventV2CoordinationServiceException(
                        message: "Failed event service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventV2CoordinationServiceException);
            }
        }

        private async ValueTask<EventV2CoordinationValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var eventV2CoordinationValidationException =
                new EventV2CoordinationValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2CoordinationValidationException);

            return eventV2CoordinationValidationException;
        }

        private async ValueTask<EventV2CoordinationDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(
                Xeption exception)
        {
            var eventV2CoordinationDependencyValidationException =
                new EventV2CoordinationDependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventV2CoordinationDependencyValidationException);

            return eventV2CoordinationDependencyValidationException;
        }

        private async ValueTask<EventV2CoordinationDependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventV2CoordinationDependencyException =
                new EventV2CoordinationDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventV2CoordinationDependencyException);

            return eventV2CoordinationDependencyException;
        }

        private async ValueTask<EventV2CoordinationServiceException> CreateAndLogServiceExceptionAsync(
            Xeption exception)
        {
            var eventV2CoordinationServiceException =
                new EventV2CoordinationServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2CoordinationServiceException);

            return eventV2CoordinationServiceException;
        }
    }
}
