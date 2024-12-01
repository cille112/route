using Routes.Models;

namespace Routes.Services
{
    public interface IRouteOptimizationService
    {
		Task<Dictionary<(string, string), int>> GetDistanceAsync(List<Stop> stops);
		RouteResponse GetRoute(Dictionary<(string, string), int> distanceMatrix, List<Stop> stops);
    }
}
