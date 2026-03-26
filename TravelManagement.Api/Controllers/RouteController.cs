using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelManagement.Api.DTOs;
using TravelManagement.Api.Services.Interfaces;

namespace TravelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requires login for all endpoints by default
public class RouteController : ControllerBase
{
    private readonly IRouteService _routeService;

    public RouteController(IRouteService routeService)
    {
        _routeService = routeService;
    }

    [HttpGet]
    // Available to both User and Admin (since [Authorize] is on the controller)
    public async Task<IActionResult> GetAllRoutes()
    {
        var routes = await _routeService.GetAllRoutesAsync();
        return Ok(routes);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // Only Admin can add
    public async Task<IActionResult> AddRoute([FromBody] RouteCreateDto request)
    {
        var result = await _routeService.CreateRouteAsync(request);
        if (!result)
            return BadRequest(new { message = "Failed to create route" });

        return Ok(new { message = "Route created successfully" });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] // Only Admin can update
    public async Task<IActionResult> UpdateRoute(int id, [FromBody] RouteUpdateDto request)
    {
        var result = await _routeService.UpdateRouteAsync(id, request);
        if (!result)
            return NotFound(new { message = "Route not found or failed to update" });

        return Ok(new { message = "Route updated successfully" });
    }
}
