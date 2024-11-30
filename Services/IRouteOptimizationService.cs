using Routes.Models;

namespace Routes.Services
{
    public interface IRouteOptimizationService
    {
		Task<Dictionary<(string, string), int>> GetDistanceAsync(List<Stop> stops);
		Task<RouteResponse> GetRouteAsync(List<Stop> stops);
    }
}
