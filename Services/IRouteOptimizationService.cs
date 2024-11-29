using Routes.Models;

namespace Routes.Services
{
    public interface IRouteOptimizationService
    {
		Task<Dictionary<(string, string), int>> GetDistanceAndDurationAsync(List<Stop> stops);
	}
}
