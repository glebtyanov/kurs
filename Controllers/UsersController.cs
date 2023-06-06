using Microsoft.AspNetCore.Mvc;
using kurs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore;

namespace kurs.Controllers;
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly TourDbContext dbContext;
    public UsersController(TourDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var users = dbContext.Users.Include(u=>u.Role).ToList();
        return View(users);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(User userAddRequest)
    {
        var user = new User()
        {
            Login = userAddRequest.Login,
            Password = userAddRequest.Password,
            RoleId = userAddRequest.RoleId,
        };

        dbContext.Users.Add(user);

        dbContext.SaveChanges();

        return RedirectToAction("Add");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = dbContext.Users.Find(id);
        if (user!=null)
        {
            return View(user);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(User userEditRequest)
    {
        var user = dbContext.Users.Find(userEditRequest.UserId);

        if (user!=null)
        {
            user.Login = userEditRequest.Login;
            user.Password = userEditRequest.Password;
            dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(User userDeleteRequest)
    {
        var user = dbContext.Users.Find(userDeleteRequest.UserId);

        if (user!=null)
        {
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    public IActionResult PrintPdf() 
    {
        return new ViewAsPdf("PrintPdf", dbContext.Users.Include(u=>u.Role).ToList())
        {
            IsGrayScale = true
        };
    }
}
