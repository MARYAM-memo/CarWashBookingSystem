using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceBooking.Core.Entities;

public class Service
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }
      public required string Name { get; set; }
      public string? Description { get; set; }
      public decimal Price { get; set; }
      public TimeSpan Duration { get; set; } = new TimeSpan(1, 20, 0);

      //Navigation
      public List<Booking> Bookings { get; set; } = [];
      
}
