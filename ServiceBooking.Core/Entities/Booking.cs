using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceBooking.Core.Entities;

public class Booking
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }
      public required string ClientName { get; set; }
      public required string ClientPhone { get; set; }
      public required string ClientCarType { get; set; }
      public DateOnly BookingDate { get; set; }
      public TimeOnly BookingTime { get; set; }
      public string? Notes { get; set; }

      //Foriegn keys
      public int ServiceId { get; set; }
      public Service? Service { get; set; }
}
