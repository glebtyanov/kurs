using Microsoft.AspNetCore.Mvc;
using kurs.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace kurs.Controllers;

public class AccessController : Controller
{
    private readonly TourDbContext dbContext;
    public AccessController(TourDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginModel)
    {
        var users = dbContext.Users.Include(u=>u.Role).ToList();
        User user = users.FirstOrDefault(u=>u.Login == loginModel.Login.Trim() && u.Password == loginModel.Password.Trim());
        if (user == null)
        {
            ViewData["ValidateMessage"] = "Пользователь не найден";
            return View();
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        AuthenticationProperties props = new AuthenticationProperties()
        {
            AllowRefresh = true,
            IsPersistent = true
        };
        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, props);
        return RedirectToAction("Index", "Home");


    }
}
