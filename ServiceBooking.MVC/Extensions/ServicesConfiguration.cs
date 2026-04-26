using Microsoft.EntityFrameworkCore;
using ServiceBooking.Infrastructure.Data;

namespace ServiceBooking.MVC.Extensions;

public static class ServicesConfiguration
{
      public static void AddDatabaseConnection(this IServiceCollection services, WebApplicationBuilder builder)
      {
            var databaseCS = builder.Configuration.GetConnectionString("DatabaseCS");
            services.AddDbContext<DatabaseContext>(opt => opt.UseNpgsql(databaseCS));
      }
}
