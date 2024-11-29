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
		public async Task<Dictionary<(string, string), int >> GetDistanceAndDurationAsync(List<Stop> stops)
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
						
					} catch (Exception)
					{
						throw new BadHttpRequestException("Could not get response from Google API");
					}
					
				}
			}
			return distancesAndDurations;
        }
    }
}
