using System.Text.Json;
using System.Text;
using Routes.Models;

namespace Routes.Services
{
    public class GoogleRoutesApiService : IGoogleRoutesApiService
    {

		private readonly IConfiguration _configuration;
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public GoogleRoutesApiService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
		{
			_configuration = configuration;
			_httpClient = httpClientFactory.CreateClient();
			_apiKey = configuration["GoogleApiKey"];
		}

		public async Task<GoogleRouteReponse> GetRouteAsync(Point origin, Point destination)
		{
			string url = "https://routes.googleapis.com/directions/v2:computeRoutes";

			var requestPayload = new
			{
				origin = new { location = new { latLng = origin } },
				destination = new { location = new { latLng = destination } },
				travelMode = "DRIVE",
				computeAlternativeRoutes = false,
				units = "METRIC"
			};

			using var request = new HttpRequestMessage(HttpMethod.Post, url)
			{
				Content = new StringContent(
				JsonSerializer.Serialize(requestPayload),
				Encoding.UTF8,
				"application/json"
				)
			};

			request.Headers.Add("X-Goog-Api-Key", _apiKey );
			request.Headers.Add("X-Goog-FieldMask", "routes.duration,routes.distanceMeters");

			var serializedPayload = JsonSerializer.Serialize(requestPayload, new JsonSerializerOptions { WriteIndented = true });
			Console.WriteLine("Request Payload:");
			Console.WriteLine(serializedPayload);

			var response = await _httpClient.SendAsync( request );

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("Error in getting distance from google api");
			}

			string jsonResponse = await response.Content.ReadAsStringAsync();
			var jsonDocument = JsonDocument.Parse(jsonResponse);
			var route = jsonDocument.RootElement
									 .GetProperty("routes")[0];

			var routeResponse = new GoogleRouteReponse
			{
				Distance = route.GetProperty("distanceMeters").GetInt32()
			};

			return routeResponse;
		}

		
	}
}
