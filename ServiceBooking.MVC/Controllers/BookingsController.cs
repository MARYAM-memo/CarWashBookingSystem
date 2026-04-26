using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceBooking.Application.Services;
using ServiceBooking.Application.ViewModels;
using ServiceBooking.Application.ViewModels.Booking;
using ServiceBooking.Core.Entities;
using ServiceBooking.Core.Interfaces;
using ServiceBooking.Infrastructure.DataAccess;

namespace ServiceBooking.MVC.Controllers
{
    public class BookingsController(IUnitOfWork uOW, IMapper iMapper, IBookingValidator bookingValidator) : Controller
    {
        readonly IUnitOfWork unitOfWork = uOW;
        readonly IMapper mapper = iMapper;
        readonly IBookingValidator validator = bookingValidator;

        //view all bookings
        public async Task<ActionResult> Index()
        {
            var bookings = await unitOfWork.Bookings.FetchAsync(true, op => op.Service!);
            var model = mapper.Map<List<BookingResponse>>(bookings);
            return View(model);
        }

        //view details of selected booking
        public async Task<IActionResult> Details(int id)
        {
            var booking = await unitOfWork.Bookings.FindAsync(id);
            if (booking == null) return NotFoundBooking();
            var service = await unitOfWork.Services.FindAsync(booking.ServiceId);
            var model = mapper.Map<BookingResponse>(booking);
            model.Service = mapper.Map<ServiceSimple>(service);
            return View(model);
        }


        //create new booking
        public async Task<IActionResult> Create()
        {
            ViewBag.Services = await GetServicesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var booking = mapper.Map<Booking>(request);
                    var valid = await validator.ValidateAsync(booking);
                    if (!valid.IsValid)
                    {
                        TempData["WarningMessage"] = valid.Message;
                        ViewBag.Services = await GetServicesSelectList();
                        return View(request);
                    }
                    unitOfWork.Bookings.Add(booking);
                    await unitOfWork.SaveChangesAsync();

                    TempData["SuccessMessage"] = valid.Message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the booking.\n\n:{ex}");
                    ViewBag.Services = await GetServicesSelectList();
                    return View(request);
                }
            }
            ViewBag.Services = await GetServicesSelectList();
            TempData["ErrorMessage"] = "فشل الحجز !!!";
            return View(request);
        }

        //update an existing booking
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await unitOfWork.Bookings.FindAsync(id);
            if (booking == null) return NotFoundBooking();

            var request = mapper.Map<BookingRequest>(booking);
            ViewBag.Services = await GetServicesSelectList();
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookingRequest request, int id)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Services = await GetServicesSelectList();
                return View(request);
            }

            try
            {
                var existingBooking = await unitOfWork.Bookings.FindAsync(id);
                if (existingBooking == null) return NotFoundBooking();

                // تحديث القيم
                mapper.Map(request, existingBooking);

                // التحقق من التعارض (نستثني الحجز الحالي)
                var validation = await validator.ValidateAsync(existingBooking, excludeBookingId: id);

                if (!validation.IsValid)
                {
                    TempData["WarningMessage"] = validation.Message;
                    ViewBag.Services = await GetServicesSelectList();
                    return View(request);
                }

                unitOfWork.Bookings.Update(existingBooking);
                await unitOfWork.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم تعديل الحجز بنجاح!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ: {ex.Message}");
                ViewBag.Services = await GetServicesSelectList();
                return View(request);
            }
        }

        //delete an existing booking
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await unitOfWork.Bookings.FindAsync(id);
            if (booking == null) return NotFoundBooking();

            var model = mapper.Map<BookingResponse>(booking);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await unitOfWork.Bookings.FindAsync(id);
            if (booking == null) return NotFoundBooking();

            unitOfWork.Bookings.Delete(booking);
            await unitOfWork.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم حذف الحجز بنجاح!";
            return RedirectToAction(nameof(Index));
        }


        private async Task<SelectList> GetServicesSelectList()
        {
            var services = await unitOfWork.Services.FetchAsync(withNoTracking: true);
            var model = mapper.Map<List<ServiceResponse>>(services);
            return new SelectList(model, "Id", "Name");
        }
        private RedirectToActionResult NotFoundBooking()
        {
            TempData["ErrorMessage"] = "الحجز غير موجود!";
            return RedirectToAction(nameof(Index));
        }
    }
}
