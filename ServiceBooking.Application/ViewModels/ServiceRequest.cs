namespace ServiceBooking.Application.ViewModels;

public class ServiceRequest
{
      public required string Name { get; set; }
      public string? Description { get; set; }
      public decimal Price { get; set; }
      public TimeSpan Duration { get; set; } = new TimeSpan(1, 20, 0);
}
