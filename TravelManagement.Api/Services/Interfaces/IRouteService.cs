using TravelManagement.Api.DTOs;

namespace TravelManagement.Api.Services.Interfaces;

public interface IRouteService
{
    Task<IEnumerable<RouteResponseDto>> GetAllRoutesAsync();
    Task<RouteResponseDto?> GetRouteByIdAsync(int id);
    Task<bool> CreateRouteAsync(RouteCreateDto request);
    Task<bool> UpdateRouteAsync(int id, RouteUpdateDto request);
}
