using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelManagement.Api.DTOs;
using TravelManagement.Api.Services.Interfaces;

namespace TravelManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Must be logged in
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> BookRoute([FromBody] BookingCreateDto request)
    {
        // Extract UserId from the JWT token (Secure approach)
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized(new { message = "Invalid user token" });
        }

        var result = await _bookingService.CreateBookingAsync(userId, request);
        if (!result)
            return BadRequest(new { message = "Booking failed" });

        return Ok(new { message = "Booking successful" });
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyBookings()
    {
        // Extract UserId from the JWT token
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
        {
            return Unauthorized(new { message = "Invalid user token" });
        }

        var bookings = await _bookingService.GetUserBookingHistoryAsync(userId);
        return Ok(bookings);
    }
}
