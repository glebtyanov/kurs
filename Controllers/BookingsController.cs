using Microsoft.AspNetCore.Mvc;
using kurs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore;

namespace kurs.Controllers;
[Authorize]
public class BookingsController : Controller
{
    private readonly TourDbContext dbContext;
    public BookingsController(TourDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var bookings = dbContext.Bookings.Include(b=>b.Client).Include(b=>b.Tour).ToList();
        return View(bookings);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View(new BookingViewModel(){
            Tours = dbContext.Tours.ToList(),
            Clients = dbContext.Clients.ToList()
        });
    }

    [HttpPost]
    public IActionResult Add(BookingViewModel bookingAddRequest)
    {
        var booking = new Booking()
        {
            TourId = bookingAddRequest.Booking.TourId,
            ClientId = bookingAddRequest.Booking.ClientId,
            BookingDate = bookingAddRequest.Booking.BookingDate,
        };

        dbContext.Bookings.Add(booking);

        dbContext.SaveChanges();

        return RedirectToAction("Add");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var booking = dbContext.Bookings.Find(id);
        if (booking!=null)
        {
            return View("Edit", new BookingViewModel(){
                Tours = dbContext.Tours.ToList(),
                Clients = dbContext.Clients.ToList(),
                Booking = booking
            });
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(BookingViewModel bookingEditRequest)
    {
        var booking = dbContext.Bookings.Find(bookingEditRequest.Booking.BookingId);

        if (booking!=null)
        {
            booking.TourId = bookingEditRequest.Booking.TourId;
            booking.ClientId = bookingEditRequest.Booking.ClientId;
            booking.BookingDate = bookingEditRequest.Booking.BookingDate;
            dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(BookingViewModel bookingDeleteRequest)
    {
        var booking = dbContext.Bookings.Find(bookingDeleteRequest.Booking.BookingId);

        if (booking!=null)
        {
            dbContext.Bookings.Remove(booking);
            dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }
    public IActionResult PrintPdf() 
    {
        return new ViewAsPdf("PrintPdf", dbContext.Bookings.Include(b=>b.Client).Include(b=>b.Tour).ToList())
        {
            IsGrayScale = true
        };
    }
}
