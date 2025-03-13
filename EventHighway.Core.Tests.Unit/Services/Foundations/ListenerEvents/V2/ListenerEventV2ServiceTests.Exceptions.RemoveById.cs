// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2;
using EventHighway.Core.Models.Services.Foundations.ListenerEvents.V2.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.ListenerEvents.V2
{
    public partial class ListenerEventV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();
            SqlException sqlException = GetSqlException();

            var failedListenerEventV2StorageException =
                new FailedListenerEventV2StorageException(
                    message: "Failed listener event storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedListenerEventV2DependencyException =
                new ListenerEventV2DependencyException(
                    message: "Listener event dependency error occurred, contact support.",
                    innerException: failedListenerEventV2StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<ListenerEventV2> removeListenerEventV2ByIdTask =
                this.listenerEventV2Service.RemoveListenerEventV2ByIdAsync(
                    someListenerEventV2Id);

            ListenerEventV2DependencyException actualListenerEventV2DependencyException =
                await Assert.ThrowsAsync<ListenerEventV2DependencyException>(
                    removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2DependencyException.Should()
                .BeEquivalentTo(expectedListenerEventV2DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationErrorOnRemoveByIdIfDbUpdateConcurrencyErrorAndLogItAsync()
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedListenerEventV2Exception =
                new LockedListenerEventV2Exception(
                    message: "Listener event is locked, try again.",
                    innerException: dbUpdateConcurrencyException);

            var expectedListenerEventV2DependencyValidationException =
                new ListenerEventV2DependencyValidationException(
                    message: "Listener event validation error occurred, fix the errors and try again.",
                    innerException: lockedListenerEventV2Exception);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            // when
            ValueTask<ListenerEventV2> removeListenerEventV2ByIdTask =
                this.listenerEventV2Service.RemoveListenerEventV2ByIdAsync(
                    someListenerEventV2Id);

            ListenerEventV2DependencyValidationException actualListenerEventV2DependencyValidationException =
                await Assert.ThrowsAsync<ListenerEventV2DependencyValidationException>(
                    removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2DependencyValidationException.Should()
                .BeEquivalentTo(expectedListenerEventV2DependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();
            var dbUpdateException = new DbUpdateException();

            var failedListenerEventV2StorageException =
                new FailedListenerEventV2StorageException(
                    message: "Failed listener event storage error occurred, contact support.",
                    innerException: dbUpdateException);

            var expectedListenerEventV2DependencyException =
                new ListenerEventV2DependencyException(
                    message: "Listener event dependency error occurred, contact support.",
                    innerException: failedListenerEventV2StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateException);

            // when
            ValueTask<ListenerEventV2> removeListenerEventV2ByIdTask =
                this.listenerEventV2Service.RemoveListenerEventV2ByIdAsync(someListenerEventV2Id);

            ListenerEventV2DependencyException actualListenerEventV2DependencyException =
                await Assert.ThrowsAsync<ListenerEventV2DependencyException>(
                    removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2DependencyException.Should()
                .BeEquivalentTo(expectedListenerEventV2DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfExceptionOccursAndLogItAsync()
        {
            // given
            Guid someListenerEventV2Id = GetRandomId();
            var serviceException = new Exception();

            var failedListenerEventV2ServiceException =
                new FailedListenerEventV2ServiceException(
                    message: "Failed listener event service error occurred, contact support.",
                    innerException: serviceException);

            var expectedListenerEventV2ServiceException =
                new ListenerEventV2ServiceException(
                    message: "Listener event service error occurred, contact support.",
                    innerException: failedListenerEventV2ServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<ListenerEventV2> removeListenerEventV2ByIdTask =
                this.listenerEventV2Service.RemoveListenerEventV2ByIdAsync(
                    someListenerEventV2Id);

            ListenerEventV2ServiceException actualListenerEventV2ServiceException =
                await Assert.ThrowsAsync<ListenerEventV2ServiceException>(
                    removeListenerEventV2ByIdTask.AsTask);

            // then
            actualListenerEventV2ServiceException.Should()
                .BeEquivalentTo(expectedListenerEventV2ServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectListenerEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedListenerEventV2ServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
