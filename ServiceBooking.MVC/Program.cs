using ServiceBooking.Application.Services;
using ServiceBooking.Core.Interfaces;
using ServiceBooking.Infrastructure.DataAccess;
using ServiceBooking.Infrastructure.Mapping;
using ServiceBooking.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDatabaseConnection(builder); //extension
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(op => op.AddProfile<MappingProfile>(), typeof(MappingProfile).Assembly);
// Program.cs
builder.Services.AddScoped<IBookingValidator, BookingValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Services}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
