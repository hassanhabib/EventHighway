// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V1.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.Foundations.EventAddresses.V2
{
    public partial class EventAddressV2ServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someEventAddressV2Id = GetRandomId();
            SqlException sqlException = CreateSqlException();

            var failedEventAddressV2StorageException =
                new FailedEventAddressV2StorageException(
                    message: "Failed event address storage error occurred, contact support.",
                    innerException: sqlException);

            var expectedEventAddressV2DependencyException =
                new EventAddressV2DependencyException(
                    message: "Event address dependency error occurred, contact support.",
                    innerException: failedEventAddressV2StorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventAddressV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<EventAddressV1> retrieveEventAddressV2ByIdTask =
                this.eventAddressV2Service.RetrieveEventAddressV2ByIdAsync(
                    someEventAddressV2Id);

            EventAddressV2DependencyException actualEventAddressV2DependencyException =
                await Assert.ThrowsAsync<EventAddressV2DependencyException>(
                    retrieveEventAddressV2ByIdTask.AsTask);

            // then
            actualEventAddressV2DependencyException.Should()
                .BeEquivalentTo(expectedEventAddressV2DependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventAddressV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2DependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByIdIfExceptionOccursAndLogItAsync()
        {
            // given
            Guid someEventAddressV2Id = GetRandomId();
            var serviceException = new Exception();

            var failedEventAddressV2ServiceException =
                new FailedEventAddressV2ServiceException(
                    message: "Failed event address service error occurred, contact support.",
                    innerException: serviceException);

            var expectedEventAddressV2ServiceException =
                new EventAddressV2ServiceException(
                    message: "Event address service error occurred, contact support.",
                    innerException: failedEventAddressV2ServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectEventAddressV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<EventAddressV1> retrieveEventAddressV2ByIdTask =
                this.eventAddressV2Service.RetrieveEventAddressV2ByIdAsync(
                    someEventAddressV2Id);

            EventAddressV2ServiceException actualEventAddressV2ServiceException =
                await Assert.ThrowsAsync<EventAddressV2ServiceException>(
                    retrieveEventAddressV2ByIdTask.AsTask);

            // then
            actualEventAddressV2ServiceException.Should()
                .BeEquivalentTo(expectedEventAddressV2ServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectEventAddressV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2ServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
