namespace ServiceBooking.Application.ViewModels.Booking;

public class BookingResponse
{
      public int Id { get; set; }
      public required string ClientName { get; set; }
      public required string ClientPhone { get; set; }
      public required string ClientCarType { get; set; }
      public DateOnly BookingDate { get; set; }
      public TimeOnly BookingTime { get; set; }
      public string? Notes { get; set; }
      public ServiceSimple? Service { get; set; }

}

public class ServiceSimple
{
      public int Id { get; set; }
      public required string Name { get; set; }
      public string? Description { get; set; }
      public decimal Price { get; set; }
      public TimeSpan Duration { get; set; } = new TimeSpan(1, 20, 0);
}