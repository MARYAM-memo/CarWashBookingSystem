using Microsoft.EntityFrameworkCore;
using ServiceBooking.Core.Entities;

namespace ServiceBooking.Infrastructure.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
      public DbSet<Service> Services { get; set; }
      public DbSet<Booking> Bookings { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>().HasOne(op => op.Service).WithMany(op => op.Bookings).HasForeignKey(op => op.ServiceId).OnDelete(DeleteBehavior.Restrict);
      }

}
