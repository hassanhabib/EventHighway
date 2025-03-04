// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using EventHighway.Core.Models.Events.V2;
using EventHighway.Core.Models.Events.V2.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.Events.V2
{
    public partial class EventV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlExceptionOccursAndLogItAsync()
        {
            // given
            EventV2 someEventV2 = CreateRandomEventV2();
            SqlException sqlException = GetSqlException();

            var failedEventV2StorageException =
                new FailedEventV2StorageException(
                    message: "Failed event storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventV2DependencyException =
                new EventV2DependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: failedEventV2StorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<EventV2> addEventV2Task =
                this.eventV2Service.AddEventV2Async(someEventV2);

            EventV2DependencyException actualEventV2DependencyException =
                await Assert.ThrowsAsync<EventV2DependencyException>(
                    addEventV2Task.AsTask);

            // then
            actualEventV2DependencyException.Should()
                .BeEquivalentTo(expectedEventV2DependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV2Async(It.IsAny<EventV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfEventV2AlreadyExistsAndLogItAsync()
        {
            // given
            string randomMessage = GetRandomString();
            EventV2 someEventV2 = CreateRandomEventV2();
            var duplicateKeyException = new DuplicateKeyException(randomMessage);

            var alreadyExistsEventV2Exception =
                new AlreadyExistsEventV2Exception(
                    message: "Event with the same id already exists.",
                    innerException: duplicateKeyException);

            var expectedEventV2DependencyValidationException =
                new EventV2DependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: alreadyExistsEventV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<EventV2> addEventV2Task =
                this.eventV2Service.AddEventV2Async(someEventV2);

            EventV2DependencyValidationException actualEventV2DependencyValidationException =
                await Assert.ThrowsAsync<EventV2DependencyValidationException>(
                    addEventV2Task.AsTask);

            // then
            actualEventV2DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2DependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV2Async(It.IsAny<EventV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDbUpdateExceptionOccursAndLogItAsync()
        {
            // given
            EventV2 someEventV2 = CreateRandomEventV2();
            var dbUpdateException = new DbUpdateException();

            var failedEventV2StorageException =
                new FailedEventV2StorageException(
                    message: "Failed event storage error occurred, contact support.",
                    innerException: dbUpdateException);

            var expectedEventV2DependencyException =
                new EventV2DependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: failedEventV2StorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(dbUpdateException);

            // when
            ValueTask<EventV2> addEventV2Task =
                this.eventV2Service.AddEventV2Async(someEventV2);

            EventV2DependencyException actualEventV2DependencyException =
                await Assert.ThrowsAsync<EventV2DependencyException>(
                    addEventV2Task.AsTask);

            // then
            actualEventV2DependencyException.Should()
                .BeEquivalentTo(expectedEventV2DependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventV2Async(It.IsAny<EventV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
