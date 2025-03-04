// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using EventHighway.Core.Models.ListenerEvents.V2;
using EventHighway.Core.Models.ListenerEvents.V2.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xeptions;

namespace EventHighway.Core.Services.Foundations.ListernEvents.V2
{
    internal partial class ListenerEventV2Service
    {
        private delegate ValueTask<ListenerEventV2> ReturningListenerEventV2Function();

        private async ValueTask<ListenerEventV2> TryCatch(
            ReturningListenerEventV2Function returningListenerEventV2Function)
        {
            try
            {
                return await returningListenerEventV2Function();
            }
            catch (NullListenerEventV2Exception
                nullListenerEventV2Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    nullListenerEventV2Exception);
            }
            catch (InvalidListenerEventV2Exception
                invalidListenerEventV2Exception)
            {
                throw await CreateAndLogValidationExceptionAsync(
                    invalidListenerEventV2Exception);
            }
        }

        private async ValueTask<ListenerEventV2ValidationException> CreateAndLogValidationExceptionAsync(
            Xeption exception)
        {
            var listenerEventV2ValidationException =
                new ListenerEventV2ValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(listenerEventV2ValidationException);

            return listenerEventV2ValidationException;
        }

        private async ValueTask<ListenerEventV2DependencyException> CreateAndLogCriticalDependencyExceptionAsync(
            Xeption exception)
        {
            var listenerEventV2DependencyException =
                new ListenerEventV2DependencyException(
                    message: "Listener event dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogCriticalAsync(listenerEventV2DependencyException);

            return listenerEventV2DependencyException;
        }

        private async ValueTask<ListenerEventV2DependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(
                Xeption exception)
        {
            var listenerEventV2DependencyValidationException =
                new ListenerEventV2DependencyValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(listenerEventV2DependencyValidationException);

            return listenerEventV2DependencyValidationException;
        }

        private async ValueTask<ListenerEventV2DependencyException> CreateAndLogDependencyExceptionAsync(
            Xeption exception)
        {
            var listenerEventV2DependencyException =
                new ListenerEventV2DependencyException(
                    message: "Listener event dependency error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(listenerEventV2DependencyException);

            return listenerEventV2DependencyException;
        }

        private async ValueTask<ListenerEventV2ServiceException> CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var listenerEventV2ServiceException =
                new ListenerEventV2ServiceException(
                    message: "Listener event service error occurred, contact support.",
                    innerException: exception);

            await this.loggingBroker.LogErrorAsync(listenerEventV2ServiceException);

            return listenerEventV2ServiceException;
        }
    }
}
