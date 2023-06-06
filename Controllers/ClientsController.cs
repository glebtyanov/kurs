using Microsoft.AspNetCore.Mvc;
using kurs.Models;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore;

namespace kurs.Controllers;
[Authorize]
public class ClientsController : Controller
{
    private readonly TourDbContext dbContext;
    public ClientsController(TourDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var clients = dbContext.Clients.ToList();
        return View(clients);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Client clientAddRequest)
    {
        var client = new Client()
        {
            FirstName = clientAddRequest.FirstName,
            LastName = clientAddRequest.LastName,
            PhoneNumber = clientAddRequest.PhoneNumber
        };

        dbContext.Clients.Add(client);

        dbContext.SaveChanges();

        return RedirectToAction("Add");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var client = dbContext.Clients.Find(id);
        if (client!=null)
        {
            return View(client);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(Client clientEditRequest)
    {
        var client = dbContext.Clients.Find(clientEditRequest.ClientId);

        if (client!=null)
        {
            client.FirstName = clientEditRequest.FirstName;
            client.LastName = clientEditRequest.LastName;
            client.PhoneNumber = clientEditRequest.PhoneNumber;
            dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(Client clientDeleteRequest)
    {
        var client = dbContext.Clients.Find(clientDeleteRequest.ClientId);

        if (client!=null)
        {
            dbContext.Clients.Remove(client);
            dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    public IActionResult PrintPdf() 
    {
        return new ViewAsPdf("PrintPdf", dbContext.Clients.ToList())
        {
            IsGrayScale = true
        };
    }
}
