// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2;
using EventHighway.Core.Models.Services.Foundations.EventListeners.V2.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventListeners.V2
{
    public partial class EventListenerV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someEventListenerV2Id = GetRandomId();
            SqlException sqlException = GetSqlException();

            var failedEventListenerV2StorageException =
                new FailedEventListenerV2StorageException(
                    message: "Failed event listener storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventListenerV2DependencyException =
                new EventListenerV2DependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: failedEventListenerV2StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventListenerV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<EventListenerV2> removeEventListenerV2ByIdTask =
                this.eventListenerV2Service.RemoveEventListenerV2ByIdAsync(
                    someEventListenerV2Id);

            EventListenerV2DependencyException actualEventListenerV2DependencyException =
                await Assert.ThrowsAsync<EventListenerV2DependencyException>(
                    removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2DependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventListenerV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationErrorOnRemoveByIdIfDbUpdateConcurrencyErrorAndLogItAsync()
        {
            // given
            Guid someEventListenerV2Id = GetRandomId();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedEventListenerV2Exception =
                new LockedEventListenerV2Exception(
                    message: "Event listener is locked, try again.",
                    innerException: dbUpdateConcurrencyException);

            var expectedEventListenerV2DependencyValidationException =
                new EventListenerV2DependencyValidationException(
                    message: "Event listener validation error occurred, fix the errors and try again.",
                    innerException: lockedEventListenerV2Exception);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventListenerV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            // when
            ValueTask<EventListenerV2> removeEventListenerV2ByIdTask =
                this.eventListenerV2Service.RemoveEventListenerV2ByIdAsync(
                    someEventListenerV2Id);

            EventListenerV2DependencyValidationException actualEventListenerV2DependencyValidationException =
                await Assert.ThrowsAsync<EventListenerV2DependencyValidationException>(
                    removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventListenerV2DependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventListenerV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            Guid someEventListenerV2Id = GetRandomId();
            var dbUpdateException = new DbUpdateException();

            var failedEventListenerV2StorageException =
                new FailedEventListenerV2StorageException(
                    message: "Failed event listener storage error occurred, contact support.",
                    innerException: dbUpdateException);

            var expectedEventListenerV2DependencyException =
                new EventListenerV2DependencyException(
                    message: "Event listener dependency error occurred, contact support.",
                    innerException: failedEventListenerV2StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventListenerV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateException);

            // when
            ValueTask<EventListenerV2> removeEventListenerV2ByIdTask =
                this.eventListenerV2Service.RemoveEventListenerV2ByIdAsync(someEventListenerV2Id);

            EventListenerV2DependencyException actualEventListenerV2DependencyException =
                await Assert.ThrowsAsync<EventListenerV2DependencyException>(
                    removeEventListenerV2ByIdTask.AsTask);

            // then
            actualEventListenerV2DependencyException.Should()
                .BeEquivalentTo(expectedEventListenerV2DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventListenerV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventListenerV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
