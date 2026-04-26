using System;
using ServiceBooking.Core.Entities;

namespace ServiceBooking.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
      IRepository<Service> Services { get; }
      IRepository<Booking> Bookings { get; }
      Task<int> SaveChangesAsync();
}
