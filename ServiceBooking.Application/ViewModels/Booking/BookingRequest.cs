namespace ServiceBooking.Application.ViewModels.Booking;

public class BookingRequest
{
      public required string ClientName { get; set; }
      public required string ClientPhone { get; set; }
      public required string ClientCarType { get; set; }
      public DateOnly BookingDate { get; set; }
      public TimeOnly BookingTime { get; set; }
      public string? Notes { get; set; }
      public int ServiceId { get; set; }
}
