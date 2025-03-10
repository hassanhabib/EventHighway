// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using EventHighway.Core.Models.Services.Foundations.EventAddresses;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace EventHighway.Core.Tests.Unit.Services.EventAddresses
{
    public partial class EventAddressServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllEventAddressesAsync()
        {
            // given
            IQueryable<EventAddress> randomEventAddresses =
                CreateRandomEventAddresses();

            IQueryable<EventAddress> storageEventAddresses =
                randomEventAddresses;

            IQueryable<EventAddress> expectedEventAddresses =
                storageEventAddresses.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllEventAddressesAsync())
                    .ReturnsAsync(storageEventAddresses);

            // when
            IQueryable<EventAddress> actualEventAddresses =
                await this.eventAddressService.RetrieveAllEventAddressesAsync();

            // then
            actualEventAddresses.Should().BeEquivalentTo(expectedEventAddresses);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllEventAddressesAsync(),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
