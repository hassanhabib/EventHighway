// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using EventHighway.Core.Models.EventAddresses;
using EventHighway.Core.Models.EventAddresses.Exceptions;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using System;
using System.Threading.Tasks;

namespace EventHighway.Core.Tests.Unit.Services.EventAddresses
{
    public partial class EventAddressServiceTests
    {
        [Fact]
        public async Task ShouldAddEventAddressAsync()
        {
            // given
            EventAddress randomEventAddress =
                CreateRandomEventAddress();

            EventAddress inputEventAddress =
                randomEventAddress;

            EventAddress insertedEventAddress =
                inputEventAddress;

            EventAddress expectedEventAddress =
                insertedEventAddress.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEventAddressAsync(inputEventAddress))
                    .ReturnsAsync(insertedEventAddress);

            // when
            EventAddress actualEventAddress =
                await this.eventAddressService.AddEventAddressAsync(
                    inputEventAddress);

            // then
            actualEventAddress.Should().BeEquivalentTo(
                expectedEventAddress);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressAsync(inputEventAddress),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionIfEventAddressIsNullAndLogItAsync()
        {
            // given
            EventAddress nullEventAddress = null;
            var nullEventAddressException = new NullEventAddressException(message: "EventAddress is null");

            var expectedEventAddressValidationException =
                new EventAddressValidationException
                (message: "EventAddress validation error occurred, fix errors and try again",
                innerException: nullEventAddressException);


            // when
            Func<Task> addEventAddressTask = async () =>
                await this.eventAddressService.AddEventAddressAsync(nullEventAddress);

            // then
            await addEventAddressTask.Should().ThrowAsync<EventAddressValidationException>()
                .WithMessage(expectedEventAddressValidationException.Message);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedEventAddressValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertEventAddressAsync(It.IsAny<EventAddress>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}