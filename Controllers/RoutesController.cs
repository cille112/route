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

	[HttpPost("all_points")]
	public async Task<IActionResult> AddAllPoints([FromBody] List<Stop> request)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		foreach (var stop in request)
		{
			if (_stop.Any(s => s.name.Equals(stop.name, StringComparison.OrdinalIgnoreCase)))
			{
				return Conflict($"A stop with the name '{stop.name}' already exists.");
			}

			_stop.Add(stop);
		}

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
