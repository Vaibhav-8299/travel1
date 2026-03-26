namespace TravelManagement.Api.DTOs;

public class RouteCreateDto
{
    public string Source { get; set; } = null!;
    public string Destination { get; set; } = null!;
    public decimal Price { get; set; }
}

public class RouteUpdateDto
{
    public string Source { get; set; } = null!;
    public string Destination { get; set; } = null!;
    public decimal Price { get; set; }
}

public class RouteResponseDto
{
    public int Id { get; set; }
    public string Source { get; set; } = null!;
    public string Destination { get; set; } = null!;
    public decimal Price { get; set; }
}
