// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses.V2;
using EventHighway.Core.Models.Services.Processings.EventAddresses.V2.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.EventAddresses.V2
{
    public partial class EventAddressV2ProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationOnRetrieveByIdIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            Guid someEventAddressV2Id = GetRandomId();

            var expectedEventAddressV2ProcessingDependencyValidationException =
                new EventAddressV2ProcessingDependencyValidationException(
                    message: "Event address validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventAddressV2ServiceMock.Setup(service =>
                service.RetrieveEventAddressV2ByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventAddressV2> retrieveEventAddressV2ByIdTask =
                this.eventAddressV2ProcessingService.RetrieveEventAddressV2ByIdAsync(
                    someEventAddressV2Id);

            EventAddressV2ProcessingDependencyValidationException
                actualEventAddressV2ProcessingDependencyValidationException =
                    await Assert.ThrowsAsync<EventAddressV2ProcessingDependencyValidationException>(
                        retrieveEventAddressV2ByIdTask.AsTask);

            // then
            actualEventAddressV2ProcessingDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventAddressV2ProcessingDependencyValidationException);

            this.eventAddressV2ServiceMock.Verify(service =>
                service.RetrieveEventAddressV2ByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventAddressV2ProcessingDependencyValidationException))),
                        Times.Once);

            this.eventAddressV2ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
