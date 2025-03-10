// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2.Exceptions;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace EventHighway.Core.Services.Foundations.EventListeners.V2
{
    internal partial class EventListenerV2Service
    {
        private delegate ValueTask<IQueryable<EventListenerV2>> ReturningEventListenerV2sFunction();

        private async ValueTask<IQueryable<EventListenerV2>> TryCatch(
            ReturningEventListenerV2sFunction returningEventListenerV2sFunction)
        {
            try
            {
                return await returningEventListenerV2sFunction();
            }
            catch (SqlException sqlException)
            {
                var failedEventListenerV2StorageException =
                    new FailedEventListenerV2StorageException(
                        message: "Failed event listener storage error occurred, contact support.",
                        innerException: sqlException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(
                    failedEventListenerV2StorageException);
            }
            catch (Exception serviceException)
            {
                var failedEventListenerV2ServiceException =
                    new FailedEventListenerV2ServiceException(
                        message: "Failed event listener service error occurred, contact support.",
                        innerException: serviceException);

                throw await CreateAndLogServiceExceptionAsync(
                    failedEventListenerV2ServiceException);
            }
        }

        private async ValueTask<EventListenerV2DependencyException> CreateAndLogCriticalDependencyExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2DependencyException =
                new EventListenerV2DependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogCriticalAsync(eventListenerV2DependencyException);

            return eventListenerV2DependencyException;
        }

        private async ValueTask<EventListenerV2ServiceException> CreateAndLogServiceExceptionAsync(
            Xeption exception)
        {
            var eventListenerV2ServiceException =
                new EventListenerV2ServiceException(
                    message: "Event listener service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventListenerV2ServiceException);

            return eventListenerV2ServiceException;
        }
    }
}
