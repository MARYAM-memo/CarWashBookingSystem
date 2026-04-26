using ServiceBooking.Core.Entities;
using ServiceBooking.Core.Interfaces;
using ServiceBooking.Infrastructure.Data;

namespace ServiceBooking.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
      readonly DatabaseContext context;
      public UnitOfWork(DatabaseContext ctx)
      {
            context = ctx;
            Services = new Repository<Service>(context);
            Bookings = new Repository<Booking>(context);
      }
      public IRepository<Service> Services { get; private set; }

      public IRepository<Booking> Bookings { get; private set; }

      public void Dispose()
      {
            context.Dispose();
            GC.SuppressFinalize(this);
      }

      public async Task<int> SaveChangesAsync()
      {
            return await context.SaveChangesAsync();
      }
}
