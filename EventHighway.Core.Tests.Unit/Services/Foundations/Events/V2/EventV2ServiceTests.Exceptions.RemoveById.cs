// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using EventHighway.Core.Models.Services.Foundations.Events.V2.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V2
{
    public partial class EventV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someEventV2Id = GetRandomId();
            SqlException sqlException = GetSqlException();

            var failedEventV2StorageException =
                new FailedEventV2StorageException(
                    message: "Failed event storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventV2DependencyException =
                new EventV2DependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: failedEventV2StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<EventV2> removeEventV2ByIdTask =
                this.eventV2Service.RemoveEventV2ByIdAsync(
                    someEventV2Id);

            EventV2DependencyException actualEventV2DependencyException =
                await Assert.ThrowsAsync<EventV2DependencyException>(
                    removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2DependencyException.Should()
                .BeEquivalentTo(expectedEventV2DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationErrorOnRemoveByIdIfDbUpdateConcurrencyErrorAndLogItAsync()
        {
            // given
            Guid someEventV2Id = GetRandomId();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedEventV2Exception =
                new LockedEventV2Exception(
                    message: "Event is locked, try again.",
                    innerException: dbUpdateConcurrencyException);

            var expectedEventV2DependencyValidationException =
                new EventV2DependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: lockedEventV2Exception);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            // when
            ValueTask<EventV2> removeEventV2ByIdTask =
                this.eventV2Service.RemoveEventV2ByIdAsync(
                    someEventV2Id);

            EventV2DependencyValidationException actualEventV2DependencyValidationException =
                await Assert.ThrowsAsync<EventV2DependencyValidationException>(
                    removeEventV2ByIdTask.AsTask);

            // then
            actualEventV2DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2DependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventV2ByIdAsync(
                    It.IsAny<Guid>()),
                        Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
