using Routes.Models;

namespace Routes.Services
{
    public interface IGoogleRoutesApiService
    {
		Task<GoogleRouteReponse> GetRouteAsync(Point origin, Point destination);

	}
}
