using Microsoft.AspNetCore.Mvc;
using kurs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore;

namespace kurs.Controllers;
[Authorize]
public class ToursController : Controller
{
    private readonly TourDbContext dbContext;
    public ToursController(TourDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var tours = dbContext.Tours.ToList();
        return View(tours);
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpPost]
    public IActionResult Add(Tour tourAddRequest)
    {
        var tour = new Tour()
        {
            Name = tourAddRequest.Name,
            Description = tourAddRequest.Description,
            Price = tourAddRequest.Price,
            StartDate = tourAddRequest.StartDate,
            EndDate = tourAddRequest.EndDate
        };

        dbContext.Tours.Add(tour);
        dbContext.SaveChanges();

        return RedirectToAction("Add");
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var tour = dbContext.Tours.Find(id);
        if (tour != null)
        {
            return View(tour);
        }
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpPost]
    public IActionResult Edit(Tour tourEditRequest)
    {
        var tour = dbContext.Tours.Find(tourEditRequest.TourId);

        if (tour != null)
        {
            tour.Name = tourEditRequest.Name;
            tour.Description = tourEditRequest.Description;
            tour.Price = tourEditRequest.Price;
            tour.StartDate = tourEditRequest.StartDate;
            tour.EndDate = tourEditRequest.EndDate;
            dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpPost]
    public IActionResult Delete(Tour tourDeleteRequest)
    {
        var tour = dbContext.Tours.Find(tourDeleteRequest.TourId);

        if (tour != null)
        {
            dbContext.Tours.Remove(tour);
            dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    public IActionResult PrintPdf() 
    {
        return new ViewAsPdf("PrintPdf", dbContext.Tours.ToList())
        {
            IsGrayScale = true
        };
    }
}
