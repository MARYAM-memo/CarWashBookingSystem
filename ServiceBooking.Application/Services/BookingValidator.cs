using ServiceBooking.Core.Entities;
using ServiceBooking.Core.Interfaces;

namespace ServiceBooking.Application.Services;

public class BookingValidator(IUnitOfWork uOW) : IBookingValidator
{
      readonly IUnitOfWork unitOfWork = uOW;
      public async Task<ValidationResult> ValidateAsync(Booking newBooking, int? excludeBookingId = null)
      {
            var service = await unitOfWork.Services.FindAsync(newBooking.ServiceId);
            if (service == null)
            {
                  return new ValidationResult
                  {
                        IsValid = false,
                        Message = "الخدمة غير موجودة!",
                  };
            }

            var bookingDate = newBooking.BookingDate;
            var bookingStart = newBooking.BookingTime;
            var serviceDuration = service.Duration;
            var bookingEnd = bookingStart.Add(serviceDuration);

            Console.WriteLine($@"
            -------------------------------------------------
            | Booking Date: {bookingDate},
            | Booking Start: {bookingStart},
            | Service Duration: {serviceDuration},
            | ======================================
            | At [{bookingDate.Day}/{bookingDate.Month}/{bookingDate.Year}], '{newBooking.ClientName}' Expected End: {bookingEnd}
            -------------------------------------------------");

            var now = DateTime.Now;
            var bookingDateTime = bookingDate.ToDateTime(bookingStart);
            if (bookingDateTime < now)
            {
                  return new ValidationResult
                  {
                        IsValid = false,
                        Message = "لا يمكن الحجز في موعد سابق! يرجى اختيار موعد قادم.",
                  };
            }

            var existingBookings = await unitOfWork.Bookings.FetchAsync(withNoTracking: true, predicate: op => op.BookingDate == bookingDate && op.Id != excludeBookingId, args: op => op.Service!);

            var conflicts = new List<BookingConflict>();
            foreach (var exist in existingBookings)
            {
                  var start = exist.BookingTime;
                  var end = start.Add(exist.Service?.Duration ?? TimeSpan.Zero);

                  bool hasOverlap = start < bookingEnd && end > bookingStart;
                  if (hasOverlap)
                  {
                        conflicts.Add(
                              new BookingConflict
                              {
                                    ClientName = exist.ClientName,
                                    StartTime = start,
                                    EndTime = end,
                                    ServiceName = exist?.Service?.Name ?? "",
                              }
                        );
                  }
            }

            if (conflicts.Count != 0)
            {
                  var firstConflict = conflicts.First();
                  return new ValidationResult
                  {
                        IsValid = false,
                        Message = $"تعارض مع حجز {firstConflict.ClientName} " +
                         $"من {firstConflict.StartTime:hh:mm tt} إلى {firstConflict.EndTime:hh:mm tt}. " +
                         $"يرجى اختيار موعد قبل {firstConflict.StartTime:hh:mm tt} أو بعد {firstConflict.EndTime:hh:mm tt}.",
                        Conflicts = conflicts
                  };
            }

            return new ValidationResult
            {
                  IsValid = true,
                  Message = $"الموعد متاح: {bookingDate:dd/MM/yyyy} من {bookingStart:hh:mm tt} إلى {bookingEnd:hh:mm tt}"
            };
      }
}
