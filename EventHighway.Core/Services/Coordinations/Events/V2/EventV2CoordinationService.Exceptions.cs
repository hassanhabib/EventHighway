// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.EventCall.V2;
using Xeptions;

namespace EventHighway.Core.Services.Coordinations.Events.V2
{
    internal partial class EventV2CoordinationService
    {
        private delegate ValueTask ReturningNothingFunction();
        private delegate ValueTask<EventCallV2> ReturningEventCallV2Function();

        private async ValueTask TryCatch(ReturningNothingFunction returningNothingFunction)
        {
            try
            {
                await returningNothingFunction();
            }
            catch (NullListenerEventV2CoordinationException nullListenerEventV2CoordinationException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullListenerEventV2CoordinationException);
            }
            catch (Exception exception)
            {
                var failedEventV2CoordinationServiceException =
                    new FailedEventV2CoordinationServiceException(
                        message: "Failed event service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedEventV2CoordinationServiceException);
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
