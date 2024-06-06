using Microsoft.AspNetCore.Mvc;
namespace Devices_E_Commerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Devices_E_Commerce.Data;
using Devices_E_Commerce.Models;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Products()
    {
        var list = new List<Devices_E_Commerce.Models.Device>();
        foreach (var x in _context.Devices)
        {
            list.Add(x);
        }
        return View(list);
    }

      [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.CustomerAccounts.SingleOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                await SignInUser(user);
                return RedirectToAction("Index", "Home"); // Redirect to the home page after successful login
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if the email is not already registered
            if (!_context.CustomerAccounts.Any(u => u.Email == model.Email))
            {
                var newUser = new CustomerAccount
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.UserName,
                    LastName = " ",
                    Phone = "null"
                    // Add other properties as needed
                };
                _context.CustomerAccounts.Add(newUser);
                await _context.SaveChangesAsync();

                await SignInUser(newUser);
                return RedirectToAction("Login", "Account"); // Redirect to the home page after successful registration
            }

            ModelState.AddModelError(nameof(RegisterViewModel.Email), "Email is already registered.");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home"); // Redirect to the home page after logout
    }

    private async Task SignInUser(CustomerAccount user)
    {
        var claims = new[]
        {
        new Claim(ClaimTypes.Name, user.Email),
        // Add other claims as needed
    };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var expirationTime = DateTime.UtcNow.AddDays(7); // Set expiration time to 7 days from now

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = expirationTime,
            IsPersistent = true
        };

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
    }

}
