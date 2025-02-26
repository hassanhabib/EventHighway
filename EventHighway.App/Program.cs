using EventHighway.Core.Clients.EventHighways;

namespace EventHighway.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var eventHighwayClient = new EventHighwayClient(
                "Server=(localdb)\\MSSQLLocalDB;Database=EventHighwayDB;Trusted_Connection=True;MultipleActiveResultSets=true");

            //await eventHighwayClient.EventAddresses.RegisterEventAddressAsync(new Core.Models.EventAddresses.EventAddress
            //{
            //    Id = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
            //    CreatedDate = DateTimeOffset.UtcNow,
            //    UpdatedDate = DateTimeOffset.UtcNow,
            //    Name = "Test",
            //    Description = "Some Desc."
            //});

            //await eventHighwayClient.EventListeners.RegisterEventListenerAsync(new Core.Models.EventListeners.EventListener
            //{
            //    Id = Guid.NewGuid(),
            //    CreatedDate = DateTimeOffset.UtcNow,
            //    UpdatedDate = DateTimeOffset.UtcNow,
            //    Endpoint = "https://localhost:7167/api/home",
            //    EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
            //});

            //await eventHighwayClient.EventListeners.RegisterEventListenerAsync(new Core.Models.EventListeners.EventListener
            //{
            //    Id = Guid.NewGuid(),
            //    CreatedDate = DateTimeOffset.UtcNow,
            //    UpdatedDate = DateTimeOffset.UtcNow,
            //    Endpoint = "https://localhost:7104/api/tests",
            //    EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
            //});

            //int numberOfEvents = 500;
            //DateTimeOffset baseTime = DateTimeOffset.UtcNow;

            //foreach (int i in Enumerable.Range(1, numberOfEvents))
            //{
            //    _ = eventHighwayClient.Events.SubmitEventAsync(new Core.Models.Events.Event
            //    {
            //        Content = $"{{ \"name\": \"Scheduled Test - {i}\" }}",
            //        EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
            //        Id = Guid.NewGuid(),
            //        CreatedDate = DateTimeOffset.UtcNow,
            //        UpdatedDate = DateTimeOffset.UtcNow,
            //        PublishedDate = baseTime.AddSeconds(i * 1)
            //    });
            //}

            //foreach (int i in Enumerable.Range(1, numberOfEvents-400))
            //{
            //    await eventHighwayClient.Events.SubmitEventAsync(new Core.Models.Events.Event
            //    {
            //        Content = $"{{ \"name\": \"Immediate Test - {i}\" }}",
            //        EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
            //        Id = Guid.NewGuid(),
            //        CreatedDate = DateTimeOffset.UtcNow,
            //        UpdatedDate = DateTimeOffset.UtcNow
            //    });
            //}

            Console.WriteLine("Input seconds for the test 1:");
            int seconds = int.Parse(Console.ReadLine());

            Console.WriteLine("Input seconds for the test 2:");
            int seconds2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Input seconds for the test 4:");
            int seconds3 = int.Parse(Console.ReadLine());

            _ = eventHighwayClient.Events.SubmitEventAsync(new Core.Models.Events.Event
            {
                Content = "{ \"name\": \"Test1\" }",
                EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
                PublishedDate = DateTimeOffset.UtcNow.AddSeconds(seconds),
            });

            _ = eventHighwayClient.Events.SubmitEventAsync(new Core.Models.Events.Event
            {
                Content = "{ \"name\": \"Test2\" }",
                EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
                PublishedDate = DateTimeOffset.UtcNow.AddSeconds(seconds2),
            });

            _ = eventHighwayClient.Events.SubmitEventAsync(new Core.Models.Events.Event
            {
                Content = "{ \"name\": \"Test3\" }",
                EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
            });

            _ = eventHighwayClient.Events.SubmitEventAsync(new Core.Models.Events.Event
            {
                Content = "{ \"name\": \"Test4\" }",
                EventAddressId = Guid.Parse("d3b3b3b3-0b3b-4b3b-8b3b-0b3b3b3b3b32"),
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
                PublishedDate = DateTimeOffset.UtcNow.AddSeconds(seconds3),
            });

            Console.ReadKey();
        }
    }
}
