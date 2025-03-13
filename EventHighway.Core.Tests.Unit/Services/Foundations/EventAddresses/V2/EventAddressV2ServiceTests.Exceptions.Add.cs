// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventAddresses.V2
{
    public partial class EventAddressV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlExceptionOccursAndLogItAsync()
        {
            // given
            EventAddressV2 someEventAddressV2 = CreateRandomEventAddressV2();
            SqlException sqlException = CreateSqlException();

            var failedEventAddressV2StorageException =
                new FailedEventAddressV2StorageException(
                    message: "Failed event address storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventAddressV2DependencyException =
                new EventAddressV2DependencyException(
                    message: "Event address dependency error occurred, contact support.",
                    innerException: failedEventAddressV2StorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<EventAddressV2> addEventAddressV2Task =
                this.eventAddressV2Service.AddEventAddressV2Async(someEventAddressV2);

            EventAddressV2DependencyException actualEventAddressV2DependencyException =
                await Assert.ThrowsAsync<EventAddressV2DependencyException>(
                    addEventAddressV2Task.AsTask);

            // then
            actualEventAddressV2DependencyException.Should()
                .BeEquivalentTo(expectedEventAddressV2DependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressV2Async(It.IsAny<EventAddressV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfEventAddressV2AlreadyExistsAndLogItAsync()
        {
            // given
            string randomMessage = GetRandomString();
            EventAddressV2 someEventAddressV2 = CreateRandomEventAddressV2();
            var duplicateKeyException = new DuplicateKeyException(randomMessage);

            var alreadyExistsEventAddressV2Exception =
                new AlreadyExistsEventAddressV2Exception(
                    message: "Event address with the same id already exists.",
                    innerException: duplicateKeyException);

            var expectedEventAddressV2DependencyValidationException =
                new EventAddressV2DependencyValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: alreadyExistsEventAddressV2Exception);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetDateTimeOffsetAsync())
                    .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<EventAddressV2> addEventAddressV2Task =
                this.eventAddressV2Service.AddEventAddressV2Async(someEventAddressV2);

            EventAddressV2DependencyValidationException actualEventAddressV2DependencyValidationException =
                await Assert.ThrowsAsync<EventAddressV2DependencyValidationException>(
                    addEventAddressV2Task.AsTask);

            // then
            actualEventAddressV2DependencyValidationException.Should()
                .BeEquivalentTo(expectedEventAddressV2DependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetDateTimeOffsetAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2DependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressV2Async(It.IsAny<EventAddressV2>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
