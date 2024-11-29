using Routes.Models;

namespace Routes.Services
{
    public interface IGoogleRoutesApiService
    {
		Task<RouteResponse> GetRouteAsync(Point origin, Point destination);

	}
}
