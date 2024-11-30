using Microsoft.AspNetCore.Mvc;
using Routes.Models;
using Routes.Services;

namespace Routes.Controllers;

[ApiController]
[Route("[controller]")]
public class RoutesController : ControllerBase
{
	private readonly IRouteOptimizationService _routeOptimizationService;

	private readonly ILogger<RoutesController> _logger;

	private static List<Stop> _stop = new List<Stop>();

	public RoutesController(ILogger<RoutesController> logger, IRouteOptimizationService routeOptimization)
    {
        _logger = logger;
		_routeOptimizationService = routeOptimization;
    }

	[HttpPost("point")]
	public async Task<IActionResult> AddPoint([FromBody] Stop request)
	{
		if (request == null)
		{
			return BadRequest("Could not add point, since it's empty ");
		}

		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		if (_stop.Any(s => s.name.Equals(request.name, StringComparison.OrdinalIgnoreCase)))
		{
			return Conflict($"A stop with the name '{request.name}' already exists.");
		}

		_stop.Add(request);
		return Ok();
	}

	[HttpGet("route")]
	public async Task<IActionResult> GetRoute()
	{
		Console.WriteLine(_stop);
		try
		{
			var route = await _routeOptimizationService.GetRouteAsync(_stop);
			return Ok(route);
			
		}
		catch (Exception ex) 
		{
			return BadRequest($"Could not calculate: {ex.Message}");
		}
	}
}
