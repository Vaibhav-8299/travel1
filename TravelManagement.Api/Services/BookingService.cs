using Microsoft.EntityFrameworkCore;
using TravelManagement.Api.Data;
using TravelManagement.Api.DTOs;
using TravelManagement.Api.Models;
using TravelManagement.Api.Services.Interfaces;

namespace TravelManagement.Api.Services;

public class BookingService : IBookingService
{
    private readonly AppDbContext _context;

    public BookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateBookingAsync(int userId, BookingCreateDto request)
    {
        var booking = new Booking
        {
            UserId = userId,
            RouteId = request.RouteId,
            BookingDate = DateTime.Now
        };

        _context.Bookings.Add(booking);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<BookingResponseDto>> GetUserBookingHistoryAsync(int userId)
    {
        // We use .Include() to "JOIN" the Users and TravelRoutes tables
        return await _context.Bookings
            .Include(b => b.Route)
            .Include(b => b.User)
            .Where(b => b.UserId == userId)
            .Select(b => new BookingResponseDto
            {
                Id = b.Id,
                UserName = b.User.Name,
                Source = b.Route.Source,
                Destination = b.Route.Destination,
                Price = b.Route.Price,
                BookingDate = b.BookingDate ?? DateTime.Now
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync()
    {
        return await _context.Bookings
            .Include(b => b.Route)
            .Include(b => b.User)
            .Select(b => new BookingResponseDto
            {
                Id = b.Id,
                UserName = b.User.Name,
                Source = b.Route.Source,
                Destination = b.Route.Destination,
                Price = b.Route.Price,
                BookingDate = b.BookingDate ?? DateTime.Now
            })
            .ToListAsync();
    }
}
