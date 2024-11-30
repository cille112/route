using Routes.Models;

namespace Routes.Services
{
    public class RouteOptimizationService : IRouteOptimizationService
    {
        private readonly IGoogleRoutesApiService _googleRouteApiService; 

		public RouteOptimizationService(IGoogleRoutesApiService googleRoutesApiService)
		{
			_googleRouteApiService = googleRoutesApiService;
		}
		public async Task<Dictionary<(string, string), int >> GetDistanceAsync(List<Stop> stops)
        {
			if (stops == null || stops.Count < 2)
			{ 
				throw new ArgumentException("At least two points are required.");
			}

			var distancesAndDurations = new Dictionary<(string, string), int>();

			for (int i = 0; i < stops.Count; i++)
			{
				for (int j = 0; j < stops.Count; j++)
				{
					if (i == j)
					{ 
						break;
					}
					
					try
					{
						var element = await _googleRouteApiService.GetRouteAsync(stops[i].point, stops[j].point);

						distancesAndDurations[(stops[i].name, stops[j].name)] = element.Distance;
						distancesAndDurations[(stops[j].name, stops[i].name)] = element.Distance;


					}
					catch (Exception ex)
					{
						throw new BadHttpRequestException($"Could not get response from Google API: {ex.Message}");
					}
					
				}
			}
			return distancesAndDurations;
        }

		public async Task<RouteResponse> GetRouteAsync(List<Stop> stops)
		{
			try
			{
				var distanceMatrix = await GetDistanceAsync(stops);

				var stopIndex = 0;

				var route = new List<String>();
				route.Add(stops[stopIndex].name);
				var distance = 0; 

				while (route.Count != stops.Count) 
				{
					var neighbour = -1;
					var shortestDistance = int.MaxValue;

                    for (int i = 0; i < stops.Count; i++)
                    {
						if (route.Contains(stops[i].name) || stopIndex == i)
						{
							continue; //Skip since we don't add the same stop twice
						}

						var dist = distanceMatrix[(stops[stopIndex].name, stops[i].name)];
						if (shortestDistance > dist)
						{
							shortestDistance = dist;
							neighbour = i;
						}
                    }

					if (neighbour == -1) 
					{
						throw new Exception("Did not find a neighbour");
					}

					route.Add(stops[neighbour].name);
					distance += distanceMatrix[(stops[stopIndex].name, stops[neighbour].name)];
					stopIndex = neighbour;


				}
				var routeReponse = new RouteResponse{ Route = route, Distance = distance};
				return routeReponse;

			}
			catch (Exception ex)
			{
				throw new Exception($"Could not calculate route: {ex.Message}");
			}
		}        
    }
}
