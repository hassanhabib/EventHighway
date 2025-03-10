// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Models.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Processings.Events.V2.Exceptions;
using Xeptions;

namespace EventHighway.Core.Services.Processings.Events.V2
{
    internal partial class EventV2ProcessingService
    {
        private delegate ValueTask<IQueryable<EventV2>> ReturningEventV2sFunction();

        private async ValueTask<IQueryable<EventV2>> TryCatch(ReturningEventV2sFunction returningEventV2sFunction)
        {
            try
            {
                return await returningEventV2sFunction();
            }
            catch (EventV2DependencyException eventV2DependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(eventV2DependencyException);
            }
            catch (EventV2ServiceException eventV2ServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(eventV2ServiceException);
            }
            catch (Exception exception)
            {
                var failedEventV2ProcessingServiceException =
                    new FailedEventV2ProcessingServiceException(
                        message: "Failed event service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedEventV2ProcessingServiceException);
            }
        }

        private async ValueTask<EventV2ProcessingDependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventV2ProcessingDependencyException =
                new EventV2ProcessingDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(eventV2ProcessingDependencyException);

            return eventV2ProcessingDependencyException;
        }

        private async ValueTask<EventV2ProcessingServiceException> CreateAndLogServiceExceptionAsync(
            Xeption exception)
        {
            var eventV2ProcessingServiceException =
                new EventV2ProcessingServiceException(
                    message: "Event service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventV2ProcessingServiceException);

            return eventV2ProcessingServiceException;
        }
    }
}
