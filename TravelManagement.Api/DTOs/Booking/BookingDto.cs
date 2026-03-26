namespace TravelManagement.Api.DTOs;

public class BookingCreateDto
{
    public int RouteId { get; set; }
}

public class BookingResponseDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Source { get; set; } = null!;
    public string Destination { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime BookingDate { get; set; }
}
