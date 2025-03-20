// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Threading.Tasks;
using EventHighway.Core.Models.Clients.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Coordinations.Events.V2.Exceptions;
using EventHighway.Core.Models.Services.Foundations.Events.V2;
using FluentAssertions;
using Moq;
using Xeptions;

namespace EventHighway.Core.Tests.Unit.Clients.Events.V2
{
    public partial class EventV2sClientTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnSubmitIfDependencyValidationErrorOccursAsync()
        {
            // given
            EventV2 someEventV2 = CreateRandomEventV2();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2CoordinationDependencyValidationException =
                new EventV2CoordinationDependencyValidationException(
                    someMessage,
                    someInnerException);

            var expectedEventV2ClientDependencyValidationException =
                new EventV2ClientDependencyValidationException(
                    message: "Event client validation error occurred, fix the errors and try again.",

                    innerException: eventV2CoordinationDependencyValidationException
                        .InnerException as Xeption);

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.SubmitEventV2Async(It.IsAny<EventV2>()))
                    .ThrowsAsync(eventV2CoordinationDependencyValidationException);

            // when
            ValueTask<EventV2> submitEventV2Task =
                this.eventV2SClient.SubmitEventV2Async(someEventV2);

            EventV2ClientDependencyValidationException actualEventV2ClientDependencyValidationException =
                await Assert.ThrowsAsync<EventV2ClientDependencyValidationException>(
                    submitEventV2Task.AsTask);

            // then
            actualEventV2ClientDependencyValidationException.Should()
                .BeEquivalentTo(expectedEventV2ClientDependencyValidationException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.SubmitEventV2Async(It.IsAny<EventV2>()),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnSubmitIfDependencyErrorOccursAsync()
        {
            // given
            EventV2 someEventV2 = CreateRandomEventV2();
            string someMessage = GetRandomString();
            var someInnerException = new Xeption();

            var eventV2CoordinationDependencyException =
                new EventV2CoordinationDependencyException(
                    someMessage,
                    someInnerException);

            var expectedEventV2ClientDependencyException =
                new EventV2ClientDependencyException(
                    message: "Event client dependency error occurred, contact support.",

                    innerException: eventV2CoordinationDependencyException
                        .InnerException as Xeption);

            this.eventV2CoordinationServiceMock.Setup(service =>
                service.SubmitEventV2Async(It.IsAny<EventV2>()))
                    .ThrowsAsync(eventV2CoordinationDependencyException);

            // when
            ValueTask<EventV2> submitEventV2Task =
                this.eventV2SClient.SubmitEventV2Async(someEventV2);

            EventV2ClientDependencyException actualEventV2ClientDependencyException =
                await Assert.ThrowsAsync<EventV2ClientDependencyException>(
                    submitEventV2Task.AsTask);

            // then
            actualEventV2ClientDependencyException.Should()
                .BeEquivalentTo(expectedEventV2ClientDependencyException);

            this.eventV2CoordinationServiceMock.Verify(service =>
                service.SubmitEventV2Async(It.IsAny<EventV2>()),
                    Times.Once);

            this.eventV2CoordinationServiceMock.VerifyNoOtherCalls();
        }
    }
}
