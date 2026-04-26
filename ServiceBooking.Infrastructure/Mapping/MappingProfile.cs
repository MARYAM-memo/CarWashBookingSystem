using AutoMapper;
using ServiceBooking.Application.ViewModels;
using ServiceBooking.Application.ViewModels.Booking;
using ServiceBooking.Core.Entities;

namespace ServiceBooking.Infrastructure.Mapping;

public class MappingProfile : Profile
{
      public MappingProfile()
      {
            CreateMap<ServiceRequest, Service>();
            CreateMap<Service, ServiceRequest>();
            CreateMap<Service, ServiceResponse>().ForMember(op => op.BookingsCount, op => op.MapFrom(src => src.Bookings.Count));

            CreateMap<BookingRequest, Booking>();
            CreateMap<Booking, BookingRequest>();
            CreateMap<Service, ServiceSimple>();
            CreateMap<Booking, BookingResponse>();
      }
}
