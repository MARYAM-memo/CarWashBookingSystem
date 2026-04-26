using ServiceBooking.Core.Entities;

namespace ServiceBooking.Core.Interfaces;

public interface IBookingValidator
{
      Task<ValidationResult> ValidateAsync(Booking newBooking, int? excludeBookingId = null);
}

public class ValidationResult
{
      public bool IsValid { get; set; }
      public string? Message { get; set; }
      public List<BookingConflict> Conflicts { get; set; } = [];
}

public class BookingConflict
{
      public string ClientName { get; set; } = string.Empty;
      public TimeOnly StartTime { get; set; }
      public TimeOnly EndTime { get; set; }
      public string ServiceName { get; set; } = string.Empty;
}