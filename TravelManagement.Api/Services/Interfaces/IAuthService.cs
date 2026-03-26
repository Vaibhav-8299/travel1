using TravelManagement.Api.DTOs;

namespace TravelManagement.Api.Services.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
}
