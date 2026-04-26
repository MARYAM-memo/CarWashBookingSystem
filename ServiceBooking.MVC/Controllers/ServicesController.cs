using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceBooking.Application.ViewModels;
using ServiceBooking.Application.ViewModels.Booking;
using ServiceBooking.Core.Entities;
using ServiceBooking.Core.Interfaces;

namespace ServiceBooking.MVC.Controllers
{
    public class ServicesController(IUnitOfWork unitOf, IMapper iMapper) : Controller
    {
        readonly IUnitOfWork unitOfWork = unitOf;
        readonly IMapper mapper = iMapper;

        //view all services
        public async Task<ActionResult> Index()
        {
            //اجيب الداتا من الباك اند
            var srvs = await unitOfWork.Services.FetchAsync(true, op => op.Bookings);
            var model = mapper.Map<List<ServiceResponse>>(srvs);
            return View(model);
        }

        //view details of selected service
        public async Task<IActionResult> Details(int id)
        {
            var srv = await unitOfWork.Services.FindAsync(id);
            if (srv == null) return NotFoundSrv();

            var bookings = await unitOfWork.Bookings.FetchAsync(withNoTracking: true, predicate: op => op.ServiceId == id);
            var model = mapper.Map<ServiceResponse>(srv);
            var bookingsModel = mapper.Map<List<BookingResponse>>(bookings);
            ViewBag.ServiceBookings = bookingsModel;
            return View(model);
        }

        //create new service
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var srv = mapper.Map<Service>(request);
                    unitOfWork.Services.Add(srv);
                    await unitOfWork.SaveChangesAsync();
                    TempData["SuccessMessage"] = "تم إنشاء الخدمة بنجاح !!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the service.\n\n:{ex}");
                    return View(request);
                }
            }
            TempData["ErrorMessage"] = "فشل إنشاء الخدمة !!!";
            return View(request);
        }

        //edit an existing service
        public async Task<IActionResult> Edit(int id)
        {
            var srv = await unitOfWork.Services.FindAsync(id);
            if (srv == null) return NotFoundSrv();
            var request = mapper.Map<ServiceRequest>(srv);
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceRequest request)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var srv = await unitOfWork.Services.FindAsync(id);
                    if (srv == null) return NotFoundSrv();
                    var editedSrv = mapper.Map(request, srv);
                    if (editedSrv == null) return NotFoundSrv();
                    unitOfWork.Services.Update(editedSrv!);
                    await unitOfWork.SaveChangesAsync();
                    TempData["SuccessMessage"] = "تم تعديل الخدمة بنجاح !!!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while Editing the service.\n\n:{ex}");
                    return View(request);
                }
            }
            TempData["ErrorMessage"] = "فشل تعديل الخدمة !!!";
            return View(request);
        }

        //delete an existing service
        public async Task<IActionResult> Delete(int id)
        {
            var srv = await unitOfWork.Services.FindAsync(id);
            if (srv == null) return NotFoundSrv();
            var bookings = await unitOfWork.Bookings.FetchAsync(withNoTracking: true, predicate: op => op.ServiceId == id);
            ViewBag.ServiceBookingsCount = bookings.Count();
            var model = mapper.Map<ServiceResponse>(srv);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var srv = await unitOfWork.Services.FindAsync(id);
            if (srv == null) return NotFoundSrv();
            if (srv!.Bookings.Count != 0)
            {
                TempData["ErrorMessage"] = "هذه الخدمه لديها حجوزات, لذالك لا يمكن حذفها حاليا!!! \nأو يمكنك حذف الحجوزات اولا من صفحة الحجوزات ثم ترجع لحذف الخدمه...";
                return RedirectToAction(nameof(Index));
            }
            unitOfWork.Services.Delete(srv);
            await unitOfWork.SaveChangesAsync();
            TempData["SuccessMessage"] = "تم حذف الخدمة بنجاح !!!";
            return RedirectToAction(nameof(Index));
        }

        private RedirectToActionResult NotFoundSrv()
        {
            TempData["ErrorMessage"] = "الخدمة غير موجودة!";
            return RedirectToAction(nameof(Index));
        }
    }

}
