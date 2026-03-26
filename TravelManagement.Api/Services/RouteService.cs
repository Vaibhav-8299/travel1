using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TravelManagement.Api.Data;
using TravelManagement.Api.DTOs;
using TravelManagement.Api.Models;
using TravelManagement.Api.Services.Interfaces;

namespace TravelManagement.Api.Services;

public class RouteService : IRouteService
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;
    private const string RoutesCacheKey = "AllTravelRoutes";

    public RouteService(AppDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<RouteResponseDto>> GetAllRoutesAsync()
    {
        // Check if the routes are already in the cache
        if (!_cache.TryGetValue(RoutesCacheKey, out IEnumerable<RouteResponseDto>? routes))
        {
            // If not in cache, fetch from database
            routes = await _context.Travelroutes
                .Select(r => new RouteResponseDto
                {
                    Id = r.Id,
                    Source = r.Source,
                    Destination = r.Destination,
                    Price = r.Price
                })
                .ToListAsync();

            // Set cache options (5 minutes expiration)
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            // Save data in cache
            _cache.Set(RoutesCacheKey, routes, cacheEntryOptions);
        }

        return routes!;
    }

    public async Task<RouteResponseDto?> GetRouteByIdAsync(int id)
    {
        var route = await _context.Travelroutes.FindAsync(id);
        if (route == null) return null;

        return new RouteResponseDto
        {
            Id = route.Id,
            Source = route.Source,
            Destination = route.Destination,
            Price = route.Price
        };
    }

    public async Task<bool> CreateRouteAsync(RouteCreateDto request)
    {
        // Create a new database entity from the DTO
        var route = new Travelroute
        {
            Source = request.Source,
            Destination = request.Destination,
            Price = request.Price
        };

        _context.Travelroutes.Add(route);
        var success = await _context.SaveChangesAsync() > 0;
        
        if (success) 
        {
            _cache.Remove(RoutesCacheKey);
        }
        
        return success;
    }

    public async Task<bool> UpdateRouteAsync(int id, RouteUpdateDto request)
    {
        var route = await _context.Travelroutes.FindAsync(id);
        if (route == null) return false;

        route.Source = request.Source;
        route.Destination = request.Destination;
        route.Price = request.Price;

        var success = await _context.SaveChangesAsync() > 0;
        
        if (success)
        {
            _cache.Remove(RoutesCacheKey);
        }
        
        return success;
    }
}
