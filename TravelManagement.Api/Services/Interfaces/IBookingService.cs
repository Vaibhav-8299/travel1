using TravelManagement.Api.DTOs;

namespace TravelManagement.Api.Services.Interfaces;

public interface IBookingService
{
    Task<bool> CreateBookingAsync(int userId, BookingCreateDto request);
    Task<IEnumerable<BookingResponseDto>> GetUserBookingHistoryAsync(int userId);
    Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync();
}
