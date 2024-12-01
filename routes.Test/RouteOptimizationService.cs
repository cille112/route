using Routes.Models;
using Routes.Services;


namespace routes.Test
{
    public class RouteOptimizationServiceTests
    {

        private readonly IGoogleRoutesApiService _service;

        public RouteOptimizationServiceTests()
        {
            // The IGoogleRouteServiceApi is not used by the function we are testing
            // So we don't initialize it.
        }

        [Fact]
        public void GetRoute()
        {
            // Arrange
            var distanceMatrix = new Dictionary<(string, string), int>
            {
                { ("Start", "stop1"), 10 },
                { ("stop1", "Start"), 10 },
                { ("Start", "stop2"), 15 },
                { ("stop2", "Start"), 15 },
                { ("Start", "stop3"), 20 },
                { ("stop3", "Start"), 20 },
                { ("stop1", "stop2"), 35 },
                { ("stop2", "stop1"), 35 },
                { ("stop1", "stop3"), 25 },
                { ("stop3", "stop1"), 25 },
                { ("stop2", "stop3"), 30 },
                { ("stop3", "stop2"), 30 }
            };

            var stops = new List<Stop>
            {
                new Stop { name = "Start", point = new Routes.Models.Point{latitude = 55.6761, longitude = 12.5683 } },
                new Stop { name = "stop1", point = new Point{latitude = 55.6750, longitude = 12.5794 } },
                new Stop { name = "stop2", point = new Point{latitude = 55.6880, longitude = 12.5987 } },
                new Stop { name = "stop3", point = new Point{latitude = 55.6690, longitude = 12.5700 } }
            };

            var expectedRoute = new List<string> { "Start", "stop1", "stop3", "stop2" };

            var routeService = new RouteOptimizationService(_service);

            // Act
            var result = routeService.GetRoute(distanceMatrix, stops);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Route.Count);
            Assert.Equal(65, result.Distance);
            Assert.Equal(expectedRoute, result.Route);
        }


        [Fact]
        public void GetRouteOnlyOneStop()
        {
            // Arrange
            var distanceMatrix = new Dictionary<(string, string), int>
            {
            };

            var stops = new List<Stop>
            {
                new Stop { name = "Start", point = new Point{latitude = 55.6761, longitude = 12.5683 } },
                new Stop { name = "stop1", point = new Point{latitude = 55.6750, longitude = 12.5794 } },
                new Stop { name = "stop2", point = new Point{latitude = 55.6880, longitude = 12.5987 } },
                new Stop { name = "stop3", point = new Point{latitude = 55.6690, longitude = 12.5700 } }
            };


            var routeService = new RouteOptimizationService(_service);

            // Act
            var exception = Assert.Throws<Exception>(() => routeService.GetRoute(distanceMatrix, stops));

            // Assert
            Assert.StartsWith("Could not calculate route", exception.Message);
        }
    }

}