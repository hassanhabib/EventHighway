using EventHighway.Core.Clients.EventHighways;

namespace EventHighway.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var eventHighwayClient = new EventHighwayClient(
                "Server=(localdb)\\MSSQLLocalDB;Database=EventHighwayDB;Trusted_Connection=True;MultipleActiveResultSets=true");

            await eventHighwayClient.EventAddresses.RegisterEventAddressAsync(new Core.Models.EventAddresses.EventAddress
            {
                Id = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
                Name = "Test",
                Description = "Some Desc."
            });

            await eventHighwayClient.EventListeners.RegisterEventListenerAsync(new Core.Models.EventListeners.EventListener
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
                Endpoint = "https://localhost:7056/api/tests",
                EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
            });

            await eventHighwayClient.EventListeners.RegisterEventListenerAsync(new Core.Models.EventListeners.EventListener
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
                Endpoint = "https://localhost:7104/api/tests",
                EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
            });

            await eventHighwayClient.Events.SubmitEventAsync(new Core.Models.Events.Event
            { 
                Content = "{ \"name\": \"Test\" }",
                EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow
            });

        }
    }
}
