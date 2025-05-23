﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.Events.V1;
using EventHighway.Core.Models.Services.Processings.Events.V1.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Services.Processings.Events.V1
{
    public partial class EventV1ProcessingServiceTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowDependencyValidationErrorOnModifyIfDependencyValidationErrorOccursAndLogItAsync(
            Xeption validationException)
        {
            // given
            EventV1 someEventV1 = CreateRandomEventV1();

            var expectedEventV1ProcessingDependencyValidationException =
                new EventV1ProcessingDependencyValidationException(
                    message: "Event validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption);

            this.eventV1ServiceMock.Setup(service =>
                service.ModifyEventV1Async(It.IsAny<EventV1>()))
                    .ThrowsAsync(validationException);

            // when
            ValueTask<EventV1> modifyEventV1Task =
                this.eventV1ProcessingService.ModifyEventV1Async(someEventV1);

            EventV1ProcessingDependencyValidationException
                actualEventV1ProcessingDependencyValidationException =
                    await Assert.ThrowsAsync<EventV1ProcessingDependencyValidationException>(
                        modifyEventV1Task.AsTask);

            // then
            actualEventV1ProcessingDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV1ProcessingDependencyValidationException);

            this.eventV1ServiceMock.Verify(service =>
                service.ModifyEventV1Async(It.IsAny<EventV1>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1ProcessingDependencyValidationException))),
                        Times.Once);

            this.eventV1ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDependencyExceptionOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            EventV1 someEventV1 = CreateRandomEventV1();

            var expectedEventV1ProcessingDependencyException =
                new EventV1ProcessingDependencyException(
                    message: "Event dependency error occurred, contact support.",
                    innerException: dependencyException.InnerException as Xeption);

            this.eventV1ServiceMock.Setup(service =>
                service.ModifyEventV1Async(It.IsAny<EventV1>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<EventV1> modifyEventV1Task =
                this.eventV1ProcessingService.ModifyEventV1Async(someEventV1);

            EventV1ProcessingDependencyException
                actualEventV1ProcessingDependencyException =
                    await Assert.ThrowsAsync<EventV1ProcessingDependencyException>(
                        modifyEventV1Task.AsTask);

            // then
            actualEventV1ProcessingDependencyException.Should()
                .BeEquivalentTo(expectedEventV1ProcessingDependencyException);

            this.eventV1ServiceMock.Verify(service =>
                service.ModifyEventV1Async(It.IsAny<EventV1>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(
                    expectedEventV1ProcessingDependencyException))),
                        Times.Once);

            this.eventV1ServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
