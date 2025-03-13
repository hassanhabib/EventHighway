// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace EventHighway.Core.Services.Foundations.EventAddresses.V2
{
    internal partial class EventAddressV2Service
    {
        private delegate ValueTask<EventAddressV2> ReturningEventAddressV2Function();

        private async ValueTask<EventAddressV2> TryCatch(
            ReturningEventAddressV2Function returningEventAddressV2Function)
        {
            try
            {
                return await returningEventAddressV2Function();
            }
            catch (InvalidEventAddressV2Exception invalidEventAddressV2Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidEventAddressV2Exception);
            }
        }

        private async ValueTask<EventAddressV2ValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var eventAddressV2ValidationException =
                new EventAddressV2ValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV2ValidationException);

            return eventAddressV2ValidationException;
        }

        private async ValueTask<EventAddressV2DependencyException> CreateAndLogCriticalDependencyExceptionAsync(
            Xeption exception)
        {
            var eventAddressV2DependencyException =
                new EventAddressV2DependencyException(
                    message: "Event address dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogCriticalAsync(eventAddressV2DependencyException);

            return eventAddressV2DependencyException;
        }

        private async ValueTask<EventAddressV2DependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(
                Xeption exception)
        {
            var eventAddressV2DependencyValidationException =
                new EventAddressV2DependencyValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV2DependencyValidationException);

            return eventAddressV2DependencyValidationException;
        }

        private async ValueTask<EventAddressV2DependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var eventAddressV2DependencyException =
                new EventAddressV2DependencyException(
                    message: "Event address dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV2DependencyException);

            return eventAddressV2DependencyException;
        }

        private async ValueTask<EventAddressV2ServiceException> CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var eventAddressV2ServiceException =
                new EventAddressV2ServiceException(
                    message: "Event address service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(eventAddressV2ServiceException);

            return eventAddressV2ServiceException;
        }
    }
}
